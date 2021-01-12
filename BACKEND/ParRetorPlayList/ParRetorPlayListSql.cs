using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace PROPOSTA
{
    public partial class ParRetorPlayList
    {
        //------------------------- Filtrar Dados ---------------------------
        public RetornoPlayListModel ParRetorPlayListFiltrar(RetornoPlayListModel Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            RetornoPlayListModel Retorno = new RetornoPlayListModel();
            List<CamposModel> Campos = new List<CamposModel>();
            List<ValidacaoModel> Validacao = new List<ValidacaoModel>();
            try
            {
                Retorno.Cod_Veiculo = Param.Cod_Veiculo;
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_ParRetorPlayList_Filtrar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", Param.Cod_Veiculo);
                Adp.Fill(dtb);
                if (dtb.Rows.Count==0)
                {
                    Campos.Add(new CamposModel(){ Nome_Campo = "Data", Campo = "DATA", Posicao = "", Tamanho = "" });
                    Campos.Add(new CamposModel() { Nome_Campo = "Horário de Exibição", Campo = "HORARIO", Posicao = "", Tamanho = "" });
                    Campos.Add(new CamposModel() { Nome_Campo = "Titulo do Comercial", Campo = "TITULO", Posicao = "", Tamanho = "" });
                    Campos.Add(new CamposModel() { Nome_Campo = "Número da Fita", Campo = "FITA", Posicao = "", Tamanho = "" });
                    Campos.Add(new CamposModel() { Nome_Campo = "Duração", Campo = "DURACAO", Posicao = "", Tamanho = "" });
                }
                else
                {
                    Retorno.Tipo_Arquivo = dtb.Rows[0]["tipo_arquivo"].ToString();
                    foreach (DataRow drw in dtb.Rows)
                    {
                        if (drw["Indica_Validacao"].ToString() == "0")
                        {
                            if (drw["Campo"].ToString().TrimEnd() == "FORMATO")
                            {
                                Retorno.Formato_Data = drw["Conteudo"].ToString().TrimEnd();
                            }
                            if (drw["Campo"].ToString().TrimEnd() == "DATA")
                            {
                                Campos.Add(new CamposModel() { Nome_Campo = "Data", Campo = "DATA", Posicao = drw["Posicao"].ToString(), Tamanho = drw["Tamanho"].ToString() });
                            }
                            if (drw["Campo"].ToString().TrimEnd() == "HORARIO")
                            {
                                Campos.Add(new CamposModel() { Nome_Campo = "Horário de Exibição", Campo = "HORARIO", Posicao = drw["Posicao"].ToString(), Tamanho = drw["Tamanho"].ToString() });
                            }
                            if (drw["Campo"].ToString().TrimEnd() == "TITULO")
                            {
                                Campos.Add(new CamposModel() { Nome_Campo = "Título do Comercial", Campo = "TITULO", Posicao = drw["Posicao"].ToString(), Tamanho = drw["Tamanho"].ToString() });
                            }
                            if (drw["Campo"].ToString().TrimEnd() == "FITA")
                            {
                                Campos.Add(new CamposModel() { Nome_Campo = "Número da Fita", Campo = "FITA", Posicao = drw["Posicao"].ToString(), Tamanho = drw["Tamanho"].ToString() });
                            }
                            if (drw["Campo"].ToString().TrimEnd() == "DURACAO")
                            {
                                Campos.Add(new CamposModel() { Nome_Campo = "Duração", Campo = "DURACAO", Posicao = drw["Posicao"].ToString(), Tamanho = drw["Tamanho"].ToString() });
                            }
                        }
                        else
                        {
                            Validacao.Add(new ValidacaoModel() {
                                Descricao = drw["Campo"].ToString().TrimEnd(),
                                Posicao = drw["Posicao"].ToString(),
                                Tamanho = drw["Tamanho"].ToString(),
                                Conteudo = drw["Conteudo"].ToString().TrimEnd()
                            });
                        }
                    }
                }
                Retorno.Campos = Campos;
                Retorno.Validacao = Validacao;
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

        //------------------------ Salvar Dados -----------------------------
        public DataTable ParRetorPlayListSalvar(RetornoPlayListModel Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            String xmlValidacao= null;
            String xmlCampos = clsLib.SerializeToString(Param.Campos);
            if (Param.Validacao.Count>0)
            {
                xmlValidacao= clsLib.SerializeToString(Param.Validacao);
            }
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_ParRetorPlayList_Salvar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", Param.Cod_Veiculo) ;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Campos", xmlCampos);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Validacao", xmlValidacao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Formato_Data", Param.Formato_Data);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Tipo_Arquivo", Param.Tipo_Arquivo);
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



