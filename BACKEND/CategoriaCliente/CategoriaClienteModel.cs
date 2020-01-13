using System;
using System.Collections.Generic;

namespace PROPOSTA
{
    public partial class CategoriaCliente
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public CategoriaCliente(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }


        public class CategoriaClienteModel
        {
            public Int32 Cod_Categoria { get; set; }
            public String Descricao_Categoria { get; set; }
            public String Cod_Fiscal { get; set; }
            public string id_operacao { get; set; }
        }


    }
}