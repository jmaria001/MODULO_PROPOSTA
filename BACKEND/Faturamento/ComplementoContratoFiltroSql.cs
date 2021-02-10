using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace PROPOSTA
{

    public partial class ComplementoContratoFiltro
    {

        //===========================Listar Tabela de ContratosComplementoListar
        public DataTable ContratosComplementoMidiaListar(FiltroModel pFiltro)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "sp_Pendente_Complemento");
                Adp.SelectCommand = cmd;
                //Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                if (pFiltro.Numero_Negociacao == 0)
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Negociacao", DBNull.Value);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Negociacao", pFiltro.Numero_Negociacao);
                }
                if (String.IsNullOrEmpty(pFiltro.Empresa))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa", DBNull.Value);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa", pFiltro.Empresa);
                }
                if (pFiltro.Contrato == 0)
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Mr", DBNull.Value);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Mr", pFiltro.Contrato);
                }
                if (pFiltro.Sequencia == 0)
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Sequencia_Mr", DBNull.Value);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Sequencia_Mr", pFiltro.Sequencia);
                }
                if (String.IsNullOrEmpty(pFiltro.Agencia))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Agencia", DBNull.Value);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Agencia", pFiltro.Agencia);
                }
                if (String.IsNullOrEmpty(pFiltro.Cliente))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Cliente", DBNull.Value);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Cliente", pFiltro.Cliente);
                }
                if (String.IsNullOrEmpty(pFiltro.Nucleo))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Nucleo", DBNull.Value);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Nucleo", pFiltro.Nucleo);
                }
                if (String.IsNullOrEmpty(pFiltro.Contato))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Contato", DBNull.Value);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Contato", pFiltro.Contato);
                }
                if (String.IsNullOrEmpty (pFiltro.Competencia))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia", DBNull.Value);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia", clsLib.CompetenciaInt( pFiltro.Competencia));
                }
                if (pFiltro.Complemento == 0)
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Complemento", DBNull.Value);
                }
                else
                {
                    SqlDataAdapter adp = Adp;
                    adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Complemento", pFiltro.Complemento);
                }
                if (pFiltro.Ind_Comprovado == 0)
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Comprovado", DBNull.Value);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Comprovado", pFiltro.Ind_Comprovado);
                }
                if (pFiltro.Retorno == 0)
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Retorno", DBNull.Value);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Retorno", pFiltro.Retorno);
                }
                if (String.IsNullOrEmpty(pFiltro.Emp_Faturamento))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Empresa_Faturamento", DBNull.Value);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Empresa_Faturamento", pFiltro.Emp_Faturamento);
                }

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
        public DataTable ContratosComplementoAntecipadoListar(FiltroModel pFiltro)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Complemento_Pendente_Antecipado");
                Adp.SelectCommand = cmd;
                
                if (pFiltro.Numero_Negociacao == 0)
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Negociacao", DBNull.Value);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Negociacao", pFiltro.Numero_Negociacao);
                }
                if (String.IsNullOrEmpty(pFiltro.Empresa))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa_Faturamento", DBNull.Value);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa_Faturamento", pFiltro.Empresa);
                }
                if (String.IsNullOrEmpty(pFiltro.Agencia))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Agencia", DBNull.Value);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Agencia", pFiltro.Agencia);
                }
                if (String.IsNullOrEmpty(pFiltro.Cliente))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Cliente", DBNull.Value);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Cliente", pFiltro.Cliente);
                }
                if (String.IsNullOrEmpty(pFiltro.Competencia))
                { 
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia", DBNull.Value);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia", clsLib.CompetenciaInt(pFiltro.Competencia));
                }

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