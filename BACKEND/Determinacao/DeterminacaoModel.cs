using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROPOSTA
{
    public partial class Determinacao
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public Determinacao(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

        public class DeterminacaoModel
        {
            public Int32 Id_Contrato { get; set; }
            public String Cod_Empresa { get; set; }
            public Int32 Numero_Mr { get; set; }
            public Int32 Sequencia_Mr { get; set; }
            public String Cod_Agencia { get; set; }
            public String Nome_Agencia { get; set; }
            public String Cod_Cliente { get; set; }
            public String Nome_Cliente { get; set; }
            public String Data_Inicio { get; set; }
            public String Data_Fim { get; set; }
            public Int32 Competencia { get; set; }
            public List<DeterminacaoComercialModel> Comerciais { get; set; }
            public List<DeterminacaoVeiculoModel> Veiculos { get; set; }
            public List<DeterminacaoProgramaModel> Programas { get; set; }

        }
        public class FiltroModel
        {
            public String Cod_Empresa { get; set; }
            public Int32 Numero_Mr { get; set; }
            public Int32 Sequencia_Mr { get; set; }
        }
        public class DeterminacaoComercialModel
        {
            public String Cod_Empresa { get; set; }
            public Int32 Numero_Mr { get; set; }
            public Int32 Sequencia_Mr { get; set; }
            public String Cod_Comercial { get; set; }
            public String Titulo_Comercial { get; set; }
            public Int32 Duracao { get; set; }
            public String Cod_Tipo_Comercial { get; set; }
            public String Nome_Tipo_Comercial { get; set; }
            public Int32 Cod_Red_Produto { get; set; }
            public String Nome_Produto { get; set; }
            public Boolean Indica_Titulo_Determinar { get; set; }
            public Boolean Selected_De{ get; set; }
            public Boolean Selected_Para { get; set; }
            public Boolean Tem_Veiculacao{ get; set; }
            public List<DeterminacaoRotateModel> Rotate { get; set; }
        }
        public class DeterminacaoVeiculoModel
        {
            public String Codigo{ get; set; }
            public String Descricao{ get; set; }
            public Boolean Selected { get; set; }
        }
        public class DeterminacaoProgramaModel
        {
            public String Codigo { get; set; }
            public String Descricao { get; set; }
            public Boolean Selected { get; set; }
        }
        public class DeterminacaoRotateModel
        {
            public String Cod_Comercial_De { get; set; }
            public String Cod_Comercial_Para { get; set; }
        }
        public class AnaliseRotateModel
        {
            public Boolean Status { get; set; }
            public String Mensagem { get; set; }
            public Int32 Id_Rotate { get; set; }
            public String Cod_Veiculo { get; set; }
            public String Cod_Programa { get; set; }
            public String Cod_Comercial { get; set; }
            public Int32 Duracao { get; set; }
            public Int32 Operacao { get; set; }
            public List<AnaliseRotateInsercoes> Insercoes { get; set; }

        }
        public class AnaliseRotateInsercoes
        {
            public Int32 Id_Rotate { get; set; }
            public DateTime Data_Exibicao { get; set; }
            public String Dia_Semana { get; set; }
            public Int32 Qtd{ get; set; }

        }
    }
}