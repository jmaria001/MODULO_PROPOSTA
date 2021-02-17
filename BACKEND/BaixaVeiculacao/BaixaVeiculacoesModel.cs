using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROPOSTA
{
    public partial class BaixaVeiculacoes
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public BaixaVeiculacoes(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

        public class BaixaVeiculacoesModel
        {

            public String Cod_Qualidade { get; set; }
            public String Cod_Qualidade_Ant { get; set; }
            public String Cod_Tipo_Midia { get; set; }
            public String Descricao_Midia { get; set; }
            public String Cod_Tipo_Comercial { get; set; }
            public String Descricao_Comercial { get; set; }
            public String Documento_De { get; set; }
            public String Documento_Para { get; set; }
            public String Numero_Mr { get; set; }
            public Int32  Sequencia_Mr { get; set; }
            public String Cod_Empresa { get; set; }
            public String Data_Exibicao { get; set; }
            public String Horario_Exibicao { get; set; }
            public String Horario_Exibicao_Ant { get; set; }
            public String Cod_Veiculo { get; set; }
            public Int32  Chave_Acesso { get; set; }
            public String Cod_Programa { get; set; }
            public Boolean Status { get; set; }
            public String Mensagem { get; set; }
            public Boolean Indica_Processado{ get; set; }
            public Int32 Qtd_Baixados{ get; set; }
            public Boolean Critica{ get; set; }


        }


        public class FiltroModel
        {
            public String Cod_Veiculo { get; set; }
            public String Data_Exibicao { get; set; }
            public String Cod_Programa { get; set; }
            public Int32  Chave_Acesso { get; set; }
            public String Numero_Mr { get; set; }
            public Int32  Sequencia_Mr { get; set; }
            public String Cod_Empresa { get; set; }
            public String Cod_Comercial { get; set; }
            public Int32  Duracao { get; set; }

        }


    }
}