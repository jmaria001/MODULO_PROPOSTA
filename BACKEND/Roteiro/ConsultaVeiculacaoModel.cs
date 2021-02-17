using System;
using System.Collections.Generic;

namespace PROPOSTA
{
    public partial class ConsultaVeiculacao
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public ConsultaVeiculacao(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }
                                                                            

        public class FiltroConsultaVeiculacaoModel
        {
            public String Veiculo { get; set; }
            public String Empresa { get; set; }
            public Int32 Contrato { get; set; }
            public Int32 Sequencia { get; set; }
            public String Programa { get; set; }
            public String Data_Inicio { get; set; }
            public String Data_Termino { get; set; }
            public String Qualidade { get; set; }
            public Boolean Baixadas{ get; set; }
            public Boolean NaoBaixadas { get; set; }
            public Boolean Ordenadas { get; set; }
            public Boolean NaoOrdenadas { get; set; }
            public Boolean Net { get; set; }
            public Boolean Local { get; set; }
        }

    }
}