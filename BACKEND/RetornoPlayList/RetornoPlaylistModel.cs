using System;
using System.Collections.Generic;

namespace PROPOSTA
{

    public partial class RetornoPlayList
    {

        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public RetornoPlayList(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

        public class RetornoPlayListModel
        {
            public string Cod_Veiculo { get; set; }
            public string Nome_Veiculo { get; set; }
            public string Data_Exibicao { get; set; }
            public string Horario_Emissora { get; set; }
            public string Sistema_Exibicao { get; set; }
            public Boolean Indica_Fitas_Avulsas { get; set; }
            public Boolean Indica_Fitas_Artisticas { get; set; }
            public string Data_Inicio { get; set; }
            public string Hora_Inicio { get; set; }
            public string Data_Fim { get; set; }
            public string Hora_Fim { get; set; }
            public string Tipo_Arquivo { get; set; }
            public string Nome_Tabela { get; set; }
            public List<AnexoModel> Anexos { get; set; }
        }

        public class RetornoPlayListBaixaModel
        {
            public Boolean Origem { get; set; }
            public Byte Status { get; set; }
            public String Cod_Veiculo { get; set; }
            public DateTime Data_Exibicao { get; set; }
            public String Cod_Programa { get; set; }
            public String Chave_Acesso { get; set; }
            public String Numero_Fita { get; set; }
            public String Horario_Exibicao { get; set; }
            public String Titulo_Comercial { get; set; }
            public String Duracao { get; set; }
            public String Cod_Programa_Origem { get; set; }
            public String Cod_Veiculo_Origem { get; set; }
            public String Cod_Empresa { get; set; }
            public String Numero_Mr  { get; set; }
            public String Sequencia_Mr { get; set; }
            public String Numero_Ce { get; set; }
            public String Cod_Qualidade { get; set; }
            public String Cod_Qualidade_Anterior { get; set; }
            public String Numero_Hora_Inicio { get; set; }
            public String Cod_Comercial { get; set; }
            public String Duracao_Exibidor { get; set; }
            public String Diferenca { get; set; }
            public String Pct_Exibido { get; set; }
            public Boolean Indica_Processado { get; set; } = false;
            public Boolean Indica_Estouro_Tolerancia { get; set; }
            public Boolean Indica_Critica { get; set; } 
            public String Mensagem_Critica { get; set; } 
        }
        public class AnexoModel
        {
            public String AnexoName { get; set; }
            public String Url { get; set; }
        }
    }
}




