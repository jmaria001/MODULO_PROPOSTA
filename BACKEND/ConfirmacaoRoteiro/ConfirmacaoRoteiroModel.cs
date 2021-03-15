using System;
using System.Collections.Generic;
namespace PROPOSTA
{
    public partial class ConfirmacaoRoteiro
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();

        public ConfirmacaoRoteiro(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

        public class VeiculosModel
        {
            public Boolean Indica_Marcado { get; set; }
            public String Cod_Veiculo { get; set; }
            public String Nome_Veiculo { get; set; }
            public String Data_Confirmacao_Rede { get; set; }
            public String Data_Confirmacao_Local { get; set; }
            public String Cod_Empresa { get; set; }
            public String Critica { get; set; }
            public Boolean Indica_Processado{ get; set; }
        }

        public class ConfirmacaoRoteiroModel
        {
            public String Data_Confirmacao_Rede{ get; set; }
            public String Data_Confirmacao_Local{ get; set; }
            public List<VeiculosModel> Veiculos { get; set; }
        }

    }
}




