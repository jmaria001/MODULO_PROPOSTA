using System;
using System.Collections.Generic;

namespace PROPOSTA
{
    public partial class ComplementoContratoDados
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public ComplementoContratoDados(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

        public class ComplementoContratoModel
        {
            public byte Origem { get; set; }
            public String Cod_Empresa_Faturamento { get; set; }
            public Int32 Numero_Negociacao { get; set; }
            public String Cod_Empresa { get; set; } 
            public Int32 Numero_Mr { get; set; }
            public int Sequencia_Mr { get; set; }
            public int Id_Contrato{ get; set; }
            public String Cod_Nucleo { get; set; }
            public String Cod_Contato { get; set; }
            public String Periodo_Inicial { get; set; }
            public String Periodo_Final { get; set; }
            public String Cod_Intermediario { get; set; }
            public Double Vlr_A_Faturar { get; set; }
            public Double Saldo_A_Faturar { get; set; }
            public String Natureza_Servico { get; set; }
            public int Cod_Historico { get; set; }
            public String Descricao { get; set; }
            public Boolean Indica_Venda_Net { get; set; }
            public Boolean Indica_Faturamento_liquido { get; set; }
            public int Forma_Pgto { get; set; }
            public int Numero_Parcela { get; set; }
            public String Nome_Forma_Pgto { get; set; }
            public List<ComplementoMapasModel> ComplementoMapas { get; set; }
            public List<RateioModel> Rateios { get; set; }
        }

        public class RateioModel
        {
            public int Id_Rateio { get; set; }
            public int Numero_Rateio { get; set; }
            public String Cod_Cliente { get; set; }
            public String Cod_Agencia { get; set; }
            public String Vlr_A_Faturar { get; set; }
            public String Data_Emissao { get; set; }
            public String Cod_Condicao { get; set; }
            public String Cod_Veiculo { get; set; }
            public int Indica_Log_Agencia { get; set; }
            public int Indica_Log_Cliente { get; set; }
            public String Referencia { get; set; }
            public String Perc_Rateio { get; set; }
            public List<DuplicataModel> Duplicatas { get; set; }

        }
        public class FiltroModel
        {
            public Int32 Negociacao { get; set; }
            public String Empresa { get; set; }
            public Int32 Contrato { get; set; }
            public int Sequencia { get; set; }
            public String Agencia { get; set; }
            public String Cliente { get; set; }
            public String Nucleo { get; set; }
            public String Contato { get; set; }
            public Int32 Competencia { get; set; }
            public Int32 Complemento { get; set; }
            public int Ind_Comprovado { get; set; }
            public int Retorno { get; set; }
            public String Emp_Faturamento { get; set; }
            public String Tipo_Numeracao { get; set; }

        }
        public class ComplementoMapasModel
        {
            public String Cod_Empresa { get; set; } 
            public Int32 Numero_Mr { get; set; } 
            public Int32 Sequencia_Mr { get; set; }
            public Int32 Id_Contrato{ get; set; }
            public String ContratoString  { get; set; }
            public Double Vlr_A_Faturar { get; set; }
        }
        public class RegraNaturezaModel
        {
        public String Cod_Empresa_Faturamento { get; set; }
        public String Cod_Empresa { get; set; }
        public Int32 Numero_Mr { get; set; }
        public Int32 Sequencia_Mr { get; set; }
        public String Tipo { get; set; }
        public Int32 Numero_Negociacao { get; set; }
        }
        public class DuplicataModel
        {
            public Int32 Id_Rateio { get; set; }
            public Int32 Id_Parcela{ get; set; }
            public Int32 Parcela { get; set; }
            public String Vencimento{ get; set; }
            public String Valor { get; set; }
            public String Dia_Semana{ get; set; }

        }
    }
}