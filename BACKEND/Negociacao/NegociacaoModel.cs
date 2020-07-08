

using System;
using System.Collections.Generic;


namespace PROPOSTA
{
    public partial class Negociacao
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public Negociacao(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

        public class NegociacaoModel
        {
            public Int32 Numero_Negociacao { get; set; }
            public String Cod_Tipo_Midia { get; set; }
            public String Nome_Tipo_Midia { get; set; }
            public Double Comissao_Agencia { get; set; }
            public String Comissao_Agencia_String { get; set; }
            public String Competencia_Inicial { get; set; }
            public String Competencia_Final { get; set; }
            public String Data_Desativacao { get; set; }
            public Double Desconto_Concedido { get; set; }
            public String Desconto_Concedido_String { get; set; }
            public Double Desconto_Real { get; set; }
            public Byte Cod_Forma_Pgto { get; set; }
            public String Forma_Pgto { get; set; }
            public String Tabela_Preco { get; set; }
            public Byte Sequencia_Tabela { get; set; }
            public Double Verba_Negociada { get; set; }
            public String Verba_Negociada_String{ get; set; }
            public DateTime Data_Cadastramento { get; set; }
            public Double Valor_Tabela { get; set; }
            public Double Valor_Negociado { get; set; }
            public String Patrocinio_Evento { get; set; }
            public String Nome_Evento { get; set; }
            public String Percentual_Reaplicacao { get; set; }
            public String Valor_Reaplicacao { get; set; }
            public String Tabela_Reaplicacao { get; set; }
            public Byte Sequencia_Tabela_Reaplicacao { get; set; }
            public String Desconto_Reaplicacao { get; set; } 
            public List<NegociacaoDescontoModel> Descontos { get; set; }
            public List<NegociacaoEmpresaVendaModel> Empresas_Venda { get; set; }
            public List<NegociacaoEmpresaFaturamentoModel> Empresas_Faturamento { get; set; }
            public List<NegociacaoAgenciaModel> Agencias { get; set; }
            public List<NegociacaoClienteModel> Clientes { get; set; }
            public List<NegociacaoContatoModel> Contatos { get; set; }
            public List<NegociacaoNucleoModel> Nucleos { get; set; }
            public List<NegociacaoIntermediarioModel> Intermediarios{ get; set; }
            public List<NegociacaoApresentadorModel> Apresentadores{ get; set; }

        }
        public class NegociacaoEmpresaVendaModel
        {
            public String Cod_Empresa { get; set; }
            public String Nome_Empresa { get; set; }
        }
        public class NegociacaoEmpresaFaturamentoModel
        {
            public String Cod_Empresa { get; set; }
            public String Nome_Empresa { get; set; }
        }
        public class NegociacaoAgenciaModel
        {
            public String Cod_Agencia { get; set; }
            public String Nome_Agencia { get; set; }
        }
        public class NegociacaoClienteModel
        {
            public String Cod_Cliente{ get; set; }
            public String Nome_Cliente{ get; set; }
        }
        public class NegociacaoContatoModel
        {
            public String Cod_Contato{ get; set; }
            public String Nome_Contato{ get; set; }
            public Double Comissao { get; set; }
        }
        public class NegociacaoNucleoModel
        {
            public String Cod_Nucleo{ get; set; }
            public String Nome_Nucleo{ get; set; }
        }
        public class NegociacaoIntermediarioModel
        {
            public String Cod_Intermediario { get; set; }
            public String Nome_Intermediario { get; set; }
            public String Comissao { get; set; }
            //public String Tipo_Intermediario { get; set; }
            //public String Nome_Tipo_Intermediario { get; set; }
            public Tipo_IntermediarioModel Tipo_Intermediario;
            public Int32 Sequencia { get; set; }
            //public String Tipo_Comissao { get; set; }
            //public String Nome_Tipo_Comissao { get; set; }
            public Tipo_ComissaoModel Tipo_Comissao { get; set; }
        }
        public class NegociacaoDescontoModel
        {
            public Int32 Id_Desconto { get; set; }
            public Double Desconto { get; set; }
            public List<NegociacaoItemDescontoModel> Items { get; set; }
        }
        public class NegociacaoItemDescontoModel
        {
            public Int32 Id_Desconto { get; set; }
            public String Cod_Tipo_Desconto{ get; set; }
            public String Nome_Tipo_Desconto { get; set; }
            public String Cod_Chave{ get; set; }
            public String Nome_Chave { get; set; }
            public String Data_Inicio { get; set; }
            public String Data_Termino { get; set; }

        }

        public class NegociacaoFiltroParam
        {
            public Int32 Numero_Negociacao { get; set; }
            public String Cod_Empresa_Venda { get; set; }
            public String Cod_Empresa_Faturamento { get; set; }
            public String Cod_Tipo_Midia { get; set; }
            public String Competencia_Inicio { get; set; }
            public String Competencia_Fim { get; set; }
            public String Agencia { get; set; }
            public String Cliente { get; set; }
            public String Contato { get; set; }
        }
        public class NegociacaoCountModel
        {
            public Int32 Qtd_Proposta{ get; set; }
            public Int32 Qtd_Negociacao{ get; set; }
        }
        public  class Tipo_IntermediarioModel
        {
            public String Tipo_Intermediario { get; set; }
            public String Nome_Tipo_Intermediario { get; set; }
        }
        public class Tipo_ComissaoModel
        {
            public String Tipo_Comissao { get; set; }
            public String Nome_Tipo_Comissao { get; set; }
        }
        public class NegociacaoApresentadorModel
        {
            public String Cod_Apresentador { get; set; }
            public String Nome_Apresentador{ get; set; }
        }
    }
}