using System;
using System.Collections.Generic;

namespace PROPOSTA
{
    public partial class TiposComercializacao
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public TiposComercializacao(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

        public class TiposComercializacaoModel
        {
            public String Cod_Tipo_Comercializacao { get; set; }
            public String Descricao { get; set; }
            public string id_operacao { get; set; }
            public Boolean Indica_Desativado { get; set; }
            public String Id_Acao { get; set; }



        }
    }
}