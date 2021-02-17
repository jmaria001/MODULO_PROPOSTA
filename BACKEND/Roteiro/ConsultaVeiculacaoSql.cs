using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace PROPOSTA
{

    public partial class ConsultaVeiculacao
    {

        //===========================Listar Tabela de Rateio Consulta
        public DataTable ConsultaVeiculacaoGet(FiltroConsultaVeiculacaoModel pFiltro)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_PROPOSTA_Consulta_Veiculacao");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", pFiltro.Veiculo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa", pFiltro.Empresa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Mr", pFiltro.Contrato);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Sequencia_Mr", pFiltro.Sequencia);
                if (!String.IsNullOrEmpty(pFiltro.Data_Inicio))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Periodo_Inicial", pFiltro.Data_Inicio.ConvertToDatetime());
                }
                if (!String.IsNullOrEmpty(pFiltro.Data_Termino))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Periodo_Final", pFiltro.Data_Termino.ConvertToDatetime());
                }

                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", pFiltro.Programa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Qualidade", pFiltro.Qualidade);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Net", pFiltro.Net);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Local   ", pFiltro.Local);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Baixado", pFiltro.Baixadas);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Nao_Baixado", pFiltro.NaoBaixadas);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Ordenado", pFiltro.Ordenadas);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Nao_Ordenado", pFiltro.NaoOrdenadas);
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