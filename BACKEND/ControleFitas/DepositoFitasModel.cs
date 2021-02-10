using System;
using System.Collections.Generic;

namespace PROPOSTA
{
    public partial class DepositoFitas
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public DepositoFitas(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

        public class DepositoFitasModel
        {
            public String  Id_operacao { get; set; }
            public String  Tipo_Fita { get; set; }
            public String  Data_Inicio { get; set; }
            public String  Data_Final { get; set; }
            public Int32   Quantidade { get; set; }
            public Int32   Duracao { get; set; }
            public String  Titulo_Comercial { get; set; }
            public Boolean Indica_Chamada { get; set; }
            public String  Cod_Tipo_Comercial { get; set; }
            public String  Descricao_Comercial { get; set; }
            public String  Cod_Veiculo { get; set; }
            public String  Nome_Veiculo { get; set; }
            public String  Cod_Programa { get; set; }
            public String  Titulo_Programa { get; set; }
            public String  Cod_Programa_Antes { get; set; }
            public String  Titulo_Programa_Antes { get; set; }
            public String  Cod_Programa_Apos { get; set; }
            public String  Titulo_Programa_Apos { get; set; }
            public Int32   Cod_Red_Produto { get; set; }
            public String  Descricao_Produto { get; set; }
            public String  Cod_Apresentador { get; set; }
            public String  Nome_Apresentador { get; set; }
            public String  Arquivo_Midia { get; set; }
            public String  Numero_Fita { get; set; }
            public Boolean Indica_DiaSeg { get; set; }
            public Boolean Indica_DiaTer { get; set; }
            public Boolean Indica_DiaQua { get; set; }
            public Boolean Indica_DiaQui { get; set; }
            public Boolean Indica_DiaSex { get; set; }
            public Boolean Indica_DiaSab { get; set; }
            public Boolean Indica_DiaDom { get; set; }
            public Boolean MarcarDesmarcar { get; set; }
            public String Cod_Cliente { get; set; }
            public String Cod_Agencia { get; set; }
            public String Cod_Empresa { get; set; }
            public Int32 Numero_Mr { get; set; }
            public Int32 Sequencia_Mr { get; set; }
            public String Cod_Comercial { get; set; }
            public String Origem { get; set; }
            public Int32 Situacao { get; set; }
            public List<Veiculos_Model> Veiculos { get; set; }
            public String Tipo_Midia { get; set; }
        }

        public class FiltroModel
        {
            public String Cod_Veiculo { get; set; }
            public Int32  Situacao { get; set; }
            public String Numero_Fita_Inicio { get; set; }
            public String Numero_Fita_Fim { get; set; }
            public String Data_Inicio { get; set; }
            public String Data_Final { get; set; }
        }



        public class Veiculos_Model
        {
            public String Cod_Veiculo { get; set; }
            public String Nome_Veiculo { get; set; }
        }

    }
}