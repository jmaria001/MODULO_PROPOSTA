using System;
using System.Collections.Generic;

namespace PROPOSTA
{
    public partial class Usuario
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public Usuario(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

        public class UsuarioModel
        {
            public Int32 Id_Usuario { get; set; }
            public String Login { get; set; }
            public String Nome { get; set; }
            public String Telefone { get; set; }
            public String Cargo{ get; set; }
            public String Email { get; set; }
            public Boolean Status { get; set; }
            public String Descricao_Status { get; set; }
            public List<PerfilModel> Perfil { get; set; }
            public List<EmpresaModel> Empresas { get; set; }
            public Int32 Id_Nivel_Acesso { get; set; }
        }
        public class PerfilModel
        {
            public Int32 Id_Funcao { get; set; }
            public Int32 Id_Funcao_Root { get; set; }
            public String Descricao_Funcao { get; set; }
            public String Path { get; set; }
            public Boolean Selected { get; set; }
            public Int32 Nivel { get; set; }
        }
        public class EmpresaModel
        {
            public String Cod_Empresa { get; set; }
            public String Nome_Empresa { get; set; }
            public Boolean Selected { get; set; }
        }

    }
}