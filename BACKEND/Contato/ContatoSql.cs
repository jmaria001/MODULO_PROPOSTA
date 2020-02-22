using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace PROPOSTA
{

    public partial class Contato
    {
        public static string Cod_Contato { get; private set; }

        public DataTable ContatoListar(Int32 pIdContato)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Contato_Listar");
                Adp.SelectCommand = cmd;
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

        public DataTable SalvarContato(ContatoModel pContato)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            String xmlEmpresa = null;
            if (pContato.Empresas.Count>0)
            {
                xmlEmpresa = clsLib.SerializeToString(pContato.Empresas);
            }
            
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Contato_Salvar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Operacao", pContato.id_operacao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Contato", pContato.Cod_Contato);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Nome", pContato.Nome);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_CGC", pContato.CGC);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Cic_Cgc", pContato.Indica_Cic_Cgc);
                //Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Desativacao", pContato.Data_Desativacao);
                //Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Usuario_Desativacao", pContato.Cod_Usuario_Desativacao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Email_Notificacao", pContato.Email_Notificacao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_CNPJ_Empresa", pContato.CNPJ_Empresa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Razao_Empresa", pContato.Razao_Empresa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Nucleo", pContato.Cod_Nucleo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Empresas", xmlEmpresa);
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


        public ContatoModel GetContatoData(String pCod_Contato)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            ContatoModel Contato = new ContatoModel();
            List<ContatoEmpresaModel> Empresas = new List<ContatoEmpresaModel>();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_Proposta_Contato_Get");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Contato", pCod_Contato);
                Adp.Fill(dtb);
                if (dtb.Rows.Count > 0)
                {
                    Contato.Cod_Contato = dtb.Rows[0]["Cod_Contato"].ToString();
                    Contato.Nome = dtb.Rows[0]["Nome"].ToString();
                    Contato.CGC = dtb.Rows[0]["CGC"].ToString();
                    Contato.Indica_Cic_Cgc = dtb.Rows[0]["Indica_Cic_Cgc"].ToString();
                    Contato.Data_Desativacao = dtb.Rows[0]["Data_Desativacao"].ToString();
                    Contato.Status = dtb.Rows[0]["Status"].ToString();
                    Contato.Cod_Usuario_Desativacao = dtb.Rows[0]["Cod_Usuario_Desativacao"].ToString();
                    Contato.Email_Notificacao = dtb.Rows[0]["Email_Notificacao"].ToString();
                    Contato.CNPJ_Empresa = dtb.Rows[0]["CNPJ_Empresa"].ToString();
                    Contato.Razao_Empresa = dtb.Rows[0]["Razao_Empresa"].ToString();
                    Contato.Login = dtb.Rows[0]["Login"].ToString();
                    Contato.Cod_Nucleo = dtb.Rows[0]["Cod_Nucleo"].ToString();
                    Contato.Empresas = GetContatoEmpresa(pCod_Contato);
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
            return Contato;
        }

        private List<ContatoEmpresaModel> GetContatoEmpresa(String pCod_Contato)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            List<ContatoEmpresaModel> Empresas = new List<ContatoEmpresaModel>();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_Proposta_Contato_Empresa_List");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Contato", pCod_Contato);
                Adp.Fill(dtb);
                foreach (DataRow drw in dtb.Rows)
                {
                    Empresas.Add(new ContatoEmpresaModel()
                    {
                        Cod_Empresa = drw["Cod_Empresa"].ToString(),
                        Nome_Empresa= drw["Nome_Empresa"].ToString(),
                        Selected= drw["Selected"].ToString().ConvertToBoolean(),
                    });
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
            return Empresas;
        }

        public DataTable ReativarContato(String pCod_Contato)
        {
            DataTable dtb = new DataTable();
            clsConexao cnn = new clsConexao(this.Credential);
            SqlDataAdapter adp = new SqlDataAdapter();
            cnn.Open();
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Contato_Ativa");
                cmd.Parameters.AddWithValue("@Par_Cod_Contato", pCod_Contato);
                //cmd.ExecuteNonQuery();
               adp.SelectCommand = cmd;
                adp.Fill(dtb);
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

        public DataTable DesativarContato(String pCod_Contato)
        {
            DataTable dtb = new DataTable();
            clsConexao cnn = new clsConexao(this.Credential);
            SqlDataAdapter adp = new SqlDataAdapter();
            cnn.Open();
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Contato_Desativa");
                cmd.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                cmd.Parameters.AddWithValue("@Par_Cod_Contato", pCod_Contato);
                cmd.Parameters.AddWithValue("@Par_Cod_Usuario_Desativacao", this.CurrentUser);
                //cmd.ExecuteNonQuery();
                adp.SelectCommand = cmd;
                adp.Fill(dtb);
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

        public DataTable ExcluirContato(ContatoModel pContato)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_CONTATO_Excluir");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Contato", pContato.Cod_Contato);
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
    }
}
