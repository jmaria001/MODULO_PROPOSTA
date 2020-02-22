using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace PROPOSTA
{

    public partial class Qualidade
    {
        public static string Cod_Qualidade { get; private set; }

        public DataTable QualidadeListar(Int32 pIdQualidade)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Qualidade_Listar");
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

        public DataTable SalvarQualidade(QualidadeModel pQualidade)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();

            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Qualidade_Salvar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Operacao", pQualidade.id_operacao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Qualidade", pQualidade.Cod_Qualidade);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Motivo_Falha", pQualidade.Cod_Motivo_Falha);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Descricao", pQualidade.Descricao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Am", pQualidade.Indica_Am);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Am_Futuro", pQualidade.Indica_Am_Futuro);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Calculo", pQualidade.Indica_Calculo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Comprovante", pQualidade.Indica_Comprovante);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Demanda", pQualidade.Indica_Demanda);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Horario", pQualidade.Indica_Horario);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Roteiro", pQualidade.Indica_Roteiro);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_CCW", pQualidade.Indica_CCW);
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

        public QualidadeModel GetQualidadeData(String pCod_Qualidade)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            QualidadeModel Qualidade = new QualidadeModel();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_Proposta_Qualidade_Get");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Qualidade", pCod_Qualidade);
                Adp.Fill(dtb);
                if (dtb.Rows.Count > 0)
                {
                    Qualidade.Cod_Qualidade = dtb.Rows[0]["Cod_Qualidade"].ToString();
                    Qualidade.Cod_Motivo_Falha = dtb.Rows[0]["Cod_Motivo_Falha"].ToString();
                    Qualidade.Descricao = dtb.Rows[0]["Descricao"].ToString();
                    Qualidade.Descricao_Motivo = dtb.Rows[0]["Descricao_Motivo"].ToString();
                    Qualidade.Indica_Am = dtb.Rows[0]["Indica_Am"].ToString().ConvertToBoolean();
                    Qualidade.Indica_Am_Futuro = dtb.Rows[0]["Indica_Am_Futuro"].ToString().ConvertToBoolean();
                    Qualidade.Indica_Calculo = dtb.Rows[0]["Indica_Calculo"].ToString().ConvertToBoolean();
                    Qualidade.Indica_Demanda = dtb.Rows[0]["Indica_Demanda"].ToString().ConvertToBoolean();
                    Qualidade.Indica_Comprovante = dtb.Rows[0]["Indica_Comprovante"].ToString().ConvertToBoolean();
                    Qualidade.Indica_Horario = dtb.Rows[0]["Indica_Horario"].ToString().ConvertToBoolean();
                    Qualidade.Indica_Roteiro = dtb.Rows[0]["Indica_Roteiro"].ToString().ConvertToBoolean();
                    Qualidade.Indica_CCW = dtb.Rows[0]["Indica_CCW"].ToString().ConvertToBoolean();
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
            return Qualidade;
        }

        public DataTable ExcluirQualidade(QualidadeModel pQualidade)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_QUALIDADE_Excluir");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Qualidade", pQualidade.Cod_Qualidade);
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