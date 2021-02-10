using System;
using System.Collections.Generic;

namespace PROPOSTA
{
    public partial class GeracaoFatura
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public GeracaoFatura(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }
        public class SolicitacaoFaturaModel
        {
            public String Id_Operacao { get; set; }
            public String Cod_Empresa { get; set; }
            public Int32 Numero_Mr { get; set; }
            public Int16 Sequencia_Mr { get; set; }
            public Int32 Numero_Complemento { get; set; }
            public Int16 Indica_Sem_Midia { get; set; }
            public String Cod_Empresa_Faturamento { get; set; }
            public String Cod_Usuario { get; set; }
            public Boolean Selected { get; set; }
        }

        public class FiltroModel
        {
            public String Emp_Faturamento { get; set; }
        }

    }
}