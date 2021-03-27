using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROPOSTA
{
    public partial class DeParaProgramacao
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public DeParaProgramacao(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

        public class DeParaPeriodoModel
        {
            public String Data_Inicio { get; set; }
            public String Data_Termino{ get; set; }
            public Boolean Dom { get; set; }
            public Boolean Seg{ get; set; }
            public Boolean Ter{ get; set; }
            public Boolean Qua { get; set; }
            public Boolean Qui{ get; set; }
            public Boolean Sex{ get; set; }
            public Boolean Sab{ get; set; }
            public String Cod_Programa_De{ get; set; }
            public String Cod_Programa_Para { get; set; }
            public List<VeiculoModel> Veiculos { get; set; }
        }
        public class DeParaDataModel
        {
            public String Data_De{ get; set; }
            public String Data_Para{ get; set; }
            public String Cod_Programa_De { get; set; }
            public String Cod_Programa_Para { get; set; }
            public List<VeiculoModel> Veiculos { get; set; }
        }
        public class VeiculoModel
        {
            public String Cod_Veiculo { get; set; }
            public String Nome_Veiculo { get; set; }
            public Boolean Selected { get; set; }
        }
    }
}