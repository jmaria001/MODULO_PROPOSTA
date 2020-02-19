using CLASSDB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PROPOSTA
{
    public partial class Negociacao

    {
        public List<NegociacaoModel> NegociacaoList(NegociacaoFiltroParam Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            List<NegociacaoModel> Negociacoes = new List<NegociacaoModel>();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Negociacao_List");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Negociacao", Param.Numero_Negociacao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa_Venda", Param.Cod_Empresa_Venda);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa_Faturamento", Param.Cod_Empresa_Faturamento);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Tipo_Midia", Param.Cod_Tipo_Midia);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia_Inicio", clsLib.CompetenciaInt( Param.Competencia_Inicio));
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia_Fim", clsLib.CompetenciaInt(Param.Competencia_Fim));
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Agencia", Param.Agencia);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cliente", Param.Cliente);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Contato", Param.Contato);
                Adp.Fill(dtb);
                foreach (DataRow drw in dtb.Rows)
                {
                    Negociacoes.Add(new NegociacaoModel {
                        Numero_Negociacao = drw["Numero_Negociacao"].ToString().ConvertToInt32(),
                        Cod_Tipo_Midia = drw["Cod_Tipo_Midia"].ToString(),
                        Comissao_Agencia = drw["Comissao_Agencia"].ToString().ConvertToDouble(),
                        Competencia_Inicial= clsLib.CompetenciaString(drw["Competencia_Inicial"].ToString().ConvertToInt32()),
                        Competencia_Final =   clsLib.CompetenciaString(drw["Competencia_Final"].ToString().ConvertToInt32()),
                        Desconto_Real = drw["Desconto_Real"].ToString().ConvertToDouble(),
                        Forma_Pgto = drw["Forma_Pgto"].ToString(),
                        Tabela_Preco = drw["Tabela_Preco"].ToString(),
                        Verba_Negociada = drw["Verba_Negociada"].ToString().ConvertToDouble(),
                        Desconto_Concedido= drw["Desconto_Concedido"].ToString().ConvertToDouble(),
                        Valor_Tabela = drw["Valor_Tabela"].ToString().ConvertToDouble(),
                        Valor_Negociado = drw["Valor_Negociado"].ToString().ConvertToDouble(),
                        Empresas_Venda = AddEmpresaVenda(drw["Numero_Negociacao"].ToString().ConvertToInt32()),
                        Empresas_Faturamento= AddEmpresaFaturamento(drw["Numero_Negociacao"].ToString().ConvertToInt32()),
                        Agencias= AddAgencias(drw["Numero_Negociacao"].ToString().ConvertToInt32()),
                        Clientes= AddClientes(drw["Numero_Negociacao"].ToString().ConvertToInt32()),
                        Contatos = AddContatos(drw["Numero_Negociacao"].ToString().ConvertToInt32()),

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
            return Negociacoes;
        }
        private List<NegociacaoEmpresaVendaModel> AddEmpresaVenda(Int32 pNegociacao)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            List<NegociacaoEmpresaVendaModel> EmpresasVendas = new List<NegociacaoEmpresaVendaModel>();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Negociacao_Empresa_Venda_List");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Negociacao", pNegociacao);
                Adp.Fill(dtb);
                foreach (DataRow drw in dtb.Rows)
                {
                    EmpresasVendas.Add(new NegociacaoEmpresaVendaModel
                    {
                        Cod_Empresa = drw["Cod_Empresa"].ToString(),
                        Nome_Empresa = drw["Nome_Empresa"].ToString(),
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
            return EmpresasVendas;
        }
        private List<NegociacaoEmpresaFaturamentoModel> AddEmpresaFaturamento(Int32 pNegociacao)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            List<NegociacaoEmpresaFaturamentoModel> EmpresasFaturamentos = new List<NegociacaoEmpresaFaturamentoModel>();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Negociacao_Empresa_Faturamento_List");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Negociacao", pNegociacao);
                Adp.Fill(dtb);
                foreach (DataRow drw in dtb.Rows)
                {
                    EmpresasFaturamentos.Add(new NegociacaoEmpresaFaturamentoModel
                    {
                        Cod_Empresa = drw["Cod_Empresa"].ToString(),
                        Nome_Empresa = drw["Nome_Empresa"].ToString(),
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
            return EmpresasFaturamentos;

        }
        private List<NegociacaoAgenciaModel> AddAgencias(Int32 pNegociacao)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            List<NegociacaoAgenciaModel> Agencias  = new List<NegociacaoAgenciaModel>();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Negociacao_Agencia_List");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Negociacao", pNegociacao);
                Adp.Fill(dtb);
                foreach (DataRow drw in dtb.Rows)
                {
                    Agencias.Add(new NegociacaoAgenciaModel
                    {
                        Cod_Agencia= drw["Cod_Agencia"].ToString(),
                        Nome_Agencia= drw["Nome_Agencia"].ToString(),
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
            return Agencias;
        }
        private List<NegociacaoClienteModel> AddClientes(Int32 pNegociacao)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            List<NegociacaoClienteModel> Clientes = new List<NegociacaoClienteModel>();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Negociacao_Cliente_List");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Negociacao", pNegociacao);
                Adp.Fill(dtb);
                foreach (DataRow drw in dtb.Rows)
                {
                    Clientes.Add(new NegociacaoClienteModel
                    {
                        Cod_Cliente = drw["Cod_Cliente"].ToString(),
                        Nome_Cliente = drw["Nome_Cliente"].ToString(),
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
            return Clientes;
        }
        private List<NegociacaoContatoModel> AddContatos(Int32 pNegociacao)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            List<NegociacaoContatoModel> Contatos = new List<NegociacaoContatoModel>();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Negociacao_Contato_List");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Negociacao", pNegociacao);
                Adp.Fill(dtb);
                foreach (DataRow drw in dtb.Rows)
                {
                    Contatos.Add(new NegociacaoContatoModel
                    {
                        Cod_Contato = drw["Cod_Contato"].ToString(),
                        Nome_Contato = drw["Nome_Contato"].ToString(),
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
            return Contatos;
        }
        public  DataTable NegociacaoDetalhe(Int32 pNegociacao)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Negociacao_Contrato_List");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Negociacao", pNegociacao);
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
