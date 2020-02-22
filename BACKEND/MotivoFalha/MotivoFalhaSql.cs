using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace PROPOSTA
{

    public partial class MotivoFalha
    {

        //===========================Listar dados do cadastro
        public DataTable MotivoFalhaListar(Int32 pIdMotivoFalha)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_MotivoFalha_Listar");
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
        public DataTable SalvarMotivoFalha(MotivoFalhaModel pMotivoFalha)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_MotivoFalha_Salvar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Operacao", pMotivoFalha.Id_operacao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Motivo_Falha", pMotivoFalha.Cod_Motivo_Falha);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Descricao", pMotivoFalha.Descricao);
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
        public MotivoFalhaModel GetMotivoFalhaData(String pCodMotivoFalha)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            MotivoFalhaModel MotivoFalha = new MotivoFalhaModel();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_MotivoFalha_Get");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Motivo_Falha", pCodMotivoFalha);
                Adp.Fill(dtb);
                if (dtb.Rows.Count > 0)
                {
                    MotivoFalha.Cod_Motivo_Falha = dtb.Rows[0]["Cod_Motivo_Falha"].ToString();
                    MotivoFalha.Descricao = dtb.Rows[0]["Descricao"].ToString().Trim();
                    MotivoFalha.Indica_Desativado = dtb.Rows[0]["Indica_Desativado"].ToString().ConvertToBoolean();
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
            return MotivoFalha;
        }


        //===========================Excluir cadastro da tabela
        public DataTable ExcluirMotivoFalha(MotivoFalhaModel pMotivoFalha)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_MotivoFalha_Excluir");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Motivo_Falha", pMotivoFalha.Cod_Motivo_Falha);
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

        //===========================Desativar/Reativar
        public DataTable DesativarReativarMotivoFalha(MotivoFalhaModel pMotivoFalha)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_MotivoFalha_Desativar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Motivo_Falha", pMotivoFalha.Cod_Motivo_Falha);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Id_Acao", pMotivoFalha.Id_Acao);
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


