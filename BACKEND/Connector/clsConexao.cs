using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using PROPOSTA;
namespace CLASSDB
{
    public class clsConexao
    {
        private AppSettingsReader AppRead = new AppSettingsReader();
        private SqlConnection oConnection;
        private String Credential;
        private String UserName;
        private String Password;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        //public clsConexao()
        //{
        //    oConnection = new SqlConnection();
        //}

        public clsConexao(String Credential)
        {
            oConnection = new SqlConnection();
            this.Credential = Credential;
            this.CurrentUser= this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

        public clsConexao(String pUser, String pSenha)
        {
            oConnection = new SqlConnection();
            this.UserName = pUser;
            this.Password = pSenha;
        }

        public SqlConnection Connection
        {
            get { return (oConnection); }
            
        }

        public void Open()
        {
            String usuario = "";
            String senha = "";

            SimLib clsLib = new SimLib();
            if (!String.IsNullOrEmpty(this.Credential))
            {
                usuario = this.CurrentUser;
                senha = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Password"));
            }
            else
            {
                if (!String.IsNullOrEmpty(this.UserName))
                {
                    usuario = clsLib.Decriptografa( this.UserName);
                    senha = clsLib.Decriptografa(this.Password);
                }
            }


            System.Security.SecureString PWS = new System.Security.SecureString();

            foreach (char character in senha)
            {
                PWS.AppendChar(character);
            }
            PWS.MakeReadOnly();
            SqlCredential Cred = new SqlCredential(usuario, PWS);

            try
            {
                //String Conexao
                oConnection.ConnectionString = "Persist Security Info=False;";
                if (AppRead.GetValue("TCPIP", typeof(string)).ToString() == "true")
                    oConnection.ConnectionString += "Network Library=dbmssocn;";
                else
                    oConnection.ConnectionString += "Network Library=dbnmpntw;";
                if (AppRead.GetValue("ApplicationName", typeof(string)).ToString() != "")
                {
                    oConnection.ConnectionString += "Application Name=";
                    oConnection.ConnectionString += AppRead.GetValue("ApplicationName", typeof(string)).ToString();
                    oConnection.ConnectionString += ";";
                }
                //---Nome Servidor SQL
                if (AppRead.GetValue("Computer", typeof(string)).ToString() != "")
                {
                    oConnection.ConnectionString += "Workstation ID=";
                    oConnection.ConnectionString += AppRead.GetValue("Computer", typeof(string)).ToString();
                    oConnection.ConnectionString += ";";
                }
                //---Nome da Instancia SQL
                oConnection.ConnectionString += "Data Source=";
                oConnection.ConnectionString += AppRead.GetValue("DataSource" , typeof(string)).ToString();
                oConnection.ConnectionString += ";";

                //---Pooling
                if (AppRead.GetValue("Pooling", typeof(string)).ToString() != "")
                {
                    oConnection.ConnectionString += "Pooling=";
                    oConnection.ConnectionString += AppRead.GetValue("Pooling", typeof(string)).ToString();
                    oConnection.ConnectionString += ";";
                }

                //---Nome do Banco de Dados
                oConnection.ConnectionString += "Initial Catalog=";
                oConnection.ConnectionString += AppRead.GetValue("Database" , typeof(string)).ToString();
                oConnection.ConnectionString += ";";
                
                //---User e Password
                if (AppRead.GetValue("SQLMODE", typeof(string)).ToString() == "APP")
                {
                    oConnection.ConnectionString += "User ID=";
                    oConnection.ConnectionString += Decriptografa(AppRead.GetValue("SQLUSER", typeof(string)).ToString());
                    oConnection.ConnectionString += ";";
                    oConnection.ConnectionString += "pwd=";
                    oConnection.ConnectionString += Decriptografa(AppRead.GetValue("SQLPASSWORD", typeof(string)).ToString());
                    oConnection.ConnectionString += ";";
                }

                //---Timeout
                if (AppRead.GetValue("Timeout", typeof(string)).ToString() != "")
                {
                    oConnection.ConnectionString += "Connect Timeout=";
                    oConnection.ConnectionString += AppRead.GetValue("Timeout", typeof(string)).ToString();
                    oConnection.ConnectionString += ";";
                }
                //oConnection.Credential = Cred;
                oConnection.Open();
            }
            catch
            {
                //throw new Exception("Não foi possivel autenticar conexão com o Servidor de dados");
                throw new Exception("Não foi possivel autenticar conexão com o Servidor de dados - Verifique a Configuração do Arquivo Web.Config");
                
            }

            finally
            {
            }
        }
        public void Close()
        {
            try
            {
                if (oConnection.State == ConnectionState.Open)
                {
                    oConnection.Close();
                    oConnection.Dispose();
                }
            }
            finally
            {
            }
        }
        public SqlCommand Procedure(SqlConnection pConexao, String pProcName)
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.Connection = pConexao;
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = pProcName;
            Cmd.CommandTimeout = pConexao.ConnectionTimeout;
            return Cmd;
        }
        public SqlCommand Text(SqlConnection pConexao, String pText)
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.Connection = pConexao;
            Cmd.CommandType = CommandType.Text;
            Cmd.CommandText = pText;
            Cmd.CommandTimeout = pConexao.ConnectionTimeout;
            return Cmd;
        }
        private String Decriptografa(string Par_Campo)
        {
            String Var_Senha = "";
            String Var_Criptografa = "";
            Int32 bt1 = 0;
            Int32 bt2 = 0;
            Int32 bt3 = 0;
            try
            {
                for (int Var_Contador = 0; Var_Contador < Par_Campo.Length; Var_Contador += 2)
                {
                    Var_Criptografa += Par_Campo.Substring(Var_Contador + 1, 1) + Par_Campo.Substring(Var_Contador, 1);
                }
                for (int Var_Contador = 0; Var_Contador < @Var_Criptografa.Length; Var_Contador += 6)
                {
                    bt1 = Int16.Parse(Var_Criptografa.Substring(Var_Contador, 3));
                    bt2 = Int16.Parse(Var_Criptografa.Substring(Var_Contador + 3, 3));
                    bt3 = bt1 - bt2;
                    Var_Senha += (Char)bt3;
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally
            {
            }
            return Var_Senha.TrimEnd();
        }
    }
}
