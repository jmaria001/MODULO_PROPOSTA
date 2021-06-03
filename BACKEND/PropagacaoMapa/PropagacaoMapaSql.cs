using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace PROPOSTA
{

    public partial class PropagacaoMapa
    {
        //==========================Propagação do Mapa

        public DataTable CarregarPropagacaoMapa(FiltroModel pFiltro)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Propagacao_Mapa");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Usuario", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa", pFiltro.Cod_Empresa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Mr", pFiltro.Numero_Mr);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Sequencia_Mr", pFiltro.Sequencia_Mr);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia_Origem", clsLib.CompetenciaInt(pFiltro.Competencia));
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia_Destino_De", clsLib.CompetenciaInt(pFiltro.Competencia_Inicial));
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia_Destino_Ate", clsLib.CompetenciaInt(pFiltro.Competencia_Final));
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


        //public PropagacaoMapaModel CarregarPropagacaoMapa(FiltroModel pFiltro)
        //{
        //    clsConexao cnn = new clsConexao(this.Credential);
        //    cnn.Open();
        //    SqlDataAdapter Adp = new SqlDataAdapter();
        //    DataTable dtb = new DataTable("dtb");
        //    SimLib clsLib = new SimLib();

        //    PropagacaoMapaModel Retorno = new PropagacaoMapaModel();
        //    try
        //    {
        //        SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Propagacao_Mapa");
        //        Adp.SelectCommand = cmd;
        //        Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Usuario", this.CurrentUser);
        //        Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa", pFiltro.Cod_Empresa);
        //        Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Mr", pFiltro.Numero_Mr);
        //        Adp.SelectCommand.Parameters.AddWithValue("@Par_Sequencia_Mr", pFiltro.Sequencia_Mr);
        //        Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia_Origem", clsLib.CompetenciaInt(pFiltro.Competencia));
        //        Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia_Destino_De", clsLib.CompetenciaInt(pFiltro.Competencia_Inicial));
        //        Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia_Destino_Ate", clsLib.CompetenciaInt(pFiltro.Competencia_Final));
        //        Adp.Fill(dtb);

        //        foreach (DataRow drw in dtb.Rows)
        //        {
        //            Retorno.Competencia = drw["Competencia"].ToString().ConvertToInt32();
        //            Retorno.Mensagem_Status = drw["Mensagem_Status"].ToString();
        //        }


        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        cnn.Close();
        //    }
        //    return Retorno;
        //}

    }
}