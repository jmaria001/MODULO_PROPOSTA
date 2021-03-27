using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace PROPOSTA
{
    public partial class ImpressaoCE
    {
        //===========================Listar Veículos - GeracaoCE
        public DataTable ImpressaoCeList(ImpressaoCeFiltroModel Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Impressao_Ce_List");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa_Faturamento", Param.Cod_Empresa_Faturamento);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia", clsLib.CompetenciaInt( Param.Competencia));
                if (String.IsNullOrEmpty(Param.Data_Processamento))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Processamento", DBNull.Value);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Processamento", Param.Data_Processamento.ConvertToDatetime());
                }
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Ce_Inicio", Param.Numero_Ce_Inicio);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Ce_Fim", Param.Numero_Ce_Fim);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa", Param.Cod_Empresa_Venda);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Mr", Param.Numero_Mr);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Sequencia_Mr", Param.Sequencia_Mr);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Fatura", Param.Numero_Fatura);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Agencia", Param.Agencia);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cliente", Param.Cliente);
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

