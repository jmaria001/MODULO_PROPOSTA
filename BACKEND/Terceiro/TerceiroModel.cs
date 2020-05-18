using System;
using System.Collections.Generic;

namespace PROPOSTA
{
    public partial class Terceiro
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public Terceiro(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

        public class TerceiroModel
        {
            public String Cod_Terceiro { get; set; }
            public String Razao_Social { get; set; }
            public String CGC { get; set; }
            public Int32 Indica_Cic_Cgc { get; set; }
            public String Inscricao_Estadual { get; set; }
            public String Inscricao_Municipal { get; set; }
            public String Nome_Fantasia { get; set; }
            public int Funcao { get; set; }
            public int Cod_Funcao_Terceiro { get; set; }
            public String Descricao_Funcao_Terceiro { get; set; }
            public String Cod_Categoria { get; set; }
            public String Descricao_Categoria { get; set; }
            public String Cod_Empresa_Principal { get; set; }
            public String Nome_Empresa_Principal { get; set; }
            public String Descricao_Status { get; set; }
            public String Tipo_Cobranca { get; set; }
            public String Descricao_Tipo_Cobranca { get; set; }
            public String Limite_Credito { get; set; }
            public String Forma_Tributacao { get; set; }
            public String Des_Forma_Tributacao { get; set; }
            public Boolean Permite_Editar{ get; set; }
            public Int32 Indica_Porte { get; set; }
            public String Descricao_Porte { get; set; }
            public Boolean Indica_Estrangeiro { get; set; }
            public Boolean Indica_Direto { get; set; }
            public Boolean Indica_Afiliada { get; set; }
            public Boolean Indica_Regional { get; set; }
            public Boolean Indica_Tv{ get; set; }
            public List<TerceiroEnderecoModel> Enderecos{ get; set; }
            public List<TerceiroComplementarModel> Complementar { get; set; }
            public List<TerceiroEmpresasModel> Empresas { get; set; }
            public string id_operacao { get; set; }
        }
        
        public class TerceiroFiltroModel
        {
            public String Codigo { get; set; }
            public String RazaoSocial{ get; set; }
            public String CNPJ { get; set; }
        }
        public class TerceiroEmpresasModel
        {
            public String Cod_Empresa { get; set; }
            public String Nome_Empresa { get; set; }
            public Boolean Selected { get; set; }
        }
        public class TerceiroEnderecoModel
        {
            public String Cod_Terceiro { get; set; }
            public String Cod_Empresa { get; set; }
            public String Nome_Empresa { get; set; }
            public String Endereco1 { get; set; }
            public String Numero1 { get; set; }
            public String Complemento1 { get; set; }
            public String Bairro1 { get; set; }
            public String Municipio1 { get; set; }
            public String Uf1 { get; set; }
            public String Cep1 { get; set; }
            public String Endereco2 { get; set; }
            public String Numero2 { get; set; }
            public String Complemento2 { get; set; }
            public String Bairro2 { get; set; }
            public String Municipio2 { get; set; }
            public String Uf2 { get; set; }
            public String Cep2 { get; set; }
            public String Telefone { get; set; }
            public String Fax { get; set; }
            public String Email { get; set; }
            public String Praca_Pgto { get; set; }
            public String Cod_Municipio { get; set; }
            public String Cod_Municipio1 { get; set; }
            public Boolean Permite_Edicao{ get; set; }
            public Boolean Base_Edicao{ get; set; }
        }

        public class TerceiroComplementarModel
        {
            public String Cod_Terceiro { get; set; }
            public String Cod_Empresa { get; set; }
            public String Nome_Empresa { get; set; }
            public String Nome_Contato { get; set; }
            public String Email_Contato { get; set; }
            public String Telefone_Contato_1 { get; set; }
            public String Telefone_Contato_2 { get; set; }
            public String Nome_Contato_Compl { get; set; }
            public String Email_Contato_Compl { get; set; }
            public String Telefone_Contato_Compl_1 { get; set; }
            public String Telefone_Contato_Compl_2 { get; set; }
            public Int32 Conta_Contabil { get; set; }
            public String Sub_Conta { get; set; }
            public DateTime Data_Inclusao { get; set; }
            public DateTime Data_Alteracao { get; set; }
            public DateTime Data_Desativacao { get; set; }
            public Byte Indica_Desativado { get; set; }
            public String Status { get; set; }
            public String Motivo_Desativacao { get; set; }
            public Byte Indica_Integracao { get; set; }
            public Byte Indica_IntegracaoIq { get; set; }
            public Byte Indica_Merchandising { get; set; }
            public Int32 Cod_Grupo_Cliente { get; set; }
            public Int32 Cod_Representante { get; set; }
            public String Cod_Banco { get; set; }
            public Byte Indica_Integracao_Executada { get; set; }
            public Byte Indica_Integracao_Executada_Contato { get; set; }
            public Boolean Indica_IN480 { get; set; }
            public String Bco_Agencia { get; set; }
            public String Bco_Agencia_DV { get; set; }
            public String Bco_Conta_Corrente { get; set; }
            public String Bco_Conta_Corrente_DV { get; set; }
            public Int32 Conta_Contabil_Passivo { get; set; }
            public Int32 Conta_Contabil_Adiantamento { get; set; }
            public Byte Indica_ERP_Integra { get; set; }
            public Byte Indica_Foi_Integrado { get; set; }
            public Double Comissao_Padrao { get; set; }
            public Boolean Permite_Edicao { get; set; }
            public Boolean Base_Edicao { get; set; }

        }
    }
}