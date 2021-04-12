using System;
using System.Collections.Generic;

namespace PROPOSTA
{
    public partial class Numeracao
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public Numeracao(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

        public class NumeracaoModel
        {
            public String   Cod_Empresa         { get; set; }
            public String   RazaoSocial         { get; set; }
            public String   Competencia_Vigente { get; set; }
            public String   Data_Ultima_Emissao { get; set; }
            public String   Tipo_Numeracao      { get; set; }
            public Int32    Numero              { get; set; }
            public String   Cod_Usuario         { get; set; }
            public Boolean  Selected            { get; set; }
            public Int32    Competencia         { get; set; }
            public String   Id_operacao         { get; set; }
            public String   Competencia_Nova    { get; set; }
            public String Critica { get; set; }
            public Boolean Status { get; set; }


        }

        public class NumeracaoSelModel
        {
            public List<NumeracaoModel> Numeracao { get; set; }
        }

    }
}