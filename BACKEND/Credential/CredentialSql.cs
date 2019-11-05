using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;

namespace PROPOSTA
{
    public partial  class apiCredential
    {
        public DataTable Login(String pCredential)
        {
            SimLib clsLib = new SimLib();
            String User = clsLib.Decriptografa(clsLib.GetJsonItem(pCredential, "Name"));
            String Password = clsLib.GetJsonItem(pCredential, "Password");

            DataTable dtb = new DataTable("dtb");
            clsConexao cnn = new clsConexao(pCredential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            try
            {
            
                
                    SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Login");
                    Adp.SelectCommand = cmd;
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", User);
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Senha", Password);
                    Adp.SelectCommand = cmd;
                    Adp.Fill(dtb);
            
            }
            catch (Exception )
            {
                throw;
            }
            finally
            {
                cnn.Close();
            }
            return dtb;
        }
        public DataTable GetUserData()
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            String pUser = this.CurrentUser;
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_Proposta_User_Data_S");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", pUser);
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
        public Boolean Permissao(String  pRota)
        {
            DataTable dtb = new DataTable("dtb");
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            Boolean bolRetorno = false;
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Permissao_Validar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Route", pRota);
                Adp.SelectCommand = cmd;
                Adp.Fill(dtb);
                bolRetorno = dtb.Rows[0]["Permissao"].ToString().ConvertToBoolean();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cnn.Close();
            }
            return bolRetorno;
        }

        public String EsqueceuSenha(RememberPassword Param)
        {

            String Retorno = "";
            DataTable dtb = new DataTable("dtb");
            clsConexao cnn = new clsConexao(this.Credential);
            SimLib clsLib = new SimLib();
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Esqueceu_Senha");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Email", Param.Email);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", Param.Login);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Esqueceu_Login", Param.EsqueceuLogin);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Url", Param.Url);
                Adp.SelectCommand = cmd;
                Adp.Fill(dtb);

                if (dtb.Rows.Count>0)
                {
                    if (int.Parse(dtb.Rows[0]["Status"].ToString())==1)
                    {
                        clsLib.EnviaEmail(Param.Email,  "", "", "Solicitação de Alteração de Senha", dtb.Rows[0]["Email"].ToString(),"");
                    }
                    Retorno = dtb.Rows[0]["Mensagem"].ToString();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cnn.Close();
            }
            return Retorno;
        }

        public String AlterarSenha(RememberPassword Param)
        {

            String Retorno = "";
            DataTable dtb = new DataTable("dtb");
            clsConexao cnn = new clsConexao(this.Credential);
            SimLib clsLib = new SimLib();
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Alterar_Senha");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Token", Param.Token);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Senha", Param.Senha);
                Adp.SelectCommand = cmd;
                Adp.Fill(dtb);

                if (dtb.Rows.Count > 0)
                {
                    Retorno = dtb.Rows[0]["Mensagem"].ToString();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cnn.Close();
            }
            return Retorno;
        }

    }
    
}