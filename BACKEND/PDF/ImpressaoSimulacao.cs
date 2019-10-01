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
    public class ImpressaoAnalise
    {
        private String Credential;
        private String CurrentUser;
        private Int32 PageNumer = 0;
        private float CurrentPosition = 0;
        private SimLib clsLib = new SimLib();
        PdfLib clsPdf = new PdfLib();
        public ImpressaoAnalise(String pUser)
        {
            this.Credential = pUser;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }
        public String ImprimirAnalise(Int32 pId_Simulacao)
        {
            String strFilePdf = string.Empty;
            String PdfFinal = string.Empty;
            DataTable dtbEsquema = new DataTable("");
            SqlDataAdapter dtaEsquema = new SqlDataAdapter();
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            try
            {
                //==================Verifica se existe a simulacao
                dtaEsquema.SelectCommand = cnn.Text(cnn.Connection, "Select Distinct Id_Simulacao,Id_Esquema From Tb_Proposta_Esquema Where Id_Simulacao = " + pId_Simulacao);
                dtaEsquema.Fill(dtbEsquema);
                if (dtbEsquema.Rows.Count > 0)
                {
                    //'------------------------Diretorio Temporario para geracao dos PDF
                    String sPath = HttpContext.Current.Server.MapPath("~/PDFFILES/ANALISE");
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
                    strFilePdf = "ANALISE_SIMULACAO_" + pId_Simulacao.ToString() + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".PDF";
                    if (GerarPdf(pId_Simulacao, sPath, strFilePdf))
                    {
                        PdfFinal = strFilePdf;
                    }
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
                this.ImprimeCabecalho(write, doc, pId_Simulacao); 
                this.ImprimDadosBase(write, doc, pId_Simulacao);
                this.ImprimeAnalise(write, doc, pId_Simulacao);
            }
            catch (Exception Ex)
            {

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
        private void ImprimDadosBase(PdfWriter ww, Document dd, Int32 pid_Simulacao)
        {
            try
            {
                //==================Carrega Dados Base
                DataTable dtb = new DataTable();
                SqlDataAdapter dta = new SqlDataAdapter();
                clsConexao cnn = new clsConexao(this.Credential);
                cnn.Open();
                dta.SelectCommand = cnn.Procedure(cnn.Connection, "Pr_Proposta_Analise_Dados_Base");
                dta.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                dta.SelectCommand.Parameters.AddWithValue("@Par_Id_Simulacao", pid_Simulacao);
                dta.Fill(dtb);
                DataRow drw = dtb.Rows[0];

                PdfContentByte pc;
                PdfPTable tbAgencia = clsPdf.CreateTable(new float[] { 301, 110 });
                clsPdf.addCell(tbAgencia, new pdfLibCell() { Text = "Agencia", colspan = 2, FontStyle = iTextSharp.text.Font.BOLD, Align = PdfPCell.ALIGN_CENTER, FontSize = 12, Background = System.Drawing.Color.Silver, Height = 20 });
                clsPdf.addCell(tbAgencia, new pdfLibCell() { Text = drw["Nome_Agencia"].ToString(), Align = PdfPCell.ALIGN_LEFT, BorderRight = 0, BorderLeft = 0, BorderTop = 0, BorderBottom = 0 });
                clsPdf.addCell(tbAgencia, new pdfLibCell() { Text = drw["Cnpj_Agencia"].ToString(), Align = PdfPCell.ALIGN_LEFT, BorderRight = 0, BorderLeft = 0, BorderTop = 0, BorderBottom = 0 });
                clsPdf.addCell(tbAgencia, new pdfLibCell() { Text = drw["Endereco_Agencia"].ToString(), Align = PdfPCell.ALIGN_LEFT, BorderRight = 0, BorderLeft = 0, BorderTop = 0, BorderBottom = 0 });
                clsPdf.addCell(tbAgencia, new pdfLibCell() { Text = drw["Bairro_Agencia"].ToString(), Align = PdfPCell.ALIGN_LEFT, BorderRight = 0, BorderLeft = 0, BorderTop = 0, BorderBottom = 0 });
                clsPdf.addCell(tbAgencia, new pdfLibCell() { Text = drw["Municipio_Agencia"].ToString(), Align = PdfPCell.ALIGN_LEFT, BorderRight = 0, BorderLeft = 0, BorderTop = 0, BorderBottom = 0 });
                clsPdf.addCell(tbAgencia, new pdfLibCell() { Text = drw["Cep_Agencia"].ToString(), Align = PdfPCell.ALIGN_LEFT, BorderRight = 0, BorderLeft = 0, BorderTop = 0, BorderBottom = 0 });
                pc = ww.DirectContent;
                tbAgencia.WriteSelectedRows(0, -1, 10, 495, pc);

                PdfPTable tbCliente = clsPdf.CreateTable(new float[] { 300, 110 });
                clsPdf.addCell(tbCliente, new pdfLibCell() { Text = "Cliente", colspan = 2, FontStyle = iTextSharp.text.Font.BOLD, Align = PdfPCell.ALIGN_CENTER, FontSize = 12, Background = System.Drawing.Color.Silver, Height = 20 });
                clsPdf.addCell(tbCliente, new pdfLibCell() { Text = drw["Nome_Cliente"].ToString(), Align = PdfPCell.ALIGN_LEFT, BorderRight = 0, BorderLeft = 1, BorderTop = 0, BorderBottom = 0 });
                clsPdf.addCell(tbCliente, new pdfLibCell() { Text = drw["Cnpj_Cliente"].ToString(), Align = PdfPCell.ALIGN_LEFT, BorderRight = 0, BorderLeft = 0, BorderTop = 0, BorderBottom = 0 });
                clsPdf.addCell(tbCliente, new pdfLibCell() { Text = drw["Endereco_Cliente"].ToString(), Align = PdfPCell.ALIGN_LEFT, BorderRight = 0, BorderLeft = 1, BorderTop = 0, BorderBottom = 0 });
                clsPdf.addCell(tbCliente, new pdfLibCell() { Text = drw["Bairro_Cliente"].ToString(), Align = PdfPCell.ALIGN_LEFT, BorderRight = 0, BorderLeft = 0, BorderTop = 0, BorderBottom = 0 });
                clsPdf.addCell(tbCliente, new pdfLibCell() { Text = drw["Municipio_Cliente"].ToString(), Align = PdfPCell.ALIGN_LEFT, BorderRight = 0, BorderLeft = 1, BorderTop = 0, BorderBottom = 0 });
                clsPdf.addCell(tbCliente, new pdfLibCell() { Text = drw["Cep_Cliente"].ToString(), Align = PdfPCell.ALIGN_LEFT, BorderRight = 0, BorderLeft = 0, BorderTop = 0, BorderBottom = 0 });
                pc = ww.DirectContent;
                tbCliente.WriteSelectedRows(0, -1, 421, 495, pc);
                CurrentPosition = 495 - tbCliente.TotalHeight;

            }
            catch (Exception)
            {
                throw;
            }

        }
        private void ImprimeCabecalho(PdfWriter ww, Document dd, Int32 pid_Simulacao)
        {
            PageNumer++;
            if (PageNumer > 1)
            {
                dd.NewPage();
            }


            //==================Carrega Dados 
            DataTable dtb = new DataTable();
            SqlDataAdapter dta = new SqlDataAdapter();
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            try
            {
                dta.SelectCommand = cnn.Procedure(cnn.Connection, "Pr_Proposta_Analise_Dados_Base");
                dta.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                dta.SelectCommand.Parameters.AddWithValue("@Par_Id_Simulacao", pid_Simulacao);
                dta.Fill(dtb);
                DataRow drw = dtb.Rows[0];

                String LogoName = "logo_" + drw["Cod_Empresa"].ToString() + ".png";
                String sPathLogo = HttpContext.Current.Server.MapPath("~/logos/" + LogoName);
                if (!File.Exists(sPathLogo))
                {

                    sPathLogo = HttpContext.Current.Server.MapPath("~/logos/" + "logo_Padrao.png");
                }

                PdfContentByte pc;

                clsPdf.addBorder(ww, dd);
                clsPdf.addLogo(dd, new pdfLibLogo() { X = 20, Y = 500, Path = sPathLogo, Scale = 100 });

                clsPdf.AddTexto(ww, new pdfLibText() { X = 150, Y = 550, Text = drw["Razao_Social"].ToString(), FontSize = 14 });
                clsPdf.AddTexto(ww, new pdfLibText() { X = 150, Y = 530, Text = "CNPJ", FontSize = 8 });
                clsPdf.AddTexto(ww, new pdfLibText() { X = 200, Y = 530, Text = drw["Cnpj"].ToString(), FontSize = 8 });
                clsPdf.AddTexto(ww, new pdfLibText() { X = 150, Y = 520, Text = "Endereço", FontSize = 8 });
                clsPdf.AddTexto(ww, new pdfLibText() { X = 200, Y = 520, Text = drw["Endereco"].ToString(), FontSize = 8 });
                clsPdf.AddTexto(ww, new pdfLibText() { X = 150, Y = 510, Text = "Município", FontSize = 8 });
                clsPdf.AddTexto(ww, new pdfLibText() { X = 200, Y = 510, Text = drw["Cidade_Text"].ToString(), FontSize = 8 });

                PdfPTable tbDados = clsPdf.CreateTable(new float[] { 100, 100 });
                clsPdf.addCell(tbDados, new pdfLibCell() { Text = "N.Simulacao" });
                clsPdf.addCell(tbDados, new pdfLibCell() { Text = drw["Id_Simulacao"].ToString() });
                clsPdf.addCell(tbDados, new pdfLibCell() { Text = "Data" });
                clsPdf.addCell(tbDados, new pdfLibCell() { Text = drw["Data_Inclusao"].ToString() });
                //clsPdf.addCell(tbDados, new pdfLibCell() { Text = "Competencia" });
                //clsPdf.addCell(tbDados, new pdfLibCell() { Text = drw["Competencia_Text"].ToString() });
                clsPdf.addCell(tbDados, new pdfLibCell() { Text = "Início Validade" });
                clsPdf.addCell(tbDados, new pdfLibCell() { Text = drw["Validade_Inicio"].ToString() });
                clsPdf.addCell(tbDados, new pdfLibCell() { Text = "Término Validade" });
                clsPdf.addCell(tbDados, new pdfLibCell() { Text = drw["Validade_Termino"].ToString() });
                clsPdf.addCell(tbDados, new pdfLibCell() { Text = "Tabela de Preços" });
                clsPdf.addCell(tbDados, new pdfLibCell() { Text = drw["Tabela_Preco"].ToString() });
                clsPdf.addCell(tbDados, new pdfLibCell() { Text = ""});
                clsPdf.addCell(tbDados, new pdfLibCell() { Text = ""});

                pc = ww.DirectContent;
                tbDados.WriteSelectedRows(0, -1, 632, 585, pc);
                CurrentPosition = 585 - tbDados.TotalHeight;
            }
            catch (Exception Ex)
            {

                throw new Exception(Ex.Message);
            }
        }
        private void ImprimeAnalise(PdfWriter ww, Document dd, Int32 pId_Simulacao)
        {
            PdfContentByte pc;
            Boolean hasRow = false;
            Boolean BolPrimeiro;
            //==================Carrega Dados
            DataTable dtb = new DataTable();
            SqlDataAdapter dta = new SqlDataAdapter();
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            dta.SelectCommand = cnn.Procedure(cnn.Connection, "Pr_Proposta_Analise_Simulacao");
            dta.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
            dta.SelectCommand.Parameters.AddWithValue("@Par_Id_Simulacao", pId_Simulacao);
            dta.Fill(dtb);
            //==================Imprime
            List<TableTemplate> Grid = new List<TableTemplate>();
            Grid.Add(new TableTemplate() { Size = 70, Header = "Mes/Ano", Field = "Competencia_Text" });
            Grid.Add(new TableTemplate() { Size = 150, Header = "Veiculo", Field = "Veiculo", Align= PdfPCell.ALIGN_LEFT});
            Grid.Add(new TableTemplate() { Size = 100, Header = "Tipo_Comercial", Field = "Tipo_Comercial" ,Align = PdfPCell.ALIGN_LEFT });
            Grid.Add(new TableTemplate() { Size = 30, Header = "Dur.", Field = "Duracao" });
            Grid.Add(new TableTemplate() { Size = 30, Header = "Qtd", Field = "Qtd_Insercoes" });
            Grid.Add(new TableTemplate() { Size = 70, Header = "Valor Tabela Unitário.", Field = "Valor_Tabela_Unitario" ,Align = PdfPCell.ALIGN_RIGHT});
            Grid.Add(new TableTemplate() { Size = 70, Header = "Valor Tabela Total", Field = "Valor_Tabela_Total", Align = PdfPCell.ALIGN_RIGHT });
            Grid.Add(new TableTemplate() { Size = 70, Header = "Valor Negociado Total", Field = "Valor_Negociado_Total", Align = PdfPCell.ALIGN_RIGHT });
            Grid.Add(new TableTemplate() { Size = 70, Header = "Desconto", Field = "Desconto_aplicado", Align = PdfPCell.ALIGN_RIGHT });
            Grid.Add(new TableTemplate() { Size = 162, Header = "Tipo Desconto", Field = "Tipo_Desconto", Align = PdfPCell.ALIGN_LEFT});
            

            float[] GridCells = new float[Grid.Count];
            for (int i = 0; i < Grid.Count; i++)
            {
                GridCells[i] = Grid[i].Size;
            }


            PdfPTable tbMidia = clsPdf.CreateTable(GridCells);
            BolPrimeiro = true;
            foreach (DataRow drwM in dtb.Rows)
            {
                if (CurrentPosition - tbMidia.TotalHeight < 50)
                {
                    if (hasRow)
                    {
                        pc = ww.DirectContent;
                        tbMidia.WriteSelectedRows(0, -1, 10, CurrentPosition, pc);
                        BolPrimeiro = true;
                    }
                    this.ImprimeCabecalho(ww, dd, pId_Simulacao);
                }
                if (BolPrimeiro)
                {
                    tbMidia = clsPdf.CreateTable(GridCells);
                    clsPdf.addCell(tbMidia, new pdfLibCell() { Text = drwM["Descricao_Tipo"].ToString(), FontSize=12, Height = 20, Background = System.Drawing.Color.Silver, colspan = Grid.Count, FontStyle = iTextSharp.text.Font.BOLD });
                    for (int i = 0; i < Grid.Count; i++)
                    {
                        clsPdf.addCell(tbMidia, new pdfLibCell() { Text = Grid[i].Header, FontSize = 7, FontStyle = iTextSharp.text.Font.BOLD });
                    }
                    BolPrimeiro = false;
                }
                if (drwM["Tipo_Linha"].ToString() == "0")
                {
                    for (int i = 0; i < Grid.Count; i++)
                    {
                        clsPdf.addCell(tbMidia, new pdfLibCell() { Text = drwM[Grid[i].Field].ToString(), FontSize = 7, Align = Grid[i].Align });
                    }
                }
                else
                {
                    clsPdf.addCell(tbMidia, new pdfLibCell() { Text = "Totais", colspan = 4, FontSize = 7, FontStyle = iTextSharp.text.Font.BOLD });
                    for (int i = 4; i < Grid.Count; i++)
                    {
                        clsPdf.addCell(tbMidia, new pdfLibCell() { Text = drwM[Grid[i].Field].ToString(), FontSize = 7, Align = Grid[i].Align });
                    }
                }


                hasRow = true;
            }
            if (hasRow)
            {
                pc = ww.DirectContent;
                tbMidia.WriteSelectedRows(0, -1, 10, CurrentPosition, pc);
                CurrentPosition -= tbMidia.TotalHeight;
            }
        }
    }
}
