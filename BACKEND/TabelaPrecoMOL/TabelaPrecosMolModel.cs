using System;
using System.Collections.Generic;
namespace PROPOSTA
{
    public partial class TabelaPrecosMol
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public TabelaPrecosMol(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

        public class FiltroMolModel
        {
            public String Competencia { get; set; }
            public String Veiculo { get; set; }
            public String Programa { get; set; }
        }

        public class TabelaPrecosMolModel
        {
            public String Id_Operacao { get; set; }
            public String Competencia { get; set; }
            public String Cod_Programa { get; set; }
            public String Titulo { get; set; }
            public String Cod_Veiculo_Mercado { get; set; }
            public String Nome_Veiculo { get; set; }
            public List<ValoresMolModel> ValoresMol { get; set; }
            public Int32 Max_Id_Linha { get; set; }
        }

        public class ValoresMolModel
        {
            public Int32 Id_Linha { get; set; }
            public String Cod_Tipo_Comercializacao { get; set; }
            public String Nome_Comercializacao { get; set; }
            public String Valor { get; set; }
        }

    }
}