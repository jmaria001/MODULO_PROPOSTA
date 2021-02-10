using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace PROPOSTA
{
    public partial class MateriaisFitas
    {
        //===========================Listar Materiais Fitas

        public DataTable MateriaisFitasListar(FiltroModel pFiltro)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Materiais_Listar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Agencia", pFiltro.Cod_Agencia);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Cliente", pFiltro.Cod_Cliente);

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

        public MateriaisFitasModel GetMateriaisFitasData(String pNumero_Fita)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            MateriaisFitasModel MateriaisFitas = new MateriaisFitasModel();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_Proposta_MateriaisFitas_Get");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Id_Fita", pNumero_Fita);

                Adp.Fill(dtb);
                if (dtb.Rows.Count > 0)
                {
                    MateriaisFitas.Tipo_Fita           = dtb.Rows[0]["Tipo_Fita"].ToString();
                    MateriaisFitas.Cod_Agencia         = dtb.Rows[0]["Cod_Agencia"].ToString();
                    MateriaisFitas.Nome_Agencia        = dtb.Rows[0]["Nome_Agencia"].ToString();
                    MateriaisFitas.Cod_Cliente         = dtb.Rows[0]["Cod_Cliente"].ToString();
                    MateriaisFitas.Nome_Cliente        = dtb.Rows[0]["Nome_Cliente"].ToString();
                    MateriaisFitas.Titulo              = dtb.Rows[0]["Titulo"].ToString();
                    MateriaisFitas.Cod_Red_Produto     = dtb.Rows[0]["Cod_Red_Produto"].ToString().ConvertToInt32();
                    MateriaisFitas.Descricao_Produto   = dtb.Rows[0]["Descricao_Produto"].ToString();
                    MateriaisFitas.Duracao             = dtb.Rows[0]["Duracao"].ToString().ConvertToInt32();
                    MateriaisFitas.Numero_Fita         = dtb.Rows[0]["Numero_Fita"].ToString();
                    MateriaisFitas.Cod_Veiculo         = dtb.Rows[0]["Cod_Veiculo"].ToString();
                    MateriaisFitas.Nome_Veiculo        = dtb.Rows[0]["Nome_Veiculo"].ToString();
                    MateriaisFitas.Cod_Tipo_Midia      = dtb.Rows[0]["Cod_Tipo_Midia"].ToString();
                    MateriaisFitas.Descricao_Midia     = dtb.Rows[0]["Descricao_Midia"].ToString();
                    MateriaisFitas.Cod_Tipo_Comercial  = dtb.Rows[0]["Cod_Tipo_Comercial"].ToString();
                    MateriaisFitas.Descricao_Comercial = dtb.Rows[0]["Descricao_Comercial"].ToString();





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
            return MateriaisFitas;
        }



        public DataTable SalvarMateriaisFitas(MateriaisFitasModel pMateriaisFitas)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();

            MateriaisFitasModel MateriaisFitas = new MateriaisFitasModel();

            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_MateriaisFitas_Salvar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Operacao", pMateriaisFitas.Id_operacao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Agencia", pMateriaisFitas.Cod_Agencia);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Cliente", pMateriaisFitas.Cod_Cliente);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Titulo", pMateriaisFitas.Titulo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Duracao", pMateriaisFitas.Duracao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Tipo_Midia", pMateriaisFitas.Cod_Tipo_Midia);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Red_Produto", pMateriaisFitas.Cod_Red_Produto);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Tipo_Comercial", pMateriaisFitas.Cod_Tipo_Comercial);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", pMateriaisFitas.Cod_Veiculo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Fita", pMateriaisFitas.Numero_Fita);
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

        // Definindo range fita

        public DataTable RangeFitaMateriais(MateriaisFitas.MateriaisFitasModel Param)
        {
            DataTable dtb = new DataTable();
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Range_Fita");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", Param.Cod_Veiculo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Tipo_Fita", Param.Tipo_Fita);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Tipo_Midia", Param.Tipo_Midia);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Tipo_Comercial", Param.Cod_Tipo_Comercial);
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


        public DataTable ExcluirMateriaisFitas(MateriaisFitasModel pMateriaisFitas)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_MateriaisFitas_Excluir");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Fita", pMateriaisFitas.Numero_Fita);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Agencia", pMateriaisFitas.Cod_Agencia);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Cliente", pMateriaisFitas.Cod_Cliente);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", pMateriaisFitas.Cod_Veiculo);

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