using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace PROPOSTA
{

    public partial class PesquisaFaturamento
    {

        //===========================Listar Tabela de FaturasListar
        public DataTable FaturasListar(FiltroModel pFiltro)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Sp_Pesquisa_Fatura");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                if (String.IsNullOrEmpty(pFiltro.Cod_Emp_Faturamento))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa_Faturamento", DBNull.Value);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa_Faturamento", pFiltro.Cod_Emp_Faturamento);
                }
                if (String.IsNullOrEmpty(pFiltro.Cod_Empresa))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa", DBNull.Value);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa", pFiltro.Cod_Empresa);
                }
                if (pFiltro.Contrato == 0)
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Mr", DBNull.Value);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Mr", pFiltro.Contrato);
                }
                if (pFiltro.Sequencia == 0)
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Sequencia_Mr", DBNull.Value);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Sequencia_Mr", pFiltro.Sequencia);
                }
                if (pFiltro.Numero_Negociacao == 0)
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Negociacao", DBNull.Value);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Negociacao", pFiltro.Numero_Negociacao);
                }
                if (String.IsNullOrEmpty(pFiltro.Competencia ))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia", DBNull.Value);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia", pFiltro.Competencia);
                }
                if (pFiltro.Nota_Fiscal == 0)
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Fatura", DBNull.Value);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Fatura", pFiltro.Nota_Fiscal);
                }
                
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Origem", pFiltro.Origem);
                
                if (String.IsNullOrEmpty(pFiltro.Cod_Agencia))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Agencia", DBNull.Value);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Agencia", pFiltro.Cod_Agencia);
                }
                if (String.IsNullOrEmpty(pFiltro.Cod_Cliente))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Cliente", DBNull.Value);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Cliente", pFiltro.Cod_Cliente);
                }
                if (pFiltro.Numero_Erp == 0)
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Selo_Fiscal", DBNull.Value);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Selo_Fiscal", pFiltro.Numero_Erp);
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
        public FaturaModel FaturaGet(FaturaModel pFatura)
        {
            FaturaModel Fatura = new FaturaModel();
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Fatura_Get");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa_Faturamento", pFatura.Cod_Empresa_Faturamento);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Fatura", pFatura.Numero_Fatura);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Origem", pFatura.Origem);
                Adp.Fill(dtb);
                Fatura.Cod_Empresa_Faturamento = dtb.Rows[0]["Cod_Empresa_Faturamento"].ToString();
                Fatura.Cod_Empresa= dtb.Rows[0]["Cod_Empresa"].ToString();
                Fatura.Numero_Fatura = dtb.Rows[0]["Numero_Fatura"].ToString().ConvertToInt32();
                Fatura.Numero_Erp = dtb.Rows[0]["Numero_Erp"].ToString().ConvertToInt32();
                Fatura.Numero_Negociacao = dtb.Rows[0]["Numero_Negociacao"].ToString().ConvertToInt32();
                Fatura.Numero_Rateio = dtb.Rows[0]["Numero_Rateio"].ToString().ConvertToInt32();
                Fatura.Cod_Nucleo = dtb.Rows[0]["Cod_Nucleo"].ToString().TrimEnd();
                Fatura.Nome_Nucleo = dtb.Rows[0]["Nome_Nucleo"].ToString().TrimEnd();
                Fatura.Cod_Contato = dtb.Rows[0]["Cod_Contato"].ToString().TrimEnd();
                Fatura.Nome_Contato = dtb.Rows[0]["Nome_Contato"].ToString().TrimEnd();
                Fatura.Cod_Agencia = dtb.Rows[0]["Cod_Agencia"].ToString();
                Fatura.Nome_Agencia = dtb.Rows[0]["Nome_Agencia"].ToString();
                Fatura.Cod_Cliente = dtb.Rows[0]["Cod_Cliente"].ToString();
                Fatura.Nome_Cliente = dtb.Rows[0]["Nome_Cliente"].ToString();
                Fatura.Valor_Bruto = dtb.Rows[0]["Valor_Bruto"].ToString().ConvertToDouble();
                Fatura.Valor_Comissao = dtb.Rows[0]["Valor_Comissao"].ToString().ConvertToDouble();
                Fatura.Valor_Liquido = dtb.Rows[0]["Valor_Liquido"].ToString().ConvertToDouble();
                Fatura.Valor_Iss = dtb.Rows[0]["Valor_Iss"].ToString().ConvertToDouble();
                Fatura.Valor_Intermediario = dtb.Rows[0]["Valor_Intermediario"].ToString().ConvertToDouble();
                Fatura.Cod_Natureza = dtb.Rows[0]["Cod_Natureza"].ToString();
                Fatura.Nome_Natureza = dtb.Rows[0]["Nome_Natureza"].ToString();
                Fatura.Percentual_Rateio = dtb.Rows[0]["Percentual_Rateio"].ToString().ConvertToDouble();
                Fatura.Tipo_Vencimento = dtb.Rows[0]["Tipo_Vencimento"].ToString();
                Fatura.Condicao_Nf = dtb.Rows[0]["Condicao_Nf"].ToString();
                Fatura.Referencia = dtb.Rows[0]["Referencia"].ToString();
                Fatura.Cod_Intermediario = dtb.Rows[0]["Cod_Intermediario"].ToString().TrimEnd();
                Fatura.Nome_Intermediario = dtb.Rows[0]["Nome_Intermediario"].ToString().TrimEnd();
                Fatura.Periodo_Inicial = dtb.Rows[0]["Periodo_Inicial"].ToString().ConvertToDatetime();
                Fatura.Periodo_Final = dtb.Rows[0]["Periodo_Final"].ToString().ConvertToDatetime();
                Fatura.Data_Emissao = dtb.Rows[0]["Data_Emissao"].ToString().ConvertToDatetime();
                Fatura.Data_Cancelamento = dtb.Rows[0]["Data_Cancelamento"].ToString().ConvertToDatetime();
                Fatura.Cod_Cancelamento = dtb.Rows[0]["Cod_Cancelamento"].ToString();
                Fatura.Obs_Cancelamento = dtb.Rows[0]["Obs_Cancelamento"].ToString();
                Fatura.Motivo_Cancelamento = dtb.Rows[0]["Motivo_Cancelamento"].ToString();
                Fatura.Duplicatas = AddDuplicatas(dtb.Rows[0]["Numero_Complemento"].ToString().ConvertToInt32(), dtb.Rows[0]["Numero_Rateio"].ToString().ConvertToInt32());
                Fatura.Composicao = AddComposicaoComplemento(dtb.Rows[0]["Numero_Complemento"].ToString().ConvertToInt32());
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cnn.Close();
            }
            return Fatura;
        }
        public List<DuplicataModel> AddDuplicatas(Int32 pComplemento, Int32 pRateio)
        {
            List<DuplicataModel> Duplicatas = new List<DuplicataModel>();
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Propostra_Rateio_Auxiliar_Get");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Complemento", pComplemento);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Rateio", pRateio);
                Adp.Fill(dtb);
                foreach (DataRow drw in dtb.Rows)
                {
                    Duplicatas.Add(new DuplicataModel()
                    {
                        Parcela= drw["Numero_Duplicata"].ToString().ConvertToInt32(),
                        Vencimento = drw["Data_Vencimento"].ToString().ConvertToDatetime(),
                        Dia_Semana = drw["Dia_Semana"].ToString(),
                        Valor = drw["Valor"].ToString().ConvertToDouble(),
                    });
                };
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cnn.Close();
            }
            return Duplicatas;
        }
        public List<ComposicaocomplementoModel> AddComposicaoComplemento(Int32 pComplemento)
        {
            List<ComposicaocomplementoModel> Composicao = new List<ComposicaocomplementoModel>();
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Propostra_Composicao_Complemento_Get");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Complemento", pComplemento);
                Adp.Fill(dtb);
                foreach (DataRow drw in dtb.Rows)
                {
                    Composicao.Add(new ComposicaocomplementoModel()
                    {
                        Numero_Mr = drw["Numero_Mr"].ToString().ConvertToInt32(),
                        Cod_Empresa = drw["Cod_Empresa"].ToString(),
                        Sequencia_Mr = drw["Sequencia_Mr"].ToString().ConvertToInt32(),
                        Numero_Complemento = drw["Numero_Complemento"].ToString().ConvertToInt32(),
                        Numero_Pi = drw["Numero_Pi"].ToString(),
                        Indica_Mestre_Grupo = drw["Indica_Mestre_Grupo"].ToString().ConvertToBoolean(),
                        Vlr_Considerado_Contrato = drw["Vlr_Considerado_Contrato"].ToString().ConvertToDouble(),
                    });
                };
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cnn.Close();
            }
            return Composicao;
        }
    }
}