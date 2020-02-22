using System;
using System.Collections.Generic;

namespace PROPOSTA
{
    public partial class TipoMidia
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public TipoMidia(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

        public class TipoMidiaModel
        {
            public String Cod_Tipo_Midia { get; set; }
            public String Descricao { get; set; }
            public Boolean Fatura_Antecipada { get; set; }
            public Boolean Gera_Receita { get; set; }
            public string id_operacao { get; set; }
        }



    }
}