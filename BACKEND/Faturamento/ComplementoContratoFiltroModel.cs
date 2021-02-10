using System;
using System.Collections.Generic;

namespace PROPOSTA
{
    public partial class ComplementoContratoFiltro
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public ComplementoContratoFiltro(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }


        public class PesquisaFaturamentoModel
        {
            public String Id_Operacao { get; set; }
            public String Cod_Empresa_Faturamento { get; set; }
            public Int32 Numero_Negociacao { get; set; }
            public String Cod_Empresa { get; set; }
            public Int32 Numero_Mr { get; set; }
            public int Sequencia_Mr { get; set; }
            public Int32 ID_Contrato { get; set; }
            public String Cod_Cliente { get; set; }
            public String Cod_Agencia { get; set; }
            public String Cod_Nucleo { get; set; }
            public String Cod_Contato { get; set; }
            public String Periodo_Inicial { get; set; }
            public String Periodo_Final { get; set; }
            public String Competencia { get; set; }
            public Decimal Vlr_Calculado { get; set; }
            public Decimal Vlr_Faturado { get; set; }
            public Decimal Vlr_Autorizado { get; set; }
            public Decimal Vlr_Exibido { get; set; }
            public Decimal Vlr_A_Faturar { get; set; }
            public Decimal Vlr_Credito_Divergencia { get; set; }
            public Decimal Vlr_Am_Emitido { get; set; }
            public Decimal Vlr_Financeiro { get; set; }
            public String Produto { get; set; }
            public String Indica_Mestre_Grupo { get; set; }
            public Int32 Numero_Complemento { get; set; }
            public int Indica_Complementar { get; set; }
            public int Indica_Autorizado { get; set; }
            public int Indica_Rejeitado { get; set; }
            public String Nome_Agencia { get; set; }
            public String Nome_Cliente { get; set; }
            public String Data_Liberacao { get; set; }
            public int Indica_Web { get; set; }
        }

        public class FiltroModel
        {
            public Byte Origem{ get; set; }
            public Int32 Numero_Negociacao { get; set; }
            public String Empresa { get; set; }
            public Int32 Contrato { get; set; }
            public int Sequencia { get; set; }
            public String Agencia { get; set; }
            public String Cliente { get; set; }
            public String Nucleo { get; set; }
            public String Contato { get; set; }
            public String Competencia { get; set; }
            public Int32 Complemento { get; set; }
            public int Ind_Comprovado { get; set; }
            public int Retorno { get; set; }
            public String Emp_Faturamento { get; set; }
        }

    }
}