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
            public List<GridConfigHeaders> Header;
            public GridConfigScrool Scrool;
        }

        public class GridConfigHeaders
        {
            public String title;
            public Boolean visible;
            public Boolean searchable;
            public Boolean config;

        }
        public class GridConfigScrool
        {
            public Boolean ScroolX;
            public Boolean ScroolY;
        }


        public class VistoMensagemParam
        {
            public Int32 Id_Mensagem;
        }

        public class FiltroAtividade
        {
            public List<Filtro> Projeto;
            public List<Filtro> Analista;
            public List<Filtro> Solicitante;
            public List<Filtro> Caracteristica;
            public List<Filtro> Situacao;
            public List<Filtro> Cliente;

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
    }
}