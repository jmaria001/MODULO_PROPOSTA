using System;
using System.Collections.Generic;

namespace PROPOSTA
{
    public partial class TipoComercial
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public TipoComercial(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

        public class TipoComercialModel
        {
            public String Cod_Tipo_Comercial { get; set; }
            public Boolean Absorcao { get; set; }
            public String Descricao { get; set; }
            public Boolean Roteiro_Tecnico { get; set; }
            public Boolean Merchandising { get; set; }
            public Boolean Cod_ADSolution { get; set; }
            public Boolean Cod_Midia_Log { get; set; }
            public Boolean INDICA_ABSORCAO_MERCHA { get; set; }
            public Boolean Tipo_Valoracao { get; set; }
            public Boolean Indica_Midia_On_Line { get; set; }
            public String Exibidora_DAD { get; set; }
            public Int32 Id_tipo_comercial { get; set; }
            public Int32 Id_Assessor { get; set; }
            public Byte Indica_Edicao_Assessor { get; set; }
            public string id_operacao { get; set; }

        }



    }
}