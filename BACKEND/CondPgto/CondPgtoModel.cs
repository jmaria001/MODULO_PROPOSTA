using System;
using System.Collections.Generic;

namespace PROPOSTA
{
    public partial class CondPgto
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public CondPgto(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

        public class CondPgtoModel
        {
            public String Cod_Condicao { get; set; }
            public String Descricao { get; set; }
            public Int32  Qtd_Dias { get; set; }
            public String Tipo_Vencimento { get; set; }
            public string id_operacao { get; set; }
            public String Usuario_Cadastro { get; set; }

        }



    }
}