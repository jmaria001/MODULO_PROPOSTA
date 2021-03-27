using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROPOSTA
{
    public partial class HorarioExibicao
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public HorarioExibicao(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

        public class GravarModel
        {
            public List<HorarioExibicaoModel> HorarioExibicao { get; set; }
            public List<Veiculos_Model> Veiculos { get; set; }
        }

        public class HorarioExibicaoModel
        {

            public String Cod_Veiculo { get; set; }
            public String Data_Exibicao { get; set; }
            public String Cod_Programa { get; set; }
            public String Titulo { get; set; }
            public String Horario_Inicial { get; set; }
            public String Horario_Final { get; set; }
            public String Horario_Inicio_Real { get; set; }
            public String Horario_Final_Real { get; set; }
            public Boolean Status { get; set; }
            public String Mensagem { get; set; }
            public Boolean Indica_Processado { get; set; }
            public Int32 Qtd_Processado { get; set; }
            

        }

        public class FiltroModel
        {
            public String Cod_Veiculo { get; set; }
            public String Data_Exibicao { get; set; }
        }


        public class Veiculos_Model
        {
            public String Codigo { get; set; }
            public String Descricao { get; set; }
            public Boolean Selected { get; set; }

        }



    }
}