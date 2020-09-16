using CLASSDB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace PROPOSTA
{
    public partial class AM

    {
        private Int32 Qtd_Total_Falha = 0;
        private Int32 Qtd_Total_Compensacao = 0;
        private Double Valor_Total_Falha = 0;
        private Double Valor_Total_Compensacao = 0;
        public DataTable AMList(AMFiltroModel Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_AM_List");
                Adp.SelectCommand = cmd;
                if (String.IsNullOrEmpty(Param.Competencia))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia",DBNull.Value);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia", clsLib.CompetenciaInt(Param.Competencia));
                }
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Nucleo", Param.Cod_Nucleo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Contato", Param.Cod_Contato);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cliente", Param.Cliente);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Agencia", Param.Agencia);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Veiculo", Param.Cod_Veiculo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Produto", Param.Cod_Red_Produto);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Empresa", Param.Cod_Empresa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Contrato", Param.Numero_Mr);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Sequencia", Param.Sequencia_Mr);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Negociacao", Param.Numero_Negociacao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Situacao", Param.Situacao);

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

        //Definindo Reencaixe de AM

        public DataTable AmReencaixe(Reencaixe_Model Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_AmReencaixe_List");
                Adp.SelectCommand = cmd;
                if (String.IsNullOrEmpty(Param.Competencia))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia", DBNull.Value);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia", clsLib.CompetenciaInt(Param.Competencia));
                }

                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa", Param.Cod_Empresa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Mr", Param.Numero_Mr);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Sequencia_Mr", Param.Sequencia_Mr);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Docto", Param.Documento_Para);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", Param.Cod_Veiculo);

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


        // Definindo reabertura de AM

        public DataTable ReabrirAM(AM.Reencaixe_Model Param)
        {
            DataTable dtb = new DataTable();
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Reabrir_AM");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa", Param.Cod_Empresa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_MR", Param.Numero_Mr);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Sequencia_Mr", Param.Sequencia_Mr);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Documento_Para", Param.Documento_Para);
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




        public AMModel AMFalhas(String Cod_Empresa, Int32 Numero_Mr, Int32 Sequencia_Mr, String Documento_Para)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            AMModel TabelaFalhas = new AMModel();
            try
            {
                //SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_AM_Falhas");
                //Adp.SelectCommand = cmd;
                //Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa", Cod_Empresa);
                //Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Mr", Numero_Mr);
                //Adp.SelectCommand.Parameters.AddWithValue("@Par_Sequencia_Mr", Sequencia_Mr);
                //Adp.SelectCommand.Parameters.AddWithValue("@Par_Documento_Para", Documento_Para);
                //Adp.Fill(dtb);
                //if (dtb.Rows.Count > 0)
                //{

                TabelaFalhas.Falhas = AddFalhas(Cod_Empresa, Numero_Mr, Sequencia_Mr, Documento_Para);
                TabelaFalhas.Compensacoes = AddCompensacoes(Cod_Empresa, Documento_Para, Numero_Mr, Sequencia_Mr);
                TabelaFalhas.Qtd_Total_Falha = Qtd_Total_Falha;
                TabelaFalhas.Valor_Total_Falha = Valor_Total_Falha;
                TabelaFalhas.Qtd_Total_Compensacao= Qtd_Total_Compensacao;
                TabelaFalhas.Valor_Total_Compensacao= Valor_Total_Compensacao;
                TabelaFalhas.Cod_Empresa = Cod_Empresa;
                TabelaFalhas.Numero_Mr = Numero_Mr;
                TabelaFalhas.Sequencia_Mr = Sequencia_Mr;
                TabelaFalhas.Documento_Para = Documento_Para;

                //}
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cnn.Close();
            }
            return TabelaFalhas;
        }

        private List<Falhas_Model> AddFalhas(String Cod_Empresa, Int32 Numero_Mr, Int32 Sequencia_Mr, String Documento_Para)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            List<Falhas_Model> ListaFalhas = new List<Falhas_Model>();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_AM_Falhas");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa", Cod_Empresa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Mr", Numero_Mr);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Sequencia_Mr", Sequencia_Mr);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Documento_Para", Documento_Para);
                Adp.Fill(dtb);
                foreach (DataRow drw in dtb.Rows)
                {
                    Qtd_Total_Falha += drw["Qtd_Falhas"].ToString().ConvertToInt32();
                    Valor_Total_Falha += drw["Valor_Total"].ToString().ConvertToDouble();
                    ListaFalhas.Add(new Falhas_Model()
                    {

                        Data_Exibicao = drw["Data_Exibicao"].ToString(),
                        Cod_Programa = drw["Cod_Programa"].ToString(),
                        Cod_Comercial = drw["Cod_Comercial"].ToString(),
                        Titulo_Comercial = drw["Titulo_Comercial"].ToString(),
                        Duracao = drw["Duracao"].ToString().ConvertToInt32(),
                        Cod_Qualidade = drw["Cod_Qualidade"].ToString(),
                        Cod_Veiculo = drw["Cod_Veiculo"].ToString(),
                        Valor = drw["Valor_Total"].ToString().ConvertToDouble(),
                        Qtd_Falhas = drw["Qtd_Falhas"].ToString().ConvertToInt32()


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
            return ListaFalhas;
        }

        // Lista de Compensaçoes
        private List<Reencaixe_Model> AddCompensacoes(String Cod_Empresa, String Documento_Para, Int32 Numero_Mr, Int32 Sequencia_Mr)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            List<Reencaixe_Model> ListaCompensacoes = new List<Reencaixe_Model>();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Sp_Am_Compensacao");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa", Cod_Empresa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Documento_De", Documento_Para);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Mr", Numero_Mr);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Sequencia_Mr", Sequencia_Mr);

                Adp.Fill(dtb);
                foreach (DataRow drw in dtb.Rows)
                {
                    Qtd_Total_Compensacao+= drw["Qtd"].ToString().ConvertToInt32();
                    Valor_Total_Compensacao += drw["Valor"].ToString().ConvertToDouble();
                    ListaCompensacoes.Add(new Reencaixe_Model()
                    {
                        Cod_Programa = drw["Cod_Programa"].ToString(),
                        Data_Exibicao = drw["Data_Exibicao"].ToString().ConvertToDatetime().ToString("dd/MM/yyyy"),
                        Cod_Comercial = drw["Cod_Comercial"].ToString(),
                        Titulo_Comercial = drw["Titulo_Comercial"].ToString(),
                        Duracao = drw["Duracao"].ToString().ConvertToInt32(),
                        Qtd_Compensacao = drw["Qtd"].ToString().ConvertToInt32(),
                        Valor = drw["Valor"].ToString().ConvertToDouble(),


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
            return ListaCompensacoes;
        }





        public DataTable AMFalhasPesquisaComerciais(String Cod_Empresa, Int32 Numero_Mr, Int32 Sequencia_Mr, String Cod_Comercial)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_AM_Comerciais");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa", Cod_Empresa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Mr", Numero_Mr);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Sequencia_Mr", Sequencia_Mr);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Comercial", Cod_Comercial);
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

        public DataTable AMFalhaListarGrade(String Cod_Veiculo, String Competencia, String Cod_Empresa, Int32 Numero_Mr, Int32 Sequencia_Mr, String Cod_Programa)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Grade_Compensacao");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", Cod_Veiculo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia", clsLib.CompetenciaInt(Competencia));
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa", Cod_Empresa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Mr", Numero_Mr);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Sequencia_Mr", Sequencia_Mr);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", Cod_Programa);
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

        public DataTable AMFalhaSalvar(Reencaixe_Model Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Compensacao_Salvar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa", Param.Cod_Empresa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Mr", Param.Numero_Mr);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Sequencia_Mr", Param.Sequencia_Mr);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Documento_Para", Param.Documento_Para);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", Param.Cod_Veiculo);
                //Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia", clsLib.CompetenciaInt(Param.Competencia));
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", Param.Cod_Programa.ToUpper());
                if (String.IsNullOrEmpty(Param.Data_Exibicao))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Exibicao", DBNull.Value);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Exibicao", Param.Data_Exibicao.ConvertToDatetime().ToString("yyyy-MM-dd"));
                }
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Comercial", Param.Cod_Comercial.ToUpper());
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Duracao", Param.Duracao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Qtd_Compensacao", Param.Qtd_Compensacao);
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


        public DataTable GravarSolucaoCompensacaoFalhaSalvar(Reencaixe_Model Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Solucao_AM");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa", Param.Cod_Empresa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Mr", Param.Numero_Mr);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Sequencia_Mr", Param.Sequencia_Mr);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Solucao", Param.Solucao.Letra);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Docto", Param.Documento_Para);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Qtd_Compensacao", Param.Qtd_Compensacao );
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


        public DataTable ExcluirCompensacao(Reencaixe_Model pCompensacao)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Sp_Am_Exclusao");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", pCompensacao.Cod_Veiculo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Exibicao", pCompensacao.Data_Exibicao.ConvertToDatetime());
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", pCompensacao.Cod_Programa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa", pCompensacao.Cod_Empresa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_MR", pCompensacao.Numero_Mr);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Sequencia_MR", pCompensacao.Sequencia_Mr);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Comercial", pCompensacao.Cod_Comercial);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Duracao", pCompensacao.Duracao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Documento_Para", pCompensacao.Documento_Para);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_qtd", pCompensacao.Qtd_Compensacao);
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


        //==========================Efetuar reencaixe

        public Boolean EfetuarReencaixe(List<Reencaixe_Model> pReencaixe)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            //SqlDataAdapter Adp = new SqlDataAdapter();
            //DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            Boolean Retorno = true;
            try
            {
                for (int i = 0; i < pReencaixe.Count; i++)
                {
                    SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Am_Reencaixe");
                    //Adp.SelectCommand = cmd;
                    cmd.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                    cmd.Parameters.AddWithValue("@Par_Cod_Veiculo", pReencaixe[i].Cod_Veiculo);
                    cmd.Parameters.AddWithValue("@Par_Data_Exibicao", pReencaixe[i].Data_Exibicao.ConvertToDatetime());
                    cmd.Parameters.AddWithValue("@Par_Cod_Programa", pReencaixe[i].Cod_Programa);
                    cmd.Parameters.AddWithValue("@Par_Chave_Acesso", pReencaixe[i].Chave_Acesso);
                    cmd.Parameters.AddWithValue("@Par_Cod_Empresa", pReencaixe[i].Cod_Empresa);
                    cmd.Parameters.AddWithValue("@Par_Numero_Mr", pReencaixe[i].Numero_Mr);
                    cmd.Parameters.AddWithValue("@Par_Sequencia_Mr", pReencaixe[i].Sequencia_Mr);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (Exception Ex)
            {
                Retorno = false;
                throw new Exception(Ex.Message);
            }
            finally
            {
                cnn.Close();
            }
            return Retorno;
        }




    }
}
