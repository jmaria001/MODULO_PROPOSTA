using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace PROPOSTA
{

    public partial class TipoMidia
    {
        public static string Cod_Tipo_Midia { get; private set; }

        public DataTable TipoMidiaListar(Int32 pIdTipoMidia)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Tipo_Midia_Listar");
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

        public DataTable SalvarTipoMidia(TipoMidiaModel pTipoMidia)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Tipo_Midia_Salvar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Operacao", pTipoMidia.id_operacao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Tipo_Midia", pTipoMidia.Cod_Tipo_Midia);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Descricao", pTipoMidia.Descricao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Fatura_Antecipada", pTipoMidia.Fatura_Antecipada);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Gera_Receita", pTipoMidia.Gera_Receita);


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

        public TipoMidiaModel GetTipoMidiaData(String pCod_Tipo_Midia)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            TipoMidiaModel TipoMidia = new TipoMidiaModel();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_Proposta_Tipo_Midia_Get");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Tipo_Midia", pCod_Tipo_Midia);
                Adp.Fill(dtb);
                if (dtb.Rows.Count > 0)
                {
                    TipoMidia.Cod_Tipo_Midia      = dtb.Rows[0]["Cod_Tipo_Midia"].ToString();
                    TipoMidia.Descricao           = dtb.Rows[0]["Descricao"].ToString();
                    TipoMidia.Fatura_Antecipada   = dtb.Rows[0]["Fatura_Antecipada"].ToString().ConvertToBoolean();
                    TipoMidia.Gera_Receita        = dtb.Rows[0]["Gera_Receita"].ToString().ConvertToBoolean();
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
            return TipoMidia;
        }

        public DataTable excluirtipomidia(TipoMidiaModel pTipoMidia)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Tipo_Midia_Excluir");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Tipo_Midia", pTipoMidia.Cod_Tipo_Midia);
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
