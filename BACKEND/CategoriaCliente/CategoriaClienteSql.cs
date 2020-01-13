using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace PROPOSTA
{

    public partial class CategoriaCliente
    {

        //===========================Listar Categoria do Cliente
        public DataTable CategoriaClienteListar(Int32 pIdCategoriaCliente)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_CategoriaCliente_Listar");
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




        //===========================Salvar Categoria do Cliente
        public DataTable SalvarCategoriaCliente(CategoriaClienteModel pCategoriaCliente)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_CategoriaCliente_Salvar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Operacao", pCategoriaCliente.id_operacao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Categoria", pCategoriaCliente.Cod_Categoria);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Descricao_Categoria", pCategoriaCliente.Descricao_Categoria);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Fiscal", pCategoriaCliente.Cod_Fiscal);

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


        //===========================Get Categoria do Cliente
        public CategoriaClienteModel GetCategoriaClienteData(Int32 pCodCategoria)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            CategoriaClienteModel CategoriaCliente = new CategoriaClienteModel();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_CategoriaCliente_Get");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Categoria", pCodCategoria);
                Adp.Fill(dtb);
                if (dtb.Rows.Count > 0)
                {
                    CategoriaCliente.Cod_Categoria = dtb.Rows[0]["Cod_Categoria"].ToString().ConvertToInt32();
                    CategoriaCliente.Descricao_Categoria = dtb.Rows[0]["Descricao_Categoria"].ToString().Trim();
                    CategoriaCliente.Cod_Fiscal = dtb.Rows[0]["Cod_Fiscal"].ToString();
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
            return CategoriaCliente;
        }



        //===========================Excluir Categoria do Cliente
        public DataTable ExcluirCategoriaCliente(CategoriaClienteModel pCategoriaCliente)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_CategoriaCliente_Excluir");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Categoria", pCategoriaCliente.Cod_Categoria);
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
