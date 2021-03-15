using System;
using System.Collections.Generic;
namespace PROPOSTA
{
    public partial class GeracaoCE
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();

        public GeracaoCE(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

        public class GeracaoCEModel
        {
            public String Cod_Empresa { get; set; }
            public String Data_Limite { get; set; }
            public Boolean Indica_Geracao { get; set; }
            public List<VeiculosModel> Veiculos { get; set; }

        }

        public class VeiculosModel
        {
            public Boolean Indica_Marcado { get; set; }
            public String Cod_Veiculo { get; set; }
            public String Nome_Veiculo { get; set; }
        }

        public class LogsModel
        {
            public String Cod_Empresa_Faturamento { get; set; }
            public String Numero_CE_Inicio { get; set; }
            public String Numero_CE_Termino { get; set; }
            public String Qtde_Pendente { get; set; }
            public String Qtde_Gerado { get; set; }
            public String Qtde_Criticado { get; set; }
        }

        public class CriticaModel
        {
            public String Cod_Emp_Fat_Crit { get; set; }
            public String Razao_Emp_Fat_Crit { get; set; }
            public String Cod_Empresa_Crit { get; set; }
            public String Numero_MR_Crit { get; set; }
            public String Sequencia_Mr_Crit { get; set; }
            public String Qtde_Crit { get; set; }
            public String Id_Contrato_Crit { get; set; }
            public String Cod_Veiculo_Crit { get; set; }
            public String Data_Process_Crit { get; set; }
            public String Mensagem_Crit { get; set; }
            public String Tentativas_Crit { get; set; }
            public String Data_Geracao_Crit { get; set; }
            public String Usuario_Crit { get; set; }
        }

    }
}




