using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace PROPOSTA
{

    public partial class BaixaSite
    {
        //===========================Carregar veiculacoes
        public DataTable CarregarVeiculacao(FiltroModel pFiltro)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Baixa_Site_Get");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", pFiltro.Cod_Veiculo);
                if (String.IsNullOrEmpty(pFiltro.Data_Exibicao))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Exibicao", DBNull.Value);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Exibicao", pFiltro.Data_Exibicao.ConvertToDatetime());
                }
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", pFiltro.Cod_Programa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa", pFiltro.Cod_Empresa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Mr", pFiltro.Numero_Mr);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Sequencia_Mr", pFiltro.Sequencia_Mr);
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

        //===========================baixar veiculacoes
        public List<BaixaModel> BaixarVeiculacao(List<BaixaModel> Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            
            SimLib clsLib = new SimLib();
            try
            {
                for (int i = 0; i < Param.Count; i++)
                {

                    SqlDataAdapter Adp = new SqlDataAdapter();
                    DataTable dtb = new DataTable("dtb");
                    Param[i].Status = false;
                    Param[i].Critica = "";
                    SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Baixa_Site_Salvar");
                    Adp.SelectCommand = cmd;
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", Param[i].Cod_Veiculo);
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Exibicao", Param[i].Data_Exibicao.ConvertToDatetime());
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", Param[i].Cod_Programa);
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa", Param[i].Cod_Empresa);
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Mr", Param[i].Numero_Mr);
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Sequencia_Mr", Param[i].Sequencia_Mr);
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Comercial", Param[i].Cod_Comercial);
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Qualidade", Param[i].Cod_Qualidade);
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Qtd_Previsto", Param[i].Qtd_Previsto);
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Qtd_Exibido", Param[i].Qtd_Exibido);
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Qtd_Falha", Param[i].Qtd_Falha);
                    Adp.Fill(dtb);
                    Param[i].Status = dtb.Rows[0]["Status"].ToString().ConvertToBoolean();
                    Param[i].Critica= dtb.Rows[0]["Mensagem"].ToString();
                    
                    dtb.Dispose();
                    Adp.Dispose();
                    cmd.Dispose();
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
            return Param;
        }
    }
}