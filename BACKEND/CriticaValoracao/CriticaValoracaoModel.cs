using System;
using System.Collections.Generic;
namespace PROPOSTA
{
    public partial class CriticaValoracao
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public CriticaValoracao(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

        public class FiltroModel
        {
            public String Competencia { get; set; }
            public String Cod_Empresa_Faturamento { get; set; }
            public String Razao_Social_Fat { get; set; }
            public String Cod_Empresa { get; set; }
            public String Razao_Social_Ven { get; set; }
            public Int32 Numero_Mr { get; set; }
            public Int32 Sequencia_Mr { get; set; }

            public String Id_Critica { get; set; }
            public String Descricao_Critica { get; set; }

            public String Tabela_Preco { get; set; }
            public String Sequencia_Tabela { get; set; }
            public String Tipo_Tabela { get; set; }
            public String Cod_Veiculo { get; set; }
            public String Cod_Programa { get; set; }
            public String Cod_Tipo_Comercial { get; set; }
            public String Duracao { get; set; }

        }


    }
}