using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace PROPOSTA
{
    public partial class CalculoValoracao
    {
        //===========================Validar Contrato
        public DataTable ValidarContrato(CalculoValoracaoModel pValidar)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_CalculoValoracao_ValidarContrato");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa", pValidar.Cod_Empresa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Mr", pValidar.Numero_Mr);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Sequencia_Mr", pValidar.Sequencia_Mr);

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

        //===========================Validar Negociação
        public DataTable ValidarNegociacao(CalculoValoracaoModel pValidarNego)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_CalculoValoracao_ValidarNegociacao");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Negociacao", pValidarNego.Numero_Negociacao);
                

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



        //===========================Valoração Negociacao
        public DataTable ValoracaoNegociacao(CalculoValoracaoModel pValidarNego)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open(); 
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_CalculoValoracao_Negociacao");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Negociacao", pValidarNego.Numero_Negociacao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia", clsLib.CompetenciaInt(pValidarNego.Competencia));

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

        //===========================Valoração Contratos
        public List<CalculoValoracao.CalculoValoracaoModel> ValoracaoContratos(List<CalculoValoracao.CalculoValoracaoModel> pContrato)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            
            try
            {
                for (int i = 0; i < pContrato.Count; i++)
                {
                    pContrato[i].Critica = "";
                    SqlDataAdapter Adp = new SqlDataAdapter();
                    SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_CalculoValoracao_ValoracaoContratos");
                    Adp.SelectCommand = cmd;
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa", pContrato[i].Cod_Empresa);
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Mr", pContrato[i].Numero_Mr);
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Sequencia_Mr", pContrato[i].Sequencia_Mr);

                    Adp.Fill(dtb);
                    if (dtb.Rows.Count > 0)
                    {
                        pContrato[i].Critica = dtb.Rows[0]["Mensagem"].ToString();
                    }
                    
                    cmd.Dispose();
                    Adp.Dispose();
                    dtb.Dispose();
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
            return pContrato;
        }
    }
}