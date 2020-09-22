using System;
using System.Collections.Generic;

namespace PROPOSTA
{
    public partial class Roteiro
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public Roteiro(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }
        //public class RoteiroModel
        //{
        //    public List<RoteiroProgramasModel> Programas { get; set; }
        //}
        //public class RoteiroProgramasModel
        //{
        //    public Int32 Id_Programa { get; set; }
        //    public String Cod_Programa { get; set; }
        //    public String Titulo_Programa { get; set; }
        //    public String Hora_Inicio { get; set; }
        //    public String Hora_Fim { get; set; }
        //    public Boolean Show { get; set; }
        //    public List<RoteiroItemModel> Items { get; set; }
        //}
        public class RoteiroModel
        {
            public Int32 Id_Programa { get; set; }
            public Int32 Id_Break { get; set; }
            public Int32 Id_Intervalo { get; set; }
            public Int32 Id_Item { get; set; }
            public String Cod_Programa { get; set; }
            public String Cod_Veiculo { get; set; }
            public DateTime Data_Exibicao { get; set; }
            public String Titulo_Programa { get; set; }
            public DateTime Hora_Inicio_Programa { get; set; }
            public DateTime Hora_Fim_Programa { get; set; }
            public Boolean Show { get; set; }
            public Int32 Break { get; set; }
            public String Titulo_Break { get; set; }
            public Int32 Sequencia_Faixa { get; set; }
            public Int32 Sequencia_Break { get; set; }
            public Int32 Sequencia_Intervalo{ get; set; }
            public String Hora_Inicio_Break { get; set; }
            public Int32 Tipo_Break { get; set; }
            public String Nome_Tipo_Break { get; set; }
            public Boolean Indica_Titulo_Programa { get; set; }
            public Boolean Indica_Titulo_Break { get; set; }
            public Boolean Indica_Titulo_Intervalo { get; set; }
            public Boolean Indica_Comercial { get; set; }
            public String Titulo_Comercial { get; set; }
            public String Cod_Comercial { get; set; }
            public Int32 Duracao { get; set; }
            public String Cod_Produto { get; set; }
            public String Nome_Produto { get; set; }
            public String Numero_Fita { get; set; }
            public Int32 Id_Fita { get; set; }
            public Int32 Encaixe { get; set; }
            public String Origem { get; set; }
            public String Cod_Empresa { get; set; }
            public Int32 Numero_Mr { get; set; }
            public Int32 Sequencia_Mr { get; set; }
            public String Cod_Veiculo_Origem { get; set; }
            public String Cod_Programa_Origem { get; set; }
            public Int32 Chave_Acesso{ get; set; }
            public Int32 Versao{ get; set; }
            public Int32 Contador_Item { get; set; }
            public Boolean Permite_Ordenacao { get; set; }

        }
        public class RoteiroFiltroModel
        {
            public String Cod_Veiculo { get; set; }
            public String Data_Exibicao { get; set; }
            public String Cod_Programa { get; set; }
            public List<GuiaProgramsaModel> Programas { get; set; }
        }
        public class GuiaProgramsaModel
        {
            public String Cod_Programa { get; set; }
            public String Titulo_Programa { get; set; }
            public Boolean Selected { get; set; }
        }

        public class RoteiroComercialModel
        {
            public Int32 Id_Item { get; set; }
            public String Origem{ get; set; }
            public Boolean Indica_Titulo_Programa { get; set; } 
            public String Cod_Programa { get; set; }
            public String Titulo_Programa { get; set; }
            public String Cod_Veiculo{ get; set; }
            public DateTime Data_Exibicao { get; set; }
            public DateTime Hora_Inicio_Programa { get; set; }
            public DateTime Hora_Fim_Programa { get; set; }
            public String Cod_Empresa { get; set; }
            public Int32 Numero_Mr { get; set; }
            public Int32 Sequencia_Mr{ get; set; }
            public String Cod_Comercial { get; set; }
            public String Titulo_Comercial { get; set; }
            public Int32 Duracao { get; set; }
            public String Numero_Fita { get; set; }
            public Int32 Id_Fita { get; set; }
            public Boolean Indica_Titulo_Determinar { get; set; }
            public String Cod_Tipo_Comercial { get; set; }
            public String Cod_Produto { get; set; }
            public String Nome_Produto { get; set; }
            public Byte Indica_Grade{ get; set; }
            public Int32? Tipo_Break { get; set; }
            public String Nome_Tipo_Break { get; set; }
            public Int32 Chave_Acesso{ get; set; }
            public String Obs_Roteiro { get; set; }
            public String Pasta { get; set; }
            public Boolean Indica_Ordenado { get; set; }
        }
        public class BreakModel
        {
            public String Cod_Programa { get; set; }
            public String Nome_Programa { get; set; }
            public String Cod_Veiculo { get; set; }
            public String Data_Exibicao { get; set; }
            public String Data_Inicio_Propagacao { get; set; }
            public String Data_Fim_Propagacao { get; set; }
            public String Ultimo_Dia_Break { get; set; }
            public Boolean Grade_Domingo { get; set; }
            public Boolean Grade_Segunda{ get; set; }
            public Boolean Grade_Terca { get; set; }
            public Boolean Grade_Quarta{ get; set; }
            public Boolean Grade_Quinta { get; set; }
            public Boolean Grade_Sexta { get; set; }
            public Boolean Grade_Sabado{ get; set; }
            public String Hora_Inicio_Programa{ get; set; }
            public Int32 Dispo_Net { get; set; }
            public Int32 Dispo_Local{ get; set; }

            public List<ComposicaoBreakModel> Composicao { get; set; }
        }
        public class ComposicaoBreakModel
        {
        public Int32 Id_Composicao { get; set; }
        public Int32? Breaks { get; set; }
        public Int32? Sequencia_Faixa { get; set; }
        public Int32? Sequencia { get; set; }
        public Int32? Duracao { get; set; }
        public TipoBreakModel Tipo_Break { get; set; }
        public String Titulo_Break { get; set; }
        public Int32? Sequencia_Break { get; set; }
        public String Observacao { get; set; }
        public String Hora_Inicio { get; set; }
        }
        public class TipoBreakModel
        {
            public Int32 Codigo { get; set; }
            public String Descricao { get; set; }
        }
        public class FiltroPreOrdModel
        {
            public String Veiculo { get; set; }
            public String Data { get; set; }
            public Boolean Indica_Somente_Prg { get; set; }
            public Boolean Indica_Todos_Prgs { get; set; }
            public String Programa { get; set; }
            public Boolean Indica_PreOrdenar_Rotativos { get; set; }
            public Boolean Indica_PreOrdenar_Vinhetas { get; set; }
            public Boolean Indica_Evitar_Choque_Produtos { get; set; }
            public Boolean Indica_Evitar_Choque_Apresent { get; set; }
            public Boolean Indica_Nao_Colar_Comerciais { get; set; }
        }

    }

}
