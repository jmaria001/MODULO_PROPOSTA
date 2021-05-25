using System;
using System.Collections.Generic;

namespace PROPOSTA
{

    public partial class PrevisaoVendas
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public PrevisaoVendas(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

        public class FiltroModel
        {
            public String Competencia { get; set; }
            public String Cod_Contato { get; set; }
            public String Nome_Contato { get; set; }
            public String TipoPrevisao { get; set; }
            public String Valor_Jan { get; set; }

        }

        public class PrevisaVendasMensalModel
        {
            public Byte  Tipo_Linha { get; set; }
            public String Cod_Contato { get; set; }
            public Int32 Ano{ get; set; }
            public String Mes { get; set; }
            public Int32 Competencia{ get; set; }
            public String Valor_Negociado { get; set; }
            public String Valor_Previsao { get; set; }
            public Boolean Status { get; set; }
        }

        public class PrevisaoVendasAgenciaModel
        {
            public Byte Tipo_Linha { get; set; }
            public String Cod_Contato{ get; set; }
            public String Cod_Agencia { get; set; }
            public String Nome_Agencia { get; set; }
            public String Cod_Cliente { get; set; }
            public String Nome_Cliente { get; set; }
            public Int32 Ano { get; set; }
            public String Valor_Negociado { get; set; }
            public String Valor_Jan { get; set; }
            public String Valor_Fev { get; set; } 
            public String Valor_Mar { get; set; } 
            public String Valor_Abr { get; set; }
            public String Valor_Mai { get; set; }
            public String Valor_Jun { get; set; } 
            public String Valor_Jul { get; set; }
            public String Valor_Ago { get; set; }
            public String Valor_Set { get; set; }
            public String Valor_Out { get; set; }
            public String Valor_Nov { get; set; }
            public String Valor_Dez { get; set; }
            public String Valor_Total { get; set; }
            public Boolean Status { get; set; }
            
        }

        public class PrevisaoVendasVeiculoModel
        {
            public Byte Tipo_Linha { get; set; }
            public String Cod_Contato { get; set; }
            public String Cod_Veiculo { get; set; }
            public String Nome_Veiculo { get; set; }
            public Int32 Ano { get; set; }
            public String Valor_Negociado { get; set; }
            public String Valor_Jan { get; set; }
            public String Valor_Fev { get; set; }
            public String Valor_Mar { get; set; }
            public String Valor_Abr { get; set; }
            public String Valor_Mai { get; set; }
            public String Valor_Jun { get; set; }
            public String Valor_Jul { get; set; }
            public String Valor_Ago { get; set; }
            public String Valor_Set { get; set; }
            public String Valor_Out { get; set; }
            public String Valor_Nov { get; set; }
            public String Valor_Dez { get; set; }
            public String Valor_Total { get; set; }
            public Boolean Status { get; set; }

        }






    }
}