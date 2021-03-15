using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace PROPOSTA
{

    public partial class CondPgto
    {
        public static string Cod_Condicao { get; private set; }

        public DataTable CondPgtoListar(Int32 pIdCondPgto)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_CondPgto_Listar");
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

        public CondPgtoModel GetCondPgtoData(String pCod_Condicao)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            CondPgtoModel CondPgto = new CondPgtoModel();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_Proposta_CondPgto_Get");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Condicao", pCod_Condicao);
                Adp.Fill(dtb);
                if (dtb.Rows.Count > 0)
                {
                    CondPgto.Cod_Condicao = dtb.Rows[0]["Cod_Condicao"].ToString();
                    CondPgto.Descricao = dtb.Rows[0]["Descricao"].ToString();
                    CondPgto.Qtd_Dias  = dtb.Rows[0]["Qtd_Dias"].ToString().ConvertToInt32();
                    CondPgto.Tipo_Vencimento = dtb.Rows[0]["Tipo_Vencimento"].ToString();


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
            return CondPgto;
        }




        public DataTable SalvarCondPgto(CondPgtoModel pCondPgto)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_CondPgto_Salvar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Operacao", pCondPgto.id_operacao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Condicao", pCondPgto.Cod_Condicao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Descricao", pCondPgto.Descricao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Qtd_Dias", pCondPgto.Qtd_Dias);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Tipo_Vencimento", pCondPgto.Tipo_Vencimento);
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

        public DataTable ExcluirCondPgto(CondPgtoModel pCondPgto)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_CondPgto_Excluir");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Condicao", pCondPgto.Cod_Condicao);
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
