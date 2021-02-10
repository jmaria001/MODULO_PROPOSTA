using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace PROPOSTA
{

    public partial class GeracaoFatura
    {

        //===========================Listar Tabela de ContratosFaturaLista
        public DataTable ContratosFaturaLista(FiltroModel pFiltro)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "sp_Solicitacao_Fatura_V2");
                Adp.SelectCommand = cmd;

                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa_Faturamento", pFiltro.Emp_Faturamento);

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


        public DataTable IncluirSolicitacao(List<SolicitacaoFaturaModel> pContratos)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            String xmlContrato = null;
            if (pContratos.Count > 0) ///ja paraou de dar erro aqui no count, porque agora sim é uma lista
            {
                xmlContrato = clsLib.SerializeToString(pContratos);
            }

            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "sp_Insert_Solicitacao_Faturamento");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Contratos", xmlContrato);
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