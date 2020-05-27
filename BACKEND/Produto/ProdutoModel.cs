using System;
using System.Collections.Generic;

namespace PROPOSTA
{
    public partial class Produto
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public Produto(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

        public class ProdutoModel
        {
            public Int32 Cod_Segmento { get; set; }
            public String Segmento { get; set; }
            public Int32 Cod_Setor { get; set; }
            public String Setor { get; set; }
            public Int32 Cod_Produto { get; set; }
            public String Produto { get; set; }
            public String Horario_Restricao { get; set; }
            public String Operacao { get; set; }
            public List<ProdutoClienteModel> Clientes { get; set; }
        }
        public class ProdutoClienteModel
        {
            public String Cod_Cliente { get; set; }
            public String Nome_Cliente { get; set; }
        }
    }
}