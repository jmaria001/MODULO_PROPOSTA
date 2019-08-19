using System;
using System.Collections.Generic;

namespace PROPOSTA
{
    public partial class Mercado
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public Mercado(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

        public class MercadoModel
        {
            public String Cod_Mercado { get; set; }
            public String Nome { get; set; }
            public String Cod_JOVE { get; set; }
            public Boolean Indica_Net { get; set; }
            public String id_operacao { get; set; }
            public List<VeiculoMercadoModel> ListaVeiculo { get; set; }
        }

        public class VeiculoMercadoModel
        {
            public String Cod_Veiculo { get;  set; }
            public String Nome_Veiculo { get; set; }
            public Boolean Selected { get; set; }
        }

    }
}