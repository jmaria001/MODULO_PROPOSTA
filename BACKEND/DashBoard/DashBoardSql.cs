using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace PROPOSTA
{

    public partial class DashBoard
    {

        public GraphModel GraficoVendas(FiltroGraficoVendasModel pFiltro)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();

            GraphModel Graph = new GraphModel();
            GraphConfigModel Config = new GraphConfigModel();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_DashBoard_Grafico_Vendas");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia_Inicio", clsLib.CompetenciaInt(pFiltro.Competencia_Inicio));
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia_Fim", clsLib.CompetenciaInt(pFiltro.Competencia_Fim));
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Grupo", pFiltro.Postipo);
                //Adp.SelectCommand.Parameters.AddWithValue("@Par_Indicador", pFiltro.Indicador);
                Adp.Fill(dtb);

                Graph.type = "bar";
                //Graph.options.tooltips = new GraphOptionToolTipModel() { enabled = false };
                Config.Title ="Gráfico de Vendas";
                //Config.TitleX = "Título abaixo do grafico";
                Config.TitleX = "Período de:" + pFiltro.Competencia_Inicio + " a " + pFiltro.Competencia_Fim;

                if (pFiltro.Indicador=="1")
                {
                    Config.TitleY = "Quantidade de Propostas";
                    Config.LabelY = "Qtd";
                }
                else
                {
                    Config.TitleY = "Valores Em Reais ";
                    Config.LabelY = "Valor";
                }
                
                Config.LabelX_Id = "Label_Codigo";
                Config.LabelX_Text = "Label_Descricao";
                
                Config.Target_Id = "Id_Status";
                Config.Target_Text = "Nome_Status";
                ConfigGraph(dtb, Graph, Config);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cnn.Close();
            }
            return Graph;
        }
        public GraphModel FunilVendas(FiltroFunilVendasModel pFiltro)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();

            GraphModel Graph = new GraphModel();
            GraphConfigModel Config = new GraphConfigModel();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_DashBoard_Funil_Vendas_H");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia_Inicio", clsLib.CompetenciaInt(pFiltro.Competencia_Inicio));
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia_Fim", clsLib.CompetenciaInt(pFiltro.Competencia_Fim));
                Adp.Fill(dtb);

                Graph.type = "horizontalBar";
                Config.Title = "Funil de Vendas";
                Config.TitleX = "Período de:" + pFiltro.Competencia_Inicio + " a " + pFiltro.Competencia_Fim;
                Config.TitleY = "Quantidade de Propostas";
                Config.LabelY = "Qtd";
                Config.LabelX_Id = "Label_Id";
                Config.LabelX_Text = "Label_Text";
                Config.Target_Id = "Target_Id";
                Config.Target_Text = "Target_Descricao";
                ConfigGraph(dtb, Graph, Config);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cnn.Close();
            }
            return Graph;
        }
        public GraphModel ModeloBarra(FiltroModel pFiltro)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();

            GraphModel Graph = new GraphModel();
            GraphConfigModel Config = new GraphConfigModel();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_DashBoard_Barra");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia_Inicio", clsLib.CompetenciaInt(pFiltro.Competencia_Inicio));
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia_Fim", clsLib.CompetenciaInt(pFiltro.Competencia_Fim));
                Adp.Fill(dtb);

                Graph.type = "bar";
                Config.Title = "Grafico de Vendas";
                Config.TitleX = "Título abaixo do grafico";
                Config.TitleY = "Valores em Reais";
                Config.LabelX_Id = "Cod_Contato";
                Config.LabelX_Text = "Nome_Contato";
                Config.LabelY = "Valor";
                Config.Target_Id = "Id_Status";
                Config.Target_Text = "Nome_Status";
                ConfigGraph(dtb, Graph, Config);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cnn.Close();
            }
            return Graph;
        }
        public GraphModel ModeloLinha(FiltroModel pFiltro)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();

            GraphModel Graph = new GraphModel();
            GraphConfigModel Config = new GraphConfigModel();
            try
            {
                //SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_DashBoard_Line");
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_DashBoard_Barra");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia_Inicio", clsLib.CompetenciaInt(pFiltro.Competencia_Inicio));
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia_Fim", clsLib.CompetenciaInt(pFiltro.Competencia_Fim));
                Adp.Fill(dtb);

                //Graph.type = "line";
                //Graph.type = "bar";
                //Config.Title = "Evolução de Vendas";
                //Config.TitleX = "Título abaixo do grafico";
                //Config.TitleY = "Valores em Reais";
                //Config.LabelX_Id = "Competencia";
                //Config.LabelX_Text = "Competencia_Text";
                //Config.LabelY = "Valor";
                //Config.Target_Id = "Cod_Contato";
                //Config.Target_Text = "Nome_Contato";
                //ConfigGraph(dtb, Graph, Config);

                Graph.type = "line";
                Config.Title = "Grafico de Vendas";
                Config.TitleX = "Título abaixo do grafico";
                Config.TitleY = "Valores em Reais";
                Config.LabelX_Id = "Cod_Contato";
                Config.LabelX_Text = "Nome_Contato";
                Config.LabelY = "Valor";
                Config.Target_Id = "Id_Status";
                Config.Target_Text = "Nome_Status";
                ConfigGraph(dtb, Graph, Config);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cnn.Close();
            }
            return Graph;
        }


        public GraphPieModel ModeloPie(FiltroModel pFiltro)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();

            GraphPieModel Graph = new GraphPieModel();
            GraphConfigPieModel Config = new GraphConfigPieModel();
            try
            {
                //SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_DashBoard_Line");
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_DashBoard_Pie");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia_Inicio", clsLib.CompetenciaInt(pFiltro.Competencia_Inicio));
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia_Fim", clsLib.CompetenciaInt(pFiltro.Competencia_Fim));
                Adp.Fill(dtb);


                Graph.type = "pie";
                Config.Title = "Grafico de Vendas";
                //Config.TitleX = "Título abaixo do grafico";
                //Config.TitleY = "Valores em Reais";
                //Config.LabelX_Id = "Cod_Contato";
                //Config.LabelX_Text = "Nome_Contato";
                //Config.LabelY = "Valor";
                //Config.Target_Id = "Id_Status";
                //Config.Target_Text = "Nome_Status";
                Config.Field_Label = "Nome_Contato";
                Config.Field_Value= "Valor";
                ConfigGraphPie(dtb, Graph, Config);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cnn.Close();
            }
            return Graph;
        }

        // Evolucao de Vendas
        public GraphModel ModeloBarraEvolucaoVendas(FiltroModel pFiltro)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();

            GraphModel Graph = new GraphModel();
            GraphConfigModel Config = new GraphConfigModel();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_DashBoard_Barra_EvolucaoVendas");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia_Inicio", clsLib.CompetenciaInt(pFiltro.Competencia_Inicio));
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia_Fim", clsLib.CompetenciaInt(pFiltro.Competencia_Fim));
                Adp.Fill(dtb);

                Graph.type = "bar";
                Config.Title = "Evolução de Vendas";
                Config.TitleX = "Título abaixo do grafico";
                Config.TitleY = "Valores em Reais";
                Config.LabelX_Id = "Competencia";
                Config.LabelX_Text = "Competencia_Text";
                Config.LabelY = "Valor";
                Config.Target_Id = "Cod_Contato";
                Config.Target_Text = "Nome_Contato";
                ConfigGraph(dtb, Graph, Config);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cnn.Close();
            }
            return Graph;
        }
        public GraphModel ModeloLinhaEvolucaoVendas(FiltroModel pFiltro)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();

            GraphModel Graph = new GraphModel();
            GraphConfigModel Config = new GraphConfigModel();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_DashBoard_Line_EvolucaoVendas");
                //SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_DashBoard_Barra_EvolucaoVendas");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia_Inicio", clsLib.CompetenciaInt(pFiltro.Competencia_Inicio));
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia_Fim", clsLib.CompetenciaInt(pFiltro.Competencia_Fim));
                Adp.Fill(dtb);

                Graph.type = "line";
                Config.Title = "Evolução de Vendas";
                Config.TitleX = "Título abaixo do grafico";
                Config.TitleY = "Valores em Reais";
                Config.LabelX_Id = "Competencia";
                Config.LabelX_Text = "Competencia_Text";
                Config.LabelY = "Valor";
                Config.Target_Id = "Cod_Contato";
                Config.Target_Text = "Nome_Contato";
                ConfigGraph(dtb, Graph, Config);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cnn.Close();
            }
            return Graph;
        }



        // Evolucao de Vendas
        public GraphModel EvolucaoVendas(FiltroEvolucaoVendasModel pFiltro)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();

            GraphModel Graph = new GraphModel();
            GraphConfigModel Config = new GraphConfigModel();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_DashBoard_Evolucao_Vendas");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia_Inicio", clsLib.CompetenciaInt(pFiltro.Competencia_Inicio));
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia_Fim", clsLib.CompetenciaInt(pFiltro.Competencia_Fim));
                //Adp.SelectCommand.Parameters.AddWithValue("@Par_Grupo", pFiltro.Postipo);
                //Adp.SelectCommand.Parameters.AddWithValue("@Par_Indicador", pFiltro.Indicador);
                Adp.Fill(dtb);

                Graph.type = "line";
                Config.Title = "Evolucao de Vendas";
                Config.TitleX = "Período de:" + pFiltro.Competencia_Inicio + " a " + pFiltro.Competencia_Fim;

                if (pFiltro.Indicador == "1")
                {
                    Config.TitleY = "Quantidade de Propostas";
                    Config.LabelY = "Qtd";
                }
                else
                {
                    Config.TitleY = "Valores Em Reais ";
                    Config.LabelY = "Valor";
                }

                Config.LabelX_Id = "Cod_Mes";
                Config.LabelX_Text = "Descricao_Mes";
                Config.Target_Id = "Id_Status";
                Config.Target_Text = "Nome_Status";
                ConfigGraph(dtb, Graph, Config);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cnn.Close();
            }
            return Graph;
        }

        //private  void ConfigGraph(DataTable dtb, GraphModel Graph, GraphConfigModel Cfg)
        //{
        //    Graph.data = new GraphDataModel();
        //    Graph.options = new GraphOptionModel();
        //    Graph.data.labels = AddDataLabels(dtb, Graph, Cfg);
        //    Graph.data.datasets = AddDataSet(dtb, Graph, Cfg);
        //    Graph.options = addOptions(dtb, Graph, Cfg);
        //}
        //private GraphOptionModel addOptions(DataTable dtb, GraphModel Graph, GraphConfigModel Cfg)
        //{
        //    List<GraphXaxesrModel> xAxe = new List<GraphXaxesrModel>();
        //    List<GraphYaxesrModel> YAxe = new List<GraphYaxesrModel>();

        //    xAxe.Add(new GraphXaxesrModel() {
        //        display=true,
        //        scaleLabel = new GraphScaleLabelModel() { display=  (string.IsNullOrEmpty(Cfg.TitleX))?false:true  ,labelString=Cfg.TitleX}
        //    });

        //    YAxe.Add(new GraphYaxesrModel()
        //    {
        //        display = true,
        //        scaleLabel = new GraphScaleLabelModel() { display = (string.IsNullOrEmpty(Cfg.TitleY)) ? false : true, labelString = Cfg.TitleY }
        //    });

        //    GraphOptionModel Option = new GraphOptionModel()
        //    {
        //        scales = new GraphOptionScalesModel() {xAxes=xAxe,yAxes=YAxe },
        //        title = new GraphOptionTitleModel() { display = (String.IsNullOrEmpty(Cfg.Title)) ? false : true, text = Cfg.Title },
        //        tooltips = new GraphOptionToolTipModel(),
        //        hover = new GraphOptionHoverModel(),
        //    };

        //    return Option;

        //}

        //private  List<String> AddDataLabels(DataTable dtb, GraphModel Graph, GraphConfigModel Cfg)
        //{
        //    List<String> Labels = new List<string>();
        //    //==================Distinct Labels 
        //    DataView viewLabel = new DataView(dtb);
        //    DataTable dtbLabel = viewLabel.ToTable(true, Cfg.LabelX_Text);
        //    foreach (DataRow drw in dtbLabel.Rows)
        //    {
        //        Labels.Add(drw[Cfg.LabelX_Text].ToString().TrimEnd());
        //    }
        //    return Labels;
        //}

        //private   List<GraphDataSetModel> AddDataSet(DataTable dtb, GraphModel Graph, GraphConfigModel Cfg)
        //{
        //    List<GraphDataSetModel> dataset = new List<GraphDataSetModel>();
        //    List<String> Targets = new List<string>();
        //    List<String> TargetsText = new List<string>();
        //    List<String> Labels = new List<string>();
        //    List<double> Listdata = new List<double>();
        //    Int32 intIndex = 0;
        //    //==================Distinct Labels 
        //    DataView viewLabel = new DataView(dtb);
        //    DataTable dtbLabel = viewLabel.ToTable(true, Cfg.LabelX_Id);
        //    foreach (DataRow drw in dtbLabel.Rows)
        //    {
        //        Labels.Add(drw[Cfg.LabelX_Id].ToString().TrimEnd());
        //    }


        //    //==================Distinct Targets
        //    DataView viewTarget = new DataView(dtb);
        //    DataTable dtbTarget = viewTarget.ToTable(true, Cfg.Target_Id,Cfg.Target_Text);
        //    foreach (DataRow drw in dtbTarget.Rows)
        //    {
        //        Targets.Add(drw[Cfg.Target_Id].ToString());
        //        TargetsText.Add(drw[Cfg.Target_Text].ToString());
        //    }


        //    //=====================Insere um dataset para cada Target
        //    String strSql = "";
        //    for (int T = 0; T < Targets.Count; T++)
        //    {
        //        GraphDataSetModel datasetTemp = new GraphDataSetModel();
        //        datasetTemp.label = TargetsText[T];
        //        datasetTemp.backgroundColor = GetColor(intIndex);
        //        datasetTemp.borderColor= GetColor(intIndex);
        //        intIndex++;
        //        Listdata = new List<double>();
        //        for (int L = 0; L < Labels.Count; L++)
        //        {
        //            strSql = Cfg.LabelX_Id + "='" + Labels[L] + "'";
        //            strSql += " And " + Cfg.Target_Id + "='" + Targets[T] + "'";
        //            DataRow[] rows = dtb.Select(strSql);
        //            if (rows.Length == 0)
        //            {
        //                Listdata.Add(0);
        //            }
        //            else
        //            {
        //                Listdata.Add(rows[0][Cfg.LabelY].ToString().ConvertToDouble());
        //            }

        //        }
        //        datasetTemp.data = Listdata;
        //        dataset.Add(datasetTemp);
        //    }
        //    return dataset;
        //}
        //private  String GetColor(Int32 pIndex)
        //{
        //    List<System.Drawing.Color> colors = new List<System.Drawing.Color>();
        //    colors.Add(System.Drawing.Color.Red);
        //    colors.Add(System.Drawing.Color.Lime);
        //    colors.Add(System.Drawing.Color.Blue);
        //    colors.Add(System.Drawing.Color.Yellow);
        //    colors.Add(System.Drawing.Color.Magenta);
        //    colors.Add(System.Drawing.Color.Silver);
        //    colors.Add(System.Drawing.Color.Gray);
        //    colors.Add(System.Drawing.Color.Maroon);
        //    colors.Add(System.Drawing.Color.Olive);
        //    colors.Add(System.Drawing.Color.Green);
        //    colors.Add(System.Drawing.Color.Purple);
        //    colors.Add(System.Drawing.Color.Navy);

        //    System.Drawing.Color color = colors[pIndex % colors.Count];
        //    return String.Format("RGB({0},{1},{2})", color.R, color.G, color.B).ToLower();

        //}

    }
}
