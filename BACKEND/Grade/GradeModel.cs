

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
        public class GradeModel
        {
            public String Action { get; set; }
            public Int32 RedeId { get; set; }
            public String Cod_Veiculo { get; set; }
            public String Nome_Veiculo { get; set; }
            public String Data_Exibicao { get; set; }
            public String Cod_Programa { get; set; }
            public String Nome_Programa { get; set; }
            public Int32 Dispo_Net { get; set; }
            public Int32 Dispo_Local { get; set; }
            public Int32 Dispo_Programa { get; set; }
            public Byte Faixa_Horaria { get; set; }
            public String Horario_Final { get; set; }
            public String Horario_Inicial { get; set; }
            public Boolean Domingo { get; set; } = false;
            public Boolean Segunda { get; set; } = false;
            public Boolean Terca { get; set; } = false;
            public Boolean Quarta { get; set; } = false;
            public Boolean Quinta { get; set; } = false;
            public Boolean Sexta { get; set; } = false;
            public Boolean Sabado { get; set; } = false;
            public String Inicio_Validade { get; set; }
            public String Termino_Validade { get; set; }
            public List<VeiculoModel> Veiculos { get; set; }

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
            public String Hora_Inicio { get; set; }
            public String Hora_Termino { get; set; }
            public String Nome_Genero { get; set; }
            public Boolean Indica_Desativado { get; set; }
        }
        public class GradeFiltroModel
        {
            public String Cod_Veiculo { get; set; }
            public String Competencia { get; set; }
            public String Cod_Programa{ get; set; }
            public Int32 RedeId{ get; set; }
        }
        public class GradeGetDataModel
        {
            public String Action    { get; set; }
            public String Cod_Veiculo { get; set; }
            public DateTime Data_Exibicao { get; set; }
            public String Cod_Programa { get; set; }
            

        }
        public class VeiculoModel
        {
            public String Cod_Veiculo { get; set; }
            public String Nome_Veiculo { get; set; }
            public Boolean Selected { get; set; }
        }

    }
}