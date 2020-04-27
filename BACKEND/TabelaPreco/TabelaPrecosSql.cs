using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace PROPOSTA
{

    public partial class TabelaPrecos
    {

        //===========================Listar Tabela de Precos
        public DataTable TabelaPrecosListar(FiltroModel pFiltro)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_TabelaPrecos_Listar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia", clsLib.CompetenciaInt(pFiltro.Competencia));
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Veiculo", pFiltro.Veiculo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Programa", pFiltro.Programa);
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


        //===========================Salvar Itens de Permuta
        public DataTable SalvarTabelaPrecos(TabelaPrecosModel pTabelaPrecos)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_TabelaPrecos_Salvar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Operacao", pTabelaPrecos.Id_Operacao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia", pTabelaPrecos.Competencia);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Sequencia", pTabelaPrecos.Sequencia);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Tipo_Preco", pTabelaPrecos.Tipo_Preco);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", pTabelaPrecos.Cod_Programa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Valor", pTabelaPrecos.Valor);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo_Mercado", pTabelaPrecos.Cod_Veiculo_Mercado);
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


        //===========================Get Tabela Preços

        public TabelaPrecosModel GetTabelaPrecosData(String pCompetencia,String pCod_Programa,String pCod_Veiculo_Mercado)
        //public TabelaPrecosModel GetTabelaPrecosData(String pCompetencia)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            TabelaPrecosModel TabelaPrecos = new TabelaPrecosModel();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_TabelaPrecos_Get");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia", pCompetencia);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", pCod_Programa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo_Mercado", pCod_Veiculo_Mercado);
                Adp.Fill(dtb);
                if (dtb.Rows.Count > 0)
                {
                    TabelaPrecos.Competencia  = dtb.Rows[0]["Competencia"].ToString();
                    TabelaPrecos.Sequencia    = dtb.Rows[0]["Sequencia"].ToString().ConvertToInt32();
                    TabelaPrecos.Tipo_Preco   = dtb.Rows[0]["Tipo_Preco"].ToString();
                    TabelaPrecos.Cod_Programa = dtb.Rows[0]["Cod_Programa"].ToString();
                    TabelaPrecos.Titulo       = dtb.Rows[0]["Titulo"].ToString();
                    TabelaPrecos.Cod_Veiculo_Mercado = dtb.Rows[0]["Cod_Veiculo_Mercado"].ToString().Trim();
                    TabelaPrecos.Nome_Veiculo = dtb.Rows[0]["Nome_Veiculo"].ToString();
                    TabelaPrecos.Valor        = dtb.Rows[0]["Valor"].ToString();


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
            return TabelaPrecos;
        }



        //===========================Excluir Categoria do Cliente
        public DataTable ExcluirTabelaPrecos(TabelaPrecosModel pTabelaPrecos)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_TabelaPrecos_Excluir");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia", pTabelaPrecos.Competencia);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", pTabelaPrecos.Cod_Programa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo_Mercado", pTabelaPrecos.Cod_Veiculo_Mercado);
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
