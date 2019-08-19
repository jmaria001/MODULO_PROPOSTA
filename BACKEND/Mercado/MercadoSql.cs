using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace PROPOSTA
{

    public partial class Mercado
    {
        public DataTable MercadoListar(Int32 pIdMercado)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Mercado_Listar");
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

        public DataTable SalvarMercado(MercadoModel pMercado)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            String xmlVeiculo = null;
            if (pMercado.ListaVeiculo.Count > 0)
            {
                xmlVeiculo = clsLib.SerializeToString(pMercado.ListaVeiculo);
            }

            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Mercado_Salvar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Operacao", pMercado.id_operacao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Mercado", pMercado.Cod_Mercado);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Nome_Mercado", pMercado.Nome);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_JOVE", pMercado.Cod_JOVE);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Net", pMercado.Indica_Net);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_ListaVeiculo", xmlVeiculo);
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

        public DataTable ExcluirMercado(MercadoModel pMercado)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Mercado_Excluir");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Mercado", pMercado.Cod_Mercado);
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

        public MercadoModel GetMercadoData(String pCodMercado)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            DataTable dtbV = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            MercadoModel Mercado = new MercadoModel();
            List<VeiculoMercadoModel> ListaVeiculo = new List<VeiculoMercadoModel>();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_Proposta_Mercado_Get");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Mercado", pCodMercado);
                Adp.Fill(dtb);
                if (dtb.Rows.Count > 0)
                {
                    Mercado.Cod_Mercado = dtb.Rows[0]["Cod_Mercado"].ToString();
                    Mercado.Nome = dtb.Rows[0]["Nome"].ToString();
                    Mercado.Cod_JOVE = dtb.Rows[0]["Cod_JOVE"].ToString();
                    Mercado.Indica_Net = dtb.Rows[0]["Indica_Net"].ToString().ConvertToBoolean();

                    SqlCommand cmdV = cnn.Procedure(cnn.Connection, "PR_Proposta_Mercado_Veiculo_Get");
                    Adp.SelectCommand = cmdV;
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Mercado", pCodMercado);
                    Adp.Fill(dtbV);

                    foreach (DataRow drw  in dtbV.Rows)
                    {
                        ListaVeiculo.Add(new VeiculoMercadoModel() {
                            Cod_Veiculo = drw["Cod_Veiculo"].ToString(),
                            Nome_Veiculo = drw["Nome_Veiculo"].ToString(),
                            Selected = drw["Selected"].ToString().ConvertToBoolean()
                        });
                    }

                    Mercado.ListaVeiculo = ListaVeiculo;
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
            return Mercado;
        }
    }
}
