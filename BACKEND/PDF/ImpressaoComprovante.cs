using CLASSDB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;


namespace PROPOSTA
{
    public class ImpressaoComprovante
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        Int32 pdfPageNumber = 0;
        public ImpressaoComprovante(String pUser)
        {
            this.Credential = pUser;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

        public String ImprimirComprovante(DataTable dtbComprovantes)
        {
            String strFilePdf = string.Empty;
            String PdfFinal = string.Empty;
            try
            {
                //'------------------------Diretorio Temporario para geracao dos PDF
                String sPath = HttpContext.Current.Server.MapPath("~/PDFFILES/COMPROVANTE");
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

                //=========================Gera um pdf para cada comprovante encontrado/ guarda o nome do arquivo gerado em uma lista para juntar todos depois
                List<String> PdfGerados = new List<string>();
                foreach (DataRow drw in dtbComprovantes.Rows)
                {
                    strFilePdf = "CE_" + drw["Id_Contrato"].ToString() + "_" + drw["Numero_Ce"].ToString() + DateTime.Now.ToString("yyyyMMddHHmmss") + ".PDF";
                    if (GerarPdf(drw["Id_Contrato"].ToString().ConvertToInt32(), drw["Numero_Ce"].ToString().ConvertToInt32(), sPath, strFilePdf))
                    {
                        PdfGerados.Add(strFilePdf);
                    }
                }

                //======================================Merge Arquivos Pdfs Gerados
                if (PdfGerados.Count > 0)
                {
                    PdfFinal = "CE-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".PDF";
                    this.MergePDF(sPath, PdfFinal, PdfGerados);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return PdfFinal;
        }
        private Boolean GerarPdf(Int32 Id_Contrato, Int32 Numero_Ce, String Path, String FileName)
        {
            
            Boolean bolRetorno = true;
            //'==================Cria uma documento pdf
            String strFile = Path + FileName;
            FileStream strea = new FileStream(strFile, FileMode.Create);
            Document doc = new Document(new Rectangle(595, 842));
            doc.SetMargins(10, 10, 10, 10);
            PdfWriter write = PdfWriter.GetInstance(doc, strea);
            doc.Open();
            PdfContentByte pc = write.DirectContent;
            try
            {
                //'==================Carrega os Dados do Comprovante
                DataSet dtsCe = CarregarCe(Id_Contrato, Numero_Ce);
                if (dtsCe.Tables["Cabecalho"].Rows.Count == 0)
                {
                    bolRetorno = false;
                }
                if (bolRetorno)
                {


                    //'==================Gerais
                    Boolean hasTable = false;
                    Boolean novaTable = true;
                    PdfPTable tbDetalhe = new PdfPTable(6);
                    float[] CellWidths = new float[] { 220f, 195f, 40f, 80f, 40f, 200f };
                    float aTotalWidth = 0;
                    for (int x = 0; x < tbDetalhe.NumberOfColumns - 1; x++)
                    {
                        aTotalWidth += CellWidths[x];
                    }
                    tbDetalhe.TotalWidth = aTotalWidth;
                    tbDetalhe.SetWidths(CellWidths);

                    //'==================Inicia da Impressao do Comprovante
                    if (dtsCe.Tables["Detalhe"].Rows.Count == 0)
                    {
                        this.ImprimeCabecalho(write, doc, dtsCe.Tables["Cabecalho"].Rows[0]);
                        tbDetalhe = new PdfPTable(6);
                        tbDetalhe.TotalWidth = aTotalWidth;
                        tbDetalhe.SetWidths(CellWidths);
                        addCell(tbDetalhe, "Programa", 1, true, new int[] { 1, 1, 1, 1 }, PdfPCell.ALIGN_CENTER, 8, true);
                        addCell(tbDetalhe, "Comercial", 1, true, new int[] { 1, 1, 1, 1 }, PdfPCell.ALIGN_CENTER, 8, true);
                        addCell(tbDetalhe, "Dur.", 1, true, new int[] { 1, 1, 1, 1 }, PdfPCell.ALIGN_CENTER, 8, true);
                        addCell(tbDetalhe, "Data", 1, true, new int[] { 1, 1, 1, 1 }, PdfPCell.ALIGN_CENTER, 8, true);
                        addCell(tbDetalhe, "Real", 1, true, new int[] { 1, 1, 1, 1 }, PdfPCell.ALIGN_CENTER, 8, true);
                        addCell(tbDetalhe, "Horário Exibição", 1, true, new int[] { 1, 1, 1, 1 }, PdfPCell.ALIGN_CENTER, 8, true);
                        hasTable = true;
                    }
                    else
                    {
                        foreach (DataRow drw in dtsCe.Tables["Detalhe"].Rows)
                        {
                            //'===============Quando atingir o limite de altura da table , grava no pdf e inicializa nova table / gera linha de cabecalho
                            if (tbDetalhe.TotalHeight > 500)
                            {
                                if (hasTable)
                                {
                                    ImprimeCabecalho(write, doc, dtsCe.Tables["Cabecalho"].Rows[0]);
                                    pc = new PdfContentByte(write);
                                    pc = write.DirectContent;
                                    tbDetalhe.WriteSelectedRows(0, -1, 10, 600, pc);
                                    doc.NewPage();
                                }
                                novaTable = true;
                            }
                            //'================Instancia Nova Table
                            if (novaTable)
                            {
                                tbDetalhe = new PdfPTable(6);
                                tbDetalhe.TotalWidth = aTotalWidth;
                                tbDetalhe.SetWidths(CellWidths);
                                addCell(tbDetalhe, "Programa", 1, true, new int[] { 1, 1, 1, 1 }, PdfPCell.ALIGN_CENTER, 8, true, 20);
                                addCell(tbDetalhe, "Comercial", 1, true, new int[] { 1, 1, 1, 1 }, PdfPCell.ALIGN_CENTER, 8, true, 20);
                                addCell(tbDetalhe, "Dur.", 1, true, new int[] { 1, 1, 1, 1 }, PdfPCell.ALIGN_CENTER, 8, true, 20);
                                addCell(tbDetalhe, "Data", 1, true, new int[] { 1, 1, 1, 1 }, PdfPCell.ALIGN_CENTER, 8, true, 20);
                                addCell(tbDetalhe, "Real", 1, true, new int[] { 1, 1, 1, 1 }, PdfPCell.ALIGN_CENTER, 8, true, 20);
                                addCell(tbDetalhe, "Horário Exibição", 1, true, new int[] { 1, 1, 1, 1 }, PdfPCell.ALIGN_CENTER, 8, true, 20);
                            }

                            //'================grava a linha detalhe
                            addCell(tbDetalhe, drw["Programa"].ToString(), 1, true, new int[] { 1, 1, 1, 1 }, PdfPCell.ALIGN_LEFT, 8, false, 20);
                            addCell(tbDetalhe, drw["Comercial"].ToString(), 1, true, new int[] { 1, 1, 1, 1 }, PdfPCell.ALIGN_LEFT, 8, false, 20);
                            addCell(tbDetalhe, drw["Duracao"].ToString(), 1, true, new int[] { 1, 1, 1, 1 }, PdfPCell.ALIGN_CENTER, 8, false, 20);
                            addCell(tbDetalhe, drw["Data_Exibicao"].ToString(), 1, true, new int[] { 1, 1, 1, 1 }, PdfPCell.ALIGN_CENTER, 8, false, 20);
                            addCell(tbDetalhe, drw["Realizado"].ToString(), 1, true, new int[] { 1, 1, 1, 1 }, PdfPCell.ALIGN_CENTER, 8, false, 20);
                            addCell(tbDetalhe, drw["Horario_Exibicao"].ToString(), 1, true, new int[] { 1, 1, 1, 1 }, PdfPCell.ALIGN_LEFT, 8, false, 20);
                            hasTable = true;
                            novaTable = false;

                        }
                    }
                    //'======================Ao final do Loop verifica se ficou table sem gravar
                    if (hasTable)
                    {
                        
                        ImprimeCabecalho(write, doc, dtsCe.Tables["Cabecalho"].Rows[0] );
                        pc = new PdfContentByte(write);
                        pc = write.DirectContent;
                        tbDetalhe.WriteSelectedRows(0, -1, 10, 600, pc);
                    }


                    //'======================Imprime Resumo


                    int xPosition = 610 - ((int)Math.Round(tbDetalhe.TotalHeight + 30));
                    AddTexto(500, xPosition, "Total Realizado:" + dtsCe.Tables["Resumo"].Rows[0]["Total_Realizado"].ToString(), "Verdana", 8, true, write);
                }
            }
            catch (Exception)
            {
                doc.Close();
                doc.Dispose();
                strea.Dispose();
                write.Dispose();
                strFile = "";
                bolRetorno = false;
                throw;
            }
            finally
            {
                doc.Close();
                doc.Dispose();
                strea.Dispose();
                write.Dispose();
            }
            return bolRetorno;
        }
        private void MergePDF(string sPath, String targetPDF, List<String> arrayContrato)
        {

            String strArquivoFinal = sPath + targetPDF;
            if (File.Exists(strArquivoFinal))
            {
                File.Delete(strArquivoFinal);
            }


            FileStream strea = new FileStream(strArquivoFinal, FileMode.Create);
            using (strea)
            {
                Document pdfDoc = new Document(PageSize.A4);
                PdfCopy pdf = new PdfCopy(pdfDoc, strea);
                pdfDoc.Open();
                foreach (String sFile in arrayContrato)
                {
                    if (sFile != targetPDF)
                    {
                        PdfReader newFile = new PdfReader(sPath + sFile);
                        pdf.AddDocument(newFile);
                        newFile.Dispose();
                        System.IO.File.Delete(sFile);
                    }
                }
                pdf.Close();
                pdfDoc.Close();
            }
        }

        private void ImprimeCabecalho(PdfWriter ww, Document dd, DataRow drw)
        {
            String strLogo = "logo_" + drw["Cod_Empresa"].ToString() + ".png";
            addBorder(ww, dd, strLogo );



            PdfPTable tbEmpresa = new PdfPTable(2);
            float[] CellWidths = new float[] { 250, 250 };
            tbEmpresa.TotalWidth = 500;
            tbEmpresa.SetWidths(CellWidths);

            addCell(tbEmpresa, "Comprovante de Exibição ", 4, true, new int[] { 0, 0, 0, 0 }, PdfPCell.ALIGN_LEFT, 16,false,30);


            addCell(tbEmpresa, "EMPRESA DE FATURAMENTO", 1, true, new int[] { 0, 0, 0, 0 }, PdfPCell.ALIGN_LEFT, 9);
            addCell(tbEmpresa, "EMPRESA DE VENDA", 1, true, new int[] { 0, 0, 0, 0 }, PdfPCell.ALIGN_LEFT, 9);
            addCell(tbEmpresa, drw["Razao_Social_Empresa_Fat"].ToString(), 1, true, new int[] { 0, 0, 0, 0 }, PdfPCell.ALIGN_LEFT, 7);
            addCell(tbEmpresa, drw["Razao_Social_Empresa"].ToString(), 1, true, new int[] { 0, 0, 0, 0 }, PdfPCell.ALIGN_LEFT, 7);

            addCell(tbEmpresa, "CNPJ:" + drw["CGC_Empresa_Fat"].ToString(), 1, false, new int[] { 0, 0, 0, 0 }, PdfPCell.ALIGN_LEFT, 7);
            addCell(tbEmpresa, "CNPJ:" + drw["CGC"].ToString(), 1, false, new int[] { 0, 0, 0, 0 }, PdfPCell.ALIGN_LEFT, 7);
            addCell(tbEmpresa, drw["Endereco_Empresa_Fat"].ToString(), 1, false, new int[] { 0, 0, 0, 0 }, PdfPCell.ALIGN_LEFT, 7);
            addCell(tbEmpresa, drw["Endereco_Empresa"].ToString(), 1, false, new int[] { 0, 0, 0, 0 }, PdfPCell.ALIGN_LEFT, 7);

            addCell(tbEmpresa, drw["Bairro_Empresa_Fat"].ToString().Trim() + "   " + drw["Cep_Empresa_Fat"].ToString(), 1, false, new int[] { 0, 0, 0, 0 }, PdfPCell.ALIGN_LEFT, 7);
            addCell(tbEmpresa, drw["Bairro_Empresa"].ToString().Trim() + "   " + drw["Cep_Empresa"].ToString(), 1, false, new int[] { 0, 0, 0, 0 }, PdfPCell.ALIGN_LEFT, 7);

            PdfContentByte pc = ww.DirectContent;
            tbEmpresa.WriteSelectedRows(0, -1, 130, 820, pc);

            PdfPTable tbCliente = new PdfPTable(4);
            float[] CellWidthsCliente = new float[] { 50, 275, 100, 140 };
            tbCliente.TotalWidth = 50 + 275 + 100 + 140;
            tbCliente.SetWidths(CellWidthsCliente);

            addCell(tbCliente, "", 4, true, new int[] { 1, 0, 0, 0 }, PdfPCell.ALIGN_LEFT, 8);
            addCell(tbCliente, "Cliente", 1, true, new int[] { 0, 0, 0, 0 }, PdfPCell.ALIGN_LEFT, 8);
            addCell(tbCliente, drw["Nome_Cliente"].ToString(), 3, true, new int[] { 0, 0, 0, 0 }, PdfPCell.ALIGN_LEFT, 8);

            addCell(tbCliente, "Agência", 1, true, new int[] { 0, 0, 0, 0 }, PdfPCell.ALIGN_LEFT, 8);
            addCell(tbCliente, drw["Nome_Agencia"].ToString(), 1, true, new int[] { 0, 0, 0, 0 }, PdfPCell.ALIGN_LEFT, 8);
            addCell(tbCliente, "CNPJ", 1, true, new int[] { 0, 0, 0, 0 }, PdfPCell.ALIGN_LEFT, 8);
            addCell(tbCliente, drw["Cgc_Agencia"].ToString(), 1, true, new int[] { 0, 0, 0, 0 }, PdfPCell.ALIGN_LEFT, 8);


            addCell(tbCliente, "Endereço", 1, true, new int[] { 0, 0, 0, 0 }, PdfPCell.ALIGN_LEFT, 8);
            addCell(tbCliente, drw["Endereco_Agencia"].ToString(), 1, true, new int[] { 0, 0, 0, 0 }, PdfPCell.ALIGN_LEFT, 8);
            addCell(tbCliente, "CEP", 1, true, new int[] { 0, 0, 0, 0 }, PdfPCell.ALIGN_LEFT, 8);
            addCell(tbCliente, drw["Cep_Agencia"].ToString(), 1, true, new int[] { 0, 0, 0, 0 }, PdfPCell.ALIGN_LEFT, 8);

            addCell(tbCliente, "Município", 1, true, new int[] { 0, 0, 0, 0 }, PdfPCell.ALIGN_LEFT, 8);
            addCell(tbCliente, drw["Municipio_Agencia"].ToString(), 1, true, new int[] { 0, 0, 0, 0 }, PdfPCell.ALIGN_LEFT, 8);
            addCell(tbCliente, "UF", 1, true, new int[] { 0, 0, 0, 0 }, PdfPCell.ALIGN_LEFT, 8);
            addCell(tbCliente, drw["Cod_UF_Agencia"].ToString(), 1, true, new int[] { 0, 0, 0, 0 }, PdfPCell.ALIGN_LEFT, 8);

            addCell(tbCliente, "Veiculo", 1, true, new int[] { 0, 0, 0, 0 }, PdfPCell.ALIGN_LEFT, 8);
            addCell(tbCliente, drw["Cod_Veiculo"].ToString() + "- " + drw["Nome_Veiculo"].ToString(), 1, true, new int[] { 0, 0, 0, 0 }, PdfPCell.ALIGN_LEFT, 8);
            addCell(tbCliente, "Número CE", 1, true, new int[] { 0, 0, 0, 0 }, PdfPCell.ALIGN_LEFT, 8);
            addCell(tbCliente, drw["Numero_Ce"].ToString(), 1, true, new int[] { 0, 0, 0, 0 }, PdfPCell.ALIGN_LEFT, 8);

            addCell(tbCliente, "Cidade ", 1, true, new int[] { 0, 0, 0, 0 }, PdfPCell.ALIGN_LEFT, 8);
            addCell(tbCliente, drw["Cidade_Veiculo"].ToString().Trim(), 1, true, new int[] { 0, 0, 0, 0 }, PdfPCell.ALIGN_LEFT, 8);
            addCell(tbCliente, "Período da Campanha ", 1, true, new int[] { 0, 0, 0, 0 }, PdfPCell.ALIGN_LEFT, 8);
            addCell(tbCliente, drw["Periodo_Campanha_Inicio"] + " a " + drw["Periodo_Campanha_Termino"].ToString(), 1, true, new int[] { 0, 0, 0, 0 }, PdfPCell.ALIGN_LEFT, 8);

            addCell(tbCliente, "Contrato", 1, true, new int[] { 0, 0, 0, 0 }, PdfPCell.ALIGN_LEFT, 8);
            addCell(tbCliente, drw["Numero_Mr"].ToString() + "/" + drw["Sequencia_Mr"].ToString(), 1, true, new int[] { 0, 0, 0, 0 }, PdfPCell.ALIGN_LEFT, 8);
            addCell(tbCliente, "Data de Emissão", 1, true, new int[] { 0, 0, 0, 0 }, PdfPCell.ALIGN_LEFT, 8);
            addCell(tbCliente, drw["Data_Geracao"].ToString(), 1, true, new int[] { 0, 0, 0, 0 }, PdfPCell.ALIGN_LEFT, 8);


            addCell(tbCliente, "Produto", 1, true, new int[] { 0, 0, 0, 0 }, PdfPCell.ALIGN_LEFT, 8);
            addCell(tbCliente, drw["Nome_Produto"].ToString(), 1, true, new int[] { 0, 0, 0, 0 }, PdfPCell.ALIGN_LEFT, 8);
            addCell(tbCliente, "Número PV", 1, true, new int[] { 0, 0, 0, 0 }, PdfPCell.ALIGN_LEFT, 8);
            addCell(tbCliente, drw["Numero_Fatura"].ToString(), 1, true, new int[] { 0, 0, 0, 0 }, PdfPCell.ALIGN_LEFT, 8);
            addCell(tbCliente, "", 1, true, new int[] { 0, 0, 0, 0 }, PdfPCell.ALIGN_LEFT, 8);
            addCell(tbCliente, "", 1, true, new int[] { 0, 0, 0, 0 }, PdfPCell.ALIGN_LEFT, 8);

            addCell(tbCliente, "Abrangência", 1, true, new int[] { 0, 0, 0, 0 }, PdfPCell.ALIGN_LEFT, 8);
            addCell(tbCliente, drw["Abrangencia"].ToString(), 1, true, new int[] { 0, 0, 0, 0 }, PdfPCell.ALIGN_LEFT, 8);
            addCell(tbCliente, "Número do PI", 1, true, new int[] { 0, 0, 0, 0 }, PdfPCell.ALIGN_LEFT, 8);
            addCell(tbCliente, drw["Numero_Pi"].ToString(), 1, true, new int[] { 0, 0, 0, 0 }, PdfPCell.ALIGN_LEFT, 8);

            pc = ww.DirectContent;
            tbCliente.WriteSelectedRows(0, -1, 15, 720, pc);
        }
        private void addCell(PdfPTable table, String Text, int colspan, Boolean FontBold, int[] Border, int pAlign, int pFontSize, Boolean background = false, float pHeight = 0f)
        {

            Font fFont;
            if (FontBold)
            {
                fFont = new Font(Font.GetFamilyIndex("Verdana"), pFontSize, Font.BOLD);
            }
            else
            {
                fFont = new Font(Font.GetFamilyIndex("Verdana"), pFontSize, Font.NORMAL);
            }


            Phrase phrase = new Phrase(Text);
            phrase.Font.SetFamily("Verdana");
            phrase.Font.Size = pFontSize;
            PdfPCell cell = new PdfPCell(phrase);
            cell.Colspan = colspan;
            cell.BorderWidthTop = BorderScale(Border[0]);
            cell.BorderWidthBottom = BorderScale(Border[1]);
            cell.BorderWidthLeft = BorderScale(Border[2]);
            cell.BorderWidthRight = BorderScale(Border[3]);
            cell.NoWrap = false;
            if (pHeight > 0)
            {
                cell.FixedHeight = pHeight;
            }


            cell.HorizontalAlignment = pAlign;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            if (background)
            {
                cell.BackgroundColor = new BaseColor(245, 245, 245);
            }
            table.AddCell(cell);
        }
        private void AddTexto(Int32 X, Int32 Y, String Text, String FontName, float FontSize, Boolean FontBold, PdfWriter writer)
        {
            if (!String.IsNullOrEmpty(Text))
            {
                Phrase Field = new Phrase(Text);
                Field.Font.SetFamily(FontName);
                Field.Font.Size = FontSize;

                if (FontBold)
                {
                    Field.Font.SetStyle(Font.BOLD);
                }
                PdfContentByte Content = writer.DirectContent;
                ColumnText.ShowTextAligned(Content, Element.ALIGN_LEFT, Field, X, Y, 0);
            }
        }
        private float BorderScale(float param)
        {
            return param / 8;
        }
        private void addBorder(PdfWriter ww, Document dd, String pLogo )
        {
            if (String.IsNullOrEmpty(pLogo))
            {
                pLogo = "logo_Padrao.png";
            }
            String sPathLogo = HttpContext.Current.Server.MapPath("~/logos/" + pLogo);
            if (!File.Exists(sPathLogo))
            {

                sPathLogo = HttpContext.Current.Server.MapPath("~/logos/" + "logo_Padrao.png");
            }


            if (File.Exists(sPathLogo))
            {
                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(sPathLogo);
                logo.SetAbsolutePosition(15, 730);
                //logo.ScalePercent(50);
                dd.Add(logo);
            }


            PdfContentByte Content = ww.DirectContent;
            Rectangle pageBorderRect = new Rectangle(dd.PageSize);
            pageBorderRect.Left += dd.LeftMargin;
            pageBorderRect.Right -= dd.RightMargin;
            pageBorderRect.Top -= dd.TopMargin;
            pageBorderRect.Bottom += dd.BottomMargin + 20;
            Content.SetColorStroke(BaseColor.BLACK);
            Content.Rectangle(pageBorderRect.Left, pageBorderRect.Bottom, pageBorderRect.Width, pageBorderRect.Height);
            Content.Stroke();
            //'======================Imprime Rodape;
            //String[] strLinha = new String[]        {
            //                "Tabela de Preços Condições Gerais - Cancelamentos, Falhas e Compensações",
            //                "1- A não contestação pela Agencia/Cliente no período de 30 (trinta) dias, contados a partir do último dia de exibição, implica o reconhecimento da entrega total da campanha contratada. ",
            //                "2 - Alegações posteriores, só serão aceitas mediante apresentação dos relatórios de 'Confirmação de Exibição' e material em VHS, DVD ou qualquer outro dispositivo eletrônico que comprove a reclamação. "
            //                };

            //PdfPTable tbRopape = new PdfPTable(1);

            //float[] CellWidths = new float[] { 500 };
            //tbRopape.SetWidths(CellWidths);
            //tbRopape.TotalWidth = 550;
            //addCell(tbRopape, strLinha[0], 1, true, new int[] { 1, 0, 0, 0 }, PdfPCell.ALIGN_LEFT, 8, false);
            //addCell(tbRopape, strLinha[1], 1, false, new int[] { 0, 0, 0, 0 }, PdfPCell.ALIGN_LEFT, 8, false);
            //addCell(tbRopape, strLinha[2], 1, false, new int[] { 0, 0, 0, 0 }, PdfPCell.ALIGN_LEFT, 8, false);
            //PdfContentByte pc = ww.DirectContent;
            //tbRopape.WriteSelectedRows(0, -1, 20, 100, pc);
            this.pdfPageNumber++;
            AddTexto(550, int.Parse(dd.BottomMargin.ToString()), "Pag." + this.pdfPageNumber.ToString(), "Verdana", 8, false, ww);
        }
        private DataSet CarregarCe(Int32 Id_Contrato, Int32 Numero_Ce)
        {
            DataSet dts = new DataSet("CE");
            DataTable dtCabecalho = new DataTable("Cabecalho");
            DataTable dtDetalhe = new DataTable("Detalhe");
            DataTable dtResumo = new DataTable("Resumo");
            DataRow dtr;


            SqlDataAdapter dtaDados = new SqlDataAdapter();
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();

            // Comando para a seleção;
            dtaDados.SelectCommand = new SqlCommand();
            dtaDados.SelectCommand = cnn.Procedure(cnn.Connection, "pr_SIM_CE_Cabecalho_S");
            dtaDados.SelectCommand.CommandType = CommandType.StoredProcedure;
            // Parâmetros
            dtaDados.SelectCommand.Parameters.AddWithValue("@PNumero_CE", Numero_Ce);
            dtaDados.SelectCommand.Parameters.AddWithValue("@PID_Contrato", Id_Contrato);

            // Preenche o DataTable
            try { dtaDados.Fill(dtCabecalho); }
            catch { throw; }

            // Comando para a seleção;
            dtaDados.SelectCommand = new SqlCommand();
            dtaDados.SelectCommand = cnn.Procedure(cnn.Connection, "pr_SIM_CE_Detalhe_S");
            dtaDados.SelectCommand.CommandType = CommandType.StoredProcedure;
            // Parâmetros
            dtaDados.SelectCommand.Parameters.AddWithValue("@PNumero_CE", Numero_Ce);
            dtaDados.SelectCommand.Parameters.AddWithValue("@PID_Contrato", Id_Contrato);

            // Preenche o DataTable
            try { dtaDados.Fill(dtDetalhe); }
            catch { throw; }

            // encerra conexao
            cnn.Close();

            dtResumo.Columns.Add("Total_Previsto", System.Type.GetType("System.Int16"));
            dtResumo.Columns.Add("Total_Realizado", System.Type.GetType("System.Int16"));

            dtr = dtResumo.NewRow();

            dtr["Total_Previsto"] = 0;
            dtr["Total_Realizado"] = 0;

            for (int i = 0; i < dtDetalhe.Rows.Count; i++)
            {
                dtr["Total_Previsto"] = (int.Parse(dtr["Total_Previsto"].ToString()) + int.Parse(dtDetalhe.Rows[i]["Previsto"].ToString()));
                dtr["Total_Realizado"] = (int.Parse(dtr["Total_Realizado"].ToString()) + int.Parse(dtDetalhe.Rows[i]["Realizado"].ToString()));
            }

            dtResumo.Rows.Add(dtr);

            dts.Tables.Add(dtCabecalho);
            dts.Tables.Add(dtDetalhe);
            dts.Tables.Add(dtResumo);

            return dts;
        }
    }
}
