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
    public class ImpressaoRoteiro
    {
        private String Credential;
        private String CurrentUser;
        private Int32 PageNumber = 0;
        private float CurrentPosition = 0;
        private SimLib clsLib = new SimLib();
        private String [] diaSemana ={"Domingo", "Segunda", "Terca", "Quarta", "Quinta", "Sexta", "Sabado" };
        PdfLib clsPdf = new PdfLib();
        public ImpressaoRoteiro(String pUser)
        {
            this.Credential = pUser;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }
        public String ImprimirRoteiro(Roteiro.RoteiroFiltroModel Filtro)
        {
            String strFilePdf = string.Empty;
            String PdfFinal = string.Empty;
            DataTable dtbEsquema = new DataTable("");
            SqlDataAdapter dtaEsquema = new SqlDataAdapter();
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            try
            {
                //if (Generic.IsMapaDoUsuario(pid_Contrato))

                //'------------------------Diretorio Temporario para geracao dos PDF
                String sPath = HttpContext.Current.Server.MapPath("~/PDFFILES/ROTEIRO");
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
                //=========================Apaga todos os arquivos da pasta antes da geracao do strFilePDf 
                var list = System.IO.Directory.GetFiles(sPath, "*.pdf");
                try
                {
                    foreach (var item in list)
                    {
                        System.IO.File.Delete(item);
                    }
                }
                catch (Exception)
                {
                }
                
                //=========================Gera pdf para cada mapa da simulacao
                strFilePdf = "Roteiro_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".PDF";
                if (GerarPdf(Filtro, sPath, strFilePdf))
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
                cnn.Close();
            }
            return PdfFinal;
        }
        private Boolean GerarPdf(Roteiro.RoteiroFiltroModel Filtro, String Path, String FileName)
        {
            Boolean bolRetorno = true;
            String strFile = Path + FileName;
            FileStream strea = new FileStream(strFile, FileMode.Create);
            Document doc = new Document(PageSize.A4);
            PdfWriter write = PdfWriter.GetInstance(doc, strea);
            clsConexao cnn = new clsConexao(this.Credential);
            PdfContentByte pc;
            Boolean hasRow = false;
            try
            {
                doc.SetMargins(10, 10, 10, 10);
                doc.Open();
                //==================Carrega o Roteiro 
                String strProgramas = "";
                for (int i = 0; i < Filtro.Programas.Count; i++)
                {
                    if (Filtro.Programas[i].Selected)
                    {
                        strProgramas += (Filtro.Programas[i].Cod_Programa + "    ").Left(4);
                    }
                }
                DataTable dtbRoteiro = new DataTable();
                SqlDataAdapter dtaRoteiro = new SqlDataAdapter();
                cnn.Open();
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "SP_RU_CARREGA_ROTEIRO_V2");
                cmd.Parameters.AddWithValue("@Par_Cod_Veiculo", Filtro.Cod_Veiculo);
                cmd.Parameters.AddWithValue("@Par_Data_Exibicao", Filtro.Data_Exibicao.ConvertToDatetime());
                cmd.Parameters.AddWithValue("@Par_Cod_Programa", strProgramas);
                dtaRoteiro.SelectCommand = cmd;
                dtaRoteiro.Fill(dtbRoteiro);
                System.Drawing.Color CorComercial = new System.Drawing.Color();
                string[] Intervalos= { "Local", "Net", "Artistico", "PE" };
                PdfPTable tbRoteiro = clsPdf.CreateTable(new float[] { 70, 50, 170, 170, 50, 60 });
                foreach (DataRow drw in dtbRoteiro.Rows)
                {
                    //------------------------Quebrou o tamanho da pagina
                    if (CurrentPosition - tbRoteiro.TotalHeight < 50)
                    {
                        if (hasRow)
                        {
                            pc = write.DirectContent;
                            tbRoteiro.WriteSelectedRows(0, -1, 10, CurrentPosition, pc);
                            tbRoteiro = clsPdf.CreateTable(new float[] { 70, 50, 170, 170, 50, 60 });
                        }
                        this.ImprimeCabecalho(write, doc, drw);
                    }
                    //------------------------Quebrou Break
                    if (drw["Indica_Titulo_Break"].ToString() == "1")
                    {
                        if (hasRow)
                        {
                            pc = write.DirectContent;
                            tbRoteiro.WriteSelectedRows(0, -1, 10, CurrentPosition, pc);
                            CurrentPosition -= tbRoteiro.TotalHeight;
                            tbRoteiro = clsPdf.CreateTable(new float[] { 70, 50, 170, 170, 50, 60 });
                        }
                        this.ImprimeTituloBreak(write, doc, drw);
                    }
                    //------------------------Imprime a linha do Comercial 
                    if (String.IsNullOrEmpty(drw["Indica_Titulo_Break"].ToString()) && String.IsNullOrEmpty(drw["Indica_Titulo_Intervalo"].ToString()))
                    {
                        if (drw["Tipo_Break"].ToString()=="1")
                        {
                            CorComercial = System.Drawing.Color.Red;
                        }
                        else
                        {
                            CorComercial = System.Drawing.Color.Black;
                        }
                        clsPdf.addCell(tbRoteiro, new pdfLibCell() { Text = drw["Numero_Fita"].ToString() , FontColor = CorComercial});
                        clsPdf.addCell(tbRoteiro, new pdfLibCell() { Text = drw["Cod_Tipo_Comercial"].ToString(), FontColor = CorComercial });
                        clsPdf.addCell(tbRoteiro, new pdfLibCell() { Text = drw["Titulo_Comercial"].ToString(), Align = PdfPCell.ALIGN_LEFT, FontColor = CorComercial });
                        clsPdf.addCell(tbRoteiro, new pdfLibCell() { Text = drw["Descricao_Produto"].ToString() , Align = PdfPCell.ALIGN_LEFT, FontColor = CorComercial });
                        clsPdf.addCell(tbRoteiro, new pdfLibCell() { Text = Intervalos[drw["Tipo_Break"].ToString().ConvertToByte()], FontColor = CorComercial });
                        clsPdf.addCell(tbRoteiro, new pdfLibCell() { Text = drw["Duracao"].ToString(), FontColor = CorComercial });
                        hasRow = true;
                    }
                }
                //------------------------Imprime ultimo bloco
                if (hasRow)
                {
                    pc = write.DirectContent;
                    tbRoteiro.WriteSelectedRows(0, -1, 10, CurrentPosition, pc);
                    tbRoteiro = clsPdf.CreateTable(new float[] { 70, 50, 170, 170, 50, 60 });
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally
            {
                cnn.Close();
                try
                {
                    doc.Close();
                    doc.Dispose();
                    strea.Dispose();
                    write.Dispose();
                }
                catch (Exception)
                {
                    bolRetorno = false;
                }
                
            }
            return bolRetorno;
        }
        private void ImprimeCabecalho(PdfWriter ww, Document dd, DataRow drw)
        {
            PageNumber++;
            if (PageNumber > 1)
            {
                dd.NewPage();
            }

            String LogoName = "logo_Roteiro_" + drw["Cod_Veiculo"].ToString() + ".png";
            String sPathLogo = HttpContext.Current.Server.MapPath("~/logos/" + LogoName);
            if (!File.Exists(sPathLogo))
            {

                sPathLogo = HttpContext.Current.Server.MapPath("~/logos/" + "logo_Roteiro_Padrao.png");
            }

            PdfContentByte pc;
            PdfPTable tbDados = clsPdf.CreateTable(new float[] { 120, 450}); 
            
            CurrentPosition = 790;
            clsPdf.addLogo(dd, new pdfLibLogo() { X = 20, Y = 770, Path = sPathLogo, Scale = 100 });
            clsPdf.AddTexto(ww, new pdfLibText() { Text = "Roteiro da Programação Comercial", X = 200, Y = CurrentPosition, FontSize = 14 });
            
            clsPdf.addCell(tbDados, new pdfLibCell() { Text = "Data da Programação" ,Height=20, Align= PdfPCell.ALIGN_LEFT,Border=false});
            clsPdf.addCell(tbDados, new pdfLibCell() {  Text = drw["Data_Exibicao"].ToString().ConvertToDatetime().ToString("dd/MM/yyyy") + "       " + diaSemana[(int)drw["Data_Exibicao"].ToString().ConvertToDatetime().DayOfWeek],Align=PdfPCell.ALIGN_LEFT , Border = false });
            clsPdf.addCell(tbDados, new pdfLibCell() { Text = "Emissora", Height = 20 , Align= PdfPCell.ALIGN_LEFT, Border = false });
            clsPdf.addCell(tbDados, new pdfLibCell() { Text =  drw["Cod_Veiculo"].ToString() + "-" + drw["Nome_Veiculo"].ToString(), Height = 20 ,Align = PdfPCell.ALIGN_LEFT, Border = false });

            CurrentPosition -= 30;
            pc = ww.DirectContent;
            tbDados.WriteSelectedRows(0, -1, 10, CurrentPosition, pc);
            CurrentPosition -=tbDados.TotalHeight;
        }
        private void ImprimeTituloBreak(PdfWriter ww, Document dd, DataRow drw)
        {
            PdfContentByte pc;

            PdfPTable tbDados = clsPdf.CreateTable(new float[] { 70, 50, 170, 170, 50, 60 });
            

            clsPdf.addCell(tbDados, new pdfLibCell() { Text = "Programa:" + drw["Cod_Programa"].ToString() + " - " + drw["Titulo_Break"].ToString(),colspan=3,Align=PdfPCell.ALIGN_LEFT,Height=20,Background=System.Drawing.Color.WhiteSmoke,BorderRight=0});
            clsPdf.addCell(tbDados, new pdfLibCell() { Text = "Break:" + drw["Breaks"].ToString() + "  " + drw["Hora_Inicio_Break"].ToString().ConvertToDatetime().ToString("hh:mm"), Align = PdfPCell.ALIGN_LEFT , Background = System.Drawing.Color.WhiteSmoke,BorderLeft=0,BorderRight=0 });
            clsPdf.addCell(tbDados, new pdfLibCell() { Text = "Total do Break:" + drw["Encaixe"].ToString(), Align = PdfPCell.ALIGN_LEFT,colspan=2 , Background = System.Drawing.Color.WhiteSmoke ,BorderLeft=0});
            if (!String.IsNullOrEmpty( drw["Observacao_Break"].ToString()))
            {
                clsPdf.addCell(tbDados, new pdfLibCell() { Text = drw["Observacao_Break"].ToString(), colspan = 6 ,Align=PdfPCell.ALIGN_LEFT});
            }
            
            clsPdf.addCell(tbDados, new pdfLibCell() { Text = "N.Fita" });
            clsPdf.addCell(tbDados, new pdfLibCell() { Text = "Tipo" });
            clsPdf.addCell(tbDados, new pdfLibCell() { Text = "Titulo Comercial" });
            clsPdf.addCell(tbDados, new pdfLibCell() { Text = "Produto"});
            clsPdf.addCell(tbDados, new pdfLibCell() { Text = "Interv." });
            clsPdf.addCell(tbDados, new pdfLibCell() { Text = "Dur."});

            pc = ww.DirectContent;
            CurrentPosition -= 10;
            tbDados.WriteSelectedRows(0, -1,10,CurrentPosition,pc);
            CurrentPosition -= tbDados.TotalHeight;
        }
    


    }
}
