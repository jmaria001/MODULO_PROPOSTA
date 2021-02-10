using System;
using System.Collections.Generic;

namespace PROPOSTA
{

    public partial class EnvioPlayList
    {

        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public EnvioPlayList(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

        public class EnvioPlayListModel
        {
            public string Cod_Veiculo { get; set; }
            public string Nome_Veiculo { get; set; }
            public string Data_Programacao { get; set; }
            public string Exibidor { get; set; }
            public string Nome_Arquivo { get; set; }
            public string Sistema_Exibicao_Digital { get; set; }
            public string Nome_Arquivo_Integracao { get; set; }
            public string Posicao_Num_Fita { get; set; }
            public string Tamanho_Num_Fita { get; set; }

        }
        public class GeracaoPlayListModel
        {
            public Boolean Status { get; set; }
            public String Mensagem{ get; set; }
            public String Url{ get; set; }
        }
    }
}




