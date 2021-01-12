using System;
using System.Collections.Generic;

namespace PROPOSTA
{
    public partial class ParamRoteiro
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public ParamRoteiro(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

        public class ParamRoteiroModel
        {
            public String Cod_Veiculo { get; set; }
            public String Intervalo { get; set; }
            public String Descricao { get; set; }
            public String Origem_Break { get; set; }
            public String Origem_Roteiro { get; set; }
            public Boolean Permite_Ordenacao { get; set; }
        }

    }
}


