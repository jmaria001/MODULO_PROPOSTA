using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace PROPOSTA
{

    public partial class Determinacao
    {
        public DataTable CarregarDados(FiltroModel Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();

            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Determinacao_Dados");
                cmd.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                cmd.Parameters.AddWithValue("@Par_Cod_Empresa", Param.Cod_Empresa);
                cmd.Parameters.AddWithValue("@Par_Numero_Mr", Param.Numero_Mr);
                cmd.Parameters.AddWithValue("@Par_Sequencia_Mr", Param.Sequencia_Mr);
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
        



    }
}