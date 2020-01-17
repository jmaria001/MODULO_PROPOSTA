using System;
using System.Collections.Generic;

namespace PROPOSTA
{
    public partial class MotivoAlterNegoc
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public MotivoAlterNegoc(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

        public class MotivoAlterNegocModel
        {
            public String Cod_Alteracao { get; set; }
            public String Descricao { get; set; }
            public string id_operacao { get; set; }
        }


    }
}


