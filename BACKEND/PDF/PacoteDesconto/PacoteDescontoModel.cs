using System;
using System.Collections.Generic;

namespace PROPOSTA
{
    public partial class Pacote
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public Pacote(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

        public class PacoteModel
        {
            public Int32 Id_Pacote { get; set; }
            public String Descricao { get; set; }
            public String Validade_Inicio{ get; set; }
            public String Validade_Termino { get; set; }
            public Int32 Max_Id_Desconto { get; set; }
            public List<Desconto_DetalheModel> DescontoDetalhe { get; set; }
        }

        public class Desconto_DetalheModel
        {
            public Int32 Id_Pacote_Detalhe { get; set; }
            public Int32 Id_Pacote { get; set; }
            public Int32 Cod_Desconto { get; set; }
            public String Descricao { get; set; }
            public String Conteudo { get; set; }
            public DateTime? Data_Inicio { get; set; }
            public DateTime? Data_Termino{ get; set; }
            public String Chave { get; set; }
            public String Desconto { get; set; }
        }


    }
}