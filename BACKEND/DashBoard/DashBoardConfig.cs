using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace PROPOSTA
{

    public partial class DashBoard
    {



        private void ConfigGraph(DataTable dtb, GraphModel Graph, GraphConfigModel Cfg)
        {
            Graph.data = new GraphDataModel();
            Graph.options = new GraphOptionModel();
            Graph.data.labels = AddDataLabels(dtb, Graph, Cfg);
            Graph.data.datasets = AddDataSet(dtb, Graph, Cfg);
            Graph.options = addOptions(dtb, Graph, Cfg);
        }
        private GraphOptionModel addOptions(DataTable dtb, GraphModel Graph, GraphConfigModel Cfg)
        {
            List<GraphXaxesrModel> xAxe = new List<GraphXaxesrModel>();
            List<GraphYaxesrModel> YAxe = new List<GraphYaxesrModel>();

            xAxe.Add(new GraphXaxesrModel()
            {
                display = true,
                scaleLabel = new GraphScaleLabelModel() { display = (string.IsNullOrEmpty(Cfg.TitleX)) ? false : true, labelString = Cfg.TitleX }
            });

            YAxe.Add(new GraphYaxesrModel()
            {
                display = true,
                scaleLabel = new GraphScaleLabelModel() { display = (string.IsNullOrEmpty(Cfg.TitleY)) ? false : true, labelString = Cfg.TitleY },
            });


            GraphOptionModel Option = new GraphOptionModel()
            {
                scales = new GraphOptionScalesModel() { xAxes = xAxe, yAxes = YAxe },
                title = new GraphOptionTitleModel() { display = (String.IsNullOrEmpty(Cfg.Title)) ? false : true, text = Cfg.Title },
                tooltips = new GraphOptionToolTipModel(),
                hover = new GraphOptionHoverModel(),
            };

            return Option;

        }
        //Definindo Grap Pie 
        private void ConfigGraphPie(DataTable dtb, GraphPieModel Graph, GraphConfigPieModel Cfg)
        {
            Int32 intIndex = 0;
            List<Double> temp_dataset = new List<double>();
            List<String> temp_label = new List<String>();
            List<String> temp_BackGround = new List<String>();
            try
            {
                foreach (DataRow drw in dtb.Rows)
                {
                    temp_label.Add(drw[Cfg.Field_Label].ToString());
                    temp_dataset.Add(drw[Cfg.Field_Value].ToString().ConvertToDouble());
                    temp_BackGround.Add(GetColor(intIndex));
                    intIndex++;
                }


                GraphDataPieModel xdata = new GraphDataPieModel();

                xdata.datasets = new List<GraphDataSetPieModel>();
                xdata.datasets.Add(new GraphDataSetPieModel() { data = temp_dataset, backgroundColor = temp_BackGround, label = "Dataset 1" });
                // xdata.labels = temp_label;
                xdata.labels = new List<string>(temp_label);




                Graph.data = xdata;

                Graph.options = new GraphOptionPieModel() { title = new GraphOptionTitleModel() { text = Cfg.Title } };
            }
            catch (Exception Ex)
            {

                throw new Exception(Ex.Message);
            }


        }
        private GraphOptionPieModel addOptionsPie(DataTable dtb, GraphPieModel Graph, GraphConfigPieModel Cfg)
        {

            GraphOptionPieModel Option = new GraphOptionPieModel()
            {
                title = new GraphOptionTitleModel() { display = (String.IsNullOrEmpty(Cfg.Title)) ? false : true, text = Cfg.Title },
            };

            return Option;

        }
        //Fim de Pie


        private List<String> AddDataLabels(DataTable dtb, GraphModel Graph, GraphConfigModel Cfg)
        {
            List<String> Labels = new List<string>();
            //==================Distinct Labels 
            DataView viewLabel = new DataView(dtb);
            DataTable dtbLabel = viewLabel.ToTable(true, Cfg.LabelX_Id, Cfg.LabelX_Text);
            foreach (DataRow drw in dtbLabel.Rows)
            {
                Labels.Add(drw[Cfg.LabelX_Text].ToString().TrimEnd());
            }
            return Labels;
        }

        private List<GraphDataSetModel> AddDataSet(DataTable dtb, GraphModel Graph, GraphConfigModel Cfg)
        {
            List<GraphDataSetModel> dataset = new List<GraphDataSetModel>();
            List<String> Targets = new List<string>();
            List<String> TargetsText = new List<string>();
            List<String> Labels = new List<string>();
            List<double> Listdata = new List<double>();
            Int32 intIndex = 0;
            //==================Distinct Labels 
            DataView viewLabel = new DataView(dtb);
            DataTable dtbLabel = viewLabel.ToTable(true, Cfg.LabelX_Id);
            foreach (DataRow drw in dtbLabel.Rows)
            {
                Labels.Add(drw[Cfg.LabelX_Id].ToString().TrimEnd());
            }
            //==================Distinct Targets

            DataView viewTarget = new DataView(dtb);
            DataTable dtbTarget = viewTarget.ToTable(true, Cfg.Target_Id, Cfg.Target_Text);
            foreach (DataRow drw in dtbTarget.Rows)
            {
                Targets.Add(drw[Cfg.Target_Id].ToString());
                TargetsText.Add(drw[Cfg.Target_Text].ToString());
            }


            //=====================Insere um dataset para cada Target
            String strSql = "";
            for (int T = 0; T < Targets.Count; T++)
            {
                GraphDataSetModel datasetTemp = new GraphDataSetModel();
                datasetTemp.label = TargetsText[T];
                datasetTemp.backgroundColor = GetColor(intIndex);
                datasetTemp.borderColor = GetColor(intIndex);
                intIndex++;
                Listdata = new List<double>();
                for (int L = 0; L < Labels.Count; L++)
                {
                    strSql = Cfg.LabelX_Id + "='" + Labels[L] + "'";
                    strSql += " And " + Cfg.Target_Id + "='" + Targets[T] + "'";
                    DataRow[] rows = dtb.Select(strSql);
                    if (rows.Length == 0)
                    {
                        Listdata.Add(0);
                    }
                    else
                    {
                        Listdata.Add(rows[0][Cfg.LabelY].ToString().ConvertToDouble());
                    }

                }
                datasetTemp.data = Listdata;
                dataset.Add(datasetTemp);
            }
            return dataset;
        }

        //Definindo label e datasets para Pie

        //private List<String> AddDataLabelsPie(DataTable dtb, GraphPieModel Graph, GraphConfigPieModel Cfg)
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

        //private List<GraphDataSetPieModel> AddDataSetPie(DataTable dtb, GraphPieModel Graph, GraphConfigPieModel Cfg)
        //{
        //    List<GraphDataSetPieModel> dataset = new List<GraphDataSetPieModel>();
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
        //    //DataTable dtbTarget = viewTarget.ToTable(true, Cfg.Target_Id, Cfg.Target_Text);
        //    //foreach (DataRow drw in dtbTarget.Rows)
        //    //{
        //    //    Targets.Add(drw[Cfg.Target_Id].ToString());
        //    //    TargetsText.Add(drw[Cfg.Target_Text].ToString());
        //    //}


        //    //=====================Insere um dataset para cada Target
        //    String strSql = "";
        //    //for (int T = 0; T < Targets.Count; T++)
        //    //{
        //        GraphDataSetPieModel datasetTemp = new GraphDataSetPieModel();

        //    datasetTemp.backgroundColor = GetColor(intIndex);
        //    //datasetTemp.borderColor = GetColor(intIndex);
        //    datasetTemp.label = "Dataset 1";
        //        intIndex++;
        //        Listdata = new List<double>();
        //        for (int L = 0; L < Labels.Count; L++)
        //        {
        //            strSql = Cfg.LabelX_Id + "='" + Labels[L] + "'";
        //            //strSql += " And " + Cfg.Target_Id + "='" + Targets[T] + "'";
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


        //    //}
        //    return dataset;
        //}
        //Fim do Pie

        private String GetColor(Int32 pIndex)
        {
            List<System.Drawing.Color> colors = new List<System.Drawing.Color>();
            colors.Add(System.Drawing.Color.Red);
            colors.Add(System.Drawing.Color.Lime);
            colors.Add(System.Drawing.Color.Blue);
            colors.Add(System.Drawing.Color.Yellow);
            colors.Add(System.Drawing.Color.Magenta);
            colors.Add(System.Drawing.Color.Silver);
            colors.Add(System.Drawing.Color.Gray);
            colors.Add(System.Drawing.Color.Maroon);
            colors.Add(System.Drawing.Color.Olive);
            colors.Add(System.Drawing.Color.Green);
            colors.Add(System.Drawing.Color.Purple);
            colors.Add(System.Drawing.Color.Navy);

            System.Drawing.Color color = colors[pIndex % colors.Count];
            return String.Format("RGB({0},{1},{2},{3})", color.R, color.G, color.B,"0.5").ToLower();

        }

    }
}
