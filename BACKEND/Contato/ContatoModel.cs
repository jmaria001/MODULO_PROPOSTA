using System;
using System.Collections.Generic;

namespace PROPOSTA
{
    public partial class Contato
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public Contato(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

        public class ContatoModel
        {
            public String Cod_Contato { get; set; }
            public String Nome { get; set; }
            public String CGC { get; set; }
            public String Indica_Cic_Cgc { get; set; }
            public String Data_Desativacao { get; set; }
            public String Status { get; set; }
            public String Descricao_Status { get; set; }
            public String Cod_Usuario_Desativacao { get; set; }
            public String Email_Notificacao { get; set; }
            public String CNPJ_Empresa { get; set; }
            public String Razao_Empresa { get; set; }
            public String Login { get; set; }
            public String Cod_Nucleo { get; set; }
            public string id_operacao { get; set; }
            public List< ContatoEmpresaModel> Empresas { get; set; }
        }

        public class ContatoEmpresaModel
        {
            public String Cod_Empresa { get; set; }
            public String Nome_Empresa { get; set; }
            public Boolean Selected { get; set; }

        }
    }
}