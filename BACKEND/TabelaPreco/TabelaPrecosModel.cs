using System;
using System.Collections.Generic;

namespace PROPOSTA
{
    public partial class TabelaPrecos
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public TabelaPrecos(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }


        public class TabelaPrecosModel
        {
            public String Id_Operacao { get; set; }
            public String Competencia { get; set; }
            public Int32  Sequencia { get; set; }
            public String Tipo_Preco { get; set; }
            public String Cod_Programa { get; set; }
            public String Titulo { get; set; }
            public String Cod_Veiculo_Mercado { get; set; }
            public String Nome_Veiculo { get; set; }
            public String Valor { get; set; }
        }

        public class FiltroModel
        {
            public String Competencia { get; set; }
            public String Veiculo{ get; set; }
            public String Programa{ get; set; }
        }

    }
}