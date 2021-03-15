using System;
using System.Collections.Generic;


namespace PROPOSTA
{
    public partial class NumeracaoFitas
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public NumeracaoFitas(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

        public class NumeracaoFitasModel
        {
            public String Cod_Tipo_Comercial { get; set; }
            public String Titulo_Comercial { get; set; }
            public Int32 Cod_Red_Produto { get; set; }
            public String Descricao_Produto { get; set; }
            public Int32 Duracao { get; set; }
            public String Cod_Cliente { get; set; }
            public String Cod_Agencia { get; set; }
            public Boolean Indica_Status { get; set; }
            public String Cod_Empresa { get; set; }
            public Int32 Numero_Mr { get; set; }
            public Int32 Sequencia_Mr { get; set; }
            public String Numero_Fita { get; set; }
            public String Data_Inicio { get; set; }
            public String Data_Final { get; set; }
            public String Nome_Apresentador { get; set; }
            public String Localizacao { get; set; }
            public String Arquivo { get; set; }
            public String Cod_Comercial { get; set; }
            public String Cod_Veiculo { get; set; }
            public String Tipo_Midia { get; set; }
            public String Cod_Tipo_Midia { get; set; }

        }


        public class FiltroModel
        {
            public String Cod_Veiculo { get; set; }
            public String Cod_Programa { get; set; }
            public String Data_Inicio { get; set; }
            public String Data_Final { get; set; }
            public String Numero_Fita_Inicio { get; set; }
            public String Numero_Fita_Fim { get; set; }
            public Boolean Indica_Pendentes_Numeracao { get; set; }
            public Boolean Indica_Numeradas { get; set; }
            public Boolean Indica_Desativadas_Devolvidas { get; set; }
            public Boolean Indica_Ativas { get; set; }

        }


        public class FiltroExibirVeiculoModel
        {
            public Int32 Id_Numeracao { get; set; }
            public String Cod_Empresa { get; set; }
            public Int32  Numero_Mr { get; set; }
            public Int32  Sequencia_Mr { get; set; }
            public String Cod_Veiculo { get; set; }
            public String Cod_Comercial { get; set; }
            public String Cod_Tipo_Comercial { get; set; }
            public String Cod_Tipo_Midia { get; set; }
            public String Tipo_Fita { get; set; }
            public String Numero_Fita { get; set; }
            public String Cod_Apresentador { get; set; }
            public String Nome_Apresentador { get; set; }
            public String Localizacao { get; set; }
            public Int32 Duracao { get; set; }
            public Boolean Status { get; set; }
            public String Mensagem { get; set; }
            public Boolean Indica_Reutilizar { get; set; }
            public Boolean Reutilizar { get; set; }

        }

    }
}