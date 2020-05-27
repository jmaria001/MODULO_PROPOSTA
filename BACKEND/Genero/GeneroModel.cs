using System;
using System.Collections.Generic;

namespace PROPOSTA
{
    public partial class Genero
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public Genero(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

        public class GeneroModel
        {
            public String Cod_Genero { get; set; }
            public String Descricao { get; set; }
            public int Id_Genero { get; set; }
            public string id_operacao { get; set; }
        }



    }
}