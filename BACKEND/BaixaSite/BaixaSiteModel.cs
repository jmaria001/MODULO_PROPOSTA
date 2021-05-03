using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROPOSTA
{
    public partial class BaixaSite
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public BaixaSite(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

        public class FiltroModel
        {
            public String Cod_Veiculo { get; set; }
            public String Data_Exibicao { get; set; }
            public String Cod_Programa { get; set; }
            public String Cod_Empresa { get; set; }
            public String Numero_Mr { get; set; }
            public Int32  Sequencia_Mr { get; set; }
            

        }
        public class BaixaModel
        {
            public String Cod_Veiculo { get; set; }
            public String Data_Exibicao { get; set; }
            public String Cod_Programa { get; set; }
            public String Cod_Empresa { get; set; }
            public Int32 Numero_Mr { get; set; }
            public Int32 Sequencia_Mr { get; set; }
            public String Cod_Comercial{ get; set; }
            public String Titulo_Comercial { get; set; }
            public String Cod_Tipo_Comercial { get; set; }
            public String Nome_Tipo_Comercial { get; set; }
            public Int32? Qtd_Previsto{ get; set; }
            public Int32? Qtd_Exibido{ get; set; }
            public Int32? Qtd_Falha { get; set; }
            public String Cod_Qualidade { get; set; }
            public Boolean Indica_Comprovado { get; set; }
            public Boolean Status { get; set; }
            public String Critica { get; set; }
        }
    }
}