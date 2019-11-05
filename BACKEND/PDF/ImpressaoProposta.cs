using CLASSDB;
using System;
using System.Globalization;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using webapi.SIMLIB;

namespace PROPOSTA
{
    public class ImpressaoProposta
    {
        private String Credential;
        private String CurrentUser;
        private String strCodEmpresa = "";
        private String strNomeEmpresa = "";
        private String strAutor = "";
        private String strNomeContato = "";
        private String strDataExtenso = "";
        private String strObservacao = "";


        private float CurrentPosition = 580;
        private SimLib clsLib = new SimLib();
        PdfLib clsPdf = new PdfLib();
        public ImpressaoProposta(String pUser)
        {
            this.Credential = pUser;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }
        public String GetPath()
        {
            String sPath = HttpContext.Current.Server.MapPath("~/PDFFILES/PROPOSTA");
            if (sPath.Right(1) != @"\")
            {
                sPath += @"\";
            }
            sPath += this.CurrentUser;
            sPath += @"\";
            if (!System.IO.Directory.Exists(sPath))
            {
                System.IO.Directory.CreateDirectory(sPath);
            }
            return sPath;
        }
        public String ImprimirProposta(Int32 pId_Simulacao)
        {
            String strFilePdf = string.Empty;
            String PdfFinal = string.Empty;
            //String sPath = HttpContext.Current.Server.MapPath("~/PDFFILES/PROPOSTA");
            String sPath = GetPath();
            try
            {
                //'------------------------Diretorio Temporario para geracao dos PDF

                //  if (sPath.Right(1) != @"\")
                //                {
                //                  sPath += @"\";
                //            }
                //          sPath += this.CurrentUser;
                //        sPath += @"\";
                //      if (!System.IO.Directory.Exists(sPath))
                //    {
                //      System.IO.Directory.CreateDirectory(sPath);
                //}
                //=========================Apaga todos os arquivos da pasta antes da geracao do PDf 
                var list = System.IO.Directory.GetFiles(sPath, "*.pdf");

                foreach (var item in list)
                {
                    try
                    {
                        System.IO.File.Delete(item);
                    }
                    catch (Exception)
                    {

                    }
                }
                //=========================Gera pdf 
                strFilePdf = "PROPOSTA" + pId_Simulacao.ToString() + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".PDF";
                if (GerarPdf(pId_Simulacao, sPath, strFilePdf))
                {
                    PdfFinal = strFilePdf;
                }
            }

            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally
            {
            }
            return PdfFinal;
        }
        private Boolean GerarPdf(Int32 pId_Simulacao, String Path, String FileName)
        {
            Boolean bolRetorno = true;
            String strFile = Path + FileName;
            FileStream strea = new FileStream(strFile, FileMode.Create);
            Document doc = new Document(PageSize.A4.Rotate());
            PdfWriter write = PdfWriter.GetInstance(doc, strea);
            try
            {
                doc.SetMargins(10, 10, 10, 10);
                doc.Open();
                this.ImprimeCapa(write, doc, pId_Simulacao);
                this.ImprimeTexto(write, doc, pId_Simulacao);
                this.ImprimeQuadro(write, doc, pId_Simulacao);
                //this.ImprimeEsquemas(write, doc, pId_Simulacao);
                //this.ImprimeResumo(write, doc, pId_Simulacao);
            }
            catch (Exception Ex)
            {
                bolRetorno = false;
                throw new Exception(Ex.Message);
            }
            finally
            {
                doc.Close();
                doc.Dispose();
                write.Dispose();
                strea.Dispose();

            }
            return bolRetorno;
        }
        private void ImprimeCapa(PdfWriter ww, Document dd, Int32 pid_Simulacao)
        {
            try
            {
                clsConexao cnn = new clsConexao(this.Credential);
                cnn.Open();
                //==================Carrega Dados Gerais
                DataTable dtb = new DataTable();
                SqlDataAdapter dta = new SqlDataAdapter();

                dta.SelectCommand = cnn.Procedure(cnn.Connection, "Pr_Imprime_Proposta_Dados");
                dta.SelectCommand.Parameters.AddWithValue("@Par_Id_Simulacao", pid_Simulacao);
                dta.Fill(dtb);
                strAutor = dtb.Rows[0]["Usuario_Text"].ToString();
                strCodEmpresa = dtb.Rows[0]["Cod_Empresa"].ToString();
                strNomeEmpresa = dtb.Rows[0]["Nome_Empresa"].ToString();
                strDataExtenso = dtb.Rows[0]["Data_Text"].ToString();
                strObservacao = dtb.Rows[0]["Observacao"].ToString();
                strNomeContato = dtb.Rows[0]["Nome_Contato"].ToString();
                //clsPdf.addBorder(ww, dd);
                this.ImprimeCabecalho(ww, dd);
                clsPdf.AddTexto(ww, new pdfLibText() { Text = "Texto para Capa", X = 300, Y = 300, FontSize = 18, FontStyle = iTextSharp.text.Font.BOLD });


            }
            catch (Exception)
            {
                throw;
            }

        }
        private void ImprimeTexto(PdfWriter ww, Document dd, Int32 pid_Simulacao)
        {
            try
            {
                String strTexto = "";


                ImprimeCabecalho(ww, dd);

                CurrentPosition -= 20;
                strTexto = strDataExtenso;
                clsPdf.AddTexto(ww, new pdfLibText() { Text = strTexto, X = 30, Y = CurrentPosition, FontSize = 11, FontStyle = iTextSharp.text.Font.ITALIC });

                CurrentPosition -= 20;
                strTexto = "Prezado(a) Sr(a)  " + strNomeContato;
                clsPdf.AddTexto(ww, new pdfLibText() { Text = strTexto, X = 30, Y = CurrentPosition, FontSize = 11, FontStyle = iTextSharp.text.Font.ITALIC });

                CurrentPosition -= 20;
                strTexto = "É com satisfação que apresentamos a V.Sa. nossa proposta Comercial.";
                clsPdf.AddTexto(ww, new pdfLibText() { Text = strTexto, X = 30, Y = CurrentPosition, FontSize = 11, FontStyle = iTextSharp.text.Font.ITALIC });

                CurrentPosition -= 20;
                strTexto = "Desde já agradecemos a oportunidade que nos foi concedida e colocamo-nos a disposição para quaisquer esclarecimentos.";
                clsPdf.AddTexto(ww, new pdfLibText() { Text = strTexto, X = 30, Y = CurrentPosition, FontSize = 11, FontStyle = iTextSharp.text.Font.ITALIC });
                CurrentPosition -= 20;

                strTexto = "Segue abaixo Esquema Comercial:";
                clsPdf.AddTexto(ww, new pdfLibText() { Text = strTexto, X = 30, Y = CurrentPosition, FontSize = 11, FontStyle = iTextSharp.text.Font.ITALIC });
                CurrentPosition -= 20;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }

        }
        private void ImprimeQuadro(PdfWriter ww, Document dd, Int32 pid_Simulacao)
        {
            PdfContentByte pc;
            Boolean hasRow = false;
            Boolean BolPrimeiro;
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            try
            {

                //==================Carrega Resumo
                DataTable dtb = new DataTable();
                SqlDataAdapter dta = new SqlDataAdapter();

                dta.SelectCommand = cnn.Procedure(cnn.Connection, "Pr_Imprime_Proposta_Resumo");
                dta.SelectCommand.Parameters.AddWithValue("@Par_Id_Simulacao", pid_Simulacao);
                dta.Fill(dtb);

                //==================Imprime
                PdfPTable tbResmo = clsPdf.CreateTable(new float[] { 80, 150, 50, 100, 80, 100, 100, 100 });
                BolPrimeiro = true;
                foreach (DataRow drw in dtb.Rows)
                {
                    if (CurrentPosition - tbResmo.TotalHeight < 50)
                    {
                        if (hasRow)
                        {
                            pc = ww.DirectContent;
                            tbResmo.WriteSelectedRows(0, -1, 30, CurrentPosition, pc);
                            BolPrimeiro = true;
                        }
                        this.ImprimeCabecalho(ww, dd);
                    }
                    if (BolPrimeiro)
                    {
                        tbResmo = clsPdf.CreateTable(new float[] { 80, 150, 50, 100, 80, 100, 100, 100 });
                        clsPdf.addCell(tbResmo, new pdfLibCell() { Text = "Mes/Ano", Height = 20, Background = System.Drawing.Color.Silver });
                        clsPdf.addCell(tbResmo, new pdfLibCell() { Text = "Item", Height = 20, Background = System.Drawing.Color.Silver });
                        clsPdf.addCell(tbResmo, new pdfLibCell() { Text = "Duração", Height = 20, Background = System.Drawing.Color.Silver });
                        clsPdf.addCell(tbResmo, new pdfLibCell() { Text = "Qtd Ins. por Emissora", Height = 20, Background = System.Drawing.Color.Silver });
                        clsPdf.addCell(tbResmo, new pdfLibCell() { Text = "Qtd Emissoras", Height = 20, Background = System.Drawing.Color.Silver });
                        clsPdf.addCell(tbResmo, new pdfLibCell() { Text = "Valor Tabela", Height = 20, Background = System.Drawing.Color.Silver });
                        clsPdf.addCell(tbResmo, new pdfLibCell() { Text = "Desconto", Height = 20, Background = System.Drawing.Color.Silver });
                        clsPdf.addCell(tbResmo, new pdfLibCell() { Text = "Valor Negociado", Height = 20, Background = System.Drawing.Color.Silver });
                        BolPrimeiro = false;
                    }
                    if (drw["Tipo_Linha"].ToString() == "1")
                    {
                        clsPdf.addCell(tbResmo, new pdfLibCell() { Text = clsLib.CompetenciaExtenso(drw["Competencia"].ToString().ConvertToInt32()), Align = PdfPCell.ALIGN_LEFT });
                        clsPdf.addCell(tbResmo, new pdfLibCell() { Text = drw["Nome_Tipo_Comercial"].ToString(), Align = PdfPCell.ALIGN_LEFT });
                        clsPdf.addCell(tbResmo, new pdfLibCell() { Text = drw["Duracao"].ToString() });
                        clsPdf.addCell(tbResmo, new pdfLibCell() { Text = drw["Qtd_Insercoes_Emissora"].ToString() });
                        clsPdf.addCell(tbResmo, new pdfLibCell() { Text = drw["Qtd_Emissora"].ToString() });
                        clsPdf.addCell(tbResmo, new pdfLibCell() { Text = drw["Valor_Tabela"].ToString(), Align = PdfPCell.ALIGN_RIGHT });
                        clsPdf.addCell(tbResmo, new pdfLibCell() { Text = drw["Desconto"].ToString(), Align = PdfPCell.ALIGN_RIGHT });
                        clsPdf.addCell(tbResmo, new pdfLibCell() { Text = drw["Valor_Negociado"].ToString(), Align = PdfPCell.ALIGN_RIGHT });
                    }
                    else
                    {
                        clsPdf.addCell(tbResmo, new pdfLibCell() { Text = "Total", Align = PdfPCell.ALIGN_CENTER, colspan = 3, FontStyle = iTextSharp.text.Font.BOLD });
                        clsPdf.addCell(tbResmo, new pdfLibCell() { Text = drw["Qtd_Insercoes_Emissora"].ToString(), FontStyle = iTextSharp.text.Font.BOLD });
                        clsPdf.addCell(tbResmo, new pdfLibCell() { Text = "" });
                        clsPdf.addCell(tbResmo, new pdfLibCell() { Text = drw["Valor_Tabela"].ToString(), Align = PdfPCell.ALIGN_RIGHT, FontStyle = iTextSharp.text.Font.BOLD });
                        clsPdf.addCell(tbResmo, new pdfLibCell() { Text = drw["Desconto"].ToString(), Align = PdfPCell.ALIGN_RIGHT, FontStyle = iTextSharp.text.Font.BOLD });
                        clsPdf.addCell(tbResmo, new pdfLibCell() { Text = drw["Valor_Negociado"].ToString(), Align = PdfPCell.ALIGN_RIGHT, FontStyle = iTextSharp.text.Font.BOLD });
                    }

                    hasRow = true;
                }
                if (hasRow)
                {
                    pc = ww.DirectContent;
                    tbResmo.WriteSelectedRows(0, -1, 30, CurrentPosition, pc);
                    CurrentPosition -= tbResmo.TotalHeight;
                }

                if (!String.IsNullOrEmpty(strObservacao))
                {
                    if (CurrentPosition - tbResmo.TotalHeight < 50)
                    {
                        this.ImprimeCabecalho(ww, dd);
                    }
                    tbResmo = clsPdf.CreateTable(new float[] { 80, 150, 50, 100, 80, 100, 100, 100 });
                    clsPdf.addCell(tbResmo, new pdfLibCell() { Text = "Observações", Align = PdfPCell.ALIGN_CENTER, FontStyle = iTextSharp.text.Font.BOLD, colspan = 8, Background = System.Drawing.Color.Silver ,Height=20});
                    clsPdf.addCell(tbResmo, new pdfLibCell() { Text = strObservacao, Align = PdfPCell.ALIGN_LEFT, FontStyle = iTextSharp.text.Font.ITALIC, colspan = 8,PaddingTop=10f,PaddingBottom=10f });

                    pc = ww.DirectContent;
                    tbResmo.WriteSelectedRows(0, -1, 30, CurrentPosition, pc);
                    CurrentPosition -= tbResmo.TotalHeight;
                }
            

                CurrentPosition -= 20;
                clsPdf.AddTexto(ww, new pdfLibText() { Text = "Atenciosamente,", X = 30, Y = CurrentPosition, FontSize = 9, FontStyle = iTextSharp.text.Font.ITALIC });

                CurrentPosition -= 20;
                clsPdf.AddTexto(ww, new pdfLibText() { Text = strAutor, X = 30, Y = CurrentPosition, FontSize = 9, FontStyle = iTextSharp.text.Font.ITALIC });

                //if (!String.IsNullOrEmpty(strObservacao))
                //{
                //    if (CurrentPosition - tbResmo.TotalHeight < 50)
                //    {
                //     this.ImprimeCabecalho(ww, dd);
                //    }
                //    CurrentPosition -= 20;
                //    clsPdf.AddTexto(ww, new pdfLibText() {Text="Observações",X=10,Y=CurrentPosition, FontStyle=iTextSharp.text.Font.ITALIC,FontSize=11});

                //    CurrentPosition -= 20;
                //    PdfPTable tbObs = clsPdf.CreateTable(new float[] { 800 });
                //    clsPdf.addCell(tbObs, new pdfLibCell() { Text = strObservacao, Align = PdfPCell.ALIGN_LEFT, FontSize = 11,FontStyle=iTextSharp.text.Font.ITALIC ,BorderLeft=0,BorderRight=0,BorderBottom=0,BorderTop=0});
                //    pc = ww.DirectContent;
                //    tbObs.WriteSelectedRows(0, -1, 10, CurrentPosition, pc);
                //    CurrentPosition -= tbResmo.TotalHeight;
                //}

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cnn.Close();
            }

        }
        private void ImprimeCabecalho(PdfWriter ww, Document dd)
        {
            dd.NewPage();
            PdfPTable tbCabecalho = clsPdf.CreateTable(new float[] { 820 });

            //String LogoName = "logo_" + strCodEmpresa + ".png";
            //String sPathLogo = HttpContext.Current.Server.MapPath("~/logos/" + LogoName);
            //if (!File.Exists(sPathLogo))
            //{
            //    sPathLogo = HttpContext.Current.Server.MapPath("~/logos/" + "logo_Padrao.png");
            //}

            //iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(sPathLogo);

            //clsPdf.addCell(tbCabecalho, new pdfLibCell()
            //{ Text = "",
            //    Picture = jpg, Height = 40, BorderBottom = 0, BorderLeft = 0, BorderRight = 0, BorderTop = 0,Background= System.Drawing.Color.WhiteSmoke
            //});

            clsPdf.addCell(tbCabecalho, new pdfLibCell()
            {
                Text = "Proposta Comercial",
                FontSize = 16,
                BorderBottom = 0,
                BorderRight = 0,
                BorderLeft = 0,
                BorderTop = 0,
                FontStyle = iTextSharp.text.Font.BOLD,
                Background = System.Drawing.Color.WhiteSmoke,
                Height = 40f
            });
            PdfContentByte pc = ww.DirectContent;
            tbCabecalho.WriteSelectedRows(0, -1, 10, 580, pc);
            CurrentPosition = 580 - tbCabecalho.TotalHeight - 30;
        }
    }
}
