

using System;
using System.Collections.Generic;


namespace PROPOSTA
{
    public partial class AM
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public AM(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

        public class AMFiltroModel
        {
            public String Competencia { get; set; }
            public String Cod_Nucleo { get; set; }
            public String Cod_Contato { get; set; }
            public String Cliente { get; set; }
            public String Agencia { get; set; }
            public String Cod_Veiculo { get; set; }
            public String Cod_Programa { get; set; }
            public Int32  Cod_Red_Produto { get; set; }
            public String Cod_Empresa { get; set; }
            public Int32 Numero_Mr { get; set; }
            public Int32 Sequencia_Mr { get; set; }
            public Int32 Numero_Negociacao { get; set; }
            public Int32 Situacao { get; set; }
        }

        public class AMModel
        {
            public String Cod_Empresa { get; set; }
            public Int32  Numero_Mr { get; set; }
            public Int32  Sequencia_Mr { get; set; }
            public String Documento_Para { get; set; }
            public String Competencia { get; set; }
            public String Cod_Veiculo { get; set; }
            public String Cod_Comercial { get; set; }
            public Int32  Qtd_Total_Falha { get; set; }
            public Double Valor_Total_Falha { get; set; }
            public Int32 Qtd_Total_Compensacao { get; set; }
            public Double Valor_Total_Compensacao { get; set; }
            public Int32 Situacao { get; set; }
            public List<Falhas_Model> Falhas { get; set; }
            public List<Reencaixe_Model> Compensacoes{ get; set; }
            

        }

        public class Falhas_Model
        {
            public String Data_Exibicao { get; set; }
            public String Cod_Programa { get; set; }
            public String Cod_Comercial { get; set; }
            public String Titulo_Comercial { get; set; }
            public Int32 Duracao { get; set; }
            public String Cod_Qualidade { get; set; }
            public String Cod_Veiculo { get; set; }
            public Double Valor { get; set; }
            public Int32 Qtd_Falhas { get; set; }

        }
        public class Reencaixe_Model
        {
            public String Cod_Empresa { get; set; }
            public Int32 Numero_Mr { get; set; }
            public Int32 Sequencia_Mr { get; set; }
            public String Documento_Para { get; set; }
            public String Cod_Veiculo { get; set; }
            public String Competencia { get; set; }
            public String Cod_Programa { get; set; }
            public String Data_Exibicao { get; set; }
            public String Cod_Comercial { get; set; }
            public String Titulo_Comercial { get; set; }
            public Int32 Duracao { get; set; }
            public Double Valor { get; set; }
            public Int32 Qtd_Compensacao { get; set; }
            public SolucaoModel Solucao { get; set; }
            public Int32 Chave_Acesso { get; set; }
        }

        public class ParamGradeModel
        {
            public String Cod_Veiculo { get; set; }
            public String Competencia { get; set; }
            public String Cod_Empresa { get; set; }
            public Int32 Numero_Mr { get; set; }
            public Int32 Sequencia_Mr { get; set; }
            public String Cod_Programa{ get; set; }
        }
        public class SolucaoModel
        {
            public Int32 Id { get; set; }
            public String Descricao { get; set; }
            public String Letra { get; set; }

        }



        }

}