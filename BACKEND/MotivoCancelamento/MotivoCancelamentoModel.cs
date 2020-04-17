using System;
using System.Collections.Generic;

namespace PROPOSTA
{
    public partial class MotivoCancelamento
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public MotivoCancelamento(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

        public class MotivoCancelamentoModel
        {
            public String Cod_Cancelamento { get; set; }
            public String Descricao { get; set; }
            public string id_operacao { get; set; }
        }



    }
}