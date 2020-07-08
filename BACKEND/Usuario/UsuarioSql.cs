using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace PROPOSTA
{

    public partial class Usuario
    {
        public DataTable UsuarioListar(Int32 pIdUsuario)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Usuario_Listar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Id_Usuario", pIdUsuario);
                Adp.Fill(dtb);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cnn.Close();
            }
            return dtb;
        }
        public UsuarioModel GetUsuario(Int32 pIdUsuario)
        {
            UsuarioModel Usuario = new UsuarioModel();
            List<PerfilModel> Perfil = new List<PerfilModel>();
            List<EmpresaModel> Empresas = new List<EmpresaModel>();

            clsConexao cnn = new clsConexao(this.Credential);
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            cnn.Open();
            SimLib clsLib = new SimLib();
            try
            {
                //=======================Dados do usuario

                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Usuario_Listar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Id_Usuario", pIdUsuario);
                Adp.Fill(dtb);
                if (dtb.Rows.Count > 0)
                {
                    Usuario.Id_Usuario = dtb.Rows[0]["Id_Usuario"].ToString().ConvertToInt32();
                    Usuario.Login = dtb.Rows[0]["Login"].ToString();
                    Usuario.Nome = dtb.Rows[0]["Nome"].ToString();
                    Usuario.Email = dtb.Rows[0]["Email"].ToString();
                    Usuario.Status = dtb.Rows[0]["Status"].ToString().ConvertToBoolean();
                    Usuario.Descricao_Status = dtb.Rows[0]["Descricao_Status"].ToString();
                    Usuario.Telefone = dtb.Rows[0]["Telefone"].ToString();
                    Usuario.Cargo = dtb.Rows[0]["Cargo"].ToString();
                    Usuario.Id_Nivel_Acesso = dtb.Rows[0]["Id_Nivel_Acesso"].ToString().ConvertToInt32();
                    Usuario.Nivel_Superior = AddNivel(pIdUsuario, 1);
                    Usuario.Nivel_Inferior= AddNivel(pIdUsuario, 2);
                }

                //=======================Perfil de Acesso
                Adp = new SqlDataAdapter();
                dtb = new DataTable("dtb");
                cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Usuario_Perfil_List");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Id_Usuario", pIdUsuario);
                Adp.Fill(dtb);
                foreach (DataRow drw in dtb.Rows)
                {
                    Perfil.Add(new PerfilModel()
                    {
                        Id_Funcao = drw["Id_Funcao"].ToString().ConvertToInt32(),
                        Id_Funcao_Root = drw["Id_Funcao_Root"].ToString().ConvertToInt32(),
                        Descricao_Funcao = drw["Descricao_Funcao"].ToString(),
                        Path = drw["Path"].ToString(),
                        Selected = drw["Selected"].ToString().ConvertToBoolean(),
                        Nivel = drw["Nivel"].ToString().ConvertToInt32()
                    }
                    );
                }
                Usuario.Perfil = Perfil;
                //=======================Perfil Empresas
                Adp = new SqlDataAdapter();
                dtb = new DataTable("dtb");
                cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Usuario_Empresa_List");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Id_Usuario", pIdUsuario);
                Adp.Fill(dtb);
                foreach (DataRow drw in dtb.Rows)
                {
                    Empresas.Add(new EmpresaModel()
                    {
                        Cod_Empresa = drw["Cod_Empresa"].ToString(),
                        Nome_Empresa = drw["Nome_Empresa"].ToString(),
                        Selected = drw["Selected"].ToString().ConvertToBoolean(),
                    }
                    );
                }
                Usuario.Empresas = Empresas;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cnn.Close();
            }
            return Usuario;
        }
        private List<HierarquiaModel> AddNivel(Int32 pIdUsuario, Byte pNivel)
        {

            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            List<HierarquiaModel> Hierarquia = new List<HierarquiaModel>();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Usuario_Hierarquia_Get");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Id_Usuario", pIdUsuario);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Nivel", pNivel);
                Adp.Fill(dtb);
                foreach (DataRow drw in dtb.Rows)
                {
                    Hierarquia.Add(new HierarquiaModel()
                    {
                        Login = drw["Login"].ToString(),
                        Nome = drw["Nome"].ToString()
                    });
                };

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cnn.Close();
            }
            return Hierarquia;
        }
        public DataTable SalvarUsuario(UsuarioModel Usuario)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            String xmlPerfil = null;
            if (Usuario.Perfil.Count > 0)
            {
                xmlPerfil = clsLib.SerializeToString(Usuario.Perfil);
            }
            String xmlEmpresas = null;
            if (Usuario.Empresas.Count > 0)
            {
                xmlEmpresas = clsLib.SerializeToString(Usuario.Empresas);
            }
            String xmlNivelSuperior= null;
            if (Usuario.Nivel_Superior.Count > 0)
            {
                xmlNivelSuperior = clsLib.SerializeToString(Usuario.Nivel_Superior);
            }
            String xmlNivelInferior = null;
            if (Usuario.Nivel_Inferior.Count > 0)
            {
                xmlNivelInferior = clsLib.SerializeToString(Usuario.Nivel_Inferior);
            }
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Usuario_Salvar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Id_Usuario", Usuario.Id_Usuario);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", Usuario.Login);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Nome", Usuario.Nome);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Telefone", Usuario.Telefone);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cargo", Usuario.Cargo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Email", Usuario.Email);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Nivel_Acesso", Usuario.Id_Nivel_Acesso);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Perfil", xmlPerfil);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Empresa", xmlEmpresas);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Hierarquia_Pai", xmlNivelSuperior);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Hierarquia_Filho", xmlNivelInferior);
                Adp.Fill(dtb);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cnn.Close();
            }
            return dtb;
        }
        public void DesativarReativar(UsuarioModel Usuario)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Update_Status");
                cmd.Parameters.AddWithValue("@Par_Id_Usuario", Usuario.Id_Usuario);
                cmd.Parameters.AddWithValue("@Par_Status", Usuario.Status);
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cnn.Close();
            }

        }
        public void ExcluirUsuario(UsuarioModel Usuario)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Excluir_Usuario");
                cmd.Parameters.AddWithValue("@Par_Id_Usuario", Usuario.Id_Usuario);
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cnn.Close();
            }

        }

    }
}
