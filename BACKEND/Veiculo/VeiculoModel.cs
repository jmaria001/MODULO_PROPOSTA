using System;
using System.Collections.Generic;

namespace PROPOSTA
{
    public partial class Veiculo
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public Veiculo(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

        public class VeiculoModel
        {

            public String Id_Operacao { get; set; }
            public String Cod_Veiculo { get; set; }
            public String Nome_Veiculo { get; set; }
            public String Sigla_Veiculo { get; set; }
            public String Cod_Empresa { get; set; }
            public String NomeEmpresaPertence { get; set; }
            public String Sigla_JOVE { get; set; }
            public String Cidade { get; set; }
            public String Hora_Inicio_Programacao { get; set; }
            public String Descritivo { get; set; }
            public String Data_Inicio { get; set; }
            public String Data_Final { get; set; }
            public Boolean Indica_Parceiro { get; set; }
            public Int32 Id_Potencia { get; set; }
            public String ds_Classe { get; set; }
            public Boolean Indica_Filiada { get; set; }
            public Boolean Indica_Afiliada { get; set; }
            public Boolean NetSim { get; set; }
            public Boolean NetNao { get; set; }
            public String Cod_Veiculo_Net { get; set; }
            public String Nome_Veiculo_Net { get; set; }
            public String Cod_Tabela_Net { get; set; }
            public String Email_Diretoria { get; set; }
            public String Email_Faturamento { get; set; }
            public String Email_Opec { get; set; }
            public String Percentual_Contratual { get; set; }
            public String Cod_Terceiro { get; set; }
            public String Nome_Terceiro { get; set; }
            public Boolean Indica_RoteiroSim { get; set; }
            public Boolean Indica_RoteiroNao { get; set; }
            public Boolean Ocultar_MMA { get; set; }
            public Boolean Indica_Nao_Emite_Ce { get; set; }
            public Boolean Indica_Midia_Online { get; set; }
            public Int32 RedeId { get; set; }
            public String NomeRede { get; set; }

        }


    }
}