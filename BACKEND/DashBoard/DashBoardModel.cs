using System;
using System.Collections.Generic;

namespace PROPOSTA
{
    
    public partial class DashBoard
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public DashBoard(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

        
        
        public class GraphModel
        {
            public String type { get; set; }
            public GraphDataModel data { get; set; }
            public GraphOptionModel options { get; set; }
        }
        //Defininido modelo para graph Pie
        public class GraphPieModel
        {
            public String type { get; set; }
            public GraphDataPieModel data { get; set; }
            public GraphOptionPieModel options { get; set; }
        }

        public class GraphDataPieModel
        {
            public List<GraphDataSetPieModel> datasets { get; set; }
            public List<String> labels { get; set; }
        }




        public class GraphDataModel
        {
            public List<String> labels { get; set; }
            public List<GraphDataSetModel> datasets { get; set; }
        }
        public class GraphDataSetModel
        {
            public String label { get; set; }
            public String backgroundColor { get; set; }
            public String borderColor { get; set; }
            public List<Double> data { get; set; }
            public Boolean fill { get; set; } = false;
        }
        public class GraphDataSetPieModel
        {
            public List<Double> data { get; set; }
            public List<String> backgroundColor { get; set; }
            public String label { get; set; }
        }

        public class GraphOptionModel
        {
            public Boolean responsive { get; set; } = true;
            public GraphOptionTitleModel title { get; set; }
            public GraphOptionToolTipModel tooltips { get; set; }
            public GraphOptionHoverModel hover { get; set; }
            public GraphOptionScalesModel scales { get; set; }
        }

        //Defininido modelo para graph Pie
        public class GraphOptionPieModel
        {
            public Boolean responsive { get; set; } = true;
            public GraphOptionTitleModel title { get; set; }
        }



        public class GraphOptionTitleModel
        {
            public Boolean display { get; set; } = true;
            public String text { get; set; }
        }
        public class GraphOptionToolTipModel
        {
            public Boolean enabled { get; set; } = true;
            public String mode { get; set; } = "index";
            public Boolean intersect { get; set; } = false;
        }
        public class GraphOptionHoverModel
        {
            public Boolean enabled { get; set; } = true;
            public String mode { get; set; } = "nearest";
            public Boolean intersect { get; set; } = false;
        }
        public class GraphOptionScalesModel
        {
            public List <GraphXaxesrModel> xAxes { get; set; }
            public List<GraphYaxesrModel> yAxes { get; set; }
        }
        public class GraphXaxesrModel
        {
            public Boolean display { get; set; }
            public GraphScaleLabelModel scaleLabel { get; set; }
            public tickModel ticks { get; set; }
        }
        public class GraphYaxesrModel
        {
            public Boolean display { get; set; } = true;
            public GraphScaleLabelModel scaleLabel { get; set; }
            public tickModel ticks { get; set; }
        }
        public class tickModel
        {
            public Boolean beginAtZero { get; set; } = true;
        }
        public class GraphScaleLabelModel {
            public Boolean display { get; set; } = true;
            public String labelString { get; set; }

        }
        public class GraphConfigModel
        {
            public String Title { get; set; }
            public String TitleX { get; set; }
            public String TitleY { get; set; }
            public String LabelX_Id { get; set; }
            public String LabelX_Text { get; set; }
            public String LabelY    { get; set; }
            public String Target_Id{ get; set; }
            public String Target_Text { get; set; }
        }

        public class GraphConfigPieModel
        {
            public String Title { get; set; }
            //public String LabelX_Id { get; set; }
            //public String LabelX_Text { get; set; }
            //public String LabelY { get; set; }
            //public String Target_Id { get; set; }
            //public String Target_Text { get; set; }
            public string Field_Label { get; set; }
            public string Field_Value { get; set; }

        }

        //===============================Models para os Filtros
        public class FiltroModel
        {
            public String Competencia_Inicio { get; set; }
            public String Competencia_Fim { get; set; }
            public String DescVisao { get; set; }
        }
        //===============================Models para parametro do Grafico de Vendas
        public class FiltroGraficoVendasModel
        {
            public String Competencia_Inicio { get; set; }
            public String Competencia_Fim { get; set; }
            public String Postipo { get; set; }
            public String Indicador { get; set; }

        }
        public class FiltroFunilVendasModel
        {
            public String Competencia_Inicio { get; set; }
            public String Competencia_Fim { get; set; }

        }
        //===============================Models para parametro do Evolucao de Vendas
        public class FiltroEvolucaoVendasModel
        {
            public String Competencia_Inicio { get; set; }
            public String Competencia_Fim { get; set; }
            public String Postipo { get; set; }
            public String Indicador { get; set; }

        }

    }
}


