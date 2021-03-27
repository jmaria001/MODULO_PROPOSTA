using System;
using System.Collections.Generic;

namespace PROPOSTA
{
    public partial class BaixaRoteiro
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public BaixaRoteiro(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }


        public class BaixaRoteiroModel
        {
            public String Id_Operacao { get; set; }
            public String Data_Inicial { get; set; }
            public String Data_Final { get; set; }
            public String Cod_Qualidade { get; set; }
            public String Cod_Programa { get; set; }
            public String Titulo { get; set; }
            public String Cod_Tipo_Comercial { get; set; }
            public String Titulo_Comercial { get; set; }
            public Boolean Domingo { get; set; }
            public Boolean Segunda { get; set; }
            public Boolean Terca { get; set; }
            public Boolean Quarta { get; set; }
            public Boolean Quinta { get; set; }
            public Boolean Sexta { get; set; }
            public Boolean Sabado { get; set; }
            public String Cod_Usuario { get; set; }
            public String DiaSemana { get; set; }
            public int Merchandising { get; set; }
            public int Indica_Convidado { get; set; }
            public int Indica_Iem { get; set; }
            public List<BaixaRoteiroVeiculoModel> Veiculos { get; set; }
        }
         public class BaixaRoteiroVeiculoModel
        {
            public String Cod_Veiculo { get; set; }
            public String Nome_Veiculo { get; set; }
            public Boolean Selected { get; set; }
        }
    }
}