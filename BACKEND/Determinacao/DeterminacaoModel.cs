using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROPOSTA
{
    public partial class Determinacao
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public Determinacao(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

        public class FiltroModel
        {
            public String Cod_Empresa { get; set; }
            public String Numero_Mr { get; set; }
            public Int32  Sequencia_Mr { get; set; }
        }


    }
}