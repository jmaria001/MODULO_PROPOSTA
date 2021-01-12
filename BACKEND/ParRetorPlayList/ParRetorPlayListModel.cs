using System;
using System.Collections.Generic;

namespace PROPOSTA
{
    public partial class ParRetorPlayList
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public ParRetorPlayList(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

        public class RetornoPlayListModel
        {
            public string Cod_Veiculo { get; set; }
            public string Formato_Data { get; set; }
            public string Tipo_Arquivo { get; set; }
            public List<CamposModel> Campos { get; set; }
            public List<ValidacaoModel> Validacao { get; set; }
        }
        public class CamposModel
        {
            public String Nome_Campo { get; set; }
            public String Campo { get; set; }
            public String Posicao { get; set; }
            public String Tamanho { get; set; }
        }

        public class ValidacaoModel
        {
            public String Descricao { get; set; }
            public String Posicao { get; set; }
            public String Tamanho { get; set; }
            public String Conteudo { get; set; }
        }
    }
}


