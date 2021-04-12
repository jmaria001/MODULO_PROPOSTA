using CLASSDB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace PROPOSTA
{

    public partial class Generic

    {
        public DataTable GetMensagem(String pUser)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Mural_Listar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Usuario", this.CurrentUser);
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
        public void EnviarMensagem(Mensagem  Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SimLib clsLib = new SimLib();

            string xmlMensagemUsuario = clsLib.SerializeToString(Param.Usuario);
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Enviar_Mensagem");
                cmd.Parameters.AddWithValue("@Par_Cod_Usuario", this.CurrentUser);
                cmd.Parameters.AddWithValue("@Par_Texto", Param.Texto);
                cmd.Parameters.AddWithValue("@Par_Usuario", xmlMensagemUsuario);
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

        public void MarcarMensagem(Int32 pId_Mensagem)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Visto_Mensagem");
                cmd.Parameters.AddWithValue("@Par_Cod_Usuario", this.CurrentUser);
                cmd.Parameters.AddWithValue("@Par_Id_Mensagem", pId_Mensagem);
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

        public void RemoverMensagem(Int32 pId_Mensagem)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Mensagem_Excluir");
                cmd.Parameters.AddWithValue("@Par_Cod_Usuario", this.CurrentUser);
                cmd.Parameters.AddWithValue("@Par_Id_Mensagem", pId_Mensagem);
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

        public DataTable GridConfigGravar(String pGridName, String pGridConfig, String pGridModo)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Grid_Config_Gravar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Usuario", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Grid_Name", pGridName);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Grid_Config", pGridConfig);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Grid_Modo", pGridModo);
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
        public GridConfig GridConfigSelect(String pGridName)
        {

            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            GridConfig GridConfig = new GridConfig();
            List<GridConfigHeaders> Header = new List<GridConfigHeaders>();
            GridConfigScrool Scrool = new GridConfigScrool();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Grid_Config_Select");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Usuario", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Grid_Name", pGridName);
                Adp.Fill(dtb);
                if (dtb.Rows.Count > 0)
                {
                    Scrool.ScroolX = Boolean.Parse(dtb.Rows[0]["ScroolX"].ToString());
                    Scrool.ScroolY = Boolean.Parse(dtb.Rows[0]["ScroolY"].ToString());
                    foreach (DataRow Drw in dtb.Rows)
                    {
                        Header.Add(new GridConfigHeaders()
                        {
                            title = Drw["title"].ToString(),
                            visible = Boolean.Parse(Drw["visible"].ToString()),
                            searchable = Boolean.Parse(Drw["searchable"].ToString()),
                            config = Boolean.Parse(Drw["config"].ToString())
                        });
                    }

                }
                else
                {
                    Header.Add(new GridConfigHeaders());
                }
                GridConfig.Header = Header;
                GridConfig.Scrool = Scrool;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cnn.Close();
            }
            return GridConfig;


        }

      

        
        public DataTable ListarTabela(String pTabela)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Listar_Tabela");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Tabela", pTabela);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);


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
        public DataTable ListarTabela(String pTabela,String pFilter)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Listar_Tabela");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Tabela", pTabela);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Filtro", pFilter);


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

        public DataTable ValidarTabela(String pTabela, String pCodigo)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Validar_Tabela");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Tabela", pTabela);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Codigo", pCodigo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
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
        public DataTable GetDataBaseName()
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                string strText = "select db_name() as DataBaseName, @@SERVERNAME  as ServerName";
                SqlCommand cmd = cnn.Text(cnn.Connection, strText);
                Adp.SelectCommand = cmd;
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

        public DataTable GetNivelAcesso()
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Get_Nivel_Acesso");
                Adp.SelectCommand = cmd;
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

        public String GetParametroKey(ParametroGeralModel Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            String Key = "";
            String strSql = "select dbo.fn_ParametrosGerais(";
            strSql += Param.Cod_Parametro.ToString() + ",";
            strSql += String.IsNullOrEmpty(Param.Cod_Empresa_Faturamento) ? "null" : Param.Cod_Empresa_Faturamento;
            strSql += ",";
            strSql += String.IsNullOrEmpty(Param.Cod_Empresa_Venda) ? "null" : Param.Cod_Empresa_Venda;
            strSql += ",";
            strSql += String.IsNullOrEmpty(Param.Cod_Veiculo) ? "null" : Param.Cod_Veiculo;
            strSql += ")";

            try
            {
                //SqlCommand cmd = cnn.Procedure(cnn.Connection, "pr_Parametro_S");
                //cmd.Parameters.AddWithValue("@Parametro_Par", 308);
                //Adp.SelectCommand = cmd;
                SqlCommand cmd = cnn.Text(cnn.Connection, strSql);
                Adp.SelectCommand = cmd;
                Adp.Fill(dtb);
                if (dtb.Rows.Count>=0)
                {
                    Key = dtb.Rows[0][0].ToString();
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
            return Key;
        }
    }

}
        