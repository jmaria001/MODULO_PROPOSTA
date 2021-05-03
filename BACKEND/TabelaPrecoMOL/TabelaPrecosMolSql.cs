using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace PROPOSTA
{
    public partial class TabelaPrecosMol
    {

        //===========================Listar Tabela de Precos MOL
        public DataTable TabelaPrecosMolListar(FiltroMolModel pFiltro)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_TabelaPrecosMol_Listar");
                Adp.SelectCommand = cmd;
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

        //----------------------- Conta linhas do grid
        Int32 ContadorLinha = 0;

        //===========================Get Tabela Preços MOL
        public TabelaPrecosMolModel GetTabelaPrecosMolData(String pCompetencia, String pCod_Programa, String pCod_Veiculo_Mercado)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            TabelaPrecosMolModel TabelaPrecosMol = new TabelaPrecosMolModel();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_TabelaPrecosMol_Get");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia", pCompetencia);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", pCod_Programa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo_Mercado", pCod_Veiculo_Mercado);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Tipo_Param", "1");
                Adp.Fill(dtb);
                if (dtb.Rows.Count > 0)
                {
                    TabelaPrecosMol.Competencia = dtb.Rows[0]["Competencia"].ToString();
                    TabelaPrecosMol.Cod_Programa = dtb.Rows[0]["Cod_Programa"].ToString();
                    TabelaPrecosMol.Titulo = dtb.Rows[0]["Titulo"].ToString();
                    TabelaPrecosMol.Cod_Veiculo_Mercado = dtb.Rows[0]["Cod_Veiculo_Mercado"].ToString();
                    TabelaPrecosMol.Nome_Veiculo = dtb.Rows[0]["Nome_Veiculo"].ToString();
                    TabelaPrecosMol.ValoresMol = AddValoresMol(pCompetencia, pCod_Programa, pCod_Veiculo_Mercado);
                    TabelaPrecosMol.Max_Id_Linha = ContadorLinha;
                }
                else
                {
                    TabelaPrecosMol.Max_Id_Linha = 1;
                    List<ValoresMolModel> ValoresMol = new List<ValoresMolModel>();
                    ValoresMol.Add(new ValoresMolModel()
                    {
                        Id_Linha = 1,
                        Cod_Tipo_Comercializacao = "",
                        Nome_Comercializacao = "",
                        Valor = "0,00",
                    });
                    TabelaPrecosMol.ValoresMol = ValoresMol;
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
            return TabelaPrecosMol;
        }
        //----------------------- Adiciona Valores MOL do TabelaPrecosMol -------------------------
        private List<ValoresMolModel> AddValoresMol(String pCompetencia, String pCod_Programa, String pCod_Veiculo_Mercado)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            List<ValoresMolModel> ValoresMol = new List<ValoresMolModel>();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_TabelaPrecosMol_Get");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia", pCompetencia);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", pCod_Programa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo_Mercado", pCod_Veiculo_Mercado);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Tipo_Param", "2");
                Adp.Fill(dtb);
                foreach (DataRow drw in dtb.Rows)
                {
                    ContadorLinha++;
                    ValoresMol.Add(new ValoresMolModel()
                    {
                        Id_Linha = drw["Id_Linha"].ToString().ConvertToInt32(),
                        Cod_Tipo_Comercializacao = drw["Cod_Tipo_Comercializacao"].ToString(),
                        Nome_Comercializacao = drw["Nome_Comercializacao"].ToString(),
                        Valor = drw["Valor"].ToString()
                    });
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
            return ValoresMol;
        }







        //===========================Salvar
        public DataTable SalvarTabelaPrecosMol(TabelaPrecosMolModel pParam)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            String xmlValoresMOL = null;
            xmlValoresMOL = clsLib.SerializeToString(pParam.ValoresMol);
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_TabelaPrecosMol_Salvar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Operacao", pParam.Id_Operacao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia", pParam.Competencia);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", pParam.Cod_Programa.ToUpper());
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo_Mercado", pParam.Cod_Veiculo_Mercado.ToUpper());
                Adp.SelectCommand.Parameters.AddWithValue("@Par_ValoresMOL", xmlValoresMOL);
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





        //===========================Excluir Categoria do Cliente
        public DataTable ExcluirTabelaPrecosMol(TabelaPrecosMolModel pTabelaPrecosMol)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_TabelaPrecosMol_Excluir");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia", pTabelaPrecosMol.Competencia);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", pTabelaPrecosMol.Cod_Programa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo_Mercado", pTabelaPrecosMol.Cod_Veiculo_Mercado);
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
