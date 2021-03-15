using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace PROPOSTA
{
    public partial class NaturezadeServico
    {
        //===========================Listar Materiais Fitas

        public DataTable NaturezadeServicoListar(FiltroModel pFiltro)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_NaturezadeServico_Listar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa_Faturamento", pFiltro.Cod_Empresa);
 
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

        public NaturezadeServicoModel GetNaturezadeServicoData(String Cod_Natureza,String Cod_Empresa)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            NaturezadeServicoModel NaturezadeServico = new NaturezadeServicoModel();

            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_Proposta_NaturezadeServico_Get");

                cmd.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                cmd.Parameters.AddWithValue("@Par_Cod_Natureza", Cod_Natureza);
                cmd.Parameters.AddWithValue("@Par_Cod_Empresa", Cod_Empresa);

                Adp.SelectCommand = cmd;
                Adp.Fill(dtb);

                if (dtb.Rows.Count > 0)
                {
                    NaturezadeServico.Cod_Natureza            = dtb.Rows[0]["Cod_Natureza"].ToString();
                    NaturezadeServico.Descricao               = dtb.Rows[0]["Descricao"].ToString();
                    NaturezadeServico.Cod_Empresa             = dtb.Rows[0]["Cod_Empresa"].ToString();
                    NaturezadeServico.Razao_Social            = dtb.Rows[0]["Razao_Social"].ToString();
                    NaturezadeServico.Cod_Atividade           = dtb.Rows[0]["Cod_Atividade"].ToString();
                    NaturezadeServico.Indica_Midia            = dtb.Rows[0]["Indica_Midia"].ToString().ConvertToBoolean();
                    NaturezadeServico.Percentual_Iss          = dtb.Rows[0]["Percentual_Iss"].ToString().ConvertToDouble();
                    NaturezadeServico.Indica_NFE              = dtb.Rows[0]["Indica_NFE"].ToString().ConvertToBoolean();
                    NaturezadeServico.Indica_NFE              = dtb.Rows[0]["Indica_NFEE"].ToString().ConvertToBoolean();
                    NaturezadeServico.Cod_Historico           = dtb.Rows[0]["Cod_Historico"].ToString();
                    NaturezadeServico.Perc_IR                 = dtb.Rows[0]["Perc_IR"].ToString().ConvertToDouble();
                    NaturezadeServico.Perc_CS                 = dtb.Rows[0]["Perc_CS"].ToString().ConvertToDouble();
                    NaturezadeServico.Perc_COFINS             = dtb.Rows[0]["Perc_COFINS"].ToString().ConvertToDouble();
                    NaturezadeServico.Perc_PIS                = dtb.Rows[0]["Perc_PIS"].ToString().ConvertToDouble();
                    NaturezadeServico.PERC_INSS               = dtb.Rows[0]["PERC_INSS"].ToString().ConvertToDouble();
                    NaturezadeServico.Data_Desativacao        = dtb.Rows[0]["Data_Desativacao"].ToString();
                    NaturezadeServico.Cod_Usuario_Desativacao = dtb.Rows[0]["Cod_Usuario_Desativacao"].ToString();
                    NaturezadeServico.Descricao_Historico     = dtb.Rows[0]["Descricao_Historico"].ToString();
                    NaturezadeServico.Indica_Desativado       = dtb.Rows[0]["Indica_Desativado"].ToString().ConvertToBoolean();

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
            return NaturezadeServico;
        }



        //public NaturezadeServicoModel GetDepositorioFitasData(String pCod_Natureza)
        //{
        //    clsConexao cnn = new clsConexao(this.Credential);
        //    cnn.Open();
        //    SqlDataAdapter Adp = new SqlDataAdapter();
        //    DataTable dtb = new DataTable("dtb");
        //    SimLib clsLib = new SimLib();
        //    NaturezadeServicoModel NaturezadeServico = new NaturezadeServicoModel();
        //    try
        //    {
        //        SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_Proposta_NaturezadeServico_Get");
        //        Adp.SelectCommand = cmd;
        //        Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
        //        Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Natureza", pCod_Natureza);


        //        Adp.Fill(dtb);
        //        if (dtb.Rows.Count > 0)
        //        {
        //            NaturezadeServico.Cod_Natureza            = dtb.Rows[0]["Cod_Natureza"].ToString();
        //            NaturezadeServico.Descricao               = dtb.Rows[0]["Descricao"].ToString();
        //            NaturezadeServico.Cod_Atividade           = dtb.Rows[0]["Cod_Atividade"].ToString();
        //            NaturezadeServico.Indica_Iss              = dtb.Rows[0]["Indica_Iss"].ToString().ConvertToBoolean();
        //            NaturezadeServico.Indica_Midia            = dtb.Rows[0]["Indica_Midia"].ToString().ConvertToBoolean();
        //            NaturezadeServico.Percentual_Iss          = dtb.Rows[0]["Percentual_Iss"].ToString().ConvertToDouble();
        //            NaturezadeServico.Indica_NFE              = dtb.Rows[0]["Indica_NFE"].ToString().ConvertToBoolean();
        //            NaturezadeServico.Indica_NFE              = dtb.Rows[0]["Indica_NFEE"].ToString().ConvertToBoolean();
        //            NaturezadeServico.Cod_Historico           = dtb.Rows[0]["Cod_Historico"].ToString();
        //            NaturezadeServico.Perc_IR                 = dtb.Rows[0]["Perc_IR"].ToString().ConvertToDouble();
        //            NaturezadeServico.Perc_CS                 = dtb.Rows[0]["Perc_CS"].ToString().ConvertToDouble();
        //            NaturezadeServico.Perc_COFINS             = dtb.Rows[0]["Perc_COFINS"].ToString().ConvertToDouble();
        //            NaturezadeServico.Perc_PIS                = dtb.Rows[0]["Perc_PIS"].ToString().ConvertToDouble();
        //            NaturezadeServico.PERC_INSS               = dtb.Rows[0]["PERC_INSS"].ToString().ConvertToDouble();
        //            NaturezadeServico.Data_Desativacao        = dtb.Rows[0]["Data_Desativacao"].ToString();
        //            NaturezadeServico.Cod_Usuario_Desativacao = dtb.Rows[0]["Cod_Usuario_Desativacao"].ToString();
        //            NaturezadeServico.Descricao_Historico     = dtb.Rows[0]["Descricao_Historico"].ToString();

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
        //    return NaturezadeServico;
        //}

        public DataTable SalvarNaturezadeServico(NaturezadeServicoModel pNaturezadeServico)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();

            NaturezadeServicoModel NaturezadeServico = new NaturezadeServicoModel();

            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_NaturezadeServico_Salvar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Operacao", pNaturezadeServico.Id_operacao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Natureza", pNaturezadeServico.Cod_Natureza);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa", pNaturezadeServico.Cod_Empresa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Descricao", pNaturezadeServico.Descricao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Atividade", pNaturezadeServico.Cod_Atividade);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Midia", pNaturezadeServico.Indica_Midia);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Percentual_Iss", pNaturezadeServico.Percentual_Iss);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_NFE", pNaturezadeServico.Indica_NFE);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_NFEE", pNaturezadeServico.Indica_NFEE);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Historico", pNaturezadeServico.Cod_Historico);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Perc_IR", pNaturezadeServico.Perc_IR);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Perc_CS", pNaturezadeServico.Perc_CS);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Perc_COFINS", pNaturezadeServico.Perc_COFINS);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Perc_PIS", pNaturezadeServico.Perc_PIS);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_PERC_INSS", pNaturezadeServico.PERC_INSS);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Desativacao", pNaturezadeServico.Data_Desativacao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Usuario_Desativacao", pNaturezadeServico.Cod_Usuario_Desativacao);
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
                         
        public DataTable ExcluirNaturezadeServico(NaturezadeServicoModel pNaturezadeServico)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_NaturezadeServico_Excluir");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Natureza", pNaturezadeServico.Cod_Natureza);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa", pNaturezadeServico.Cod_Empresa);

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

        //===========================Desativar/Reativar
        public DataTable DesativarReativarNaturezadeServico(NaturezadeServicoModel pNaturezadeServico)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_NaturezadeServico_Desativar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Natureza", pNaturezadeServico.Cod_Natureza);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa", pNaturezadeServico.Cod_Empresa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Id_Acao", pNaturezadeServico.Id_Acao);
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