using CLASSDB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PROPOSTA
{
    public partial class Simulacao
       
    {
        Int32 ContadorMidia = 0;
        Int32 ContadorEsquema = 0;
        public DataTable ListSimulacao()
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Simulacao_List");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login ", this.CurrentUser);
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
        public DataTable SimulacaoDestroy(SimulacaoModel Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Simulacao_Delete");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login ", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Id_Simulacao", Param.Id_Simulacao);
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
        public SimulacaoModel GetSimulacao(Int32 pId_Simulacao)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SimLib clsLib = new SimLib();
            SimulacaoModel Simulacao = new SimulacaoModel();
            try
            {
                //============================Simulacao Dados BASE
                DataTable dtbBase = new DataTable("dtb");
                SqlDataAdapter AdpBase = new SqlDataAdapter();
                SqlCommand cmdBase = cnn.Procedure(cnn.Connection, "Pr_Proposta_Simulacao_Get");
                AdpBase.SelectCommand = cmdBase;
                AdpBase.SelectCommand.Parameters.AddWithValue("@Par_Login ", this.CurrentUser);
                AdpBase.SelectCommand.Parameters.AddWithValue("@Par_Id_Simulacao", pId_Simulacao);
                AdpBase.Fill(dtbBase);
                foreach (DataRow drwBase in dtbBase.Rows)
                {
                    Simulacao.Id_Simulacao = drwBase["Id_Simulacao"].ToString().ConvertToInt32();
                    Simulacao.Identificacao = drwBase["Identificacao"].ToString();
                    Simulacao.Validade_Inicio = drwBase["Validade_Inicio"].ToString().ConvertToDatetime().ToString("dd/MM/yyyy");
                    Simulacao.Validade_Termino = drwBase["Validade_Termino"].ToString().ConvertToDatetime().ToString("dd/MM/yyyy");
                    Simulacao.Cod_Empresa_Venda = drwBase["Cod_Empresa_Venda"].ToString();
                    Simulacao.Nome_Empresa_Venda = drwBase["Nome_Empresa_Venda"].ToString();
                    Simulacao.PendenteCalculo = false;
                    Simulacao.Tabela_Preco = clsLib.CompetenciaString(drwBase["Tabela_Preco"].ToString().ConvertToInt32());
                    Simulacao.Desconto_Padrao = drwBase["Desconto_Padrao"].ToString().ConvertToPercent();
                    Simulacao.Valor_Informado = drwBase["Valor_Informado"].ToString().ConvertToMoney();
                    Simulacao.Valor_Total_Negociado = drwBase["Valor_Total_Negociado"].ToString().ConvertToMoney();
                    Simulacao.Valor_Total_Tabela = drwBase["Valor_Total_Tabela"].ToString().ConvertToMoney();
                    Simulacao.Desconto_Real = drwBase["Desconto_Real"].ToString().ConvertToPercent();
                    Simulacao.Fixar_Desconto = drwBase["Fixar_Desconto"].ToString().ConvertToBoolean();
                    Simulacao.Fixar_Valor = drwBase["Fixar_Valor"].ToString().ConvertToBoolean();
                    Simulacao.Id_Usuario = drwBase["Id_Usuario"].ToString().ConvertToInt32();
                    Simulacao.Id_Status = drwBase["Id_Status"].ToString().ConvertToInt32();
                }
                //===========================================Adicionas esquemas/Midias/Insercoes/Veiculos
                //DataTable dtbDesconto = new DataTable();
                //SqlDataAdapter AdpDesconto = new SqlDataAdapter();
                //SqlCommand cmdDesconto = cnn.Procedure(cnn.Connection, "Pr_Proposta_Desconto_Get");
                //AdpDesconto.SelectCommand = cmdDesconto;
                //AdpDesconto.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                //AdpDesconto.SelectCommand.Parameters.AddWithValue("@Par_Id_Simulacao", pId_Simulacao);
                //AdpDesconto.Fill(dtbDesconto);
                Simulacao.Esquemas = AddListEsquema(pId_Simulacao);
                Simulacao.ContadorEsquema = ContadorEsquema;
                Simulacao.ContadorMidia = ContadorMidia;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cnn.Close();
            }
            return Simulacao;
        }
        private List<EsquemaModel> AddListEsquema(Int32 pId_Simulacao)
        {
            List<EsquemaModel> ListEsquema = new List<EsquemaModel>();
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            try
            {
                SimLib clsLib = new SimLib();
                DataTable dtb = new DataTable();
                SqlDataAdapter Adp = new SqlDataAdapter();
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Esquema_Get");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Id_Simulacao", pId_Simulacao);
                Adp.Fill(dtb);

                foreach (DataRow drw in dtb.Rows)
                {
                    ContadorEsquema++;
                    ListEsquema.Add(new EsquemaModel()
                    {
                        //Id_Esquema = drw["Id_Esquema"].ToString().ConvertToInt32(),
                        Id_Esquema = ContadorEsquema,
                        Id_Simulacao = drw["Id_Simulacao"].ToString().ConvertToInt32(),
                        Competencia = clsLib.CompetenciaString( drw["Competencia"].ToString().ConvertToInt32()),
                        Abrangencia = drw["Abrangencia"].ToString().ConvertToByte(),
                        Cod_Mercado = drw["Cod_Mercado"].ToString().Trim(),
                        Valor_Total_Negociado = drw["Valor_Total_Negociado"].ToString().ConvertToMoney(),
                        Valor_Total_Tabela = drw["Valor_Total_Tabela"].ToString().ConvertToMoney(),
                        Desconto_Padrao = drw["Desconto_Padrao"].ToString().ConvertToPercent(),
                        Fixar_Desconto = drw["Fixar_Desconto"].ToString().ConvertToBoolean(),
                        Fixar_Valor = drw["Fixar_Valor"].ToString().ConvertToBoolean(),
                        Cod_Empresa_Faturamento = drw["Cod_Empresa_Faturamento"].ToString(),
                        Midias= AddListMidia(drw["Id_Esquema"].ToString().ConvertToInt32()),
                        Veiculos  = AddListVeiculos(drw["Id_Esquema"].ToString().ConvertToInt32()),
                        
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
            return ListEsquema;
        }
        private List<MidiaModel> AddListMidia(Int32 pId_Esquema)
        {
            List<MidiaModel> ListMidia = new List<MidiaModel>();
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            try
            {
                SimLib clsLib = new SimLib();
                DataTable dtb = new DataTable();
                SqlDataAdapter Adp = new SqlDataAdapter();
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Midia_Get");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Id_Esquema", pId_Esquema);
                Adp.Fill(dtb);

                foreach (DataRow drw in dtb.Rows)
                {
                    ContadorMidia++;
                    ListMidia.Add(new MidiaModel()
                    {
                        //Id_Midia = drw["Id_Midia"].ToString().ConvertToInt32(),
                        Id_Midia = ContadorMidia,
                        Id_Esquema = ContadorEsquema,
                        Cod_Programa = drw["Cod_Programa"].ToString(),
                        Nome_Programa = drw["Nome_Programa"].ToString(),
                        Cod_Caracteristica = drw["Cod_Caracteristica"].ToString(),
                        Nome_Caracteristica = drw["Nome_Caracteristica"].ToString(),
                        Cod_Tipo_Comercial = drw["Cod_Tipo_Comercial"].ToString(),
                        Nome_Tipo_Comercial = drw["Nome_Tipo_Comercial"].ToString(),
                        Titulo_Comercial = drw["Titulo_Comercial"].ToString(),
                        Cod_Comercial = drw["Cod_Comercial"].ToString(),
                        Duracao = drw["Duracao"].ToString().ConvertToInt32(),
                        Dia_Inicio = drw["Dia_Inicio"].ToString().ConvertToInt32(),
                        Dia_Fim = drw["Dia_Fim"].ToString().ConvertToInt32(),
                        Qtd_Insercoes = drw["Qtd_Insercoes"].ToString().ConvertToInt32(),
                        Desconto_Informado = drw["Desconto_Informado"].ToString().ConvertToPercent(),
                        Desconto_Real = drw["Desconto_Real"].ToString().ConvertToPercent(),
                        Valor_Informado= drw["Valor_Informado"].ToString().ConvertToMoney(),
                        Valor_Tabela_Unitario = drw["Valor_Tabela_Unitario"].ToString().ConvertToMoney(),
                        Valor_Tabela_Total = drw["Valor_Tabela_Total"].ToString().ConvertToMoney(),
                        Valor_Negociado_Total = drw["Valor_Negociado_Total"].ToString().ConvertToMoney(),
                        Critica= drw["Critica"].ToString(),
                        IsValid = true,
                        Insercoes = AddListInsercoes(drw["Id_Midia"].ToString().ConvertToInt32()),
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
            return ListMidia;
        }
        private List<VeiculoModel> AddListVeiculos(Int32 pId_Esquema)
        {
            List<VeiculoModel> ListVeiculo = new List<VeiculoModel>();
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            try
            {
                SimLib clsLib = new SimLib();
                DataTable dtb = new DataTable();
                SqlDataAdapter Adp = new SqlDataAdapter();
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Esquema_Veiculo_Get");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Id_Esquema", pId_Esquema);
                Adp.Fill(dtb);

                foreach (DataRow drw in dtb.Rows)
                {
                    ListVeiculo.Add(new VeiculoModel()
                    {
                        Id_Esquema = ContadorEsquema,
                        Cod_Veiculo = drw["Cod_Veiculo"].ToString(),
                        Nome_Veiculo = drw["Nome_Veiculo"].ToString(),
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
            return ListVeiculo;
        }
        private List<InsercaoModel> AddListInsercoes(Int32 pId_Midia)
        {
            List<InsercaoModel> ListInsercoes = new List<InsercaoModel>();
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            try
            {
                SimLib clsLib = new SimLib();
                DataTable dtb = new DataTable();
                SqlDataAdapter Adp = new SqlDataAdapter();
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Insercoes_Get");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Id_Midia", pId_Midia);
                Adp.Fill(dtb);

                foreach (DataRow drw in dtb.Rows)
                {
                    ListInsercoes.Add(new InsercaoModel()
                    {
                        //Id_Insercao = drw["Id_Insercoes"].ToString().ConvertToInt32(),
                        Id_Midia = ContadorMidia,
                        Data_Exibicao = drw["Data_Exibicao"].ToString().ConvertToDatetime(),
                        Qtd = (drw["Qtd"].ToString()) == "0" ? null : drw["Qtd"].ToString(),
                        Valor_Tabela_Unitario= drw["Valor_Tabela_Unitario"].ToString().ConvertToMoney(),
                        Valor_Negociado_Unitario= drw["Valor_Negociado_Unitario"].ToString().ConvertToMoney(),
                        Valor_Negociado_Total= drw["Valor_Negociado_Total"].ToString().ConvertToMoney(),
                        Desconto_Aplicado = drw["Desconto_Aplicado"].ToString().ConvertToMoney(),
                        Tipo_Desconto= drw["Tipo_Desconto"].ToString(),
                        Tem_Grade= drw["Tem_Grade"].ToString().ConvertToBoolean(),
                        Dia_Semana = drw["Dia_Semana"].ToString(),
                        Dia= drw["Dia"].ToString(),
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
            return ListInsercoes;
        }
        public DataTable GetOpcoesDesconto(Int32 pTipoDesconto)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Opcoes_Desconto");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@pTipoDesconto", pTipoDesconto);
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
        public DataTable GetVeiculos(Int32 pAbrangencia, String pCod_Mercado, String pCod_Empresa, String pCod_Empresa_Faturamento)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Lista_Veiculos");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@pLogin ", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@pAbrangencia", pAbrangencia);
                Adp.SelectCommand.Parameters.AddWithValue("@pCodMercado", pCod_Mercado);
                Adp.SelectCommand.Parameters.AddWithValue("@pCodEmpresa", pCod_Empresa);
                Adp.SelectCommand.Parameters.AddWithValue("@pCodEmpresa_Faturamento", pCod_Empresa_Faturamento);
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
        public DataTable GetProgramasGrade(GetProgramasGradeParam Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                String xmlVeiculo = null;
                if (Param.Veiculos.Count > 0)
                {
                    xmlVeiculo = clsLib.SerializeToString(Param.Veiculos);
                }

                Int32 intCompetencia = clsLib.CompetenciaInt(Param.Competencia);
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Get_Programas_Grade");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia ", intCompetencia);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Veiculos ", xmlVeiculo);
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
        public List<InsercaoModel> DistribuirInsercoes(DistribuicaoInsecoesParam Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            List<InsercaoModel> Insercoes = new List<InsercaoModel>();
            try
            {
                String xmlVeiculo = null;
                if (Param.Veiculos.Count > 0)
                {
                    xmlVeiculo = clsLib.SerializeToString(Param.Veiculos);
                }

                Int32 intCompetencia = clsLib.CompetenciaInt(Param.Competencia);
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Insercoes_Distrubuir");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia", intCompetencia);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", Param.Cod_Programa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Veiculos", xmlVeiculo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Tipo_Comercial", Param.Cod_Tipo_Comercial);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Caracteristica", Param.Cod_Caracteristica);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Qtd", Param.Qtd_Insercoes);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Dia_Inicio", Param.Dia_Inicio);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Dia_Fim", Param.Dia_Fim);
                Adp.Fill(dtb);
                if (dtb.Rows[0]["Status"].ToString().ConvertToBoolean())
                {
                    foreach (DataRow drw in dtb.Rows)
                    {
                        Insercoes.Add(new InsercaoModel()
                        {
                            Id_Insercao = 0,
                            Id_Midia = Param.Id_Midia,
                            Data_Exibicao = drw["Data_Exibicao"].ToString().ConvertToDatetime(),
                            Dia = drw["Dia"].ToString(),
                            Dia_Semana = drw["Dia_Semana"].ToString(),
                            Tem_Grade = drw["Tem_Grade"].ToString().ConvertToBoolean(),
                            Status = drw["Status"].ToString().ConvertToBoolean(),
                            Critica = drw["Critica"].ToString(),
                            Qtd = (drw["Qtd"].ToString()) == "0" ? null : drw["Qtd"].ToString(),
                        });
                    }
                }
                else
                {
                    Insercoes.Add(new InsercaoModel()
                    {
                        Status = dtb.Rows[0]["Status"].ToString().ConvertToBoolean(),
                        Critica = dtb.Rows[0]["Critica"].ToString()
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
            return Insercoes;
        }
        public DataTable SalvarSimulacao(SimulacaoModel Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                String xmlEsquemas= null;
                if (Param.Esquemas.Count > 0)
                {
                    xmlEsquemas= clsLib.SerializeToString(Param.Esquemas);
                }
                Int32? iTabelaPreco = null;
                if (Param.Tabela_Preco!=null)
                {
                    iTabelaPreco = clsLib.CompetenciaInt(Param.Tabela_Preco);
                }

                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_Proposta_Simulacao_Salvar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Id_Simulacao", Param.Id_Simulacao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Identificacao", Param.Identificacao);
                if (Param.Validade_Inicio!=null)
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Validade_Inicio", Param.Validade_Inicio.ConvertToDatetime());
                }
                if (Param.Validade_Termino!=null)
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Validade_Termino", Param.Validade_Termino.ConvertToDatetime());
                }                
                
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa_Venda", Param.Cod_Empresa_Venda);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Tabela_Preco", iTabelaPreco);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Desconto_Padrao", Param.Desconto_Padrao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Valor_Informado", Param.Valor_Informado);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Valor_Total_Negociado", Param.Valor_Total_Negociado);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Valor_Total_Tabela", Param.Valor_Total_Tabela);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Desconto_Real", Param.Desconto_Real);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Fixar_Desconto", Param.Fixar_Desconto);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Fixar_Valor", Param.Fixar_Valor);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Id_Status", Param.Id_Status);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Esquemas", xmlEsquemas);
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
