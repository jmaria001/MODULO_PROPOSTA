

using System;
using System.Collections.Generic;


namespace PROPOSTA
{
    public partial class Grade
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public Grade(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

        public class GradeListModel
        {
            public List<GradeListDiaModel> Dias { get; set; }
        }
        public class GradeListDiaModel
        {
            public DateTime Data_Exibicao { get; set; }
            public String Dia_Semana { get; set; }
            public List<GradeListProgramaModel> Programas { get; set; }
        }
        public class GradeListProgramaModel
        {
            public String Cod_Programa { get; set; }
            public String Nome_Programa { get; set; }
            public String Hora_Inicio{ get; set; }
            public String Hora_Termino{ get; set; }
            public String Nome_Genero{ get; set; }
        }
        public class GradeFiltroModel
        {
            public String Cod_Veiculo { get; set; }
            public String Competencia { get; set; }
        }


    }
}