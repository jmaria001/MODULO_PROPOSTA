using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace PROPOSTA
{

    public partial class BaixaVeiculacoes
    {
        //===========================baixar veiculacoes
        public DataTable BaixaVeiculacoesListar(FiltroModel pFiltro)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "[Pr_Proposta_Listar_Baixa_Veiculacao]");
                Adp.SelectCommand = cmd;
                //Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", pFiltro.Cod_Veiculo);
                if (String.IsNullOrEmpty(pFiltro.Data_Exibicao))
                {

                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Exibicao", pFiltro.Data_Exibicao);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Exibicao", pFiltro.Data_Exibicao.ConvertToDatetime());

                }

                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", pFiltro.Cod_Programa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Chave_Acesso", pFiltro.Chave_Acesso);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa", pFiltro.Cod_Empresa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Mr", pFiltro.Numero_Mr);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Sequencia_Mr", pFiltro.Sequencia_Mr);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Comercial", pFiltro.Cod_Comercial);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Duracao", pFiltro.Duracao);
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


        public DataTable ValidarQualidade(BaixaVeiculacoesModel pcodQualidade)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();

            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Validar_Baixa");
                Adp.SelectCommand = cmd;
                clsLib.NewParameter(Adp, "@Par_Cod_Veiculo", pcodQualidade.Cod_Veiculo);
                clsLib.NewParameter(Adp, "@Par_Data_Exibicao", pcodQualidade.Data_Exibicao.ConvertToDatetime());
                clsLib.NewParameter(Adp, "@Par_Cod_Programa", pcodQualidade.Cod_Programa);
                clsLib.NewParameter(Adp, "@Par_Chave_Acesso", pcodQualidade.Chave_Acesso);
                clsLib.NewParameter(Adp, "@Par_Cod_Qualidade", pcodQualidade.Cod_Qualidade.ToUpper());
                clsLib.NewParameter(Adp, "@Par_Cod_Qualidade_Ant", pcodQualidade.Cod_Qualidade_Ant);
                clsLib.NewParameter(Adp, "@Par_Horario_Exibicao", pcodQualidade.Horario_Exibicao);
                clsLib.NewParameter(Adp, "@Par_Documento_De", pcodQualidade.Documento_De);
                clsLib.NewParameter(Adp, "@Par_Documento_Para", pcodQualidade.Documento_Para);
                clsLib.NewParameter(Adp, "@Par_Cod_Empresa", pcodQualidade.Cod_Empresa);
                clsLib.NewParameter(Adp, "@Par_Numero_Mr", pcodQualidade.Numero_Mr);
                clsLib.NewParameter(Adp, "@Par_Sequencia_Mr", pcodQualidade.Sequencia_Mr);

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

        public List<BaixaVeiculacoesModel> DaBaixaVeiculaçoes(List<BaixaVeiculacoesModel> pBaixaVeiculacoes)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            pBaixaVeiculacoes[0].Qtd_Baixados = 0;
            pBaixaVeiculacoes[0].Critica = false;
            SimLib clsLib = new SimLib();
            int nCont = 0;
            try
            {
                for (int i = 0; i < pBaixaVeiculacoes.Count; i++)
                {
                    if (pBaixaVeiculacoes[i].Cod_Qualidade_Ant != pBaixaVeiculacoes[i].Cod_Qualidade || pBaixaVeiculacoes[i].Horario_Exibicao_Ant != pBaixaVeiculacoes[i].Horario_Exibicao)
                    {
                        nCont++;
                        //---------------------Limpa as critica da linha
                        pBaixaVeiculacoes[i].Mensagem = "";
                        pBaixaVeiculacoes[i].Status = true;
                        pBaixaVeiculacoes[i].Indica_Processado = false;
                        //---------------------Processa a Linha
                        SqlDataAdapter Adp = new SqlDataAdapter();
                        DataTable dtb = new DataTable("dtb");
                        SqlCommand cmd = cnn.Procedure(cnn.Connection, "[Pr_Proposta_Baixa_Veiculacao]");
                        Adp.SelectCommand = cmd;
                        clsLib.NewParameter(Adp, "@Par_Login", this.CurrentUser);
                        clsLib.NewParameter(Adp, "@Par_Cod_Qualidade", pBaixaVeiculacoes[i].Cod_Qualidade);
                        clsLib.NewParameter(Adp, "@Par_Cod_Veiculo", pBaixaVeiculacoes[i].Cod_Veiculo);
                        clsLib.NewParameter(Adp, "@Par_Documento_De", pBaixaVeiculacoes[i].Documento_De);
                        clsLib.NewParameter(Adp, "@Par_Documento_Para", pBaixaVeiculacoes[i].Documento_Para);
                        clsLib.NewParameter(Adp, "@Par_Cod_Empresa", pBaixaVeiculacoes[i].Cod_Empresa);
                        clsLib.NewParameter(Adp, "@Par_Numero_Mr", pBaixaVeiculacoes[i].Numero_Mr);
                        clsLib.NewParameter(Adp, "@Par_Sequencia_Mr", pBaixaVeiculacoes[i].Sequencia_Mr);
                        clsLib.NewParameter(Adp, "@Par_Data_Exibicao", pBaixaVeiculacoes[i].Data_Exibicao.ConvertToDatetime());
                        clsLib.NewParameter(Adp, "@Par_Horario_Exibicao", pBaixaVeiculacoes[i].Horario_Exibicao);
                        clsLib.NewParameter(Adp, "@Par_Horario_Exibicao_Ant", pBaixaVeiculacoes[i].Horario_Exibicao_Ant);
                        clsLib.NewParameter(Adp, "@Par_Cod_Qualidade_Ant", pBaixaVeiculacoes[i].Cod_Qualidade_Ant);
                        clsLib.NewParameter(Adp, "@Par_Chave_Acesso", pBaixaVeiculacoes[i].Chave_Acesso);
                        clsLib.NewParameter(Adp, "@Par_Cod_Programa", pBaixaVeiculacoes[i].Cod_Programa);

                        Adp.Fill(dtb);
                        pBaixaVeiculacoes[i].Status = dtb.Rows[0]["Status"].ToString().ConvertToBoolean();
                        pBaixaVeiculacoes[i].Mensagem = dtb.Rows[0]["Mensagem"].ToString();
                        pBaixaVeiculacoes[i].Indica_Processado = true;
                        //-----Se nao houve houve erro, seta os dados atuais da veiculacao
                        if (dtb.Rows[0]["Status"].ToString().ConvertToBoolean())
                        {
                            pBaixaVeiculacoes[i].Cod_Qualidade= dtb.Rows[0]["Cod_Qualidade"].ToString();
                            pBaixaVeiculacoes[i].Cod_Qualidade_Ant = dtb.Rows[0]["Cod_Qualidade"].ToString();
                            pBaixaVeiculacoes[i].Horario_Exibicao = dtb.Rows[0]["Horario_Exibicao"].ToString();
                            pBaixaVeiculacoes[i].Horario_Exibicao_Ant = dtb.Rows[0]["Horario_Exibicao"].ToString();
                            pBaixaVeiculacoes[i].Documento_De= dtb.Rows[0]["Documento_De"].ToString();
                            pBaixaVeiculacoes[i].Documento_Para = dtb.Rows[0]["Documento_Para"].ToString();
                            pBaixaVeiculacoes[i].Mensagem = "Baixa concluida com Sucesso";
                        }
                        //-----Se houve erro em qualquer linha, marca a linha zero como critica
                        if (!dtb.Rows[0]["Status"].ToString().ConvertToBoolean())
                        {
                            pBaixaVeiculacoes[0].Critica = true;
                        }

                        cmd.Dispose();
                        Adp.Dispose();
                        dtb.Dispose();
                    }
                }
                //-----------------Marca na linha zero a qtd de veiculacoes baixadas
                pBaixaVeiculacoes[0].Qtd_Baixados = nCont;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cnn.Close();
            }
            return pBaixaVeiculacoes;
        }




    }
}