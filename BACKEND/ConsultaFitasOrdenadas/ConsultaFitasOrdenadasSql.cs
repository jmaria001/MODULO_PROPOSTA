using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace PROPOSTA
{
    public partial class ConsultaFitasOrdenadas
    {
        //===========================Consulta de Fitas Ordenadas
        public DataTable ConsultaFitasOrdenadasListar(FiltroModel pFiltro)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "[Pr_Proposta_Listar_ConsultaFitasOrdenadas]");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", pFiltro.Cod_Veiculo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", pFiltro.Cod_Programa);
                if (String.IsNullOrEmpty(pFiltro.Data_Inicio))
                {

                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Inicio", pFiltro.Data_Inicio);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Inicio", pFiltro.Data_Inicio.ConvertToDatetime());

                }

                if (String.IsNullOrEmpty(pFiltro.Data_Fim))
                {

                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Fim", pFiltro.Data_Fim);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Fim", pFiltro.Data_Fim.ConvertToDatetime());

                }
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Fita_Inicio", pFiltro.Numero_Fita_Inicio);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Fita_Fim", pFiltro.Numero_Fita_Fim);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa", pFiltro.Empresa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Mr", pFiltro.Numero_Mr.ToString().ConvertToInt32());
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Sequencia_Mr", pFiltro.Sequencia_Mr.ToString().ConvertToInt32());




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