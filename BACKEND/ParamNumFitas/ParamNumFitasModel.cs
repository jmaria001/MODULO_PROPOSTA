using System;
using System.Collections.Generic;

namespace PROPOSTA
{
    public partial class ParamNumFitas
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();

        public ParamNumFitas(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

        public class ParamFitaModel
        {
            public String Cod_Veiculo { get; set; }
            public String Nome_Veiculo { get; set; }
            public Int32 Rg_Comerc_De { get; set; }
            public Int32 Rg_Comerc_Ate { get; set; }
            public Int32 Rg_Artist_De { get; set; }
            public Int32 Rg_Artist_Ate { get; set; }
            public Int32 Rg_Reserv_De { get; set; }
            public Int32 Rg_Reserv_Ate { get; set; }
            public Boolean Indica_Numerac_Compart { get; set; }
            public Boolean Indica_Numerac_Propria { get; set; }
            public List<ParamFitaRegraModel> Regras { get; set; }
            public Int32 Max_Id_Linha { get; set; }
        }

        public class ParamFitaRegraModel
        {
            public Int32 Id_Linha { get; set; }
            public String Tipo_Midia { get; set; }
            public String Tipo_Comercial { get; set; }
            public Int32 Num_Fita_De { get; set; }
            public Int32 Num_Fita_Ate { get; set; }
        }


    }
}