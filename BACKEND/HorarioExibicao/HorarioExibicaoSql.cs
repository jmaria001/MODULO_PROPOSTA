using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace PROPOSTA
{
    public partial class HorarioExibicao
    {
        //===========================Horario Exibicao
        public DataTable HorarioExibicaoListar(FiltroModel pFiltro)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "[Pr_Proposta_Listar_HorarioExibicao]");
                Adp.SelectCommand = cmd;

                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", pFiltro.Cod_Veiculo);
                if (String.IsNullOrEmpty(pFiltro.Data_Exibicao))
                {

                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Exibicao", pFiltro.Data_Exibicao);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Exibicao", pFiltro.Data_Exibicao.ConvertToDatetime());

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

        public DataTable VeiculosListar(HorarioExibicaoModel Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            List<Veiculos_Model> Veiculos = new List<Veiculos_Model>();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_HorarioExibicao_Veiculo_Listar");
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

        public GravarModel SalvarHorarioExibicao(GravarModel pHoraExibicao)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SimLib clsLib = new SimLib();
            String xmlVeiculo = null;
            if (pHoraExibicao.Veiculos.Count > 0)
            {
                xmlVeiculo = clsLib.SerializeToString(pHoraExibicao.Veiculos);
            }

            try
            {
                for (int i = 0; i < pHoraExibicao.HorarioExibicao.Count; i++)
                {

                    //---------------------Limpa as critica da linha
                    pHoraExibicao.HorarioExibicao[i].Mensagem = "";
                    pHoraExibicao.HorarioExibicao[i].Status= true;


                    //---------------------Processa a Linha
                    SqlDataAdapter Adp = new SqlDataAdapter();
                    DataTable dtb = new DataTable("dtb");
                    SqlCommand cmd = cnn.Procedure(cnn.Connection, "[Pr_Proposta_HorarioExibicao_Salvar]");
                    Adp.SelectCommand = cmd;
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", pHoraExibicao.HorarioExibicao[i].Cod_Programa);
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", pHoraExibicao.HorarioExibicao[i].Cod_Veiculo);
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Exibicao", pHoraExibicao.HorarioExibicao[i].Data_Exibicao.ConvertToDatetime());

                    if (String.IsNullOrEmpty(pHoraExibicao.HorarioExibicao[i].Horario_Inicio_Real))
                    {
                        Adp.SelectCommand.Parameters.AddWithValue("@Par_Horario_Inicio_Real", DBNull.Value);
                    }
                    else
                    {
                        Adp.SelectCommand.Parameters.AddWithValue("@Par_Horario_Inicio_Real", pHoraExibicao.HorarioExibicao[i].Horario_Inicio_Real);
                    }

                    if (String.IsNullOrEmpty(pHoraExibicao.HorarioExibicao[i].Horario_Final_Real))
                    {
                        Adp.SelectCommand.Parameters.AddWithValue("@Par_Horario_Final_Real", DBNull.Value);
                    }
                    else
                    {
                        Adp.SelectCommand.Parameters.AddWithValue("@Par_Horario_Final_Real", pHoraExibicao.HorarioExibicao[i].Horario_Final_Real);
                    }
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Veiculos_Replicar", xmlVeiculo);
                    Adp.Fill(dtb);

                    if (String.IsNullOrEmpty(dtb.Rows[0]["Horario_Inicio_Real"].ToString()))
                    {
                        if (String.IsNullOrEmpty(dtb.Rows[0]["HoraErrado"].ToString()))
                        {
                            pHoraExibicao.HorarioExibicao[i].Mensagem = "";
                        }
                        else {

                            pHoraExibicao.HorarioExibicao[i].Mensagem = dtb.Rows[0]["Mensagem"].ToString();

                        }
                    }
                    else
                    {
                        pHoraExibicao.HorarioExibicao[i].Mensagem = dtb.Rows[0]["Mensagem"].ToString();
                    }


                    if (String.IsNullOrEmpty(dtb.Rows[0]["Horario_Final_Real"].ToString()))
                    {
                        if (String.IsNullOrEmpty(dtb.Rows[0]["HoraErrado"].ToString()))
                        {
                            pHoraExibicao.HorarioExibicao[i].Mensagem = "";
                        }
                        else
                        {

                            pHoraExibicao.HorarioExibicao[i].Mensagem = dtb.Rows[0]["Mensagem"].ToString();

                        }

                        // pHoraExibicao.HorarioExibicao[i].Mensagem = "";
                    }
                    else
                    {
                        pHoraExibicao.HorarioExibicao[i].Mensagem = dtb.Rows[0]["Mensagem"].ToString();
                    }

                    //pHoraExibicao.HorarioExibicao[i].Mensagem = dtb.Rows[0]["Mensagem"].ToString();
                    pHoraExibicao.HorarioExibicao[i].Status = dtb.Rows[0]["Status"].ToString().ConvertToBoolean();


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
            return pHoraExibicao;

        }


        public List<HorarioExibicaoModel> ReplicarHorarioExibicao(List<HorarioExibicaoModel> pVeiculos)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            pVeiculos[0].Qtd_Processado = 0;
            SimLib clsLib = new SimLib();

            int nCont = 0;
            pVeiculos[0].Mensagem = "";
            pVeiculos[0].Status = true;
            try
            {
                for (int i = 0; i < pVeiculos.Count; i++)
                {

                    nCont++;
                    //---------------------Limpa as critica da linha
                    //pHoraExibicao[i].Mensagem = "";
                    //pHoraExibicao[i].Status = true;


                    //---------------------Processa a Linha
                    SqlDataAdapter Adp = new SqlDataAdapter();
                    DataTable dtb = new DataTable("dtb");
                    SqlCommand cmd = cnn.Procedure(cnn.Connection, "[Pr_Proposta_ReplicarHorarioExibicao]");
                    Adp.SelectCommand = cmd;
                    if (String.IsNullOrEmpty(pVeiculos[i].Cod_Veiculo))
                    {
                        Adp.SelectCommand.Parameters.AddWithValue("Par_Cod_Veiculo", DBNull.Value);
                    }
                    else
                    {
                        Adp.SelectCommand.Parameters.AddWithValue("Par_Cod_Veiculo", pVeiculos[i].Cod_Veiculo);
                    }

                    Adp.Fill(dtb);


                    pVeiculos[i].Status = dtb.Rows[0]["Status"].ToString().ConvertToBoolean();
                    pVeiculos[i].Mensagem = dtb.Rows[0]["Mensagem"].ToString();

                    cmd.Dispose();
                    Adp.Dispose();
                    dtb.Dispose();

                }
                pVeiculos[0].Qtd_Processado = nCont;

                //SqlDataAdapter Adp2 = new SqlDataAdapter();
                //DataTable dtb2 = new DataTable("dtb");
                //SqlCommand cmd2 = cnn.Procedure(cnn.Connection, "[Pr_Proposta_Excluir_HorarioExibicao]");
                //Adp2.SelectCommand = cmd2;

                //Adp2.Fill(dtb2);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cnn.Close();
            }
            return pVeiculos;

        }

    }
}