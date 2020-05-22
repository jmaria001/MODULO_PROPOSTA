using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace PROPOSTA
{

    public partial class Produto
    {
        public static string Cod_Red_Produto { get; private set; }

        public DataTable ProdutoListar(ProdutoModel pFiltro)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Produto_Listar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Segmento", pFiltro.Segmento);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Setor", pFiltro.Setor);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Produto", pFiltro.Produto);
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

        public DataTable SalvarProduto(ProdutoModel pProduto)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();

            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Produto_Salvar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Operacao", pProduto.Operacao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Segmento", pProduto.Cod_Segmento);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Segmento", pProduto.Segmento);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Setor", pProduto.Cod_Setor);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Setor", pProduto.Setor);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Produto", pProduto.Cod_Produto);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Produto", pProduto.Produto);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Horario_Restricao", pProduto.Horario_Restricao);
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

        public ProdutoModel GetProdutoData(Int32 pProduto)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            ProdutoModel Produto = new ProdutoModel();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_Proposta_Produto_Get");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Produto", pProduto);
                Adp.Fill(dtb);
                if (dtb.Rows.Count > 0)
                {
                    Produto.Cod_Segmento = dtb.Rows[0]["Cod_Segmento"].ToString().ConvertToInt32();
                    Produto.Segmento = dtb.Rows[0]["Segmento"].ToString().TrimEnd();
                    Produto.Cod_Setor = dtb.Rows[0]["Cod_Setor"].ToString().ConvertToInt32();
                    Produto.Setor = dtb.Rows[0]["Setor"].ToString().TrimEnd();
                    Produto.Cod_Produto = dtb.Rows[0]["Cod_Produto"].ToString().ConvertToInt32();
                    Produto.Produto = dtb.Rows[0]["Produto"].ToString().TrimEnd();
                    Produto.Horario_Restricao = dtb.Rows[0]["Horario_Restricao"].ToString();
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
            return Produto;
        }
        public DataTable SetorListar(Int32 pCodSegmento)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            ProdutoModel Produto = new ProdutoModel();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Produto_Setor_List");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Segmento", pCodSegmento);
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

        public DataTable ExcluirProduto(ProdutoModel pProduto)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            Int32 pCod_Produto = 0;
            switch (pProduto.Operacao)
            {
                case "EditSegmento":
                    pCod_Produto = pProduto.Cod_Segmento;
                    break;
                case "EditSetor":
                    pCod_Produto = pProduto.Cod_Setor;
                    break;
                case "EditProduto":
                    pCod_Produto = pProduto.Cod_Produto;
                    break;
                default:
                    break;
            }
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Produto_Excluir");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Operacao", pProduto.Operacao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Red_Produto", pCod_Produto);
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