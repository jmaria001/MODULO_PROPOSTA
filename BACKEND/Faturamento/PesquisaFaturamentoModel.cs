using System;
using System.Collections.Generic;

namespace PROPOSTA
{
    public partial class PesquisaFaturamento
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public PesquisaFaturamento(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }


        public class FaturaModel
        {
            public String Cod_Empresa_Faturamento { get; set; }
            public String Cod_Empresa{ get; set; }
            public Int32 Numero_Fatura { get; set; }
            public Int32 Numero_Erp { get; set; }
            public Int32 Numero_Negociacao { get; set; }
            public Int32 Numero_Rateio { get; set; }
            public String Cod_Nucleo { get; set; }
            public String Nome_Nucleo     { get; set; }
            public String Cod_Contato { get; set; }
            public String Nome_Contato  { get; set; }
            public String Cod_Agencia { get; set; }
            public String Nome_Agencia { get; set; }
            public String Cod_Cliente { get; set; }
            public String Nome_Cliente { get; set; }
            public Double Valor_Bruto    { get; set; }
            public Double Valor_Comissao { get; set; }
            public Double Valor_Liquido { get; set; }
            public Double Valor_Iss { get; set; }
            public Double Valor_Intermediario { get; set; }
            public String Cod_Natureza { get; set; }
            public String Nome_Natureza { get; set; }
            public Double Percentual_Rateio { get; set; }
            public String Tipo_Vencimento { get; set; }
            public String Condicao_Nf { get; set; }
            public String Referencia { get; set; }
            public String Cod_Intermediario { get; set; }
            public String Nome_Intermediario { get; set; }
            public DateTime Periodo_Inicial { get; set; }
            public DateTime Periodo_Final { get; set; }
            public DateTime Data_Emissao { get; set; }
            public DateTime Data_Cancelamento { get; set; }
            public String Cod_Cancelamento { get; set; }
            public String Obs_Cancelamento { get; set; }
            public String Motivo_Cancelamento { get; set; }
            public Int32 Origem { get; set; }
            public List<DuplicataModel> Duplicatas { get; set; }
            public List<ComposicaocomplementoModel> Composicao { get; set; }
        }

        public class FiltroModel
        {
            public String Cod_Emp_Faturamento { get; set; }
            public Int32 Numero_Negociacao { get; set; }
            public String Competencia { get; set; }
            public Int32 Nota_Fiscal { get; set; }
            public Int32 Numero_Erp{ get; set; }
            public String Cod_Empresa { get; set; }
            public Int32 Contrato { get; set; }
            public int Sequencia { get; set; }
            public String Cod_Agencia { get; set; }
            public String Cod_Cliente { get; set; }
            public int Origem { get; set; }
        }
        public class DuplicataModel
        {
            public Int32 Parcela { get; set; }
            public DateTime Vencimento { get; set; }
            public Double Valor { get; set; }
            public String Dia_Semana { get; set; }

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
    }
}