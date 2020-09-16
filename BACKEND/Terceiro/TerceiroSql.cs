using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace PROPOSTA
{

    public partial class Terceiro
    {
        //public static String Cod_Terceiro { get; private set; }

        public DataTable TerceiroListar(TerceiroFiltroModel pFiltro)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Terceiro_Listar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Terceiro", pFiltro.Codigo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Razao_Social", pFiltro.RazaoSocial);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_CNPJ", pFiltro.CNPJ);
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

        public DataTable SalvarTerceiro(TerceiroModel pTerceiro)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();

            String xmlEmpresas = null;
            Int32 IBaseEndereco = -1;
            Int32 IBaseComplementar = -1;
            if (pTerceiro.Empresas.Count > 0)
            {
                xmlEmpresas = clsLib.SerializeToString(pTerceiro.Empresas);
            }

            if (pTerceiro.Enderecos.Count > 0)
            {
                for (int i = 0; i < pTerceiro.Enderecos.Count ; i++)
                {
                    if (pTerceiro.Enderecos[i].Base_Edicao)
                    {
                        IBaseEndereco = i;
                        break;
                    }
                };
            }
            if (pTerceiro.Complementar.Count > 0)
            {


                for (int i = 0; i < pTerceiro.Complementar.Count ; i++)
                {
                    if (pTerceiro.Complementar[i].Base_Edicao)
                    {
                        IBaseComplementar = i;
                        break;
                    }
                };
            }
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Terceiro_Salvar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Operacao", pTerceiro.id_operacao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Terceiro", pTerceiro.Cod_Terceiro);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Razao_Social", pTerceiro.Razao_Social);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Porte", pTerceiro.Indica_Porte);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Funcao", pTerceiro.Funcao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_CGC", pTerceiro.CGC);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Inscricao_Estadual", pTerceiro.Inscricao_Estadual);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Inscricao_Municipal", pTerceiro.Inscricao_Municipal);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Cic_Cgc", pTerceiro.Indica_Cic_Cgc);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa_Principal", pTerceiro.Cod_Empresa_Principal);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Nome_Fantasia", pTerceiro.Nome_Fantasia);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Categoria", pTerceiro.Cod_Categoria);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Tipo_Cobranca", pTerceiro.Tipo_Cobranca);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Forma_Tributacao", pTerceiro.Forma_Tributacao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Limite_Credito", pTerceiro.Limite_Credito);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Estrangeiro", pTerceiro.Indica_Estrangeiro ? 1 : 0);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Direto", pTerceiro.Indica_Direto ? 1 : 0);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Afiliada", pTerceiro.Indica_Afiliada ? 1 : 0);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Regional", pTerceiro.Indica_Regional ? 1 : 0);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Tv", pTerceiro.Indica_Tv ? 1 : 0);
                if (IBaseEndereco > -1)
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Endereco1", pTerceiro.Enderecos[IBaseEndereco].Endereco1);
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero1", pTerceiro.Enderecos[IBaseEndereco].Numero1);
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Complemento1", pTerceiro.Enderecos[IBaseEndereco].Complemento1);
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Bairro1", pTerceiro.Enderecos[IBaseEndereco].Bairro1);
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Municipio1", pTerceiro.Enderecos[IBaseEndereco].Municipio1);
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Uf1", pTerceiro.Enderecos[IBaseEndereco].Uf1);
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cep1", pTerceiro.Enderecos[IBaseEndereco].Cep1);
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Endereco2", pTerceiro.Enderecos[IBaseEndereco].Endereco2);
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero2", pTerceiro.Enderecos[IBaseEndereco].Numero2);
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Complemento2", pTerceiro.Enderecos[IBaseEndereco].Complemento2);
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Bairro2", pTerceiro.Enderecos[IBaseEndereco].Bairro2);
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Municipio2", pTerceiro.Enderecos[IBaseEndereco].Municipio2);
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Uf2", pTerceiro.Enderecos[IBaseEndereco].Uf2);
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cep2", pTerceiro.Enderecos[IBaseEndereco].Cep2);
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Telefone", pTerceiro.Enderecos[IBaseEndereco].Telefone);
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Fax", pTerceiro.Enderecos[IBaseEndereco].Fax);
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Email", pTerceiro.Enderecos[IBaseEndereco].Email);
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Praca_Pgto", pTerceiro.Enderecos[IBaseEndereco].Praca_Pgto);
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Municipio", pTerceiro.Enderecos[IBaseEndereco].Cod_Municipio);
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Municipio1", pTerceiro.Enderecos[IBaseEndereco].Cod_Municipio1);
                }
                if (IBaseComplementar > -1)
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Nome_Contato", pTerceiro.Complementar[IBaseComplementar].Nome_Contato);
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Telefone_Contato_1", pTerceiro.Complementar[IBaseComplementar].Telefone_Contato_1);
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Telefone_Contato_2", pTerceiro.Complementar[IBaseComplementar].Telefone_Contato_2);
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Email_Contato", pTerceiro.Complementar[IBaseComplementar].Email_Contato);
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Nome_Contato_Compl", pTerceiro.Complementar[IBaseComplementar].Nome_Contato_Compl);
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Telefone_Contato_Compl_1", pTerceiro.Complementar[IBaseComplementar].Telefone_Contato_Compl_1);
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Telefone_Contato_Compl_2", pTerceiro.Complementar[IBaseComplementar].Telefone_Contato_Compl_2);
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Email_Contato_Compl", pTerceiro.Complementar[IBaseComplementar].Email_Contato_Compl);
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Comissao_Padrao", pTerceiro.Complementar[IBaseComplementar].Comissao_Padrao);
                }
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Empresas", xmlEmpresas);
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

        public TerceiroModel GetTerceiroData(string pCod_Terceiro)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            TerceiroModel Terceiro = new TerceiroModel();

            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Terceiro_Get");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Terceiro", pCod_Terceiro);
                Adp.Fill(dtb);
                if (dtb.Rows.Count > 0)
                {
                    Terceiro.Cod_Terceiro = dtb.Rows[0]["Cod_Terceiro"].ToString().TrimEnd();
                    Terceiro.Razao_Social = dtb.Rows[0]["Razao_Social"].ToString().TrimEnd();
                    Terceiro.CGC = dtb.Rows[0]["CGC"].ToString().TrimEnd();
                    Terceiro.Indica_Cic_Cgc = dtb.Rows[0]["Indica_Cic_Cgc"].ToString().ConvertToInt32();
                    Terceiro.Inscricao_Estadual = dtb.Rows[0]["Inscricao_Estadual"].ToString().TrimEnd();
                    Terceiro.Inscricao_Municipal = dtb.Rows[0]["Inscricao_Municipal"].ToString();
                    Terceiro.Nome_Fantasia = dtb.Rows[0]["Nome_Fantasia"].ToString().TrimEnd();
                    Terceiro.Funcao = dtb.Rows[0]["Funcao"].ToString().ConvertToInt32();
                    Terceiro.Cod_Funcao_Terceiro = dtb.Rows[0]["Cod_Funcao_Terceiro"].ToString().ConvertToInt32();
                    Terceiro.Descricao_Funcao_Terceiro = dtb.Rows[0]["Descricao_Funcao_Terceiro"].ToString().TrimEnd();
                    Terceiro.Cod_Categoria = dtb.Rows[0]["Cod_Categoria"].ToString().TrimEnd();
                    Terceiro.Descricao_Categoria = dtb.Rows[0]["Descricao_Categoria"].ToString().TrimEnd();
                    Terceiro.Cod_Empresa_Principal = dtb.Rows[0]["Cod_Empresa_Principal"].ToString().TrimEnd();
                    Terceiro.Nome_Empresa_Principal = dtb.Rows[0]["Nome_Empresa_Principal"].ToString().TrimEnd();
                    Terceiro.Tipo_Cobranca = dtb.Rows[0]["Tipo_Cobranca"].ToString().TrimEnd();
                    Terceiro.Descricao_Tipo_Cobranca = dtb.Rows[0]["Descricao_Tipo_Cobranca"].ToString().TrimEnd();
                    Terceiro.Limite_Credito = dtb.Rows[0]["Limite_Credito"].ToString().ConvertToMoney();
                    Terceiro.Forma_Tributacao = dtb.Rows[0]["Forma_Tributacao"].ToString().TrimEnd();
                    Terceiro.Des_Forma_Tributacao = dtb.Rows[0]["Des_Forma_Tributacao"].ToString().TrimEnd();
                    Terceiro.Permite_Editar = dtb.Rows[0]["Permite_Editar"].ToString().ConvertToBoolean();
                    Terceiro.Indica_Porte = dtb.Rows[0]["Indica_Porte"].ToString().ConvertToInt32();
                    Terceiro.Indica_Estrangeiro = dtb.Rows[0]["Indica_Estrangeiro"].ToString().ConvertToBoolean();
                    Terceiro.Indica_Direto = dtb.Rows[0]["Indica_Direto"].ToString().ConvertToBoolean();
                    Terceiro.Indica_Afiliada = dtb.Rows[0]["Indica_Afiliada"].ToString().ConvertToBoolean();
                    Terceiro.Indica_Regional = dtb.Rows[0]["indica_regional"].ToString().ConvertToBoolean();
                    Terceiro.Indica_Tv = dtb.Rows[0]["Indica_Tv"].ToString().ConvertToBoolean();
                    Terceiro.Enderecos = AddTerceiroEndereco(pCod_Terceiro);
                    Terceiro.Complementar = AddTerceiroComplementar(pCod_Terceiro);
                    Terceiro.Empresas = AddEmpresas(pCod_Terceiro);

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
            return Terceiro;
        }

        private List<TerceiroEnderecoModel> AddTerceiroEndereco(String pCod_Terceiro)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            List<TerceiroEnderecoModel> Enderecos = new List<TerceiroEnderecoModel>();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "[Pr_Proposta_TerceiroEndereco_List]");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Terceiro", pCod_Terceiro);
                Adp.Fill(dtb);
                foreach (DataRow drw in dtb.Rows)
                {
                    Enderecos.Add(new TerceiroEnderecoModel()
                    {
                        Cod_Terceiro = drw["Cod_Terceiro"].ToString().TrimEnd(),
                        Cod_Empresa = drw["Cod_Empresa"].ToString().TrimEnd(),
                        Nome_Empresa = drw["Nome_Empresa"].ToString().TrimEnd(),
                        Endereco1 = drw["Endereco1"].ToString().TrimEnd(),
                        Numero1 = drw["Numero1"].ToString().TrimEnd(),
                        Complemento1 = drw["Complemento1"].ToString().TrimEnd(),
                        Bairro1 = drw["Bairro1"].ToString().TrimEnd(),
                        Municipio1 = drw["Municipio1"].ToString().TrimEnd(),
                        Uf1 = drw["Uf1"].ToString().TrimEnd(),
                        Cep1 = drw["Cep1"].ToString().TrimEnd(),
                        Endereco2 = drw["Endereco2"].ToString().TrimEnd(),
                        Numero2 = drw["Numero2"].ToString().TrimEnd(),
                        Complemento2 = drw["Complemento2"].ToString().TrimEnd(),
                        Bairro2 = drw["Bairro2"].ToString().TrimEnd(),
                        Municipio2 = drw["Municipio2"].ToString().TrimEnd(),
                        Uf2 = drw["Uf2"].ToString().TrimEnd(),
                        Cep2 = drw["Cep2"].ToString().TrimEnd(),
                        Telefone = drw["Telefone"].ToString().TrimEnd(),
                        Fax = drw["Fax"].ToString().TrimEnd(),
                        Email = drw["Email"].ToString().TrimEnd(),
                        Praca_Pgto = drw["Praca_Pgto"].ToString().TrimEnd(),
                        Cod_Municipio = drw["Cod_Municipio"].ToString().TrimEnd(),
                        Cod_Municipio1 = drw["Cod_Municipio1"].ToString().TrimEnd(),
                        Permite_Edicao = drw["Permite_Edicao"].ToString().ConvertToBoolean(),
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
            return Enderecos;
        }
        private List<TerceiroComplementarModel> AddTerceiroComplementar(String pCod_Terceiro)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            List<TerceiroComplementarModel> Complementar = new List<TerceiroComplementarModel>();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_TerceiroComplementar_List");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Terceiro", pCod_Terceiro);
                Adp.Fill(dtb);
                foreach (DataRow drw in dtb.Rows)
                {
                    Complementar.Add(new TerceiroComplementarModel()
                    {
                        Cod_Terceiro = drw["Cod_Terceiro"].ToString().TrimEnd(),
                        Cod_Empresa = drw["Cod_Empresa"].ToString().TrimEnd(),
                        Nome_Empresa = drw["Nome_Empresa"].ToString().TrimEnd(),
                        Nome_Contato = drw["Nome_Contato"].ToString().TrimEnd(),
                        Email_Contato = drw["Email_Contato"].ToString().TrimEnd(),
                        Telefone_Contato_1 = drw["Telefone_Contato_1"].ToString().TrimEnd(),
                        Telefone_Contato_2 = drw["Telefone_Contato_2"].ToString().TrimEnd(),
                        Nome_Contato_Compl = drw["Nome_Contato_Compl"].ToString().TrimEnd(),
                        Email_Contato_Compl = drw["Email_Contato_Compl"].ToString().TrimEnd(),
                        Telefone_Contato_Compl_1 = drw["Telefone_Contato_Compl_1"].ToString().TrimEnd(),
                        Telefone_Contato_Compl_2 = drw["Telefone_Contato_Compl_2"].ToString().TrimEnd(),
                        Conta_Contabil = drw["Conta_Contabil"].ToString().ConvertToInt32(),
                        Sub_Conta = drw["Sub_Conta"].ToString().TrimEnd(),
                        Data_Desativacao = drw["Data_Desativacao"].ToString().ConvertToDatetime(),
                        Indica_Desativado = drw["indica_desativado"].ToString().ConvertToByte(),
                        Motivo_Desativacao = drw["Motivo_Desativacao"].ToString().TrimEnd(),
                        Indica_Merchandising = drw["Indica_Merchandising"].ToString().ConvertToByte(),
                        Cod_Grupo_Cliente = drw["Cod_Grupo_Cliente"].ToString().ConvertToInt32(),
                        Cod_Representante = drw["Cod_Representante"].ToString().ConvertToInt32(),
                        Cod_Banco = drw["Cod_Banco"].ToString().TrimEnd(),
                        Indica_IN480 = drw["Indica_IN480"].ToString().ConvertToBoolean(),
                        Bco_Agencia = drw["Bco_Agencia"].ToString().TrimEnd(),
                        Bco_Agencia_DV = drw["Bco_Agencia_DV"].ToString().TrimEnd(),
                        Bco_Conta_Corrente = drw["Bco_Conta_Corrente"].ToString().TrimEnd(),
                        Bco_Conta_Corrente_DV = drw["Bco_Conta_Corrente_DV"].ToString().TrimEnd(),
                        Conta_Contabil_Passivo = drw["Conta_Contabil_Passivo"].ToString().ConvertToInt32(),
                        Conta_Contabil_Adiantamento = drw["Conta_Contabil_Adiantamento"].ToString().ConvertToInt32(),
                        Comissao_Padrao = drw["Comissao_Padrao"].ToString().ConvertToPercent(),
                        Permite_Edicao = drw["Permite_Edicao"].ToString().ConvertToBoolean(),
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
            return Complementar;
        }
        public List<TerceiroEmpresasModel> AddEmpresas(String pCod_Terceiro)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            List<TerceiroEmpresasModel> Empresas = new List<TerceiroEmpresasModel>();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Terceiro_Empresa_List");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Terceiro", pCod_Terceiro);
                Adp.Fill(dtb);
                foreach (DataRow drw in dtb.Rows)
                {
                    Empresas.Add(new TerceiroEmpresasModel()
                    {
                        Cod_Empresa = drw["Cod_Empresa"].ToString(),
                        Nome_Empresa = drw["Nome_Empresa"].ToString(),
                        Selected = drw["Selected"].ToString().ConvertToBoolean(),
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
            return Empresas;
        }

        public DataTable ReativarTerceiro(TerceiroModel pTerceiro)
        {
            DataTable dtb = new DataTable();
            clsConexao cnn = new clsConexao(this.Credential);
            SqlDataAdapter adp = new SqlDataAdapter();
            cnn.Open();
            SimLib clsLib = new SimLib();

            String xmlEmpresas = null;
            if (pTerceiro.Empresas.Count > 0)
            {
                xmlEmpresas = clsLib.SerializeToString(pTerceiro.Empresas);
            }
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Terceiro_Reativa");
                cmd.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                cmd.Parameters.AddWithValue("@Par_Cod_Terceiro", pTerceiro.Cod_Terceiro);
                cmd.Parameters.AddWithValue("@Par_Empresas", xmlEmpresas);
                adp.SelectCommand = cmd;
                adp.Fill(dtb);
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

        public DataTable DesativarTerceiro(TerceiroModel pTerceiro)
        {
            DataTable dtb = new DataTable();
            clsConexao cnn = new clsConexao(this.Credential);
            SqlDataAdapter adp = new SqlDataAdapter();
            cnn.Open();
            SimLib clsLib = new SimLib();

            String xmlEmpresas = null;
            if (pTerceiro.Empresas.Count > 0)
            {
                xmlEmpresas = clsLib.SerializeToString(pTerceiro.Empresas);
            }

            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Terceiro_Desativa");
                cmd.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                cmd.Parameters.AddWithValue("@Par_Cod_Terceiro", pTerceiro.Cod_Terceiro);
                cmd.Parameters.AddWithValue("@Par_Empresas", xmlEmpresas);
                cmd.Parameters.AddWithValue("@Par_Motivo_Desativacao", pTerceiro.Complementar[0].Motivo_Desativacao);
                adp.SelectCommand = cmd;
                adp.Fill(dtb);
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

        public DataTable ExcluirTerceiro(TerceiroModel pTerceiro)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "sp_Deleta_Terceiro");
                Adp.SelectCommand = cmd;
                //Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Terceiro", pTerceiro.Cod_Terceiro);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa", DBNull.Value);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Delecao", 1);

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
        public DataTable GetCodigoIbge(String pCod_Municipio)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Municipio_Get");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Municipio", pCod_Municipio);
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
