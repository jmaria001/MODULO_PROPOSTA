using System;
using System.Collections.Generic;

namespace PROPOSTA
{
    public partial class PropagacaoMapa
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public PropagacaoMapa(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }


        public class PropagacaoMapaModel
        {
            public String  Competencia     { get; set; }
            public String Mensagem_Status { get; set; }
            public Int32  Indica_Erro     { get; set; }
            public String Cod_Empresa     { get; set; }
            public Int32  Numero_Mr       { get; set; }
            public Int32  Sequencia_Mr    { get; set; }

        }

        public class FiltroModel
        {

            public String Cod_Empresa         { get; set; }
            public Int32  Numero_Mr           { get; set; }
            public int    Sequencia_Mr        { get; set; }
            public String Competencia         { get; set; }
            public String Competencia_Inicial { get; set; }
            public String Competencia_Final   { get; set; }
           

        }


    }
}