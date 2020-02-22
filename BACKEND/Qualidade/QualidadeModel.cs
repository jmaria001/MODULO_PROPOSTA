using System;
using System.Collections.Generic;

namespace PROPOSTA
{
    public partial class Qualidade 
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public Qualidade(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }
                  

        public class QualidadeModel
        {
            public String Cod_Qualidade { get; set; }
            public String Cod_Motivo_Falha { get; set; }
            public String Descricao { get; set; }
            public String Descricao_Motivo { get; set; }
            public Boolean Indica_Am { get; set; }
            public Boolean Indica_Am_Futuro { get; set; }
            public Boolean Indica_Calculo { get; set; }
            public Boolean Indica_Comprovante { get; set; }
            public Boolean Indica_Demanda { get; set; }
            public Boolean Indica_Horario { get; set; }
            public Boolean Indica_Roteiro { get; set; }
            public Boolean Indica_CCW { get; set; }
            public String id_operacao { get; set; }
        }
    }
}