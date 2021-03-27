using System;
using System.Collections.Generic;

namespace PROPOSTA
{
    public partial class ReabreCE
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public ReabreCE(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }


        public class ReabreCEModel
        {
            public String Id_Operacao { get; set; }
            public String Cod_Empresa { get; set; }
            public Int32 Numero_Mr { get; set; }
            public int Sequencia_Mr { get; set; }
            public String Cod_Veiculo { get; set; }
            public String Motivo_Reabertura { get; set; }
        }

        public class FiltroReabreCEModel
        {
            public String Empresa { get; set; }
            public Int32 Contrato { get; set; }
            public Int32 Sequencia { get; set; }
            public String Veiculo { get; set; }
            public String MotivoReabertura { get; set; }
        }

    }
}