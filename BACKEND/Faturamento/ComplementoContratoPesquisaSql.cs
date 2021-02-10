using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace PROPOSTA
{
    public partial class ComplementoContratoPesquisa
    {
        //===========================Listar Tabela de ComplementosPesquisar
        public DataTable ComplementosPesquisar(FiltroModel pFiltro)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Sp_PROPOSTA_Pendente_Pesquisa_Complemento");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Negociacao", pFiltro.Negociacao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Fatura", pFiltro.Fatura);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa_Faturamento", pFiltro.Cod_Empresa_Faturamento);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Mr", pFiltro.Contrato);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Sequencia_Mr", pFiltro.Sequencia);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Agencia", pFiltro.Agencia);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Cliente", pFiltro.Cliente);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Somente_Pendente", pFiltro.Indica_Somente_Pendente);
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
        public ComplementoModel ComplementosGet(Int32 pComplemento)
        {
            ComplementoModel Complemento = new ComplementoModel();

            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Propostra_Ci_Complementar_Get");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Complemento", pComplemento);
                Adp.Fill(dtb);
                if (dtb.Rows.Count>0)
                {

                
                Complemento.Cod_Empresa = dtb.Rows[0]["Cod_Empresa"].ToString();
                Complemento.Numero_Complemento = dtb.Rows[0]["Numero_Complemento"].ToString().ConvertToInt32();
                Complemento.Numero_Negociacao = dtb.Rows[0]["Numero_Negociacao"].ToString().ConvertToInt32();
                Complemento.Cod_Historico = dtb.Rows[0]["Cod_Historico"].ToString();
                Complemento.Cod_Natureza = dtb.Rows[0]["Cod_Natureza"].ToString();
                Complemento.Natureza_Servico = dtb.Rows[0]["Natureza_Servico"].ToString();
                Complemento.Cod_Contato = dtb.Rows[0]["Cod_Contato"].ToString();
                Complemento.Valor = dtb.Rows[0]["Valor"].ToString().ConvertToDouble();
                Complemento.Periodo_Inicial = dtb.Rows[0]["Periodo_Inicial"].ToString().ConvertToDatetime();
                Complemento.Periodo_Final = dtb.Rows[0]["Periodo_Final"].ToString().ConvertToDatetime();
                Complemento.Origem = dtb.Rows[0]["Origem"].ToString().ConvertToByte();
                Complemento.Data_Cadastramento = dtb.Rows[0]["Data_Cadastramento"].ToString().ConvertToDatetime();
                Complemento.Data_Cancelamento = dtb.Rows[0]["Data_Cancelamento"].ToString().ConvertToDatetime();
                Complemento.Descricao = dtb.Rows[0]["Descricao"].ToString();
                Complemento.Cod_Nucleo = dtb.Rows[0]["Cod_Nucleo"].ToString();
                Complemento.Cod_Usuario = dtb.Rows[0]["Cod_Usuario"].ToString();
                Complemento.Comissao_Agencia = dtb.Rows[0]["Comissao_Agencia"].ToString().ConvertToDouble();
                Complemento.Comissao_Intermediario = dtb.Rows[0]["Comissao_Intermediario"].ToString().ConvertToDouble();
                Complemento.Intermediario = dtb.Rows[0]["Nome_Intermediario"].ToString();
                Complemento.Nome_Intermediario = dtb.Rows[0]["Intermediario"].ToString();
                Complemento.Forma_Pgto = dtb.Rows[0]["Forma_Pgto"].ToString().ConvertToByte();
                Complemento.Descricao_Forma_Pgto = dtb.Rows[0]["Descricao_Forma_Pgto"].ToString();
                Complemento.Cod_Empresa_Faturamento = dtb.Rows[0]["Cod_Empresa_Faturamento"].ToString();
                Complemento.Indica_Faturamento_Liquido = dtb.Rows[0]["Indica_Faturamento_Liquido"].ToString().ConvertToBoolean();
                Complemento.Indica_Venda_Net = dtb.Rows[0]["Indica_Venda_Net"].ToString().ConvertToBoolean();
                Complemento.Cod_Usuario_Cancelar = dtb.Rows[0]["Cod_Usuario_Cancelar"].ToString();
                Complemento.ComposicaoComplemento = AddComposicaoComplemento(pComplemento);
                Complemento.Rateios= AddRateio(pComplemento);
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
            return Complemento;
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
                foreach (DataRow drw  in dtb.Rows)
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
        public List<RateioModel> AddRateio(Int32 pComplemento)
        {
            List<RateioModel> Rateio = new List<RateioModel>();
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Propostra_Rateio_Get");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Complemento", pComplemento);
                Adp.Fill(dtb);
                foreach (DataRow drw in dtb.Rows)
                {
                    Rateio.Add(new RateioModel()
                    {
                        Numero_Rateio = drw["Numero_Rateio"].ToString().ConvertToInt32(),
                        Condicao_Nf = drw["Condicao_Nf"].ToString().ConvertToInt32(),
                        Cod_Empresa = drw["Cod_Empresa"].ToString(),
                        Numero_Complemento = drw["Numero_Complemento"].ToString().ConvertToInt32(),
                        Numero_Negociacao = drw["Numero_Negociacao"].ToString().ConvertToInt32(),
                        Data_Emissao = drw["Data_Emissao"].ToString().ConvertToDatetime(),
                        Numero_Fatura = drw["Numero_Fatura"].ToString().ConvertToInt32(),
                        Origem = drw["Origem"].ToString().ConvertToByte(),
                        Referencia = drw["Referencia"].ToString(),
                        Tipo_Vencimento = drw["Tipo_Vencimento"].ToString(),
                        Vencimento_Nf = drw["Vencimento_Nf"].ToString().ConvertToDatetime(),
                        Vlr_Nf = drw["Vlr_Nf"].ToString().ConvertToDouble(),
                        Indica_Log_Cliente = drw["Indica_Log_Cliente"].ToString().ConvertToByte(),
                        Indica_Log_Agencia = drw["Indica_Log_Agencia"].ToString().ConvertToByte(),
                        Cod_Veiculo = drw["Cod_Veiculo"].ToString(),
                        Nome_Veiculo = drw["Nome_Veiculo"].ToString(),
                        Cod_Cliente = drw["Cod_Cliente"].ToString(),
                        Nome_Cliente = drw["Nome_Cliente"].ToString(),
                        Percentual_Rateio = drw["Percentual_Rateio"].ToString().ConvertToDouble(),
                        Cod_Agencia = drw["Cod_Agencia"].ToString(),
                        Nome_Agencia = drw["Nome_Agencia"].ToString(),
                        Data_Cadastramento = drw["Data_Cadastramento"].ToString().ConvertToDatetime(),
                        Cod_Usuario = drw["Cod_Usuario"].ToString(),
                        Cod_Cobranca = drw["Cod_Cobranca"].ToString(),
                        Cod_Endereco_Cobranca = drw["Cod_Endereco_Cobranca"].ToString().ConvertToByte(),
                        Duplicatas = AddDuplicatas(pComplemento,drw["Numero_Rateio"].ToString().ConvertToInt32())
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
            return Rateio;
        }
        public List<Rateio_AuxiliarModel> AddDuplicatas(Int32 pComplemento,Int32 pRateio)
        {
            List<Rateio_AuxiliarModel> Duplicatas = new List<Rateio_AuxiliarModel>();
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
                    Duplicatas.Add(new Rateio_AuxiliarModel()
                    {
                        Numero_Complemento = drw["Numero_Complemento"].ToString().ConvertToInt32(),
                        Numero_Rateio = drw["Numero_Rateio"].ToString().ConvertToInt32(),
                        Numero_Duplicata = drw["Numero_Duplicata"].ToString().ConvertToInt32(),
                        Data_Vencimento = drw["Data_Vencimento"].ToString().ConvertToDatetime(),
                        Dia_Semana= drw["Dia_Semana"].ToString(),
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

        public DataTable ExcluirComplemento(ComplementoModel pComplemento)
        {
            ComplementoModel Complemento = new ComplementoModel();

            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Complemento_Excluir");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Complemento", pComplemento.Numero_Complemento);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Origem", pComplemento.Origem);
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