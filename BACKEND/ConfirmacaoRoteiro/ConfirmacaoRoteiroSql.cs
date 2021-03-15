using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace PROPOSTA
{
    public partial class ConfirmacaoRoteiro
    {
        //===========================Listar Veículos - Confirmacao do Roteiro
        public DataTable ConfirmacaoRoteiroListar(Int32 pParam)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_ConfirmacaoRoteiro_Listar");
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

        //===========================Confirmar Roteiro
        public List<VeiculosModel>  ConfirmaRoteiro(ConfirmacaoRoteiroModel pParam)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            String sData_Rede = pParam.Data_Confirmacao_Rede;
            String sData_Local = pParam.Data_Confirmacao_Local;
            SimLib clsLib = new SimLib();
            try
            {
                for (int i = 0; i < pParam.Veiculos.Count; i++)
                {
                    if (pParam.Veiculos[i].Indica_Marcado)
                    {
                        pParam.Veiculos[i].Critica = "";
                        pParam.Veiculos[i].Indica_Processado = false;
                        SqlDataAdapter Adp = new SqlDataAdapter();
                        DataTable dtb = new DataTable("dtb");
                        SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_ConfirmacaoRoteiro");
                        Adp.SelectCommand = cmd;
                        Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                        Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", pParam.Veiculos[i].Cod_Veiculo);
                        Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa", pParam.Veiculos[i].Cod_Empresa);
                        Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Confirmacao_Rede", pParam.Data_Confirmacao_Rede.ConvertToDatetime());
                        Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Confirmacao_Local", pParam.Data_Confirmacao_Local.ConvertToDatetime());
                        Adp.Fill(dtb);
                        if (dtb.Rows[0]["Status"].ToString().ConvertToBoolean())
                        {
                            pParam.Veiculos[i].Indica_Processado = true;
                            pParam.Veiculos[i].Critica = dtb.Rows[0]["Mensagem"].ToString();
                            pParam.Veiculos[i].Data_Confirmacao_Rede = sData_Rede;
                            pParam.Veiculos[i].Data_Confirmacao_Local = sData_Local;
                            pParam.Veiculos[i].Indica_Marcado = false;
                        }
                        else
                        {
                            pParam.Veiculos[i].Critica = dtb.Rows[0]["Mensagem"].ToString();
                        }
                        cmd.Dispose();
                        Adp.Dispose();
                        dtb.Dispose();
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
            return pParam.Veiculos;
        }

    }
}
