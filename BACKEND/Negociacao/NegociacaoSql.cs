using CLASSDB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PROPOSTA
{
    public partial class Negociacao

    {
        Int32 Sequenciador_Id_Desconto = 0;
        Int32 Sequenciador_Id_Parcela= 0;
        Int32 MaxGrupo = 0;
        Int32 Maxparcela= 0;
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
                    Negociacao.Indica_Desativado = !String.IsNullOrEmpty(drw["Data_Desativacao"].ToString());
                    Negociacao.Percentual_Reaplicacao = drw["Percentual_Reaplicacao"].ToString().ConvertToPercent();
                    Negociacao.Valor_Reaplicacao = drw["Valor_Reaplicacao"].ToString().ConvertToMoney();
                    Negociacao.Tabela_Reaplicacao = clsLib.CompetenciaString(drw["Tabela_Reaplicacao"].ToString().ConvertToInt32());
                    Negociacao.Sequencia_Tabela_Reaplicacao = drw["Sequencia_Reaplicacao"].ToString().ConvertToByte();
                    Negociacao.Desconto_Reaplicacao = drw["Desconto_Reaplicacao"].ToString().ConvertToPercent();
                    Negociacao.Indica_Antecipado = drw["Indica_Antecipado"].ToString().ConvertToBoolean();
                    Negociacao.Texto= drw["Texto"].ToString();
                    Negociacao.Condicao_Pagamento = drw["Condicao_Pagamento"].ToString();
                    Negociacao.Empresas_Venda = AddEmpresaVenda(drw["Numero_Negociacao"].ToString().ConvertToInt32());
                    Negociacao.Empresas_Faturamento = AddEmpresaFaturamento(drw["Numero_Negociacao"].ToString().ConvertToInt32());
                    Negociacao.Permite_Editar = String.IsNullOrEmpty(drw["Data_Desativacao"].ToString());
                    Negociacao.Tem_Contrato= drw["Tem_Contrato"].ToString().ConvertToBoolean();
                    Negociacao.Tem_Complemento= drw["Tem_Complemento"].ToString().ConvertToBoolean();
                    Negociacao.Agencias = AddAgencias(drw["Numero_Negociacao"].ToString().ConvertToInt32());
                    Negociacao.Clientes = AddClientes(drw["Numero_Negociacao"].ToString().ConvertToInt32());
                    Negociacao.Nucleos= AddNucleos(drw["Numero_Negociacao"].ToString().ConvertToInt32());
                    Negociacao.Contatos = AddContatos(drw["Numero_Negociacao"].ToString().ConvertToInt32());
                    Negociacao.Intermediarios = AddIntermediarios(drw["Numero_Negociacao"].ToString().ConvertToInt32());
                    Negociacao.Apresentadores= AddApresentadores(drw["Numero_Negociacao"].ToString().ConvertToInt32());
                    Negociacao.Descontos = AddDescontos(drw["Numero_Negociacao"].ToString().ConvertToInt32());
                    Negociacao.Descontos = AddDescontos(drw["Numero_Negociacao"].ToString().ConvertToInt32());
                    Negociacao.Parcelas = AddParcelas(drw["Numero_Negociacao"].ToString().ConvertToInt32());
                    Negociacao.MaxGrupo = MaxGrupo;
                    Negociacao.MaxParcela = Maxparcela;
                    Negociacao.Sequenciador_Desconto = Sequenciador_Id_Desconto;
                    Negociacao.Sequenciador_Parcela = Sequenciador_Id_Parcela;
                    
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
                        Permite_Editar= drw["Permite_Editar"].ToString().ConvertToBoolean(),
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
                        Permite_Editar = drw["Permite_Editar"].ToString().ConvertToBoolean(),
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
                        Comissao = drw["Comissao"].ToString().ConvertToDouble(),

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
        private List<NegociacaoDescontoModel> AddDescontos(Int32 pNegociacao)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            List<NegociacaoDescontoModel> Descontos = new List<NegociacaoDescontoModel>();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Negociacao_Desconto_List");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Negociacao", pNegociacao);
                Adp.Fill(dtb);
                foreach (DataRow drw in dtb.Rows)
                {
                    Sequenciador_Id_Desconto  ++;
                    MaxGrupo = drw["Grupo"].ToString().ConvertToInt32();
                    Descontos.Add(new NegociacaoDescontoModel
                    {
                        Id_Desconto = Sequenciador_Id_Desconto,
                        Grupo = drw["Grupo"].ToString().ConvertToInt32(),
                        Cod_Tipo_Desconto = drw["Cod_Tipo_Desconto"].ToString().ConvertToInt32(),
                        Nome_Tipo_Desconto = drw["Nome_Tipo_Desconto"].ToString(),
                        Cod_Chave = drw["Cod_Chave"].ToString(),
                        Nome_Chave = drw["Nome_Chave"].ToString(),
                        Data_Inicio = drw["Data_Inicio"].ToString(),
                        Data_Termino = drw["Data_Termino"].ToString(),
                        Desconto = drw["Desconto"].ToString(),
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
            return Descontos;
        }
        private List<NegociacaoParcelaModel> AddParcelas(Int32 pNegociacao)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            List<NegociacaoParcelaModel> Parcelas = new List<NegociacaoParcelaModel>();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Negociacao_Parcela_List");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Negociacao", pNegociacao);
                Adp.Fill(dtb);
                foreach (DataRow drw in dtb.Rows)
                {
                    Sequenciador_Id_Parcela++;
                    Maxparcela++;   
                    Parcelas.Add(new NegociacaoParcelaModel
                    {
                        Id_Parcela = Sequenciador_Id_Parcela,
                        Numero_Parcela = drw["Numero_Parcela"].ToString().ConvertToInt32(),
                        Data_Parcela =drw["Data_Parcela"].ToString().ConvertToDatetime().ToString("dd/MM/yyyy"),
                        Percentual =drw["Percentual"].ToString().ConvertToDouble(),
                        Percentual_Text = drw["Percentual"].ToString(),
                        Valor_Fatura =drw["Valor_Fatura"].ToString().ConvertToDouble(),
                        Valor_Fatura_Text = drw["Valor_Fatura"].ToString().ConvertToMoney(),
                        Data_Cancelamento =drw["Data_Cancelamento"].ToString().ConvertToDatetime(),
                        Data_Complemento =drw["Data_Complemento"].ToString().ConvertToDatetime(),
                        Situacao= drw["Situacao"].ToString(),
                        Numero_Fatura= drw["Numero_Fatura"].ToString().ConvertToInt32(),
                        Permite_Editar= drw["Permite_Editar"].ToString().ConvertToBoolean(),
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
            return Parcelas;
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
        public List<NegociacaoCriticaModel> SalvarNegociacao(NegociacaoModel Param)
        {
            String strToken = this.GetToken();
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            List<NegociacaoCriticaModel> Critica = new List<NegociacaoCriticaModel>();
            SimLib clsLib = new SimLib();
            Int32 New_Numero_Negociacao = 0;
            try
            {
                this.GravarNegociacaoManutencao(Param, strToken);
                DataTable dtbCritica = this.ConsistirNegociacao(strToken);
                if (dtbCritica.Rows.Count==0)
                {
                    New_Numero_Negociacao = this.ConfirmarGravacao(Param,strToken);
                    Critica.Add(new NegociacaoCriticaModel()
                    {
                        Numero_Negociacao = Param.Numero_Negociacao,
                        Cod_Erro = 0,
                        Mensagem = "Negociação Gravada com Sucesso - Numero:" + New_Numero_Negociacao.ToString()
                    });
                }
                else
                {
                    foreach (DataRow drw in dtbCritica.Rows)
                    {
                        Critica.Add(new NegociacaoCriticaModel()
                        {
                            Numero_Negociacao = Param.Numero_Negociacao,
                            Cod_Erro = drw["Codigo_Erro"].ToString().ConvertToInt32(),
                            Mensagem = drw["Mensagem_Erro"].ToString(),
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
            return Critica;
        }
        
        private DataTable ConsistirNegociacao(String pToken)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            DataTable Critica = new DataTable();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_SF_Consistencia");
                Adp.SelectCommand = cmd;
                clsLib.NewParameter(Adp, "@Par_Token", pToken);
                Adp.Fill(Critica);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cnn.Close();
            }
            return Critica;
        }
        private  void GravarNegociacaoManutencao(NegociacaoModel Param, String pToken )
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();

            String Descontos_Xml = null;
            String Empresas_Venda_Xml = null;
            String Empresas_Faturamento_Xml = null;
            String Agencias_Xml = null;
            String Clientes_Xml = null;
            String Contatos_Xml = null;
            String Nucleos_Xml = null;
            String Intermediarios_Xml = null;
            String Apresentadores_Xml = null;
            String Parcelas_Xml = null;
            if (Param.Descontos.Count>0)
            {
                Descontos_Xml = clsLib.SerializeToString(Param.Descontos);
            }
            if (Param.Empresas_Venda.Count > 0)
            {
                Empresas_Venda_Xml = clsLib.SerializeToString(Param.Empresas_Venda);
            }
            if (Param.Empresas_Faturamento.Count > 0)
            {
                Empresas_Faturamento_Xml = clsLib.SerializeToString(Param.Empresas_Faturamento);
            }
            if (Param.Agencias.Count > 0)
            {
                Agencias_Xml = clsLib.SerializeToString(Param.Agencias);
            }
            if (Param.Clientes.Count > 0)
            {
                Clientes_Xml= clsLib.SerializeToString(Param.Clientes);
            }
            if (Param.Contatos.Count > 0)
            {
                Contatos_Xml = clsLib.SerializeToString(Param.Contatos);
            }
            if (Param.Nucleos.Count > 0)
            {
                Nucleos_Xml = clsLib.SerializeToString(Param.Nucleos);
            }
            if (Param.Intermediarios.Count > 0)
            {
                Intermediarios_Xml = clsLib.SerializeToString(Param.Intermediarios);
            }
            if (Param.Apresentadores.Count > 0)
            {
                Apresentadores_Xml = clsLib.SerializeToString(Param.Apresentadores);
            }
            if (Param.Parcelas.Count > 0)
            {
                Parcelas_Xml = clsLib.SerializeToString(Param.Parcelas);
            }
            try
            {

                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Negociacao_Manutencao_Insert");
                SqlDataAdapter Adp = new SqlDataAdapter();
                Adp.SelectCommand = cmd;
                clsLib.NewParameter(Adp, "@Par_Login", this.CurrentUser);
                clsLib.NewParameter(Adp, "@Par_Token", pToken);
                clsLib.NewParameter(Adp, "@Par_Numero_Negociacao", Param.Numero_Negociacao);
                clsLib.NewParameter(Adp, "@Par_Cod_Tipo_Midia", Param.Cod_Tipo_Midia);
                clsLib.NewParameter(Adp, "@Par_Comissao_Agencia", Param.Comissao_Agencia_String);
                clsLib.NewParameter(Adp, "@Par_Competencia_Inicial", clsLib.CompetenciaInt(Param.Competencia_Inicial));
                clsLib.NewParameter(Adp, "@Par_Competencia_Final", clsLib.CompetenciaInt(Param.Competencia_Final));
                clsLib.NewParameter(Adp, "@Par_Desconto_Concedido", Param.Desconto_Concedido_String);
                clsLib.NewParameter(Adp, "@Par_Cod_Forma_Pgto", Param.Cod_Forma_Pgto);
                clsLib.NewParameter(Adp, "@Par_Condicao_Pagamento", Param.Condicao_Pagamento);
                if (String.IsNullOrEmpty(Param.Tabela_Preco))
                {
                    clsLib.NewParameter(Adp, "@Par_Tabela_Preco", null);
                }
                else
                {
                    if (Param.Tabela_Preco.TrimEnd().ToUpper()=="VIGENTE")
                    {
                        clsLib.NewParameter(Adp, "@Par_Tabela_Preco", null);
                    }
                    else
                    {
                        clsLib.NewParameter(Adp, "@Par_Tabela_Preco", clsLib.CompetenciaInt(Param.Tabela_Preco));
                    }
                }
                clsLib.NewParameter(Adp, "@Par_Sequencia_Tabela", Param.Sequencia_Tabela);
                clsLib.NewParameter(Adp, "@Par_Verba_Negociada", Param.Verba_Negociada_String);
                clsLib.NewParameter(Adp, "@Par_Patrocinio_Evento", Param.Patrocinio_Evento);
                clsLib.NewParameter(Adp, "@Par_Percentual_Reaplicacao", Param.Percentual_Reaplicacao);
                clsLib.NewParameter(Adp, "@Par_Valor_Reaplicacao", Param.Valor_Reaplicacao);
                clsLib.NewParameter(Adp, "@Par_Tabela_Reaplicacao", clsLib.CompetenciaInt( Param.Tabela_Reaplicacao));
                clsLib.NewParameter(Adp, "@Par_Sequencia_Tabela_Reaplicacao", Param.Sequencia_Tabela_Reaplicacao);
                clsLib.NewParameter(Adp, "@Par_Desconto_Reaplicacao", Param.Desconto_Reaplicacao);
                clsLib.NewParameter(Adp, "@Par_Texto", Param.Texto);
                clsLib.NewParameter(Adp, "@Par_Descontos", Descontos_Xml);
                clsLib.NewParameter(Adp, "@Par_Empresas_Venda", Empresas_Venda_Xml);
                clsLib.NewParameter(Adp, "@Par_Empresas_Faturamento", Empresas_Faturamento_Xml);
                clsLib.NewParameter(Adp, "@Par_Agencias", Agencias_Xml);
                clsLib.NewParameter(Adp, "@Par_Clientes", Clientes_Xml);
                clsLib.NewParameter(Adp, "@Par_Contatos", Contatos_Xml);
                clsLib.NewParameter(Adp, "@Par_Nucleos", Nucleos_Xml);
                clsLib.NewParameter(Adp, "@Par_Intermediarios", Intermediarios_Xml);
                clsLib.NewParameter(Adp, "@Par_Apresentadores", Apresentadores_Xml);
                clsLib.NewParameter(Adp, "@Par_Parcelas", Parcelas_Xml);
                Adp.SelectCommand = cmd;
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
        }
        private Int32 ConfirmarGravacao(NegociacaoModel Param, String pToken)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            Int32 Numero_Negociacao = 0;
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Sp_Negociacao_Manutencao");
                Adp.SelectCommand = cmd;
                clsLib.NewParameter(Adp, "@Par_Numero_Negociacao", Param.Numero_Negociacao);
                clsLib.NewParameter(Adp, "@Par_Cod_Motivo_Alteracao", Param.Cod_Motivo_Alteracao);
                clsLib.NewParameter(Adp, "@Par_Cod_Usuario", "SIM/" + this.CurrentUser);
                clsLib.NewParameter(Adp, "@Par_Indica_SF", 0);
                clsLib.NewParameter(Adp, "@Par_Token", pToken);
                clsLib.NewParameter(Adp, "@PAR_INDICA_MSA", 0);
                clsLib.NewParameter(Adp, "@Par_Indica_Retorno", 1);
                Adp.Fill(dtb);
                Numero_Negociacao = dtb.Rows[0]["Numero_Negociacao"].ToString().ConvertToInt32();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cnn.Close();
            }
            return Numero_Negociacao;
        }
        private String GetToken ()
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            String strToken = "";
            DataTable dtb = new DataTable();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_SF_GetId");
                SqlDataAdapter Adp = new SqlDataAdapter();
                Adp.SelectCommand = cmd;
                Adp.Fill(dtb);
                strToken = dtb.Rows[0]["Token"].ToString();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                cnn.Close();
            }
            return strToken;
        }
        public DataTable NegociacaoDesativar(NegociacaoDesativarModel Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            DataTable Critica = new DataTable();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "[Pr_Proposta_Negociacao_Desativar]");
                Adp.SelectCommand = cmd;
                clsLib.NewParameter(Adp, "@Par_Login", this.CurrentUser);
                clsLib.NewParameter(Adp, "@Par_Negociacao",Param.Numero_Negociacao);
                clsLib.NewParameter(Adp, "@Par_Motivo_Desativacao", Param.Motivo_Desativacao);
                clsLib.NewParameter(Adp, "@Par_Operacao", Param.Operacao);
                Adp.Fill(Critica);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cnn.Close();
            }
            return Critica;
        }
    }
}
