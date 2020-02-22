using System;
using System.Collections.Generic;

namespace PROPOSTA
{
    public partial class MotivoFalha
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public MotivoFalha(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

        public class MotivoFalhaModel
        {
            public String Cod_Motivo_Falha { get; set; }
            public String Descricao { get; set; }
            public Boolean Indica_Desativado { get; set; }
            public String  Id_operacao { get; set; }
            public String Id_Acao { get; set; }
        }

    }
}


