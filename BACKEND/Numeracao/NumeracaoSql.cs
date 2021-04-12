using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace PROPOSTA
{

    public partial class Numeracao
    {


        public DataTable NumeracaoListar()
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Numeracao_Listar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.Fill(dtb);
                foreach (DataRow drw in dtb.Rows)
                {
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
            return dtb;
        }


        public NumeracaoModel GetNumeracaoData(String Cod_Empresa)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            NumeracaoModel Numeracao = new NumeracaoModel();

            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_Proposta_Numeracao_Get");

                cmd.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                cmd.Parameters.AddWithValue("@Par_Cod_Empresa", Cod_Empresa);

                Adp.SelectCommand = cmd;
                Adp.Fill(dtb);

                if (dtb.Rows.Count > 0)
                {
                    Numeracao.Competencia_Nova = dtb.Rows[0]["Competencia_Nova"].ToString();


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
            return Numeracao;
        }

        public DataTable SalvarNumeracao(NumeracaoModel pNumeracao)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Numeracao_Salvar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Operacao", pNumeracao.Id_operacao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa", pNumeracao.Cod_Empresa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia_Vigente", clsLib.CompetenciaInt(pNumeracao.Competencia_Vigente));
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero", pNumeracao.Numero);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia_Nova", clsLib.CompetenciaInt(pNumeracao.Competencia_Nova));

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
        public List<NumeracaoModel> ConfirmarFechamento(List<NumeracaoModel> pNumeracao)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            
            SimLib clsLib = new SimLib();
            try
            {
                for (int i = 0; i < pNumeracao.Count; i++)
                {
                    pNumeracao[i].Status = false;
                    pNumeracao[i].Critica = "";
                    if (pNumeracao[i].Selected)
                    {
                        SqlDataAdapter Adp = new SqlDataAdapter();
                        DataTable dtb = new DataTable("dtb");
                        SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Numeracao_Salvar");
                        Adp.SelectCommand = cmd;
                        Adp.SelectCommand.Parameters.AddWithValue("@Par_Operacao", "E");
                        Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                        Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa", pNumeracao[i].Cod_Empresa);
                        Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia_Nova", clsLib.CompetenciaInt(pNumeracao[i].Competencia_Nova));
                        Adp.Fill(dtb);
                        pNumeracao[i].Status = dtb.Rows[0]["Status"].ToString().ConvertToBoolean();
                        pNumeracao[i].Critica = dtb.Rows[0]["Mensagem"].ToString();
                        
                        if (dtb.Rows[0]["Status"].ToString().ConvertToBoolean())
                        {
                            pNumeracao[i].Cod_Usuario = this.CurrentUser;
                            pNumeracao[i].Selected = false;
                        }
                        dtb.Dispose();
                        Adp.Dispose();
                    }
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
            return pNumeracao;
        }



    }
}
