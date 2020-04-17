using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace PROPOSTA
{

    public partial class MotivoCancelamento
    {
        public static String Cod_Cancelamento { get; private set; }

        public DataTable MotivoCancelamentoListar(String pidCod_Cancelamento)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Motivo_Cancelamento_Listar");
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

        public DataTable SalvarMotivoCancelamento(MotivoCancelamentoModel pMotivoCancelamento)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Motivo_Cancelamento_Salvar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Operacao", pMotivoCancelamento.id_operacao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Cancelamento", pMotivoCancelamento.Cod_Cancelamento);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Descricao", pMotivoCancelamento.Descricao);
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

        public MotivoCancelamentoModel GetMotivoCancelamentoData(string pCod_Cancelamento)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            MotivoCancelamentoModel MotivoCancelamento = new MotivoCancelamentoModel();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Motivo_Cancelamento_Get");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Cancelamento", pCod_Cancelamento.Trim());
                Adp.Fill(dtb);
                if (dtb.Rows.Count > 0)
                {
                    MotivoCancelamento.Cod_Cancelamento = dtb.Rows[0]["Cod_Cancelamento"].ToString();
                    MotivoCancelamento.Descricao = dtb.Rows[0]["Descricao"].ToString();
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
            return MotivoCancelamento;
        }

        //public DataTable ExcluirRede(RedeModel pRede)
        //{
        //    clsConexao cnn = new clsConexao(this.Credential);
        //    cnn.Open();
        //    SqlDataAdapter Adp = new SqlDataAdapter();
        //    DataTable dtb = new DataTable("dtb");
        //    SimLib clsLib = new SimLib();
        //    try
        //    {
        //        SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Tipo_Midia_Excluir");
        //        Adp.SelectCommand = cmd;
        //        Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
        //        Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Tipo_Midia", pTipoMidia.Cod_Tipo_Midia);
        //        Adp.Fill(dtb);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        cnn.Close();
        //    }
        //    return dtb;
        //}
    }
}
