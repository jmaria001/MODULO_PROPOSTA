using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace PROPOSTA
{
    public partial class CriticaValoracao
    {

        //---------------Get----------------
        public DataTable CriticaValoracaoGet(FiltroModel pParam)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_CriticaValoracao_Get");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                if (String.IsNullOrEmpty(pParam.Competencia))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia", DBNull.Value);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia", clsLib.CompetenciaInt(pParam.Competencia));
                }
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa_Faturamento", pParam.Cod_Empresa_Faturamento);
                if (String.IsNullOrEmpty(pParam.Cod_Empresa))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa", DBNull.Value);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa", pParam.Cod_Empresa);
                }
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Mr", pParam.Numero_Mr);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Sequencia_Mr", pParam.Sequencia_Mr);
                
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

