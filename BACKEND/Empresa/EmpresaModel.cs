using System;
using System.Collections.Generic;

namespace PROPOSTA
{
    public partial class Empresa
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public Empresa(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

        public class EmpresaModel
        {
            public String Cod_Empresa { get; set; }
            public String Bairro { get; set; }
            public String Cod_UF { get; set; }
            public String CEP { get; set; }
            public String CGC { get; set; }
            public String Cidade { get; set; }
            public String Empresa_Pertence { get; set; }
            public String Endereco { get; set; }
            public String Inscricao_Estadual { get; set; }
            public String Inscricao_Municipal { get; set; }
            public String Razao_Social { get; set; }
            public String Cod_JOVE { get; set; }
            public String Telefone { get; set; }
            public String Id_operacao { get; set; }
        }



    }
}