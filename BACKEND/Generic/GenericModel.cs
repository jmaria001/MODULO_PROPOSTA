using System;
using System.Collections.Generic;
namespace PROPOSTA
{
    public partial class Generic
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public Generic(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

        public class jsonFiltroAtividade
        {
            public List<Newtonsoft.Json.Linq.JObject> Projeto { get; set; }
            public List<Newtonsoft.Json.Linq.JObject> Analista { get; set; }
            public List<Newtonsoft.Json.Linq.JObject> Solicitante { get; set; }
            public List<Newtonsoft.Json.Linq.JObject> Caracteristica { get; set; }
            public List<Newtonsoft.Json.Linq.JObject> Situacao { get; set; }
            public List<Newtonsoft.Json.Linq.JObject> Cliente { get; set; }
        }

        public class GridConfigGridParam
        {
            public List<Newtonsoft.Json.Linq.JObject> GridConfig { get; set; }
            public Newtonsoft.Json.Linq.JObject GridModo{ get; set; }
            public String GridName { get; set; }
        }

        public class GridConfig
        {
            public List<GridConfigHeaders> Header { get; set; } 
            public GridConfigScrool Scrool { get; set; }         }

        public class GridConfigHeaders
        {
            public String title { get; set; }
            public Boolean visible { get; set; }
            public Boolean searchable { get; set; }
            public Boolean config { get; set; }

        }
        public class GridConfigScrool
        {
            public Boolean ScroolX { get; set; } 
            public Boolean ScroolY { get; set; } 
        }


        public class VistoMensagemParam
        {
            public Int32 Id_Mensagem { get; set; } 
        }

        
        public class Filtro
        {
            public Int32 Id;
            public String Descricao;
            public Boolean Selecionado;
        }
        
        public class Mensagem
        {
            public String Texto;
            public List<MensagemUsuario> Usuario;
        }

        public class MensagemUsuario
        {
            public Int32 Usuario_Id;
            public String Usuario_Nome;
        }
        public class ParametroGeralModel
        {
            public Int32 Cod_Parametro { get; set; }
            public String Cod_Empresa_Venda { get; set; }
            public String Cod_Empresa_Faturamento { get; set; }
            public String Cod_Veiculo { get; set; }

        }
    }
}