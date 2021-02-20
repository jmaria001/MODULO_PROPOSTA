using System;
using System.Collections.Generic;

namespace PROPOSTA
{
    public partial class FitaPatrocinio
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public FitaPatrocinio(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

        public class FiltroModel
        {
            public String Cod_Veiculo { get; set; }
            public String Cod_Programa{ get; set; }
            public String CompetenciaInicial { get; set; }
            public String CompetenciaFinal { get; set; }
            public Boolean Indica_Pendente{ get; set; }
            public Boolean Indica_Numerada{ get; set; }


        }
        public class FitaPatrocinioModel
        {
            public String Competencia{get; set;}
            public String Competencia_String{get; set;}
            public Int32 Id_Fita_Patrocinio{get; set;}
            public String Cod_Veiculo{get; set;}
            public String Nome_Veiculo{get; set;}
            public String Cod_Programa{get; set;}
            public String Nome_Programa{get; set;}
            public String Cod_Tipo_Comercial{get; set;}
            public String Nome_Tipo_Comercial{get; set;}
            public String Inicio_Validade{get; set;}
            public String Fim_Validade{get; set;}
            public String Numero_Fita{get; set;}
            public Int32 Duracao{get; set;}
            public Int32 Duracao_Cabeca {get; set;}
            public String Data_Desativacao{get; set;}
            public String Obs_Texto{get; set;}
            public String Titulo_Texto{get; set;}
            public Int32 Duracao_Total {get; set;}	
            public String Id_Apresentador{get; set;}
            public String Nome_Apresentador{get; set;}
            public String Cod_Apresentador { get; set; }
            public Boolean Indica_Desativada { get; set; }
        }
    }
}