using System;
using System.Collections.Generic;

namespace PROPOSTA
{
    public partial class Rede
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public Rede(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

        public class RedeModel
        {
            public Int32 RedeID { get; set; }
            public String NomeRede { get; set; }
            public String BackColorTab{ get; set; }
            public string id_operacao { get; set; }
        }



    }
}