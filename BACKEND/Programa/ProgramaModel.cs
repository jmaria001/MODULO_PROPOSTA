using System;
using System.Collections.Generic;

namespace PROPOSTA
{
    public partial class Programa
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public Programa(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

        public class ProgramaModel
        {
            public String  Id_operacao { get; set; }
            public Int32   RedeId { get; set; }
            public String  NomeRede { get; set; }
            public String  Cod_Programa { get; set; }
            public String  Titulo { get; set; }
            public String  Sub_Titulo { get; set; }
            public String  Cod_Genero { get; set; }
            public String  Genero { get; set; }
            public Boolean Indica_evento { get; set; }
            public Boolean Indica_Rotativo { get; set; }
            public Boolean Indica_Local { get; set; }
            public Boolean Indica_Desativado { get; set; }
            public Boolean Indica_Programet { get; set; }
            public Boolean Indica_Boletim { get; set; }
            public Boolean Indica_Internet { get; set; }
            public Boolean Indica_Faixa { get; set; }
            public Int32   Qtd_Cotas { get; set; }
            public String Horario_Exibicao { get; set; }
            public Boolean Indica_Patrocinio { get; set; }
            public String  Cod_A_JOVE { get; set; }
            public Int32   Cod_N_JOVE { get; set; }
            public Boolean DiaSeg { get; set; }
            public Boolean DiaTer { get; set; }
            public Boolean DiaQua { get; set; }
            public Boolean DiaQui { get; set; }
            public Boolean DiaSex { get; set; }
            public Boolean DiaSab { get; set; }
            public Boolean DiaDom { get; set; }
            public String  Sinopse { get; set; }
            public Boolean Tem_Veiculo { get; set; }
            public List<Apresentador_Model> Apresentadores { get; set; }
            public List<Veiculos_Model> Veiculos { get; set; }


        }

        public class Apresentador_Model {
           public Int32 Id_Apresentador { get; set; }
           public String Nome_Apresentador { get; set; }
           public String Cod_Programa { get; set; }
           public List<ApresentadorSel_Model> ApresentadoresSel { get; set; }

        }

        public class Veiculos_Model
        {
            public String Cod_Veiculo { get; set; }
            public String Nome_Veiculo { get; set; }
            public String Cod_Programa { get; set; }
        }



        public class ApresentadorSel_Model
        {
            public Int32 Id_Apresentador { get; set; }
            public String Nome_Apresentador { get; set; }
         }
    }
}