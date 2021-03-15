using System;
using System.Collections.Generic;

namespace PROPOSTA
{

    public partial class NaturezadeServico
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public NaturezadeServico(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

        public class NaturezadeServicoModel
        {
            public String Id_operacao { get; set; }
            public String Cod_Natureza   { get; set; }
            public String Descricao      { get; set; }
            public String Cod_Atividade  { get; set; }
            public Boolean Indica_Midia  { get; set; }
            public Double Percentual_Iss { get; set; }
            public Boolean Indica_NFE    { get; set; }
            public Boolean Indica_NFEE   { get; set; }
            public String Cod_Historico  { get; set; }
            public Double Perc_IR        { get; set; }
            public Double Perc_CS        { get; set; }
            public Double Perc_COFINS   { get; set; }
            public Double Perc_PIS      { get; set; }
            public Double PERC_INSS     { get; set; }
            public String Data_Desativacao { get; set; }
            public String Cod_Usuario_Desativacao { get; set; }
            public String Descricao_Historico { get; set; }
            public String Cod_Empresa { get; set; }
            public String Razao_Social { get; set; }
            public Boolean Indica_Desativado { get; set; }
            public String Id_Acao { get; set; }

        }

        public class FiltroModel
        {
            public String Cod_Empresa { get; set; }
            public String Nome_Empresa_Faturamento { get; set; }
            public String Cod_Natureza { get; set; }

        }


    }
}