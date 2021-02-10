using System;
using System.Collections.Generic;

namespace PROPOSTA
{
    public partial class ConsultaProgramacaoDiaria
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public ConsultaProgramacaoDiaria(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }


        public class FiltroModel
        {
            public String Veiculo { get; set; }
            public String Data_Inicial { get; set; }
            public String Data_Final { get; set; }
            public String Programa { get; set; }
            public Boolean Indica_Progs_Saldo_Zero { get; set; }
            public Boolean Indica_Progs_Saldo_Estou { get; set; }
            public Boolean Indica_Progs_Saldo_Posit { get; set; }
            public Boolean Par_Indica_Sem_Disponibilidade { get; set; }
            public Boolean Par_Indica_Invasao_Espaco { get; set; }
            public Boolean Indica_Progs_Desativados { get; set; }
            public Boolean Par_Programa_chkVendaSP { get; set; }
            public Boolean Indica_Local { get; set; }
            public Boolean Indica_Net { get; set; }
            public Boolean Par_Tipo_Retorno { get; set; }
        }

        public class FiltroDetalheModel
        {
            public String Cod_Veiculo { get; set; }
            public String Data_Exibicao { get; set; }
            public String Cod_Programa { get; set; }
            public Byte Indica_Grade { get; set; }
        }


        public class ConsultaProgramacaoDiariaDetalheModel
        {
            public Boolean Indica_Grade { get; set; }
            public String Cod_Veiculo { get; set; }
            public String Cod_Empresa { get; set; }
            public String Chave_Acesso { get; set; }
            public String Cod_Nucleo { get; set; }
            public String Cod_Cliente { get; set; }
            public String Cod_Agencia { get; set; }
            public String Forma_Pgto { get; set; }
            public String Merchandising { get; set; }
            public String Cod_Programa { get; set; }
            public String Cod_Tipo_Midia { get; set; }
            public String Cod_Caracteristica { get; set; }
            public String Cod_Qualidade { get; set; }
            public String Duracao { get; set; }
            public String Documento_DE { get; set; }
            public String Descricao { get; set; }
            public String Numero_MR { get; set; }
            public String Sequencia_MR { get; set; }
            public Boolean Indica_Absorcao { get; set; }
            public Boolean Indica_Estouro { get; set; }
            public String Valor { get; set; }
            public String Valor_Tabela { get; set; }
            public String Data_Cadastramento { get; set; }
            public Boolean Indica_Falha { get; set; }
            public String Id_Parametro { get; set; }
            public String Descricao_Parametro { get; set; }
            public String Prioridade { get; set; }
            public String Nome_Cliente { get; set; }
            public String Nome_Agencia { get; set; }
            public String Invasao_Local { get; set; }
            public String Ind_diferenciado { get; set; }
        }

        
    }
}


