using System;
using System.Collections.Generic;

namespace PROPOSTA
{
    public partial class ParametroValoracao
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public ParametroValoracao(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }


        public class ParametroValoracaoModel
        {
            
            public String Competencia { get; set; }
            public String Cod_Programa { get; set; }
            public String Titulo { get; set; }
            public String Cod_Empresa { get; set; }
            public String Razao_Social { get; set; }
            public String TipoParametroValoracao { get; set; }
            public List<Parametro_Tipo_Comercial_Model> Tipo_Comercial { get; set; }
            public List<Parametro_Duracao_Model> Duracao { get; set; }
        }

        public class Parametro_Tipo_Comercial_Model
        {
            public String Id_Operacao { get; set; }
            public String Competencia { get; set; }
            public String Cod_Programa { get; set; }
            public String Cod_Empresa { get; set; }
            public String  Cod_Tipo_Comercial { get; set; }
            public String  Descricao { get; set; }
            public String  Vlr_Parametro{ get; set; }
            public Boolean Indica_Vlr_Duracao { get; set; }
            public Boolean Indica_Vlr_Proporcional { get; set; }
            public String Tipo_Valoracao { get; set; }
            public Boolean Indica_Desativado{ get; set; }
            public String TipoParametroValoracao { get; set; }
        }

        public class Parametro_Duracao_Model
        {
            public String Competencia { get; set; }
            public String Cod_Programa { get; set; }
            public String Cod_Empresa { get; set; }
            public Int32 Duracao { get; set; }
            public String Vlr_Parametro{ get; set; }
            public Boolean Indica_Desativado{ get; set; }
            public String TipoParametroValoracao { get; set; }
        }


        public class FiltroModel
        {
            public String Competencia { get; set; }
            public String Cod_Programa { get; set; }
            public String Titulo_Programa { get; set; }
            public String TipoParametroValoracao { get; set; }

        }

    }
}