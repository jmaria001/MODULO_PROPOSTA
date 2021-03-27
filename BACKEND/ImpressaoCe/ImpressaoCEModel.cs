using System;
using System.Collections.Generic;
namespace PROPOSTA
{
    public partial class ImpressaoCE
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();

        public ImpressaoCE(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

        public class ImpressaoCeFiltroModel
        {
            public String Cod_Empresa_Faturamento { get; set; }
            public String Competencia { get; set; }
            public String Nome_Empresa_Faturamento { get; set; }
            public String Data_Processamento { get; set; }
            public Int32 Numero_Ce_Inicio { get; set; }
            public Int32 Numero_Ce_Fim { get; set; }
            public String Cod_Empresa_Venda { get; set; }
            public Int32 Numero_Mr { get; set; }
            public Int32 Sequencia_Mr { get; set; }
            public Int32 Numero_Fatura { get; set; }
            public String Agencia { get; set; }
            public String Cliente { get; set; }
        }
        public class RetornoImpressaoCeModel
        {
            public Boolean Status { get; set; }
            public String Mensagem{ get; set; }
            public String pdfFileName { get; set; }
        }
    }
}




