using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace PROPOSTA
{

    public partial class ConsultaProgramacaoDiaria
    {
        //===========================Listar dados do cadastro
        public DataTable ConsultaProgramacaoDiariaListar(FiltroModel pFiltro)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Sp_Consulta_Programacao");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", pFiltro.Veiculo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Inicio", pFiltro.Data_Inicial.ConvertToDatetime());
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Fim", pFiltro.Data_Final.ConvertToDatetime());
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", pFiltro.Programa);
                if (pFiltro.Indica_Progs_Saldo_Zero)
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Saldo_Zerado", 1);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Saldo_Zerado", 0);
                }
                if (pFiltro.Indica_Progs_Saldo_Estou)
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Saldo_Estourado", 1);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Saldo_Estourado", 0);
                }
                if (pFiltro.Indica_Progs_Saldo_Posit)
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Saldo_Positivo", 1);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Saldo_Positivo", 0);
                }
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Sem_Disponibilidade", pFiltro.Par_Indica_Sem_Disponibilidade);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Invasao_Espaco", pFiltro.Par_Indica_Invasao_Espaco);
                if (pFiltro.Indica_Progs_Desativados)
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Desativado", 1);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Desativado", 0);
                }
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Programa", pFiltro.Par_Programa_chkVendaSP);
                if (pFiltro.Indica_Local)
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Local", 1);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Local", 0);
                }
                if (pFiltro.Indica_Net)
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Net", 1);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Net", 0);
                }
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Tipo_Retorno", pFiltro.Par_Tipo_Retorno);
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

        public DataTable ListarConsultaProgramacaoDiariaDetalhe(FiltroDetalheModel pFiltro2)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                //---------------Dados da Programacao Diaria
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Sp_Consulta_Programacao_Prevista");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", pFiltro2.Cod_Veiculo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Exibicao", pFiltro2.Data_Exibicao.ConvertToDatetime());
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", pFiltro2.Cod_Programa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Grade", pFiltro2.Indica_Grade);
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


