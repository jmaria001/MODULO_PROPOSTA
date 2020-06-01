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
                clsConexao cnn = new clsConexao(this.Credential);
                cnn.Open();
                //==================Carrega Dados Gerais
                DataTable dtb = new DataTable();
                SqlDataAdapter dta = new SqlDataAdapter();

                dta.SelectCommand = cnn.Procedure(cnn.Connection, "Pr_Imprime_Proposta_Dados");
                dta.SelectCommand.Parameters.AddWithValue("@Par_Id_Simulacao", pId_Simulacao);
                dta.Fill(dtb);

                strAutor = dtb.Rows[0]["Usuario_Text"].ToString();
                strCodEmpresa = dtb.Rows[0]["Cod_Empresa"].ToString();
                strNomeEmpresa = dtb.Rows[0]["Nome_Empresa"].ToString();
                strDataExtenso = dtb.Rows[0]["Data_Text"].ToString();
                strObservacao = dtb.Rows[0]["Observacao"].ToString();
                strNomeContato = dtb.Rows[0]["Nome_Contato"].ToString();

                doc.SetMargins(10, 10, 10, 10);
                doc.Open();
                this.ImprimeTexto(write, doc, dtb);
                if (!dtb.Rows[0]["Indica_Sem_Midia"].ToString().ConvertToBoolean())
                {
                    this.ImprimeResumoComMidia(write, doc, pId_Simulacao);
                }
                this.ImprimeInvestimento(write, doc,  dtb);

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
        private void ImprimeCapa(PdfWriter ww, Document dd, DataTable dtb)
        {
            try
            {
                clsPdf.addBorder(ww, dd);
                this.ImprimeCabecalho(ww, dd);

                PdfPTable tbCapa= clsPdf.CreateTable(new float[] { 200, 620 });

                clsPdf.addCell(tbCapa, new pdfLibCell(){Text = "Cliente",FontSize = 16,BorderBottom = 1,BorderRight = 0,BorderLeft = 0,BorderTop = 0,FontStyle = iTextSharp.text.Font.BOLD,Background = System.Drawing.Color.Transparent,Height = 40f});
                clsPdf.addCell(tbCapa, new pdfLibCell() { Text = dtb.Rows[0]["Nome_Cliente"].ToString(), FontSize = 16, BorderBottom = 1, BorderRight = 0, BorderLeft = 0, BorderTop = 0, FontStyle = iTextSharp.text.Font.BOLD, Background = System.Drawing.Color.Transparent, Height = 40f });

                clsPdf.addCell(tbCapa, new pdfLibCell() { Text = "Agencia", FontSize = 16, BorderBottom = 1, BorderRight = 0, BorderLeft = 0, BorderTop = 0, FontStyle = iTextSharp.text.Font.BOLD, Background = System.Drawing.Color.Transparent, Height = 40f });
                clsPdf.addCell(tbCapa, new pdfLibCell() { Text = dtb.Rows[0]["Nome_Agencia"].ToString(), FontSize = 16, BorderBottom = 1, BorderRight = 0, BorderLeft = 0, BorderTop = 0, FontStyle = iTextSharp.text.Font.BOLD, Background = System.Drawing.Color.Transparent, Height = 40f });


                clsPdf.addCell(tbCapa, new pdfLibCell() { Text = "Forma de Pagamento", FontSize = 16, BorderBottom = 1, BorderRight = 0, BorderLeft = 0, BorderTop = 0, FontStyle = iTextSharp.text.Font.BOLD, Background = System.Drawing.Color.Transparent, Height = 40f });
                clsPdf.addCell(tbCapa, new pdfLibCell() { Text = dtb.Rows[0]["Forma_pgto"].ToString(), FontSize = 16, BorderBottom = 1, BorderRight = 0, BorderLeft = 0, BorderTop = 0, FontStyle = iTextSharp.text.Font.BOLD, Background = System.Drawing.Color.Transparent, Height = 40f });

                clsPdf.addCell(tbCapa, new pdfLibCell() { Text = "Condições de Pagamento", FontSize = 16, BorderBottom = 1, BorderRight = 0, BorderLeft = 0, BorderTop = 0, FontStyle = iTextSharp.text.Font.BOLD, Background = System.Drawing.Color.Transparent, Height = 40f });
                clsPdf.addCell(tbCapa, new pdfLibCell() { Text = dtb.Rows[0]["Condicao_Pagamento"].ToString(), FontSize = 16, BorderBottom = 1, BorderRight = 0, BorderLeft = 0, BorderTop = 0, FontStyle = iTextSharp.text.Font.BOLD, Background = System.Drawing.Color.Transparent, Height = 40f });

                clsPdf.addCell(tbCapa, new pdfLibCell() { Text = "Observações", FontSize = 16, BorderBottom = 1, BorderRight = 0, BorderLeft = 0, BorderTop = 0, FontStyle = iTextSharp.text.Font.BOLD, Background = System.Drawing.Color.Transparent, Height = 40f });
                clsPdf.addCell(tbCapa, new pdfLibCell() { Text = dtb.Rows[0]["Observacao"].ToString(), FontSize = 16, BorderBottom = 1, BorderRight = 0, BorderLeft = 0, BorderTop = 0, FontStyle = iTextSharp.text.Font.BOLD, Background = System.Drawing.Color.Transparent, Height = 40f });

                PdfContentByte pc = ww.DirectContent;
                tbCapa.WriteSelectedRows(0, -1, 10, 580, pc);
                CurrentPosition = CurrentPosition - tbCapa.TotalHeight - 30;

            }
            catch (Exception)
            {
                throw;
            }

        }
        private void ImprimeTexto(PdfWriter ww, Document dd, DataTable dtb)
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
                CurrentPosition -= 50;
                clsPdf.AddTexto(ww, new pdfLibText() { Text = "Atenciosamente,", X = 30, Y = CurrentPosition, FontSize = 9, FontStyle = iTextSharp.text.Font.ITALIC });
                CurrentPosition -= 20;
                clsPdf.AddTexto(ww, new pdfLibText() { Text = strAutor, X = 30, Y = CurrentPosition, FontSize = 9, FontStyle = iTextSharp.text.Font.ITALIC });
                CurrentPosition -= 50;
                
                PdfPTable tbCapa = clsPdf.CreateTable(new float[] { 200, 550 });

                clsPdf.addCell(tbCapa, new pdfLibCell() { Text = "Dados Gerais", FontSize = 12, BorderBottom = 1, BorderRight = 1, BorderLeft = 1, BorderTop = 1, FontStyle = iTextSharp.text.Font.BOLD, Background = System.Drawing.Color.Silver, Height = 40f, Align = PdfPCell.ALIGN_CENTER,colspan=2});

                clsPdf.addCell(tbCapa, new pdfLibCell() { Text = "N.Proposta", FontSize = 11, BorderBottom = 1, BorderRight = 1, BorderLeft = 1, BorderTop = 1, FontStyle = iTextSharp.text.Font.NORMAL, Background = System.Drawing.Color.Transparent, Height = 40f, Align = PdfPCell.ALIGN_LEFT });
                clsPdf.addCell(tbCapa, new pdfLibCell() { Text = dtb.Rows[0]["Id_Simulacao"].ToString(), FontSize = 11, BorderBottom = 1, BorderRight = 1, BorderLeft = 1, BorderTop = 1, FontStyle = iTextSharp.text.Font.NORMAL, Background = System.Drawing.Color.Transparent, Height = 40f, Align = PdfPCell.ALIGN_LEFT });

                clsPdf.addCell(tbCapa, new pdfLibCell() { Text = "Cliente", FontSize = 11, BorderBottom = 1, BorderRight = 1, BorderLeft = 1, BorderTop = 1, FontStyle = iTextSharp.text.Font.NORMAL, Background = System.Drawing.Color.Transparent, Height = 40f ,Align = PdfPCell.ALIGN_LEFT });
                clsPdf.addCell(tbCapa, new pdfLibCell() { Text = dtb.Rows[0]["Nome_Cliente"].ToString(), FontSize = 11, BorderBottom = 1, BorderRight = 1, BorderLeft = 1, BorderTop = 1, FontStyle = iTextSharp.text.Font.NORMAL, Background = System.Drawing.Color.Transparent, Height = 40f, Align = PdfPCell.ALIGN_LEFT });

                clsPdf.addCell(tbCapa, new pdfLibCell() { Text = "Agencia", FontSize = 11, BorderBottom = 1, BorderRight = 1, BorderLeft = 1, BorderTop = 1, FontStyle = iTextSharp.text.Font.NORMAL, Background = System.Drawing.Color.Transparent, Height = 40f, Align = PdfPCell.ALIGN_LEFT });
                clsPdf.addCell(tbCapa, new pdfLibCell() { Text = dtb.Rows[0]["Nome_Agencia"].ToString(), FontSize = 11, BorderBottom = 1, BorderRight = 1, BorderLeft = 1, BorderTop = 1, FontStyle = iTextSharp.text.Font.NORMAL, Background = System.Drawing.Color.Transparent, Height = 40f, Align = PdfPCell.ALIGN_LEFT });


                clsPdf.addCell(tbCapa, new pdfLibCell() { Text = "Forma de Pagamento", FontSize = 11, BorderBottom = 1, BorderRight = 1, BorderLeft = 1, BorderTop = 1, FontStyle = iTextSharp.text.Font.NORMAL, Background = System.Drawing.Color.Transparent, Height = 40f, Align = PdfPCell.ALIGN_LEFT });
                clsPdf.addCell(tbCapa, new pdfLibCell() { Text = dtb.Rows[0]["Forma_pgto"].ToString(), FontSize = 11, BorderBottom = 1, BorderRight = 1, BorderLeft = 1, BorderTop = 1, FontStyle = iTextSharp.text.Font.NORMAL, Background = System.Drawing.Color.Transparent, Height = 40f, Align = PdfPCell.ALIGN_LEFT });

                clsPdf.addCell(tbCapa, new pdfLibCell() { Text = "Condições de Pagamento", FontSize = 11, BorderBottom = 1, BorderRight = 1, BorderLeft = 1, BorderTop = 1, FontStyle = iTextSharp.text.Font.NORMAL, Background = System.Drawing.Color.Transparent, Height = 40f, Align = PdfPCell.ALIGN_LEFT });
                clsPdf.addCell(tbCapa, new pdfLibCell() { Text = dtb.Rows[0]["Condicao_Pagamento"].ToString(), FontSize = 11, BorderBottom = 1, BorderRight = 1, BorderLeft = 1, BorderTop = 1, FontStyle = iTextSharp.text.Font.NORMAL, Background = System.Drawing.Color.Transparent, Height = 40f, Align = PdfPCell.ALIGN_LEFT });

                clsPdf.addCell(tbCapa, new pdfLibCell() { Text = "Observações", FontSize = 11, BorderBottom = 1, BorderRight = 1, BorderLeft = 1, BorderTop = 1, FontStyle = iTextSharp.text.Font.NORMAL, Background = System.Drawing.Color.Transparent, Height = 40f, Align = PdfPCell.ALIGN_LEFT });
                clsPdf.addCell(tbCapa, new pdfLibCell() { Text = dtb.Rows[0]["Observacao"].ToString(), FontSize = 11, BorderBottom = 1, BorderRight = 1, BorderLeft = 1, BorderTop = 1, FontStyle = iTextSharp.text.Font.NORMAL, Background = System.Drawing.Color.Transparent, Height = 40f, Align = PdfPCell.ALIGN_LEFT });


                PdfContentByte pc = ww.DirectContent;
                tbCapa.WriteSelectedRows(0, -1, 30, CurrentPosition, pc);
                CurrentPosition = CurrentPosition - tbCapa.TotalHeight - 30;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }

        }
        private void ImprimeResumoComMidia(PdfWriter ww, Document dd, Int32 pid_Simulacao)
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
                if (dtb.Rows.Count>0)
                {
                    this.ImprimeCabecalho(ww, dd);
                }

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
                        clsPdf.addCell(tbResmo, new pdfLibCell() { Text = "Esquema Comercial", Height = 40, Background = System.Drawing.Color.Silver ,colspan=8,FontStyle=iTextSharp.text.Font.BOLD,FontSize=12});
                        clsPdf.addCell(tbResmo, new pdfLibCell() { Text = "Mes/Ano", Height = 20, Background = System.Drawing.Color.Transparent});
                        clsPdf.addCell(tbResmo, new pdfLibCell() { Text = "Item", Height = 20, Background = System.Drawing.Color.Transparent });
                        clsPdf.addCell(tbResmo, new pdfLibCell() { Text = "Duração", Height = 20, Background = System.Drawing.Color.Transparent });
                        clsPdf.addCell(tbResmo, new pdfLibCell() { Text = "Qtd Ins. por Emissora", Height = 20, Background = System.Drawing.Color.Transparent });
                        clsPdf.addCell(tbResmo, new pdfLibCell() { Text = "Qtd Emissoras", Height = 20, Background = System.Drawing.Color.Transparent });
                        clsPdf.addCell(tbResmo, new pdfLibCell() { Text = "Valor Tabela", Height = 20, Background = System.Drawing.Color.Transparent });
                        clsPdf.addCell(tbResmo, new pdfLibCell() { Text = "Desconto", Height = 20, Background = System.Drawing.Color.Transparent });
                        clsPdf.addCell(tbResmo, new pdfLibCell() { Text = "Valor Negociado", Height = 20, Background = System.Drawing.Color.Transparent });
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
        private void ImprimeInvestimento(PdfWriter ww, Document dd,DataTable dtb)
        {
            //this.ImprimeCabecalho(ww,dd);
            PdfPTable tbInvestimento= clsPdf.CreateTable(new float[] { 200, 200 });
            

            clsPdf.addCell(tbInvestimento, new pdfLibCell() { Text = "Investimento Total", FontSize = 12, BorderBottom = 1, BorderRight = 1, BorderLeft = 1, BorderTop = 1, Background = System.Drawing.Color.Silver, Height = 40f,FontStyle=iTextSharp.text.Font.BOLD,colspan=2 });

            clsPdf.addCell(tbInvestimento, new pdfLibCell(){Text = "Valor Tabela",FontSize = 11,BorderBottom = 1,BorderRight = 1,BorderLeft = 1,BorderTop = 1,Background = System.Drawing.Color.Transparent,Height = 40f});
            clsPdf.addCell(tbInvestimento, new pdfLibCell() { Text = dtb.Rows[0]["Valor_Tabela"].ToString(), FontSize = 11, BorderBottom = 1, BorderRight = 1, BorderLeft = 1, BorderTop = 1, Background = System.Drawing.Color.Transparent, Height = 40f });

            clsPdf.addCell(tbInvestimento, new pdfLibCell() { Text = "Desconto Concedido", FontSize = 11, BorderBottom = 1, BorderRight = 1, BorderLeft = 1, BorderTop = 1, Background = System.Drawing.Color.Transparent, Height = 40f });
            clsPdf.addCell(tbInvestimento, new pdfLibCell() { Text = dtb.Rows[0]["Desconto_Real"].ToString(), FontSize = 11, BorderBottom = 1, BorderRight = 1, BorderLeft = 1, BorderTop = 1, Background = System.Drawing.Color.Transparent, Height = 40f });

            clsPdf.addCell(tbInvestimento, new pdfLibCell() { Text = "Valor Negociado", FontSize = 11, BorderBottom = 1, BorderRight = 1, BorderLeft = 1, BorderTop = 1, Background = System.Drawing.Color.Transparent, Height = 40f });
            clsPdf.addCell(tbInvestimento, new pdfLibCell() { Text = dtb.Rows[0]["Valor_Negociado"].ToString(), FontSize = 11, BorderBottom = 1, BorderRight = 1, BorderLeft = 1, BorderTop = 1, Background = System.Drawing.Color.Transparent, Height = 40f });

            PdfContentByte pc = ww.DirectContent;
            CurrentPosition -= 50;

            if (CurrentPosition-tbInvestimento.TotalHeight < 50)
            {
                this.ImprimeCabecalho(ww,dd);
            }
            tbInvestimento.WriteSelectedRows(0, -1, 200, CurrentPosition, pc);

            
                        

        }
        private void ImprimeCabecalho(PdfWriter ww, Document dd)
        {
            dd.NewPage();
            PdfPTable tbCabecalho = clsPdf.CreateTable(new float[] { 200,620 });

            String LogoName = "logo_" + strCodEmpresa + ".png";
            String sPathLogo = HttpContext.Current.Server.MapPath("~/logos/" + LogoName);
            if (!File.Exists(sPathLogo))
            {
                sPathLogo = HttpContext.Current.Server.MapPath("~/logos/" + "Logo_Proposta_Padrao.png");
            }

            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(sPathLogo);

            clsPdf.addCell(tbCabecalho, new pdfLibCell()
            {
                Text = "",
                Picture = jpg,
                Height = 40,
                BorderBottom = 1,
                BorderLeft = 0,
                BorderRight = 0,
                BorderTop = 0,
                Align = PdfPCell.ALIGN_LEFT,
                Background = System.Drawing.Color.Transparent
            });

            clsPdf.addCell(tbCabecalho, new pdfLibCell()
            {
                Text = "Proposta Comercial",
                FontSize = 16,
                BorderBottom = 1,
                BorderRight = 0,
                BorderLeft = 0,
                BorderTop = 0,
                Align = PdfPCell.ALIGN_LEFT,
                FontStyle = iTextSharp.text.Font.BOLD,
                Background = System.Drawing.Color.Transparent,
                Height = 40f
            });
            PdfContentByte pc = ww.DirectContent;
            tbCabecalho.WriteSelectedRows(0, -1, 10, 580, pc);
            CurrentPosition = 580 - tbCabecalho.TotalHeight - 30;
        }
    }
}
