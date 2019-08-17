using System;
using System.Collections.Generic;

namespace PROPOSTA
{
    public partial class Veiculo
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public Veiculo(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

    }
}