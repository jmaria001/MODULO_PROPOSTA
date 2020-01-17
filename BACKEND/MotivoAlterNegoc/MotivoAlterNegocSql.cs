using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace PROPOSTA
{

    public partial class MotivoAlterNegoc
    {

        //===========================Listar dados do cadastro
        public DataTable MotivoAlterNegocListar(Int32 pIdMotivoAlterNegoc)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_MotivoAlterNegoc_Listar");
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


        //===========================Salvar cadastro na tabela
        public DataTable SalvarMotivoAlterNegoc(MotivoAlterNegocModel pMotivoAlterNegoc)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_MotivoAlterNegoc_Salvar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Operacao", pMotivoAlterNegoc.id_operacao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Alteracao", pMotivoAlterNegoc.Cod_Alteracao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Descricao", pMotivoAlterNegoc.Descricao);

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


        //===========================Get cadastro da tabela
        public MotivoAlterNegocModel GetMotivoAlterNegocData(String pCodAlteracao)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            MotivoAlterNegocModel MotivoAlterNegoc = new MotivoAlterNegocModel();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_MotivoAlterNegoc_Get");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Alteracao", pCodAlteracao);
                Adp.Fill(dtb);
                if (dtb.Rows.Count > 0)
                {
                    MotivoAlterNegoc.Cod_Alteracao = dtb.Rows[0]["Cod_Alteracao"].ToString();
                    MotivoAlterNegoc.Descricao = dtb.Rows[0]["Descricao"].ToString().Trim();
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
            return MotivoAlterNegoc;
        }


        //===========================Excluir cadastro da tabela
        public DataTable ExcluirMotivoAlterNegoc(MotivoAlterNegocModel pMotivoAlterNegoc)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_MotivoAlterNegoc_Excluir");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Alteracao", pMotivoAlterNegoc.Cod_Alteracao);
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


