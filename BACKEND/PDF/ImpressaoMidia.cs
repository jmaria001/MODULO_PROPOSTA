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
    public class ImpressaoMidia
    {
        private String Credential;
        private String CurrentUser;
        private Int32 PageNumer = 0;
        private float CurrentPosition = 0;
        private SimLib clsLib = new SimLib();
        PdfLib clsPdf = new PdfLib();
        public ImpressaoMidia(String pUser)
        {
            this.Credential = pUser;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }
        public String ImprimirMIDIA(Int32 pId_Simulacao)
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
                    String sPath = HttpContext.Current.Server.MapPath("~/PDFFILES/MIDIA");
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
                    strFilePdf = "SIMULACAO_" + pId_Simulacao.ToString() + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".PDF";
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
            clsConexao cnn = new clsConexao(this.Credential);
            try
            {
                doc.SetMargins(10, 10, 10, 10);
                doc.Open();
                //==================Carrega Os Mapas da simulacao
                DataTable dtbEsquema = new DataTable("Contrato");
                SqlDataAdapter dtaEsquema = new SqlDataAdapter();
                
                cnn.Open();
                dtaEsquema.SelectCommand = cnn.Text(cnn.Connection, "Select Distinct Id_Simulacao,Id_Esquema,Indica_Midia_OnLine From Tb_Proposta_Esquema Where Id_Simulacao = " + pId_Simulacao);
                dtaEsquema.Fill(dtbEsquema);

                foreach (DataRow drw in dtbEsquema.Rows)
                {
                    this.ImprimeCabecalho(write, doc, drw["Id_Esquema"].ToString().ConvertToInt32());
                    this.ImprimDadosBase(write, doc, drw["Id_Esquema"].ToString().ConvertToInt32());
                    this.ImprimeComerciais(write, doc, drw["Id_Esquema"].ToString().ConvertToInt32());
                    if (drw["Indica_Midia_OnLine"].ToString().ConvertToBoolean())
                    {
                        this.ImprimeInsercaoOnLine(write, doc, drw["Id_Esquema"].ToString().ConvertToInt32());
                    }
                    else
                    {
                        this.ImprimeInsercao(write, doc, drw["Id_Esquema"].ToString().ConvertToInt32());
                    }
                    this.ImprimeEmissoras(write, doc, drw["Id_Esquema"].ToString().ConvertToInt32());
                }

            }
            catch (Exception)
            {

                throw;
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
        private void ImprimDadosBase(PdfWriter ww, Document dd, Int32 pid_Esquema)
        {
            try
            {
                //==================Carrega Dados Base
                DataTable dtb = new DataTable();
                SqlDataAdapter dta = new SqlDataAdapter();
                clsConexao cnn = new clsConexao(this.Credential);
                cnn.Open();
                dta.SelectCommand = cnn.Procedure(cnn.Connection, "Pr_Proposta_Imprime_Mapa_DadosBase");
                dta.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                dta.SelectCommand.Parameters.AddWithValue("@Par_Id_Esquema", pid_Esquema);
                dta.Fill(dtb);
                DataRow drw = dtb.Rows[0];

                PdfContentByte pc;
                PdfPTable tbAgencia = clsPdf.CreateTable(new float[] { 301, 110 });
                clsPdf.addCell(tbAgencia, new pdfLibCell() { Text = "Agencia", colspan = 2, FontStyle = iTextSharp.text.Font.BOLD, Align = PdfPCell.ALIGN_CENTER, FontSize = 12, Background = System.Drawing.Color.Silver, Height = 20 });
                clsPdf.addCell(tbAgencia, new pdfLibCell() { Text = drw["Nome_Agencia"].ToString(),Align= PdfPCell.ALIGN_LEFT,BorderRight=0,BorderLeft=0,BorderTop=0,BorderBottom=0});
                clsPdf.addCell(tbAgencia, new pdfLibCell() { Text = drw["Cnpj_Agencia"].ToString() ,Align = PdfPCell.ALIGN_LEFT, BorderRight = 0, BorderLeft = 0, BorderTop = 0, BorderBottom = 0 });
                clsPdf.addCell(tbAgencia, new pdfLibCell() { Text = drw["Endereco_Agencia"].ToString(), Align = PdfPCell.ALIGN_LEFT, BorderRight = 0, BorderLeft = 0, BorderTop = 0, BorderBottom = 0 });
                clsPdf.addCell(tbAgencia, new pdfLibCell() { Text = drw["Bairro_Agencia"].ToString() ,Align = PdfPCell.ALIGN_LEFT, BorderRight = 0, BorderLeft = 0, BorderTop = 0, BorderBottom = 0 });
                clsPdf.addCell(tbAgencia, new pdfLibCell() { Text = drw["Municipio_Agencia"].ToString(), Align = PdfPCell.ALIGN_LEFT, BorderRight = 0, BorderLeft = 0, BorderTop = 0, BorderBottom = 0 });
                clsPdf.addCell(tbAgencia, new pdfLibCell() { Text = drw["Cep_Agencia"].ToString() ,Align = PdfPCell.ALIGN_LEFT, BorderRight = 0, BorderLeft = 0, BorderTop = 0, BorderBottom = 0 });
                pc = ww.DirectContent;
                tbAgencia.WriteSelectedRows(0, -1, 10, 480, pc);

                PdfPTable tbCliente = clsPdf.CreateTable(new float[] { 300, 110 });
                clsPdf.addCell(tbCliente, new pdfLibCell() { Text = "Cliente", colspan = 2, FontStyle = iTextSharp.text.Font.BOLD, Align = PdfPCell.ALIGN_CENTER, FontSize = 12, Background = System.Drawing.Color.Silver, Height = 20 });
                clsPdf.addCell(tbCliente, new pdfLibCell() { Text = drw["Nome_Cliente"].ToString(), Align = PdfPCell.ALIGN_LEFT, BorderRight = 0, BorderLeft = 1, BorderTop = 0, BorderBottom = 0 });
                clsPdf.addCell(tbCliente, new pdfLibCell() { Text = drw["Cnpj_Cliente"].ToString(), Align = PdfPCell.ALIGN_LEFT, BorderRight = 0, BorderLeft = 0, BorderTop = 0, BorderBottom = 0 });
                clsPdf.addCell(tbCliente, new pdfLibCell() { Text = drw["Endereco_Cliente"].ToString(), Align = PdfPCell.ALIGN_LEFT, BorderRight = 0, BorderLeft = 1, BorderTop = 0, BorderBottom = 0 });
                clsPdf.addCell(tbCliente, new pdfLibCell() { Text = drw["Bairro_Cliente"].ToString(), Align = PdfPCell.ALIGN_LEFT, BorderRight = 0, BorderLeft = 0, BorderTop = 0, BorderBottom = 0 });
                clsPdf.addCell(tbCliente, new pdfLibCell() { Text = drw["Municipio_Cliente"].ToString(), Align = PdfPCell.ALIGN_LEFT, BorderRight = 0, BorderLeft = 1, BorderTop = 0, BorderBottom = 0 });
                clsPdf.addCell(tbCliente, new pdfLibCell() { Text = drw["Cep_Cliente"].ToString(), Align = PdfPCell.ALIGN_LEFT, BorderRight = 0, BorderLeft = 0, BorderTop = 0, BorderBottom = 0 });
                pc = ww.DirectContent;
                tbCliente.WriteSelectedRows(0, -1, 421, 480, pc);
                CurrentPosition = 480 - tbCliente.TotalHeight;
                

            }
            catch (Exception)
            {
                throw;
            }

        }
        private void ImprimeCabecalho(PdfWriter ww, Document dd, Int32 pid_Esquema)
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
            dta.SelectCommand = cnn.Procedure(cnn.Connection, "Pr_Proposta_Imprime_Mapa_DadosBase");
            dta.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
            dta.SelectCommand.Parameters.AddWithValue("@Par_Id_Esquema", pid_Esquema);
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
            clsPdf.addCell(tbDados, new pdfLibCell() { Text = "Competencia" });
            clsPdf.addCell(tbDados, new pdfLibCell() { Text = drw["Competencia_Text"].ToString() });
            clsPdf.addCell(tbDados, new pdfLibCell() { Text = "Início Campanha" });
            clsPdf.addCell(tbDados, new pdfLibCell() { Text = drw["Inicio_Campanha"].ToString() });
            clsPdf.addCell(tbDados, new pdfLibCell() { Text = "Término Campanha" });
            clsPdf.addCell(tbDados, new pdfLibCell() { Text = drw["Termino_Campanha"].ToString() });
            clsPdf.addCell(tbDados, new pdfLibCell() { Text = "Tabela de Preços" });
            clsPdf.addCell(tbDados, new pdfLibCell() { Text = drw["Tabela_Preco"].ToString() });
            clsPdf.addCell(tbDados, new pdfLibCell() { Text = "Tipo" });
            if (drw["Indica_Midia_OnLine"].ToString().ConvertToBoolean())
            {
                clsPdf.addCell(tbDados, new pdfLibCell() { Text = "ON-LINE" });
            }
            else
            {
                clsPdf.addCell(tbDados, new pdfLibCell() { Text = "OFF-LINE" });
            }
            clsPdf.addCell(tbDados, new pdfLibCell() { Text = drw["Tabela_Preco"].ToString() });

            pc = ww.DirectContent;
            tbDados.WriteSelectedRows(0, -1, 632, 585, pc);
            CurrentPosition = 585 - tbDados.TotalHeight;
            
            
        }
        private void ImprimeComerciais(PdfWriter ww, Document dd, Int32 pId_Esquema)
        {
            PdfContentByte pc;
            Boolean hasRow = false;
            Boolean BolPrimeiro;
            //==================Carrega COmercial
            DataTable dtb = new DataTable();
            SqlDataAdapter dta = new SqlDataAdapter();
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            dta.SelectCommand = cnn.Procedure(cnn.Connection, "Pr_Proposta_Imprime_Mapa_Comercial");
            dta.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
            dta.SelectCommand.Parameters.AddWithValue("@Par_Id_Esquema", pId_Esquema);
            dta.Fill(dtb);
            //==================Imprime
            PdfPTable tbComercial = clsPdf.CreateTable(new float[] { 70, 340, 70, 341 });
            BolPrimeiro = true;
            foreach (DataRow drwC in dtb.Rows)
            {
                if (CurrentPosition - tbComercial.TotalHeight < 50)
                {
                    if (hasRow)
                    {
                        pc = ww.DirectContent;
                        tbComercial.WriteSelectedRows(0, -1, 10, CurrentPosition, pc);
                        BolPrimeiro = true;
                    }
                    this.ImprimeCabecalho(ww, dd, pId_Esquema);
                }
                if (BolPrimeiro)
                {
                    tbComercial = clsPdf.CreateTable(new float[] { 70, 340, 70, 341 });
                    clsPdf.addCell(tbComercial, new pdfLibCell() { Text = "Comerciais", Height = 20, Background = System.Drawing.Color.Silver, colspan = 4 });
                    clsPdf.addCell(tbComercial, new pdfLibCell() { Text = "Leg." });
                    clsPdf.addCell(tbComercial, new pdfLibCell() { Text = "Título" });
                    clsPdf.addCell(tbComercial, new pdfLibCell() { Text = "Dur." });
                    clsPdf.addCell(tbComercial, new pdfLibCell() { Text = "Tipo" });
                    BolPrimeiro = false;
                }

                clsPdf.addCell(tbComercial, new pdfLibCell() { Text = drwC["Cod_Comercial"].ToString() });
                clsPdf.addCell(tbComercial, new pdfLibCell() { Text = drwC["Titulo_Comercial"].ToString() });
                clsPdf.addCell(tbComercial, new pdfLibCell() { Text = drwC["Duracao"].ToString() });
                clsPdf.addCell(tbComercial, new pdfLibCell() { Text = drwC["Nome_Tipo_Comercial"].ToString() });

                hasRow = true;
            }
            if (hasRow)
            {
                pc = ww.DirectContent;
                tbComercial.WriteSelectedRows(0, -1, 10, CurrentPosition, pc);
                CurrentPosition -= tbComercial.TotalHeight;
            }
        }
        private void ImprimeInsercao(PdfWriter ww, Document dd, Int32 pId_Esquema)
        {
            PdfContentByte pc;
            Boolean hasRow = false;
            Boolean BolPrimeiro;
            //==================Carrega Comercial
            DataTable dtb = new DataTable();
            SqlDataAdapter dta = new SqlDataAdapter();
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            dta.SelectCommand = cnn.Procedure(cnn.Connection, "Pr_Proposta_Imprime_Mapa_Midia");
            dta.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
            dta.SelectCommand.Parameters.AddWithValue("@Par_Id_Esquema", pId_Esquema);
            dta.Fill(dtb);
            //==================Imprime
            List<TableTemplate> Grid = new List<TableTemplate>();
            Grid.Add(new TableTemplate() { Size = 41, Header = "Programa", Field = "Cod_Programa" });
            Grid.Add(new TableTemplate() { Size = 25, Header = "Car.", Field = "Cod_Caracteristica" });
            Grid.Add(new TableTemplate() { Size = 25, Header = "Leg.", Field = "Cod_Comercial" });
            Grid.Add(new TableTemplate() { Size = 15, Header = "01", Field = "D01" });
            Grid.Add(new TableTemplate() { Size = 15, Header = "02", Field = "D02" });
            Grid.Add(new TableTemplate() { Size = 15, Header = "03", Field = "D03" });
            Grid.Add(new TableTemplate() { Size = 15, Header = "04", Field = "D04" });
            Grid.Add(new TableTemplate() { Size = 15, Header = "05", Field = "D05" });
            Grid.Add(new TableTemplate() { Size = 15, Header = "06", Field = "D06" });
            Grid.Add(new TableTemplate() { Size = 15, Header = "07", Field = "D07" });
            Grid.Add(new TableTemplate() { Size = 15, Header = "08", Field = "D08" });
            Grid.Add(new TableTemplate() { Size = 15, Header = "09", Field = "D09" });
            Grid.Add(new TableTemplate() { Size = 15, Header = "10", Field = "D10" });
            Grid.Add(new TableTemplate() { Size = 15, Header = "11", Field = "D11" });
            Grid.Add(new TableTemplate() { Size = 15, Header = "12", Field = "D12" });
            Grid.Add(new TableTemplate() { Size = 15, Header = "13", Field = "D13" });
            Grid.Add(new TableTemplate() { Size = 15, Header = "14", Field = "D14" });
            Grid.Add(new TableTemplate() { Size = 15, Header = "15", Field = "D15" });
            Grid.Add(new TableTemplate() { Size = 15, Header = "16", Field = "D16" });
            Grid.Add(new TableTemplate() { Size = 15, Header = "17", Field = "D17" });
            Grid.Add(new TableTemplate() { Size = 15, Header = "18", Field = "D18" });
            Grid.Add(new TableTemplate() { Size = 15, Header = "19", Field = "D19" });
            Grid.Add(new TableTemplate() { Size = 15, Header = "20", Field = "D20" });
            Grid.Add(new TableTemplate() { Size = 15, Header = "21", Field = "D21" });
            Grid.Add(new TableTemplate() { Size = 15, Header = "22", Field = "D22" });
            Grid.Add(new TableTemplate() { Size = 15, Header = "23", Field = "D23" });
            Grid.Add(new TableTemplate() { Size = 15, Header = "24", Field = "D24" });
            Grid.Add(new TableTemplate() { Size = 15, Header = "25", Field = "D25" });
            Grid.Add(new TableTemplate() { Size = 15, Header = "26", Field = "D26" });
            Grid.Add(new TableTemplate() { Size = 15, Header = "27", Field = "D27" });
            Grid.Add(new TableTemplate() { Size = 15, Header = "28", Field = "D28" });
            Grid.Add(new TableTemplate() { Size = 15, Header = "29", Field = "D29" });
            Grid.Add(new TableTemplate() { Size = 15, Header = "30", Field = "D30" });
            Grid.Add(new TableTemplate() { Size = 15, Header = "31", Field = "D31" });
            Grid.Add(new TableTemplate() { Size = 35, Header = "Qtd Total", Field = "Qtd_Linha", Align = PdfPCell.ALIGN_RIGHT });
            Grid.Add(new TableTemplate() { Size = 55, Header = "Vlr Tabela Unit.", Field = "Valor_Tabela_Unitario", Align = PdfPCell.ALIGN_RIGHT });
            Grid.Add(new TableTemplate() { Size = 55, Header = "Vlr Tabela Total.", Field = "Valor_Tabela_Total", Align = PdfPCell.ALIGN_RIGHT });
            Grid.Add(new TableTemplate() { Size = 60, Header = "Vlr Negociado", Field = "Valor_Negociado_Total", Align = PdfPCell.ALIGN_RIGHT });
            Grid.Add(new TableTemplate() { Size = 60, Header = "Desc.", Field = "Desconto_Real", Align = PdfPCell.ALIGN_RIGHT });

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
                    this.ImprimeCabecalho(ww, dd, pId_Esquema);
                }
                if (BolPrimeiro)
                {
                    tbMidia = clsPdf.CreateTable(GridCells);
                    clsPdf.addCell(tbMidia, new pdfLibCell() { Text = "Mídia", Height = 20, Background = System.Drawing.Color.Silver, colspan = Grid.Count, FontStyle = iTextSharp.text.Font.BOLD });
                    for (int i = 0; i < Grid.Count; i++)
                    {
                        clsPdf.addCell(tbMidia, new pdfLibCell() { Text = Grid[i].Header, FontSize = 7, FontStyle = iTextSharp.text.Font.BOLD ,Background=System.Drawing.Color.Silver});
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
                    clsPdf.addCell(tbMidia, new pdfLibCell() { Text = "Totais", colspan = 3, FontSize = 7, FontStyle = iTextSharp.text.Font.BOLD });
                    for (int i = 3; i < Grid.Count; i++)
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
        private void ImprimeInsercaoOnLine(PdfWriter ww, Document dd, Int32 pId_Esquema)
        {
            PdfContentByte pc;
            Boolean hasRow = false;
            Boolean BolPrimeiro;
            //==================Carrega Midia On Line
            DataTable dtb = new DataTable();
            SqlDataAdapter dta = new SqlDataAdapter();
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            dta.SelectCommand = cnn.Procedure(cnn.Connection, "Pr_Proposta_Imprime_Mapa_Midia_OnLine");
            dta.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
            dta.SelectCommand.Parameters.AddWithValue("@Par_Id_Esquema", pId_Esquema);
            dta.Fill(dtb);
            //==================Imprime
            List<TableTemplate> Grid = new List<TableTemplate>();
            Grid.Add(new TableTemplate() { Size = 116, Header = "Localização", Field = "Nome_Programa", Align = PdfPCell.ALIGN_LEFT});
            Grid.Add(new TableTemplate() { Size = 25, Header = "Car.", Field = "Cod_Caracteristica" });
            Grid.Add(new TableTemplate() { Size = 25, Header = "Leg.", Field = "Cod_Comercial" });
            Grid.Add(new TableTemplate() { Size = 100, Header = "Título Comercial", Field = "Titulo_Comercial", Align = PdfPCell.ALIGN_LEFT });
            Grid.Add(new TableTemplate() { Size = 100, Header = "Tipo Comercial", Field = "Tipo_Comercial" });
            Grid.Add(new TableTemplate() { Size = 80, Header = "Tipo Comercialização", Field = "Nome_Tipo_Comercializacao", Align = PdfPCell.ALIGN_LEFT });
            Grid.Add(new TableTemplate() { Size = 100, Header = "Período", Field = "Periodo", Align = PdfPCell.ALIGN_CENTER});
            Grid.Add(new TableTemplate() { Size = 35, Header = "Qtd Acões", Field = "Qtd_Insercoes", Align = PdfPCell.ALIGN_RIGHT });
            Grid.Add(new TableTemplate() { Size = 60, Header = "Vlr Tabela Unit.", Field = "Valor_Tabela_Unitario", Align = PdfPCell.ALIGN_RIGHT });
            Grid.Add(new TableTemplate() { Size = 60, Header = "Vlr Tabela Total.", Field = "Valor_Tabela_Total", Align = PdfPCell.ALIGN_RIGHT });
            Grid.Add(new TableTemplate() { Size = 60, Header = "Vlr Negociado", Field = "Valor_Negociado_Total", Align = PdfPCell.ALIGN_RIGHT });
            Grid.Add(new TableTemplate() { Size = 60, Header = "Desc.", Field = "Desconto_Real", Align = PdfPCell.ALIGN_RIGHT });

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
                    this.ImprimeCabecalho(ww, dd, pId_Esquema);
                }
                if (BolPrimeiro)
                {
                    tbMidia = clsPdf.CreateTable(GridCells);
                    clsPdf.addCell(tbMidia, new pdfLibCell() { Text = "Mídia", Height = 20, Background = System.Drawing.Color.Silver, colspan = Grid.Count, FontStyle = iTextSharp.text.Font.BOLD });
                    for (int i = 0; i < Grid.Count; i++)
                    {
                        clsPdf.addCell(tbMidia, new pdfLibCell() { Text = Grid[i].Header, FontSize = 7, FontStyle = iTextSharp.text.Font.BOLD, Background = System.Drawing.Color.Silver });
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
                    clsPdf.addCell(tbMidia, new pdfLibCell() { Text = "Totais", colspan = 3, FontSize = 7, FontStyle = iTextSharp.text.Font.BOLD });
                    for (int i = 3; i < Grid.Count; i++)
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
        private void ImprimeEmissoras(PdfWriter ww, Document dd, Int32 pId_Esquema)
        {
            PdfContentByte pc;
            Boolean hasRow = false;
            Boolean BolPrimeiro;
            //==================Carrega Dados
            DataTable dtb = new DataTable();
            SqlDataAdapter dta = new SqlDataAdapter();
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            dta.SelectCommand = cnn.Procedure(cnn.Connection, "Pr_Proposta_Imprime_Mapa_Emissoras");
            dta.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
            dta.SelectCommand.Parameters.AddWithValue("@Par_Id_Esquema", pId_Esquema);
            dta.Fill(dtb);
            int qtdEmissorasLinha = 0;
            //==================Imprime
            PdfPTable tbEmissora = clsPdf.CreateTable(new float[] { 205, 205, 205, 206 });
            BolPrimeiro = true;
            foreach (DataRow drwV in dtb.Rows)
            {
                if (CurrentPosition - tbEmissora.TotalHeight < 50)
                {
                    if (hasRow)
                    {
                        for (int i = qtdEmissorasLinha + 1; i <= 4; i++)
                        {
                            clsPdf.addCell(tbEmissora, new pdfLibCell() { Text = "" });
                        }
                        pc = ww.DirectContent;
                        tbEmissora.WriteSelectedRows(0, -1, 10, CurrentPosition, pc);
                        BolPrimeiro = true;
                    }
                    this.ImprimeCabecalho(ww, dd, pId_Esquema);
                }
                if (BolPrimeiro)
                {
                    tbEmissora = clsPdf.CreateTable(new float[] { 205, 205, 205, 206 });
                    clsPdf.addCell(tbEmissora, new pdfLibCell() { Text = "Emissoras", Height = 20, Background = System.Drawing.Color.Silver, colspan = 4 });
                    BolPrimeiro = false;
                    qtdEmissorasLinha = 0;
                }
                clsPdf.addCell(tbEmissora, new pdfLibCell() { Text = drwV["Nome_Veiculo_Text"].ToString() });
                qtdEmissorasLinha++;
                hasRow = true;
            }
            if (hasRow)
            {
                for (int i = qtdEmissorasLinha + 1; i <= 4; i++)
                {
                    clsPdf.addCell(tbEmissora, new pdfLibCell() { Text = "" });
                }
                pc = ww.DirectContent;
                tbEmissora.WriteSelectedRows(0, -1, 10, CurrentPosition, pc);
                CurrentPosition -= tbEmissora.TotalHeight;
            }
        }

    }
}
