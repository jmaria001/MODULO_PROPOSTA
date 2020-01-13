using System;
using System.Collections.Generic;

namespace PROPOSTA
{
    public partial class CaracVeicul
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public CaracVeicul(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

        public class CaracVeiculModel
        {
            public String Cod_Caracteristica { get; set; }
            public String Descricao { get; set; }
            public String Imprime_Ce { get; set; }
            public Byte Posicao_Calculo { get; set; }
            public Boolean Indica_Basket { get; set; }
            public string id_operacao { get; set; }
        }



    }
}