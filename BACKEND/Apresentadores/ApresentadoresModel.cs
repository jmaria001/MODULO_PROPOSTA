using System;
using System.Collections.Generic;


namespace PROPOSTA
{
    public partial class Apresentadores
    {

        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public Apresentadores(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }


        public class ApresentadoresModel
        {
            public String Id_operacao { get; set; }
            public String Cod_Apresentador { get; set; }
            public String Nome_Apresentador { get; set; }
            public String CGC { get; set; }
            public String Nome_Fantasia { get; set; }
            public String Razao_Social { get; set; }
            public String Cod_UF { get; set; }
            public String Email { get; set; }
            public Double Salario { get; set; }
            public String Data_Inclusao { get; set; }
            public List<Programa_Model> Programas { get; set; }
        }
        public class Programa_Model
        {
            public String Cod_Apresentador { get; set; }
            public String Cod_Programa { get; set; }
            public String Titulo { get; set; }
        }

    }
}