using System;
using System.Collections.Generic;

namespace PROPOSTA
{
    public partial class RegraAprovacao
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public RegraAprovacao(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

        public class Regra_Aprovacao_Model
        {
            public Int32 Id_Regra { get; set; }
            public String Nome_Regra { get; set; }
            public String Descricao_Regra { get; set; }
            public Boolean SemEmpresa { get; set; }
            public Boolean SemVeiculo { get; set; }
            public Boolean SemCliente { get; set; }
            public Boolean SemAgencia { get; set; }
            public List<Regra_Aprovacao_Empresa_Model> Empresas{ get; set; }
            public List<Regra_Aprovacao_Veiculo_Model> Veiculos{ get; set; }
            public List<Regra_Aprovacao_Agencia_Model> Agencias { get; set; }
            public List<Regra_Aprovacao_Cliente_Model> Clientes { get; set; }
            public List<Range_Model> Range { get; set; }
            public Int32 Max_Id_Range { get; set; }

        }

        public class Range_Model
        {
            public Int32 Id_Range { get; set; }
            public Int32 Id_Regra { get; set; }
            public String Desconto_De { get; set; }
            public String Desconto_Ate { get; set; }
            public String Range_Text{ get; set; }
            public Int32? QtdAprovadores { get; set; }
            public List<Aprovador_Model> Aprovadores { get; set; }
        }

        public class Aprovador_Model
        {
            public Int32 Id_Regra_Aprovacao_Usuario { get; set; }
            public Int32 Id_Range{ get; set; }
            public Int32 Id_Usuario { get; set; }
            public String Nome_Usuario { get; set; }
            public Boolean Indica_Obrigatorio{ get; set; }
        }

        public class Regra_Aprovacao_Empresa_Model
        {
            public Int32 Id_Regra_Empresa { get; set; }
            public Int32 Id_Regra { get; set; }
            public String Cod_Empresa { get; set; }
            public String Nome_Empresa { get; set; }
        }
        public class Regra_Aprovacao_Veiculo_Model
        {
            public Int32 Id_Regra_Veiculo { get; set; }
            public Int32 Id_Regra { get; set; }
            public String Cod_Veiculo { get; set; }
            public String Nome_Veiculo { get; set; }
        }
        public class Regra_Aprovacao_Agencia_Model
        {
            public Int32 Id_Regra_Agencia { get; set; }
            public Int32 Id_Regra { get; set; }
            public String Cod_Agencia { get; set; }
            public String Nome_Agencia { get; set; }
        }
        public class Regra_Aprovacao_Cliente_Model
        {
            public Int32 Id_Regra_Cliente { get; set; }
            public Int32 Id_Regra { get; set; }
            public String Cod_Cliente { get; set; }
            public String Nome_Cliente { get; set; }
        }

    }
 

}