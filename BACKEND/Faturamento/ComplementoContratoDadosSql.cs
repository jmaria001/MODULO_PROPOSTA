using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace PROPOSTA
{

    public partial class ComplementoContratoDados
    {
        //===========================Listar Tabela de GetComplementoData
        public ComplementoContratoModel GetComplementoData(List<ComplementoContratoDados.ComplementoContratoModel> pFiltro)
        {
            Double Vlr_Fatura = 0;
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SimLib clsLib = new SimLib();
            ComplementoContratoModel dados = new ComplementoContratoModel();
            List<ComplementoMapasModel> ComplementoMapas = new List<ComplementoMapasModel>();
            List<RateioModel> Rateios = new List<RateioModel>();
            List<DuplicataModel> Duplicatas = new List<DuplicataModel>();
            try
            {
                for (int i = 0; i < pFiltro.Count; i++)
                {

                    DataTable dtb = new DataTable("dtb");
                    SqlCommand cmd = cmd = new SqlCommand();
                    

                    if (pFiltro[i].Origem==1) //-----Complemento de Contrato Midia 
                    {
                        cmd = cnn.Procedure(cnn.Connection, "sp_Pendente_Complemento");
                        cmd.Parameters.AddWithValue("@Par_Numero_Negociacao", DBNull.Value);
                        cmd.Parameters.AddWithValue("@Par_Cod_Empresa", pFiltro[i].Cod_Empresa);
                        cmd.Parameters.AddWithValue("@Par_Numero_Mr", pFiltro[i].Numero_Mr);
                        cmd.Parameters.AddWithValue("@Par_Sequencia_Mr", pFiltro[i].Sequencia_Mr);
                        cmd.Parameters.AddWithValue("@Par_Cod_Agencia", DBNull.Value);
                        cmd.Parameters.AddWithValue("@Par_Cod_Cliente", DBNull.Value);
                        cmd.Parameters.AddWithValue("@Par_Cod_Nucleo", DBNull.Value);
                        cmd.Parameters.AddWithValue("@Par_Cod_Contato", DBNull.Value);
                        cmd.Parameters.AddWithValue("@Par_Competencia", DBNull.Value);
                        cmd.Parameters.AddWithValue("@Par_Numero_Complemento", DBNull.Value);
                        cmd.Parameters.AddWithValue("@Par_Indica_Comprovado", DBNull.Value);
                        cmd.Parameters.AddWithValue("@Par_retorno", DBNull.Value);
                        cmd.Parameters.AddWithValue("@Par_Empresa_Faturamento", pFiltro[i].Cod_Empresa_Faturamento);
                    }
                    else // Antecipado 
                    {
                        cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Complemento_Pendente_Antecipado");
                        cmd.Parameters.AddWithValue("@Par_Negociacao", pFiltro[i].Numero_Negociacao);
                        cmd.Parameters.AddWithValue("@Par_Numero_Parcela", pFiltro[i].Numero_Parcela);

                    }
                    SqlDataAdapter Adp = new SqlDataAdapter(cmd);
                    Adp.Fill(dtb);
                    if (dtb.Rows.Count>0)
                    {
                        if (i==0) // carrega dados somente do 1. contrato e acumula o valor de todos 
                        {
                            dados.Cod_Empresa = dtb.Rows[0]["Cod_Empresa"].ToString();
                            dados.Numero_Negociacao = dtb.Rows[0]["Numero_Negociacao"].ToString().ConvertToInt32();
                            dados.Cod_Nucleo = dtb.Rows[0]["Cod_Nucleo"].ToString();
                            dados.Cod_Contato = dtb.Rows[0]["Cod_Contato"].ToString();
                            if (!string.IsNullOrEmpty(dtb.Rows[0]["Periodo_Inicial"].ToString()))
                            {
                                dados.Periodo_Inicial = dtb.Rows[0]["Periodo_Inicial"].ToString().ConvertToDatetime().ToString("dd/MM/yyyy");
                            }
                            if (!string.IsNullOrEmpty(dtb.Rows[0]["Periodo_Final"].ToString()))
                            {
                                dados.Periodo_Inicial = dtb.Rows[0]["Periodo_Final"].ToString().ConvertToDatetime().ToString("dd/MM/yyyy");
                            }
                            dados.Cod_Intermediario = "";
                            dados.Descricao = "";
                            dados.Forma_Pgto = dtb.Rows[0]["Forma_Pgto"].ToString().ConvertToInt32();
                            dados.Nome_Forma_Pgto = dtb.Rows[0]["Nome_Forma_Pgto"].ToString();
                            dados.Cod_Empresa_Faturamento = dtb.Rows[0]["Cod_Empresa_Faturamento"].ToString();
                            dados.Indica_Venda_Net = false;
                            dados.Indica_Faturamento_liquido = false;
                            dados.Numero_Mr = dtb.Rows[0]["Numero_Mr"].ToString().ConvertToInt32();
                            dados.Sequencia_Mr= dtb.Rows[0]["Sequencia_Mr"].ToString().ConvertToInt32();
                            dados.Id_Contrato= dtb.Rows[0]["Id_Contrato"].ToString().ConvertToInt32();
                            dados.Origem = dtb.Rows[0]["Origem"].ToString().ConvertToByte();
                            dados.Numero_Parcela = dtb.Rows[0]["Numero_Parcela"].ToString().ConvertToByte();
                            //=============================Adiciona o Rateio  -- no inicio tem apenas 1
                            Rateios.Add(new RateioModel
                            {
                                Id_Rateio = 0,
                                Numero_Rateio = 1,
                                Cod_Cliente = dtb.Rows[0]["Cod_Cliente"].ToString().TrimEnd(),
                                Cod_Agencia = dtb.Rows[0]["Cod_Agencia"].ToString().TrimEnd(),
                                Data_Emissao = DateTime.Now.ToString("dd/MM/yyyy"),
                                Cod_Condicao = dtb.Rows[0]["Cod_Condicao"].ToString().TrimEnd(),
                                Cod_Veiculo = "",
                                Indica_Log_Agencia = 1,
                                Indica_Log_Cliente = 1,
                                Referencia = "",
                                Perc_Rateio = "100"
                                
                            }); ;
                            };
                        Vlr_Fatura += dtb.Rows[0]["Vlr_A_Faturar"].ToString().ConvertToDouble();
                        
                        //-------------------------Seta Periodo
                        if (dtb.Rows[0]["Periodo_Inicial"].ToString().ConvertToDatetime()  < dados.Periodo_Inicial.ConvertToDatetime())
                        {
                            dados.Periodo_Inicial = dtb.Rows[0]["Periodo_Inicial"].ToString().ConvertToDatetime().ToString("dd/MM/yyyy");
                        }
                        if (dtb.Rows[0]["Periodo_Final"].ToString().ConvertToDatetime() > dados.Periodo_Final.ConvertToDatetime())
                        {
                            dados.Periodo_Final = dtb.Rows[0]["Periodo_Final"].ToString().ConvertToDatetime().ToString("dd/MM/yyyy");
                        }
                        //----------------------Adiciona Contratos
                        ComplementoMapas.Add(new ComplementoMapasModel
                        {
                            Cod_Empresa = dtb.Rows[0]["Cod_Empresa"].ToString(),
                            Numero_Mr = dtb.Rows[0]["Numero_Mr"].ToString().ConvertToInt32(),
                            Sequencia_Mr = dtb.Rows[0]["Sequencia_Mr"].ToString().ConvertToInt32(),
                            ContratoString = dtb.Rows[0]["Cod_Empresa"].ToString() + '-' + dtb.Rows[0]["Numero_Mr"].ToString() + '-' + dtb.Rows[0]["Sequencia_Mr"].ToString(),
                            Vlr_A_Faturar = dtb.Rows[0]["Vlr_A_Faturar"].ToString().ConvertToDouble(),
                            Id_Contrato = dtb.Rows[0]["Id_Contrato"].ToString().ConvertToInt32(),
                        }) ;
                    }
                    //if (Duplicatas.Count>0)
                    //{
                    //    Rateios[0].Duplicatas = Duplicatas;
                    //}
                    

                    dtb.Dispose();
                    cmd.Dispose();
                    Adp.Dispose();
                }
                //-------------------Seta o valor do Rateio e a primeira parcela / duplicata
                if (Rateios.Count>0)
                {

                
                Rateios[0].Vlr_A_Faturar = Vlr_Fatura.ToString().ConvertToMoney();
                Rateios[0].Perc_Rateio = Rateios[0].Perc_Rateio.ConvertToPercent();

                var _dtemissao = Rateios[0].Data_Emissao.ConvertToDatetime();
                var _vencimento_year = _dtemissao.Year;
                var _vencimento_month = _dtemissao.Month;
                DateTime _Vencimento_base =  new DateTime(_vencimento_year, _vencimento_month, DateTime.DaysInMonth(_vencimento_year, _vencimento_month));
                _Vencimento_base.AddDays(-1);
                var _Vencimento = _Vencimento_base.AddDays(15);
                Duplicatas.Add(new DuplicataModel()
                {
                    Id_Rateio = 0,
                    Id_Parcela = 1,
                    Parcela = 1,
                    Vencimento = _Vencimento.ToString("dd/MM/yyyy"),
                    Dia_Semana = ((int)_Vencimento.DayOfWeek).ToString(),
                    Valor = Rateios[0].Vlr_A_Faturar,

                    });
                Rateios[0].Duplicatas = Duplicatas;
                }
                //===================Complementa dados 

                dados.Vlr_A_Faturar = Vlr_Fatura;
                dados.Saldo_A_Faturar = 0;
                dados.ComplementoMapas = ComplementoMapas;
                dados.Rateios = Rateios;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cnn.Close();
            }
            return dados;
        }

        public DataTable SalvarComplemento(ComplementoContratoModel Complemento)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();

            String xmlComplementoMapas = null;
            String xmlRateios = null;
            if (Complemento.ComplementoMapas.Count > 0)
            {
                xmlComplementoMapas = clsLib.SerializeToString(Complemento.ComplementoMapas);
            }
            if (Complemento.Rateios.Count > 0)
            {
                xmlRateios = clsLib.SerializeToString(Complemento.Rateios);
            }

            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_PROPOSTA_Complemento_Salvar");
                Adp.SelectCommand = cmd;
                //Adp.SelectCommand.Parameters.AddWithValue("@Par_Operacao", ID_Contrato.Id_Operacao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Origem", Complemento.Origem);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa", Complemento.Cod_Empresa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa_Faturamento", Complemento.Cod_Empresa_Faturamento);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Id_Contrato", Complemento.Id_Contrato);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Negociacao", Complemento.Numero_Negociacao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Parcela", Complemento.Numero_Parcela);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Nucleo", Complemento.Cod_Nucleo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Intermediario", Complemento.Cod_Intermediario);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Contato", Complemento.Cod_Contato);
                if (!String.IsNullOrEmpty(Complemento.Periodo_Inicial))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Periodo_Inicial", Complemento.Periodo_Inicial.ConvertToDatetime());
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Periodo_Inicial", DBNull.Value);
                }
                if (!String.IsNullOrEmpty(Complemento.Periodo_Final))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Periodo_Final", Complemento.Periodo_Final.ConvertToDatetime());
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Periodo_Final", DBNull.Value);
                }
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Natureza", Complemento.Natureza_Servico);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Historico", Complemento.Cod_Historico);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Venda_Net", Complemento.Indica_Venda_Net);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Faturamento_Liquido", Complemento.Indica_Faturamento_liquido);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Forma_Pgto", Complemento.Forma_Pgto);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Valor_Fatura", Complemento.Vlr_A_Faturar);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Descricao", Complemento.Descricao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_ComplementoMapas", xmlComplementoMapas);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Rateios", xmlRateios);

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
        public DataTable GetNaturezaRegra(RegraNaturezaModel Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
                       try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "[Pr_Natureza_Servico_Complemento_Regra_S]");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa_Faturamento", Param.Cod_Empresa_Faturamento);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa", Param.Cod_Empresa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Mr", Param.Numero_Mr);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Sequencia_Mr", Param.Sequencia_Mr);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Negociacao", Param.Numero_Negociacao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Tipo", Param.Tipo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Tipo_Chamada", 0);
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