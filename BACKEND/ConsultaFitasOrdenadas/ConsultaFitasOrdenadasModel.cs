using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROPOSTA
{
    public partial class ConsultaFitasOrdenadas
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public ConsultaFitasOrdenadas(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }



        public class ConsultaFitasOrdenadasModel
        {

            public String Numero_Fita       { get; set; }
            public String Cod_Veiculo       { get; set; }
            public String Data_Exibicao     { get; set; }
            public String Cod_Programa      { get; set; }
            public String Cod_Comercial     { get; set; }
            public String Titulo_Comercial  { get; set; }
            public String Duracao           { get; set; }
            public String Breaks            { get; set; }
            public String Posicao           { get; set; }
            public String Tipo              { get; set; }
            public String Cod_Empresa       { get; set; }
            public Int32  Numero_Mr         { get; set; }
            public int    Sequencia_Mr      { get; set; }
            public String Cod_Agencia       { get; set; }
            public String Cod_Cliente       { get; set; }


        }

        public class FiltroModel
        {
            public String Cod_Veiculo        { get; set; }
            public String Cod_Programa       { get; set; }
            public String Data_Inicio        { get; set; }
            public String Data_Fim           { get; set; }
            public String Numero_Fita_Inicio { get; set; }
            public String Numero_Fita_Fim    { get; set; }
            public String Empresa            { get; set; }
            public String Nome_Empresa       { get; set; }
            public Int32  Numero_Mr          { get; set; }
            public int    Sequencia_Mr       { get; set; }

        }


    }
}