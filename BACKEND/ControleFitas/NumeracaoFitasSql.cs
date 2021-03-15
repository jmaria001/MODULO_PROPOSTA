using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace PROPOSTA
{

    public partial class NumeracaoFitas
    {
        //===========================Listar Fitas Avulsos
        public DataTable NumeracaoFitasListar(FiltroModel pFiltro)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Numeracao_Fita_List");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", pFiltro.Cod_Veiculo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", pFiltro.Cod_Programa);

                if (String.IsNullOrEmpty(pFiltro.Data_Inicio))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Inicio", DBNull.Value);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Inicio", pFiltro.Data_Inicio.ConvertToDatetime());
                }
                if (String.IsNullOrEmpty(pFiltro.Data_Final))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Fim", DBNull.Value);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Fim", pFiltro.Data_Final.ConvertToDatetime());
                }

                Adp.SelectCommand.Parameters.AddWithValue("@Par_Fita_Inicio", pFiltro.Numero_Fita_Inicio);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Fita_Fim", pFiltro.Numero_Fita_Fim);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Pendente", pFiltro.Indica_Pendentes_Numeracao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Numerada", pFiltro.Indica_Numeradas);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Ativa", pFiltro.Indica_Ativas);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Desativada", pFiltro.Indica_Desativadas_Devolvidas);

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

        public List<FiltroExibirVeiculoModel> ExibirVeiculosFitas(FiltroExibirVeiculoModel pFiltro)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            List<FiltroExibirVeiculoModel> NumeracaoFitas = new List<FiltroExibirVeiculoModel>();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_ExibirVeiculosFitas");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa", pFiltro.Cod_Empresa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Mr", pFiltro.Numero_Mr);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Sequencia_Mr", pFiltro.Sequencia_Mr);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Comercial", pFiltro.Cod_Comercial);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Tipo_Comercial", pFiltro.Cod_Tipo_Comercial);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Tipo_Midia", pFiltro.Cod_Tipo_Midia);
                Adp.Fill(dtb);
                foreach (DataRow drw in dtb.Rows)
                {
                    NumeracaoFitas.Add(new FiltroExibirVeiculoModel()
                    {

                        Cod_Veiculo = drw["Cod_Veiculo"].ToString(),
                        Cod_Empresa = drw["Cod_Empresa"].ToString(),
                        Numero_Mr = drw["Numero_Mr"].ToString().ConvertToInt32(),
                        Sequencia_Mr = drw["Sequencia_Mr"].ToString().ConvertToInt32(),
                        Cod_Comercial = drw["Cod_Comercial"].ToString(),
                        Cod_Tipo_Comercial = drw["Cod_Tipo_Comercial"].ToString(),
                        Cod_Tipo_Midia = drw["Cod_Tipo_Midia"].ToString(),
                        Duracao = drw["Duracao"].ToString().ConvertToInt32(),
                        Numero_Fita = drw["Numero_Fita"].ToString(),
                        Cod_Apresentador = drw["Cod_Apresentador"].ToString(),
                        Id_Numeracao = pFiltro.Id_Numeracao

                    });
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
            return NumeracaoFitas;
        }


        public DataTable RangeFitaNumeracao(NumeracaoFitas.FiltroExibirVeiculoModel Param)
        {
            DataTable dtb = new DataTable();
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Range_Fita");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", Param.Cod_Veiculo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Tipo_Fita", Param.Tipo_Fita);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Tipo_Midia", Param.Cod_Tipo_Midia);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Tipo_Comercial", Param.Cod_Tipo_Comercial);
                Adp.Fill(dtb);

                SqlCommand cmdDelete = cnn.Text(cnn.Connection, "Delete From Reserva_Fita Where Cod_Usuario = '" + this.CurrentUser + "'");
                cmdDelete.ExecuteNonQuery();
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

        public DataTable NumeracaoFitasApresentadores(String Cod_Apresentador)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_NumeracaoFitas_Apresentador");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Apresentador", Cod_Apresentador);
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
        public DataTable NumeracaoFitasValidarApresentador(String Cod_Apresentador)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_NumeracaoFitasValidar_Apresentador");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Apresentador", Cod_Apresentador);
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
        public List<FiltroExibirVeiculoModel> SalvarNumeracaoFitas(List<FiltroExibirVeiculoModel> pNumeracaoFitas)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SimLib clsLib = new SimLib();
            try
            {
                for (int i = 0; i < pNumeracaoFitas.Count; i++)
                {
                    //---------------------Limpa as critica da linha
                    pNumeracaoFitas[i].Mensagem = "";
                    pNumeracaoFitas[i].Status = false;
                    pNumeracaoFitas[i].Indica_Reutilizar = false;
                        //---------------------Processa a Linha
                        SqlDataAdapter Adp = new SqlDataAdapter();
                        DataTable dtb = new DataTable("dtb");
                        SqlCommand cmd = cnn.Procedure(cnn.Connection, "[Pr_Proposta_NumeracaoFitas_Salvar]");
                        Adp.SelectCommand = cmd;
                        clsLib.NewParameter(Adp, "@Par_Login", this.CurrentUser);
                        clsLib.NewParameter(Adp, "@Par_Cod_Empresa", pNumeracaoFitas[i].Cod_Empresa);
                        clsLib.NewParameter(Adp, "@Par_Numero_Mr", pNumeracaoFitas[i].Numero_Mr.ToString().ConvertToInt32());
                        clsLib.NewParameter(Adp, "@Par_Sequencia_Mr", pNumeracaoFitas[i].Sequencia_Mr.ToString().ConvertToInt32());
                        clsLib.NewParameter(Adp, "@Par_Cod_Comercial", pNumeracaoFitas[i].Cod_Comercial);
                        clsLib.NewParameter(Adp, "@Par_Cod_Veiculo", pNumeracaoFitas[i].Cod_Veiculo);
                        clsLib.NewParameter(Adp, "@Par_Duracao", pNumeracaoFitas[i].Duracao.ToString().ConvertToInt32());
                        clsLib.NewParameter(Adp, "@Par_Numero_Fita", pNumeracaoFitas[i].Numero_Fita);
                        clsLib.NewParameter(Adp, "@Par_Localizacao", pNumeracaoFitas[i].Localizacao);
                        clsLib.NewParameter(Adp, "@Par_Cod_Apresentador", pNumeracaoFitas[i].Cod_Apresentador);
                        clsLib.NewParameter(Adp, "@Par_Reutilizar", pNumeracaoFitas[i].Reutilizar);
                        Adp.Fill(dtb);
                        pNumeracaoFitas[i].Status = dtb.Rows[0]["Status"].ToString().ConvertToBoolean();
                        pNumeracaoFitas[i].Mensagem = dtb.Rows[0]["Mensagem"].ToString();
                        pNumeracaoFitas[i].Indica_Reutilizar = dtb.Rows[0]["Indica_Reutilizar"].ToString().ConvertToBoolean();
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
            return pNumeracaoFitas;
        }


        public void ExcluirNumeracaoFitas(NumeracaoFitasModel Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_NumeracaoFitas_Excluir");
                cmd.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                cmd.Parameters.AddWithValue("@Par_Cod_Empresa", Param.Cod_Empresa);
                cmd.Parameters.AddWithValue("@Par_Cod_Numero_Mr", Param.Numero_Mr);
                cmd.Parameters.AddWithValue("@Par_Cod_Sequencia_Mr", Param.Sequencia_Mr);
                cmd.Parameters.AddWithValue("@Par_Cod_Comercial", Param.Cod_Comercial);
                cmd.Parameters.AddWithValue("@Par_Cod_Veiculo", Param.Cod_Veiculo);
                cmd.Parameters.AddWithValue("@Par_Numero_Fita", Param.Numero_Fita);
                cmd.ExecuteNonQuery();
                
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cnn.Close();
            }
        }
    }
}