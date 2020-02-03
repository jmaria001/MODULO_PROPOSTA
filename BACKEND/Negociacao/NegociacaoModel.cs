

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
            public Double Comissao_Agencia { get; set; }
            public String Competencia_Inicial { get; set; }
            public String Competencia_Final { get; set; }
            public DateTime Data_Desativacao { get; set; }
            public Double Desconto_Concedido { get; set; }
            public Double Desconto_Real { get; set; }
            public String Forma_Pgto { get; set; }
            public String Tabela_Preco { get; set; }
            public Byte Sequencia_Tabela { get; set; }
            public Double Verba_Negociada { get; set; }
            public DateTime Data_Cadastramento { get; set; }
            public Double Valor_Tabela { get; set; }
            public Double Valor_Negociado { get; set; }
            public List<NegociacaoEmpresaVendaModel> Empresas_Venda { get; set; }
            public List<NegociacaoEmpresaFaturamentoModel> Empresas_Faturamento { get; set; }
            public List<NegociacaoAgenciaModel> Agencias { get; set; }
            public List<NegociacaoClienteModel> Clientes { get; set; }
            public List<NegociacaoContatoModel> Contatos { get; set; }

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
    }
}