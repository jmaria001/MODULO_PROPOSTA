using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace PROPOSTA
{

    public partial class CaracVeicul
    {

        //===========================Listar Característica da Veiculação
        public DataTable CaracVeiculListar(Int32 pIdCaracVeicul)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_CaracVeicul_Listar");
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


        //===========================Salvar Característica da Veiculação
        public DataTable SalvarCaracVeicul(CaracVeiculModel pCaracVeicul)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_CaracVeicul_Salvar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Operacao", pCaracVeicul.id_operacao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Caracteristica", pCaracVeicul.Cod_Caracteristica);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Descricao", pCaracVeicul.Descricao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Imprime_Ce", pCaracVeicul.Imprime_Ce);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Posicao_Calculo", pCaracVeicul.Posicao_Calculo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Basket", pCaracVeicul.Indica_Basket);

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


        //===========================Get Característica da Veiculação
        public CaracVeiculModel GetCaracVeiculData(String pCodCaracteristica)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            CaracVeiculModel CaracVeicul = new CaracVeiculModel();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_CaracVeicul_Get");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Caracteristica", pCodCaracteristica);
                Adp.Fill(dtb);
                if (dtb.Rows.Count > 0)
                {
                    CaracVeicul.Cod_Caracteristica = dtb.Rows[0]["Cod_Caracteristica"].ToString();
                    CaracVeicul.Descricao = dtb.Rows[0]["Descricao"].ToString().Trim();
                    CaracVeicul.Imprime_Ce = dtb.Rows[0]["Imprime_Ce"].ToString();
                    CaracVeicul.Posicao_Calculo = dtb.Rows[0]["Posicao_Calculo"].ToString().ConvertToByte();
                    CaracVeicul.Indica_Basket = dtb.Rows[0]["Indica_Basket"].ToString().ConvertToBoolean();
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
            return CaracVeicul;
        }


        //===========================Excluir Característica da Veiculação
        public DataTable ExcluirCaracVeicul(CaracVeiculModel pCaracVeicul)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_CaracVeicul_Excluir");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Caracteristica", pCaracVeicul.Cod_Caracteristica);
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
