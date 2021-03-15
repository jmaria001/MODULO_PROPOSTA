using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace PROPOSTA
{
    public partial class GeracaoCE
    {
        //===========================Listar Veículos - GeracaoCE
        public DataTable GeracaoCEListaVeiculos(Int32 pParam)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_GeracaoCE_ListaVeiculos");
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

        //---------------Geração de Comprovante / Carrega Contratos Pendentes----------------
        public DataTable GeraCE_CarregaPendentes(GeracaoCEModel Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            String xmlVeiculos = null;
            xmlVeiculos = clsLib.SerializeToString(Param.Veiculos);
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_GeracaoCE");
                Adp.SelectCommand = cmd;
                if (!String.IsNullOrEmpty(Param.Cod_Empresa))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@pPar_Cod_Empresa_Solicitada", Param.Cod_Empresa);
                }
                Adp.SelectCommand.Parameters.AddWithValue("@pPar_Data_Solicitada", Param.Data_Limite.ConvertToDatetime());
                Adp.SelectCommand.Parameters.AddWithValue("@pPar_Indica_Geracao", Param.Indica_Geracao);
                Adp.SelectCommand.Parameters.AddWithValue("@pPar_Veiculos", xmlVeiculos);
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

        //---------------Carrega Criticas----------------
        public DataTable Carrega_Criticas(String pEmp_Fat)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Carrega_Criticas");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa_Faturamento", pEmp_Fat);
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

