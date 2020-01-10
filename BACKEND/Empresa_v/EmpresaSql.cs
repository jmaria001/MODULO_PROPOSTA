using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace PROPOSTA
{

    public partial class Empresa
    {
        public static string Cod_Empresa { get; private set; }

        public DataTable EmpresaListar(Int32 pIdEmpresa)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Empresa_Listar");
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

        public EmpresaModel GetEmpresaData(String pCod_Empresa)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            EmpresaModel Empresa = new EmpresaModel();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_Proposta_Empresa_Get");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa", pCod_Empresa);
                Adp.Fill(dtb);
                if (dtb.Rows.Count > 0)
                {
                    Empresa.Cod_Empresa = dtb.Rows[0]["Cod_Empresa"].ToString();
                    Empresa.Bairro = dtb.Rows[0]["Bairro"].ToString();
                    Empresa.Cod_UF = dtb.Rows[0]["Cod_UF"].ToString();
                    Empresa.CEP = dtb.Rows[0]["CEP"].ToString();
                    Empresa.CGC = dtb.Rows[0]["CGC"].ToString();
                    Empresa.Cidade = dtb.Rows[0]["Cidade"].ToString();
                    Empresa.Empresa_Pertence = dtb.Rows[0]["Empresa_Pertence"].ToString();
                    Empresa.Endereco = dtb.Rows[0]["Endereco"].ToString();
                    Empresa.Inscricao_Estadual = dtb.Rows[0]["Inscricao_Estadual"].ToString();
                    Empresa.Inscricao_Municipal = dtb.Rows[0]["Inscricao_Municipal"].ToString();
                    Empresa.Razao_Social = dtb.Rows[0]["Razao_Social"].ToString();
                    Empresa.Cod_JOVE = dtb.Rows[0]["Cod_JOVE"].ToString();
                    Empresa.Telefone = dtb.Rows[0]["Telefone"].ToString();
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
            return Empresa;
        }

        public DataTable SalvarEmpresa(EmpresaModel pEmpresa)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Empresa_Salvar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Operacao", pEmpresa.Id_operacao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa", pEmpresa.Cod_Empresa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Bairro", pEmpresa.Bairro);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_UF", pEmpresa.Cod_UF);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_CEP", pEmpresa.CEP);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_CGC", pEmpresa.CGC);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cidade", pEmpresa.Cidade);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Empresa_Pertence", pEmpresa.Empresa_Pertence);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Endereco", pEmpresa.Endereco);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Inscricao_Estadual", pEmpresa.Inscricao_Estadual);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Inscricao_Municipal", pEmpresa.Inscricao_Municipal);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Razao_Social", pEmpresa.Razao_Social);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_JOVE", pEmpresa.Cod_JOVE);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Telefone", pEmpresa.Telefone);


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

        public DataTable excluirEmpresa(EmpresaModel pEmpresa)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Empresa_Excluir");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa", pEmpresa.Cod_Empresa);
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
