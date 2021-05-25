using CLASSDB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PROPOSTA
{
    public partial class PrevisaoVendas
    {
        public DataTable CarregarPrevisaoVendasAgencia(FiltroModel pParam,Boolean Mostra_Rascunho)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Carregar_Previsao_Agencia");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia", pParam.Competencia);
                if (pParam.Cod_Contato == "undefined")
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Contato", DBNull.Value);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Contato", pParam.Cod_Contato);
                }
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Mostra_Rascunho", Mostra_Rascunho);
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

        public DataTable CarregarPrevisaoVendasVeiculo(FiltroModel pParam,Boolean Mostra_Rascunho)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Carregar_Previsao_Veiculo");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia", pParam.Competencia);
                if (pParam.Cod_Contato == "undefined")
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Contato", DBNull.Value);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Contato", pParam.Cod_Contato);
                }
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Mostra_Rascunho", Mostra_Rascunho);
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


        public DataTable CarregarPrevisaoVendasMensal(FiltroModel pParam)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Carregar_Previsao_Mensal");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia", pParam.Competencia);
                if (pParam.Cod_Contato == "undefined")
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Contato", DBNull.Value);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Contato", pParam.Cod_Contato);
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

        public List<PrevisaVendasMensalModel> CarregarHistoricoMensal(List<PrevisaVendasMensalModel> pParam)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            Int32 _Ano = pParam[0].Ano - 1;
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Carregar_Previsao_Historico_Mensal");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia", _Ano);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Contato", pParam[0].Cod_Contato);
                Adp.Fill(dtb);
                foreach (DataRow drw in dtb.Rows)
                {
                    for (int i = 0; i < pParam.Count; i++)
                    {
                        if (drw["Competencia_Atual"].ToString().ConvertToInt32() == pParam[i].Competencia)
                        {
                            pParam[i].Valor_Negociado = drw["Valor_Negociado"].ToString().ConvertToMoney();
                        }

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
            return pParam;
        }

        public Boolean SalvarPrevisaoVendasMensal(List<PrevisaoVendas.PrevisaVendasMensalModel> pParam)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SimLib clsLib = new SimLib();
            Boolean Retorno = true;
            try
            {
                for (int i = 0; i < pParam.Count; i++)
                {
                    //---------------------Processa a Linha
                    if (pParam[i].Tipo_Linha == 1)
                    {
                        SqlDataAdapter Adp = new SqlDataAdapter();
                        DataTable dtb = new DataTable("dtb");
                        SqlCommand cmd = cnn.Procedure(cnn.Connection, "[Pr_Proposta_PrevisaoVendasMensal_Salvar]");
                        Adp.SelectCommand = cmd;
                        Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                        Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Contato", pParam[i].Cod_Contato);
                        Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia", pParam[i].Competencia.ToString().ConvertToInt32());
                        Adp.SelectCommand.Parameters.AddWithValue("@Par_Valor_Negociado", pParam[i].Valor_Previsao);
                        Adp.Fill(dtb);
                        Retorno = dtb.Rows[0]["Status"].ToString().ConvertToBoolean();
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
            return Retorno;

        }

        public List<PrevisaoVendasAgenciaModel> CarregarHistoricoAgencia(List<PrevisaoVendasAgenciaModel> pParam)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            Int32 _Ano = pParam[0].Ano - 1;
            Boolean bolAchouAgenciaCliente = false;
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Carregar_Previsao_Historico_Agencia");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia", _Ano);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Contato", pParam[0].Cod_Contato);
                Adp.Fill(dtb);
                var iLinhas = pParam.Count;
                foreach (DataRow drw in dtb.Rows)
                {
                    bolAchouAgenciaCliente = false;
                    //==================Se encontrar a agencia Atualiza a linha
                    for (int i = 0; i < iLinhas; i++)
                    {
                        //==================Se encontrar a agencia e o cliente adiciona alinha
                        if (drw["Cod_Agencia"].ToString().TrimEnd() == pParam[i].Cod_Agencia.TrimEnd() && drw["Cod_Cliente"].ToString().TrimEnd() == pParam[i].Cod_Cliente.TrimEnd())
                        {
                            pParam[i].Valor_Jan = drw["Valor_Jan"].ToString().ConvertToMoney();
                            pParam[i].Valor_Fev = drw["Valor_Fev"].ToString().ConvertToMoney();
                            pParam[i].Valor_Mar = drw["Valor_Mar"].ToString().ConvertToMoney();
                            pParam[i].Valor_Abr = drw["Valor_Abr"].ToString().ConvertToMoney();
                            pParam[i].Valor_Mai = drw["Valor_Mai"].ToString().ConvertToMoney();
                            pParam[i].Valor_Jun = drw["Valor_Jun"].ToString().ConvertToMoney();
                            pParam[i].Valor_Jul = drw["Valor_Jul"].ToString().ConvertToMoney();
                            pParam[i].Valor_Ago = drw["Valor_Ago"].ToString().ConvertToMoney();
                            pParam[i].Valor_Set = drw["Valor_Set"].ToString().ConvertToMoney();
                            pParam[i].Valor_Out = drw["Valor_Out"].ToString().ConvertToMoney();
                            pParam[i].Valor_Nov = drw["Valor_Nov"].ToString().ConvertToMoney();
                            pParam[i].Valor_Dez = drw["Valor_Dez"].ToString().ConvertToMoney();
                            pParam[i].Valor_Total = drw["Valor_Total"].ToString().ConvertToMoney();
                            pParam[i].Valor_Negociado = drw["Valor_Total"].ToString().ConvertToMoney();
                            bolAchouAgenciaCliente = true;
                        }
                    }
                    //==================Se nao encontrar a agencia adiciona  linha
                    if (!bolAchouAgenciaCliente)
                        {
                            pParam.Add(new PrevisaoVendasAgenciaModel()
                            {
                                Tipo_Linha = 1,
                                Ano = pParam[0].Ano,
                                Cod_Contato = drw["Cod_Contato"].ToString(),
                                Cod_Agencia = drw["Cod_Agencia"].ToString(),
                                Nome_Agencia = drw["Nome_Agencia"].ToString(),
                                Cod_Cliente = drw["Cod_Cliente"].ToString(),
                                Nome_Cliente = drw["Nome_Cliente"].ToString(),
                                Valor_Jan = drw["Valor_Jan"].ToString().ConvertToMoney(),
                                Valor_Fev = drw["Valor_Fev"].ToString().ConvertToMoney(),
                                Valor_Mar = drw["Valor_Mar"].ToString().ConvertToMoney(),
                                Valor_Abr = drw["Valor_Abr"].ToString().ConvertToMoney(),
                                Valor_Mai = drw["Valor_Mai"].ToString().ConvertToMoney(),
                                Valor_Jun = drw["Valor_Jun"].ToString().ConvertToMoney(),
                                Valor_Jul = drw["Valor_Jul"].ToString().ConvertToMoney(),
                                Valor_Ago = drw["Valor_Ago"].ToString().ConvertToMoney(),
                                Valor_Set = drw["Valor_Set"].ToString().ConvertToMoney(),
                                Valor_Out = drw["Valor_Out"].ToString().ConvertToMoney(),
                                Valor_Nov = drw["Valor_Nov"].ToString().ConvertToMoney(),
                                Valor_Dez = drw["Valor_Dez"].ToString().ConvertToMoney(),
                                Valor_Total = drw["Valor_Total"].ToString().ConvertToMoney(),
                                Valor_Negociado = drw["Valor_Total"].ToString().ConvertToMoney()
                            });
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
            return pParam;
        }

        public DataTable PrevisaoExcluirAgenciaCliente(PrevisaoVendasAgenciaModel pExcluirAgenciaCliente)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Previsao_Excluir_Agencia");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Ano", pExcluirAgenciaCliente.Ano);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Contato", pExcluirAgenciaCliente.Cod_Contato);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Agencia", pExcluirAgenciaCliente.Cod_Agencia);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Cliente", pExcluirAgenciaCliente.Cod_Cliente);
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

        public DataTable SalvarPrevisaoVendasAgencia(List<PrevisaoVendas.PrevisaoVendasAgenciaModel> pParam)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SimLib clsLib = new SimLib();
            DataTable dtbConsiste = new DataTable("dtvConsiste");
            try
            {
                for (int i = 0; i < pParam.Count; i++)
                {
                    //---------------------Processa a Linha
                    if (pParam[i].Tipo_Linha == 1 && !String.IsNullOrEmpty(pParam[i].Cod_Agencia ) && !String.IsNullOrEmpty(pParam[i].Cod_Cliente))
                    {
                        SqlDataAdapter Adp = new SqlDataAdapter();
                        DataTable dtb = new DataTable("dtb");
                        SqlCommand cmd = cnn.Procedure(cnn.Connection, "[Pr_Proposta_PrevisaoVendasAgencia_Salvar]");
                        Adp.SelectCommand = cmd;
                        Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                        Adp.SelectCommand.Parameters.AddWithValue("@Par_Ano", pParam[i].Ano);
                        Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Contato", pParam[i].Cod_Contato);
                        Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Agencia", pParam[i].Cod_Agencia);
                        Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Cliente", pParam[i].Cod_Cliente);
                        Adp.SelectCommand.Parameters.AddWithValue("@Par_Valor_Jan", pParam[i].Valor_Jan);
                        Adp.SelectCommand.Parameters.AddWithValue("@Par_Valor_Fev", pParam[i].Valor_Fev);
                        Adp.SelectCommand.Parameters.AddWithValue("@Par_Valor_Mar", pParam[i].Valor_Mar);
                        Adp.SelectCommand.Parameters.AddWithValue("@Par_Valor_Abr", pParam[i].Valor_Abr);
                        Adp.SelectCommand.Parameters.AddWithValue("@Par_Valor_Mai", pParam[i].Valor_Mai);
                        Adp.SelectCommand.Parameters.AddWithValue("@Par_Valor_Jun", pParam[i].Valor_Jun);
                        Adp.SelectCommand.Parameters.AddWithValue("@Par_Valor_Jul", pParam[i].Valor_Jul);
                        Adp.SelectCommand.Parameters.AddWithValue("@Par_Valor_Ago", pParam[i].Valor_Ago);
                        Adp.SelectCommand.Parameters.AddWithValue("@Par_Valor_Set", pParam[i].Valor_Set);
                        Adp.SelectCommand.Parameters.AddWithValue("@Par_Valor_Out", pParam[i].Valor_Out);
                        Adp.SelectCommand.Parameters.AddWithValue("@Par_Valor_Nov", pParam[i].Valor_Nov);
                        Adp.SelectCommand.Parameters.AddWithValue("@Par_Valor_Dez", pParam[i].Valor_Dez);

                        Adp.Fill(dtb);
                        cmd.Dispose();
                        Adp.Dispose();
                        dtb.Dispose();
                    }
                }
                //-----------------Consiste Valores Agencia x Valores Mensal
                SqlCommand cmdConsiste = cnn.Procedure(cnn.Connection, "[Pr_Proposta_PrevisaoVendas_Consiste_Valores]");
                SqlDataAdapter AdpConsiste = new SqlDataAdapter();
                cmdConsiste.Parameters.AddWithValue("@Par_Ano", pParam[0].Ano);
                cmdConsiste.Parameters.AddWithValue("@Par_Cod_Contato", pParam[0].Cod_Contato);
                cmdConsiste.Parameters.AddWithValue("@Par_Tipo", "A");
                AdpConsiste.SelectCommand = cmdConsiste;
                AdpConsiste.Fill(dtbConsiste);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                cnn.Close();
            }
            return dtbConsiste;

        }

        public Boolean CarregarPrevisaoConsisteCompetencia(FiltroModel pFiltro)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            Boolean Retorno = false;
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Previsao_Consiste_Competencia");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Ano", pFiltro.Competencia);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Contato", pFiltro.Cod_Contato);
                Adp.Fill(dtb);
                if (dtb.Rows.Count > 0)
                {
                    Retorno = true;
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
            return Retorno;
        }

        //// Definindo Veículo ////

        public List<PrevisaoVendasVeiculoModel> CarregarHistoricoVeiculo(List<PrevisaoVendasVeiculoModel> pParam)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            Int32 _Ano = pParam[0].Ano - 1;
            Boolean bolAchouVeiculo = false;
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Carregar_Previsao_Historico_Veiculo");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia", _Ano);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Contato", pParam[0].Cod_Contato);
                Adp.Fill(dtb);
                var iLinhas = pParam.Count;
                foreach (DataRow drw in dtb.Rows)
                {
                    bolAchouVeiculo = false;
                    //==================Se encontrar a agencia Atualiza a linha
                    for (int i = 0; i < iLinhas; i++)
                    {
                        //==================Se encontrar a agencia e o cliente adiciona alinha
                        if (drw["Cod_Veiculo"].ToString().TrimEnd() == pParam[i].Cod_Veiculo.TrimEnd())
                        {
                            pParam[i].Valor_Jan = drw["Valor_Jan"].ToString().ConvertToMoney();
                            pParam[i].Valor_Fev = drw["Valor_Fev"].ToString().ConvertToMoney();
                            pParam[i].Valor_Mar = drw["Valor_Mar"].ToString().ConvertToMoney();
                            pParam[i].Valor_Abr = drw["Valor_Abr"].ToString().ConvertToMoney();
                            pParam[i].Valor_Mai = drw["Valor_Mai"].ToString().ConvertToMoney();
                            pParam[i].Valor_Jun = drw["Valor_Jun"].ToString().ConvertToMoney();
                            pParam[i].Valor_Jul = drw["Valor_Jul"].ToString().ConvertToMoney();
                            pParam[i].Valor_Ago = drw["Valor_Ago"].ToString().ConvertToMoney();
                            pParam[i].Valor_Set = drw["Valor_Set"].ToString().ConvertToMoney();
                            pParam[i].Valor_Out = drw["Valor_Out"].ToString().ConvertToMoney();
                            pParam[i].Valor_Nov = drw["Valor_Nov"].ToString().ConvertToMoney();
                            pParam[i].Valor_Dez = drw["Valor_Dez"].ToString().ConvertToMoney();
                            pParam[i].Valor_Total = drw["Valor_Total"].ToString().ConvertToMoney();
                            pParam[i].Valor_Negociado = drw["Valor_Total"].ToString().ConvertToMoney();
                            bolAchouVeiculo = true;
                        }
                    }
                    //==================Se nao encontrar Veiculo adiciona  linha
                    if (!bolAchouVeiculo)
                    {
                        pParam.Add(new PrevisaoVendasVeiculoModel()
                        {
                            Tipo_Linha = 1,
                            Ano = pParam[0].Ano,
                            Cod_Veiculo = drw["Cod_Veiculo"].ToString(),
                            Nome_Veiculo = drw["Nome_Veiculo"].ToString(),
                            Valor_Jan = drw["Valor_Jan"].ToString().ConvertToMoney(),
                            Valor_Fev = drw["Valor_Fev"].ToString().ConvertToMoney(),
                            Valor_Mar = drw["Valor_Mar"].ToString().ConvertToMoney(),
                            Valor_Abr = drw["Valor_Abr"].ToString().ConvertToMoney(),
                            Valor_Mai = drw["Valor_Mai"].ToString().ConvertToMoney(),
                            Valor_Jun = drw["Valor_Jun"].ToString().ConvertToMoney(),
                            Valor_Jul = drw["Valor_Jul"].ToString().ConvertToMoney(),
                            Valor_Ago = drw["Valor_Ago"].ToString().ConvertToMoney(),
                            Valor_Set = drw["Valor_Set"].ToString().ConvertToMoney(),
                            Valor_Out = drw["Valor_Out"].ToString().ConvertToMoney(),
                            Valor_Nov = drw["Valor_Nov"].ToString().ConvertToMoney(),
                            Valor_Dez = drw["Valor_Dez"].ToString().ConvertToMoney(),
                            Valor_Total = drw["Valor_Total"].ToString().ConvertToMoney(),
                            Valor_Negociado = drw["Valor_Total"].ToString().ConvertToMoney()
                        });
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
            return pParam;
        }

        public DataTable PrevisaoExcluirVeiculo(PrevisaoVendasVeiculoModel pExcluirAgenciaCliente)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Previsao_Excluir_Veiculo");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Ano", pExcluirAgenciaCliente.Ano);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Contato", pExcluirAgenciaCliente.Cod_Contato);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", pExcluirAgenciaCliente.Cod_Veiculo);
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
                       
        public DataTable SalvarPrevisaoVendasVeiculo(List<PrevisaoVendas.PrevisaoVendasVeiculoModel> pParam)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SimLib clsLib = new SimLib();
            DataTable dtbConsiste = new DataTable("dtbConsiste");
            try
            {
                for (int i = 0; i < pParam.Count; i++)
                {
                    //---------------------Processa a Linha
                    if (pParam[i].Tipo_Linha == 1)
                    {
                        SqlDataAdapter Adp = new SqlDataAdapter();
                        DataTable dtb = new DataTable("dtb");
                        SqlCommand cmd = cnn.Procedure(cnn.Connection, "[Pr_Proposta_PrevisaoVendasVeiculo_Salvar]");
                        Adp.SelectCommand = cmd;
                        Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                        Adp.SelectCommand.Parameters.AddWithValue("@Par_Ano", pParam[i].Ano);
                        Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Contato", pParam[i].Cod_Contato);
                        Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", pParam[i].Cod_Veiculo);
                        Adp.SelectCommand.Parameters.AddWithValue("@Par_Valor_Jan", pParam[i].Valor_Jan);
                        Adp.SelectCommand.Parameters.AddWithValue("@Par_Valor_Fev", pParam[i].Valor_Fev);
                        Adp.SelectCommand.Parameters.AddWithValue("@Par_Valor_Mar", pParam[i].Valor_Mar);
                        Adp.SelectCommand.Parameters.AddWithValue("@Par_Valor_Abr", pParam[i].Valor_Abr);
                        Adp.SelectCommand.Parameters.AddWithValue("@Par_Valor_Mai", pParam[i].Valor_Mai);
                        Adp.SelectCommand.Parameters.AddWithValue("@Par_Valor_Jun", pParam[i].Valor_Jun);
                        Adp.SelectCommand.Parameters.AddWithValue("@Par_Valor_Jul", pParam[i].Valor_Jul);
                        Adp.SelectCommand.Parameters.AddWithValue("@Par_Valor_Ago", pParam[i].Valor_Ago);
                        Adp.SelectCommand.Parameters.AddWithValue("@Par_Valor_Set", pParam[i].Valor_Set);
                        Adp.SelectCommand.Parameters.AddWithValue("@Par_Valor_Out", pParam[i].Valor_Out);
                        Adp.SelectCommand.Parameters.AddWithValue("@Par_Valor_Nov", pParam[i].Valor_Nov);
                        Adp.SelectCommand.Parameters.AddWithValue("@Par_Valor_Dez", pParam[i].Valor_Dez);

                        Adp.Fill(dtb);
                        cmd.Dispose();
                        Adp.Dispose();
                        dtb.Dispose();
                    }
                }
                //-----------------Consiste Valores Veiculos x Valores Mensal
                SqlCommand cmdConsiste = cnn.Procedure(cnn.Connection, "[Pr_Proposta_PrevisaoVendas_Consiste_Valores]");
                SqlDataAdapter AdpConsiste = new SqlDataAdapter();
                cmdConsiste.Parameters.AddWithValue("@Par_Ano", pParam[0].Ano);
                cmdConsiste.Parameters.AddWithValue("@Par_Cod_Contato", pParam[0].Cod_Contato);
                cmdConsiste.Parameters.AddWithValue("@Par_Tipo", "V");
                AdpConsiste.SelectCommand = cmdConsiste;
                AdpConsiste.Fill(dtbConsiste);

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                cnn.Close();
            }
            return dtbConsiste;

        }

    }
}