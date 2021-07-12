using System;
using System.Collections.Generic;

namespace PROPOSTA
{
    public partial class CalculoValoracao
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public CalculoValoracao(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

        public class CalculoValoracaoModel
        {
            public String Cod_Empresa { get; set; }
            public String Nome_Empresa { get; set; }
            public Int32 Numero_Mr { get; set; }
            public Int32 Sequencia_Mr { get; set; }
            public String Competencia { get; set; }
            public Int32 Numero_Negociacao { get; set; }
            public String Critica { get; set; }

        }

    }
}