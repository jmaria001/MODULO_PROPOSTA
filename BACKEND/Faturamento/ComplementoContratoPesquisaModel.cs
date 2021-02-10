using System;
using System.Collections.Generic;

namespace PROPOSTA
{
    public partial class ComplementoContratoPesquisa
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public ComplementoContratoPesquisa(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }


        public class ComplementoModel
        {
            public String Cod_Empresa { get; set; }
            public Int32 Numero_Complemento { get; set; }
            public Int32 Numero_Negociacao { get; set; }
            public String Cod_Historico { get; set; }
            public String Cod_Natureza { get; set; }
            public String Natureza_Servico { get; set; }
            public String Cod_Contato { get; set; }
            public Double Valor { get; set; }
            public Int32 Qtde_Rateio { get; set; }
            public DateTime Periodo_Inicial { get; set; }
            public DateTime Periodo_Final { get; set; }
            public byte Origem { get; set; }
            public DateTime Data_Cadastramento { get; set; }
            public DateTime Data_Cancelamento { get; set; }
            public String Descricao { get; set; }
            public String Cod_Nucleo { get; set; }
            public String Cod_Usuario { get; set; }
            public Double Comissao_Agencia { get; set; }
            public Double Comissao_Intermediario { get; set; }
            public String Intermediario { get; set; }
            public String Nome_Intermediario { get; set; }
            public Byte Forma_Pgto { get; set; }
            public String Descricao_Forma_Pgto { get; set; }
            public String Cod_Empresa_Faturamento { get; set; }
            public Boolean Indica_Faturamento_Liquido { get; set; }
            public Boolean Indica_Venda_Net { get; set; }
            public String Cod_Usuario_Cancelar { get; set; }
            public List<ComposicaocomplementoModel> ComposicaoComplemento { get; set; }
            public List<RateioModel> Rateios { get; set; }
        }

        public class ComposicaocomplementoModel
        {
            public Int32 Numero_Mr { get; set; }
            public String Cod_Empresa { get; set; }
            public Int32 Sequencia_Mr { get; set; }
            public Int32 Numero_Complemento { get; set; }
            public String Numero_Pi { get; set; }
            public DateTime Data_Autorizacao { get; set; }
            public DateTime Data_Cadastramento { get; set; }
            public DateTime Data_Cancelamento { get; set; }
            public Boolean Indica_Mestre_Grupo { get; set; }
            public Double Vlr_Considerado_Contrato { get; set; }
        }
        public class RateioModel
        {
            public Int32 Numero_Rateio { get; set; }
            public Int32 Condicao_Nf { get; set; }
            public String Cod_Empresa { get; set; }
            public Int32 Numero_Complemento { get; set; }
            public Int32 Numero_Negociacao { get; set; }
            public DateTime Data_Emissao { get; set; }
            public Int32 Numero_Fatura { get; set; }
            public Byte Origem { get; set; }
            public String Referencia { get; set; }
            public String Tipo_Vencimento { get; set; }
            public DateTime Vencimento_Nf { get; set; }
            public Double Vlr_Nf { get; set; }
            public Byte Indica_Log_Cliente { get; set; }
            public Byte Indica_Log_Agencia { get; set; }
            public String Cod_Veiculo { get; set; }
            public String Nome_Veiculo { get; set; }
            public String Cod_Cliente { get; set; }
            public String Nome_Cliente { get; set; }
            public Double Percentual_Rateio { get; set; }
            public String Cod_Agencia { get; set; }
            public String Nome_Agencia{ get; set; }
            public DateTime Data_Cadastramento { get; set; }
            public String Cod_Usuario { get; set; }
            public String Cod_Cobranca { get; set; }
            public Byte Cod_Endereco_Cobranca { get; set; }
            public List<Rateio_AuxiliarModel> Duplicatas { get; set; }
        }

        public class FiltroModel
        {
            public Int32 Negociacao { get; set; }
            public Int32 Fatura { get; set; }
            public String Cod_Empresa_Faturamento { get; set; }
            public Int32 Contrato { get; set; }
            public int Sequencia { get; set; }
            public String Agencia { get; set; }
            public String Cliente { get; set; }
            public  Boolean Indica_Somente_Pendente { get; set; }
        }
        public class Rateio_AuxiliarModel
        {
            public Int32 Numero_Complemento { get; set; }
            public Int32 Numero_Rateio { get; set; }
            public Int32 Numero_Duplicata { get; set; }
            public DateTime Data_Vencimento { get; set; }
            public String Dia_Semana{ get; set; }
            public Double Valor { get; set; }

}

    }
}