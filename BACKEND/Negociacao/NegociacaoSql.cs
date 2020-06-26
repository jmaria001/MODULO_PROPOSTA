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
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia_Inicio", clsLib.CompetenciaInt(Param.Competencia_Inicio));
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia_Fim", clsLib.CompetenciaInt(Param.Competencia_Fim));
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Agencia", Param.Agencia);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cliente", Param.Cliente);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Contato", Param.Contato);
                Adp.Fill(dtb);
                foreach (DataRow drw in dtb.Rows)
                {
                    Negociacoes.Add(new NegociacaoModel
                    {
                        Numero_Negociacao = drw["Numero_Negociacao"].ToString().ConvertToInt32(),
                        Cod_Tipo_Midia = drw["Cod_Tipo_Midia"].ToString(),
                        Comissao_Agencia = drw["Comissao_Agencia"].ToString().ConvertToDouble(),
                        Competencia_Inicial = clsLib.CompetenciaString(drw["Competencia_Inicial"].ToString().ConvertToInt32()),
                        Competencia_Final = clsLib.CompetenciaString(drw["Competencia_Final"].ToString().ConvertToInt32()),
                        Desconto_Real = drw["Desconto_Real"].ToString().ConvertToDouble(),
                        Forma_Pgto = drw["Forma_Pgto"].ToString(),
                        Tabela_Preco = drw["Tabela_Preco"].ToString(),
                        Verba_Negociada = drw["Verba_Negociada"].ToString().ConvertToDouble(),
                        Desconto_Concedido = drw["Desconto_Concedido"].ToString().ConvertToDouble(),
                        Valor_Tabela = drw["Valor_Tabela"].ToString().ConvertToDouble(),
                        Valor_Negociado = drw["Valor_Negociado"].ToString().ConvertToDouble(),
                        Data_Desativacao = drw["Data_Desativacao"].ToString(),
                        Empresas_Venda = AddEmpresaVenda(drw["Numero_Negociacao"].ToString().ConvertToInt32()),
                        Empresas_Faturamento = AddEmpresaFaturamento(drw["Numero_Negociacao"].ToString().ConvertToInt32()),
                        Agencias = AddAgencias(drw["Numero_Negociacao"].ToString().ConvertToInt32()),
                        Clientes = AddClientes(drw["Numero_Negociacao"].ToString().ConvertToInt32()),
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
        public NegociacaoModel NegociacaoGet(NegociacaoModel Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            NegociacaoModel Negociacao = new NegociacaoModel();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Negociacao_Get");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Negociacao", Param.Numero_Negociacao);
                Adp.Fill(dtb);
                if (dtb.Rows.Count > 0)
                {
                    DataRow drw = dtb.Rows[0];
                    Negociacao.Numero_Negociacao = drw["Numero_Negociacao"].ToString().ConvertToInt32();
                    Negociacao.Competencia_Inicial = clsLib.CompetenciaString(drw["Competencia_Inicial"].ToString().ConvertToInt32());
                    Negociacao.Competencia_Final = clsLib.CompetenciaString(drw["Competencia_Final"].ToString().ConvertToInt32());
                    Negociacao.Tabela_Preco = drw["Tabela_Preco"].ToString();
                    Negociacao.Sequencia_Tabela = drw["Sequencia_Tabela"].ToString().ConvertToByte();
                    Negociacao.Verba_Negociada_String = drw["Verba_Negociada"].ToString().ConvertToMoney();
                    Negociacao.Desconto_Concedido_String = drw["Desconto_Concedido"].ToString().ConvertToPercent();
                    Negociacao.Patrocinio_Evento = drw["Patrocinio_Evento"].ToString();
                    Negociacao.Nome_Evento = drw["Nome_Evento"].ToString();
                    Negociacao.Comissao_Agencia_String = drw["Comissao_Agencia"].ToString().ConvertToPercent();
                    Negociacao.Cod_Forma_Pgto = drw["Cod_Forma_Pgto"].ToString().ConvertToByte();
                    Negociacao.Forma_Pgto = drw["Forma_Pgto"].ToString();
                    Negociacao.Cod_Tipo_Midia = drw["Cod_Tipo_Midia"].ToString();
                    Negociacao.Nome_Tipo_Midia = drw["Cod_Tipo_Midia"].ToString();
                    Negociacao.Data_Desativacao = drw["Data_Desativacao"].ToString();
                    Negociacao.Percentual_Reaplicacao = drw["Percentual_Reaplicacao"].ToString().ConvertToPercent();
                    Negociacao.Valor_Reaplicacao = drw["Valor_Reaplicacao"].ToString().ConvertToMoney();
                    Negociacao.Tabela_Reaplicacao = clsLib.CompetenciaString(drw["Tabela_Reaplicacao"].ToString().ConvertToInt32());
                    Negociacao.Sequencia_Tabela_Reaplicacao = drw["Sequencia_Reaplicacao"].ToString().ConvertToByte();
                    Negociacao.Desconto_Reaplicacao = drw["Desconto_Reaplicacao"].ToString().ConvertToPercent();

                    Negociacao.Empresas_Venda = AddEmpresaVenda(drw["Numero_Negociacao"].ToString().ConvertToInt32());
                    Negociacao.Empresas_Faturamento = AddEmpresaFaturamento(drw["Numero_Negociacao"].ToString().ConvertToInt32());
                    Negociacao.Agencias = AddAgencias(drw["Numero_Negociacao"].ToString().ConvertToInt32());
                    Negociacao.Clientes = AddClientes(drw["Numero_Negociacao"].ToString().ConvertToInt32());
                    Negociacao.Nucleos= AddNucleos(drw["Numero_Negociacao"].ToString().ConvertToInt32());
                    Negociacao.Contatos = AddContatos(drw["Numero_Negociacao"].ToString().ConvertToInt32());
                    Negociacao.Intermediarios = AddIntermediarios(drw["Numero_Negociacao"].ToString().ConvertToInt32());
                    Negociacao.Apresentadores= AddApresentadores(drw["Numero_Negociacao"].ToString().ConvertToInt32());
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
            return Negociacao;
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
            List<NegociacaoAgenciaModel> Agencias = new List<NegociacaoAgenciaModel>();
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
                        Cod_Agencia = drw["Cod_Agencia"].ToString(),
                        Nome_Agencia = drw["Nome_Agencia"].ToString(),
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
        private List<NegociacaoNucleoModel> AddNucleos(Int32 pNegociacao)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            List<NegociacaoNucleoModel> Nucleos = new List<NegociacaoNucleoModel>();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Negociacao_Nucleo_List");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Negociacao", pNegociacao);
                Adp.Fill(dtb);
                foreach (DataRow drw in dtb.Rows)
                {
                    Nucleos.Add(new NegociacaoNucleoModel
                    {
                        Cod_Nucleo = drw["Cod_Nucleo"].ToString(),
                        Nome_Nucleo = drw["Nome_Nucleo"].ToString(),
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
            return Nucleos;
        }
        private List<NegociacaoIntermediarioModel> AddIntermediarios(Int32 pNegociacao)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            List<NegociacaoIntermediarioModel> Intermediarios = new List<NegociacaoIntermediarioModel>();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Negociacao_Intermediario_List");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Negociacao", pNegociacao);
                Adp.Fill(dtb);
                foreach (DataRow drw in dtb.Rows)
                {
                    Intermediarios.Add(new NegociacaoIntermediarioModel
                    {
                        Cod_Intermediario = drw["Cod_Intermediario"].ToString(),
                        Nome_Intermediario = drw["Nome_Intermediario"].ToString(),
                        Comissao = drw["Comissao"].ToString(),
                        //Tipo_Intermediario = drw["Tipo_Intermediario"].ToString(),
                        //Nome_Tipo_Intermediario = drw["Nome_Tipo_Intermediario"].ToString(),
                        Tipo_Intermediario = new Tipo_IntermediarioModel { Tipo_Intermediario = drw["Tipo_Intermediario"].ToString(), Nome_Tipo_Intermediario = drw["Nome_Tipo_Intermediario"].ToString() },
                        Sequencia = drw["Sequencia"].ToString().ConvertToInt32(),
                        //Tipo_Comissao = drw["Tipo_Comissao"].ToString(),
                        //Nome_Tipo_Comissao = drw["Tipo_Comissao"].ToString(),
                        Tipo_Comissao = new Tipo_ComissaoModel { Tipo_Comissao = drw["Tipo_Comissao"].ToString(), Nome_Tipo_Comissao = drw["Nome_Tipo_Comissao"].ToString() },
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
            return Intermediarios;
        }
        private List<NegociacaoApresentadorModel> AddApresentadores(Int32 pNegociacao)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            List<NegociacaoApresentadorModel> Apresentadores= new List<NegociacaoApresentadorModel>();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Negociacao_Apresentador_List");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Negociacao", pNegociacao);
                Adp.Fill(dtb);
                foreach (DataRow drw in dtb.Rows)
                {
                    Apresentadores.Add(new NegociacaoApresentadorModel
                    {
                        Cod_Apresentador= drw["Cod_Apresentador"].ToString(),
                        Nome_Apresentador= drw["Nome_Apresentador"].ToString(),
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
            return Apresentadores;
        }
        public DataTable NegociacaoDetalhe(Int32 pNegociacao)
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
        public NegociacaoCountModel NegociacaoContar()
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            NegociacaoCountModel Retorno = new NegociacaoCountModel();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Negociacao_Contar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.Fill(dtb);
                if (dtb.Rows.Count > 0)
                {
                    Retorno.Qtd_Negociacao = dtb.Rows[0]["Qtd_Negociacao"].ToString().ConvertToInt32();
                    Retorno.Qtd_Proposta = dtb.Rows[0]["Qtd_Proposta"].ToString().ConvertToInt32();
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
    }
}
