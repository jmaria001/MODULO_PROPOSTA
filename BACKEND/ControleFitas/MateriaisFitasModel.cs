using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROPOSTA
{
    public partial class MateriaisFitas
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public MateriaisFitas(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

        public class MateriaisFitasModel
        {

            public String Id_operacao         { get; set; }
            public String Id_Fita             { get; set; }
            public String Cod_Cliente         { get; set; }
            public String Nome_Cliente        { get; set; }
            public String Cod_Agencia         { get; set; }
            public String Nome_Agencia        { get; set; }
            public String Titulo              { get; set; }
            public Int32  Cod_Red_Produto     { get; set; }
            public String Descricao_Produto   { get; set; }
            public Int32  Duracao             { get; set; }
            public String Numero_Fita         { get; set; }
            public String Cod_Veiculo         { get; set; }
            public String Nome_Veiculo        { get; set; }
            public String Cod_Tipo_Midia      { get; set; }
            public String Descricao_Midia     { get; set; }
            public String Cod_Tipo_Comercial  { get; set; }
            public String Descricao_Comercial { get; set; }
            public Int32  Range               { get; set; }
            public String Tipo_Fita           { get; set; }
            public String Tipo_Midia          { get; set; }
        }


        public class FiltroModel
        {
            public String Cod_Agencia { get; set; }
            public String Cod_Cliente { get; set; }
          
        }






    }
}