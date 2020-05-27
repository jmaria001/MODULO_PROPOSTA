using System;
using System.Collections.Generic;

namespace PROPOSTA
{
    public partial class Parametro
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public Parametro(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

        public class ParametroModel
        {
            public Int32 Cod_Parametro { get; set; }
            public String Descricao { get; set; }
            public String Cod_Chave { get; set; }
            public Boolean Indica_Valor_Individual { get; set; }
            public string id_operacao { get; set; }
            public List<ParametroValorModel> Valores { get; set; }
            public Int32 MaxSequenciador { get; set; }
        }
        public class ParametroValorModel
        {
            public Int32 Cod_Parametro { get; set; }
            public String Cod_Empresa_Faturamento { get; set; }
            public String Nome_Empresa_Faturamento { get; set; }
            public String Cod_Empresa_Venda { get; set; }
            public String Nome_Empresa_Venda { get; set; }
            public String Cod_Veiculo { get; set; }
            public String Nome_Veiculo { get; set; }
            public String Cod_Chave { get; set; }
            public Int32 Sequenciador { get; set; }
        }
    }
}