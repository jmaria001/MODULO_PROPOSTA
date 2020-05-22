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
        public class GraphOptionModel
        {
            public Boolean responsive { get; set; } = true;
            public GraphOptionTitleModel title { get; set; }
            public GraphOptionToolTipModel tooltips { get; set; }
            public GraphOptionHoverModel hover { get; set; }
            public GraphOptionScalesModel scales { get; set; }
        }
        public class GraphOptionTitleModel
        {
            public Boolean display { get; set; } = true;
            public String text { get; set; }
        }
        public class GraphOptionToolTipModel
        {
            public String mode { get; set; } = "index";
            public Boolean intersect { get; set; } = false;
        }
        public class GraphOptionHoverModel
        {
            public String mode { get; set; } = "nearest";
            public Boolean intersect { get; set; } = true;
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
        }
        public class GraphYaxesrModel
        {
            public Boolean display { get; set; } = true;
            public GraphScaleLabelModel scaleLabel { get; set; }
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

        //===============================Models para os Filtros
        public class FiltroModel
        {
            public String Competencia_Inicio { get; set; }
            public String Competencia_Fim { get; set; }
        }
    }
}


