using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Data.OleDb;

namespace PROPOSTA
{
    public partial class RetornoPlayList
    {

        //------------------------- Carrega Dados ---------------------------
        public RetornoPlayListModel RetornoPlayListDados(RetornoPlayListModel Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            RetornoPlayListModel Retorno = new RetornoPlayListModel();
            try
            {
                Retorno.Cod_Veiculo = Param.Cod_Veiculo;
                Retorno.Nome_Veiculo = Param.Nome_Veiculo;
                Retorno.Data_Exibicao = Param.Data_Exibicao;
                Retorno.Indica_Fitas_Avulsas = true;
                Retorno.Indica_Fitas_Artisticas = true;

                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Retorno_Playlist_Dados");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", Param.Cod_Veiculo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Exibicao", Param.Data_Exibicao.ConvertToDatetime());
                Adp.Fill(dtb);

                if (dtb.Rows.Count > 0)
                {
                    Retorno.Horario_Emissora = dtb.Rows[0]["Horario_Emissora"].ToString().Trim();
                    Retorno.Sistema_Exibicao = dtb.Rows[0]["Sistema_Exibicao"].ToString().Trim();
                    Retorno.Data_Inicio = dtb.Rows[0]["Data_Inicio"].ToString().Trim().Substring(0, 10);
                    Retorno.Hora_Inicio = dtb.Rows[0]["Hora_Inicio"].ToString().Trim();
                    Retorno.Data_Fim = dtb.Rows[0]["Data_Fim"].ToString().Trim().Substring(0, 10);
                    Retorno.Hora_Fim = dtb.Rows[0]["Hora_Fim"].ToString().Trim();
                    Retorno.Tipo_Arquivo = dtb.Rows[0]["Tipo_Arquivo"].ToString().Trim();
                    Retorno.Nome_Tabela = dtb.Rows[0]["Nome_Tabela"].ToString().Trim();
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
        //------------------------ Faz Consistencias ----------------------------------
        public DataTable RetornoPlayListConsistencias(RetornoPlayListModel pParam)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Retorno_Playlist_Consistencias");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", pParam.Cod_Veiculo);
                if (!String.IsNullOrEmpty(pParam.Data_Exibicao))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Exibicao", pParam.Data_Exibicao.ConvertToDatetime());
                }
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Qtd_Arquivo", pParam.Anexos.Count);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Fitas_Avulsas", pParam.Indica_Fitas_Avulsas);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Fitas_Artisticas", pParam.Indica_Fitas_Artisticas);
                if (!String.IsNullOrEmpty(pParam.Data_Inicio))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Inicio", pParam.Data_Inicio.ConvertToDatetime());
                }
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Hora_Inicio", pParam.Hora_Inicio);
                if (!String.IsNullOrEmpty(pParam.Data_Fim))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Fim", pParam.Data_Fim.ConvertToDatetime());
                }
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Hora_Fim", pParam.Hora_Fim);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Sistema_Exibicao", pParam.Sistema_Exibicao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Tipo_Arquivo", pParam.Tipo_Arquivo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Nome_Tabela", pParam.Nome_Tabela);
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
        //------------------------ Processa Arquivos CSV ------------------------------
        public void RetornoPlayListProcessaCSV(RetornoPlayListModel pParam, String Arquivo)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            //DataTable dtbRetorno = new DataTable("dtbBaixaAutomatica");
            cnn.Open();
            try
            {
                //---------------------Apaga Processamento Anterior
                //this.ApagaArquivoAnterior(pParam.Cod_Veiculo);

                //-----------------Abre o arquivo texto que fez upload
                //String sPath = HttpContext.Current.Server.MapPath("RETORNO_PLAY_LIST");
                String sPath = HttpContext.Current.Server.MapPath("~/ANEXOS/RETORNO_PLAYLIST");
                if (sPath.Right(1) != @"\")
                {
                    sPath += @"\";
                }
                sPath += this.CurrentUser;
                sPath += @"\";
                String strFile = sPath + Arquivo;

                //-----------------Ler o Arquivo de retorno e gravar no sql 
                var lines = File.ReadAllLines(strFile);

                for (var i = 0; i < lines.Length; i += 1)
                {
                    //-------------------------------------Executa a procedure para cada linha
                    SqlCommand cmdInsert = cnn.Procedure(cnn.Connection, "Pr_Proposta_Retorno_PlayList_Processa_CSV");
                    cmdInsert.Parameters.AddWithValue("@Par_Cod_Veiculo", pParam.Cod_Veiculo);
                    cmdInsert.Parameters.AddWithValue("@Par_Data_Inicio", pParam.Data_Inicio.ConvertToDatetime());
                    cmdInsert.Parameters.AddWithValue("@Par_Hora_Inicio", pParam.Hora_Inicio);
                    cmdInsert.Parameters.AddWithValue("@Par_Data_Fim", pParam.Data_Fim.ConvertToDatetime());
                    cmdInsert.Parameters.AddWithValue("@Par_Hora_Fim", pParam.Hora_Fim);
                    cmdInsert.Parameters.AddWithValue("@Par_Linha", lines[i]);
                    cmdInsert.ExecuteNonQuery();

                    // -----fim do for mata a instancia criada dentro do for
                    cmdInsert.Dispose();
                }

                ////---------------------Carrega Conciliacao da baixa
                //SqlCommand cmdBaixa = cnn.Procedure(cnn.Connection, "Sp_Retorno_Play_List");
                //cmdBaixa.Parameters.AddWithValue("@Par_Cod_Veiculo", pParam.Cod_Veiculo);
                //cmdBaixa.Parameters.AddWithValue("@Par_Data_Exibicao", pParam.Data_Exibicao.ConvertToDatetime());
                //cmdBaixa.Parameters.AddWithValue("@Par_Avulso", pParam.Indica_Fitas_Avulsas);
                //cmdBaixa.Parameters.AddWithValue("@Par_Artistico", pParam.Indica_Fitas_Artisticas);
                //SqlDataAdapter adpBaixa = new SqlDataAdapter(cmdBaixa);
                ////adpBaixa.Fill(dtbRetorno);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cnn.Close();
            }
            //return dtbRetorno;
        }
        //------------------------ Processa Arquivos ------------------------------
        public void RetornoPlayListProcessaTXT(RetornoPlayListModel pParam, String Arquivo)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            //DataTable dtbRetorno = new DataTable("dtbBaixaAutomatica");
            cnn.Open();
            try
            {

                //-----------------Abre o arquivo texto que fez upload
                //String sPath = HttpContext.Current.Server.MapPath("RETORNO_PLAY_LIST");
                String sPath = HttpContext.Current.Server.MapPath("~/ANEXOS/RETORNO_PLAYLIST");
                if (sPath.Right(1) != @"\")
                {
                    sPath += @"\";
                }
                sPath += this.CurrentUser;
                sPath += @"\";
                String strFile = sPath + Arquivo;

                //-----------------Lê o Arquivo de retorno e grava no sql 
                var lines = File.ReadAllLines(strFile);

                for (var i = 0; i < lines.Length; i += 1)
                {
                    //-------------------------------------Executa a procedure para cada linha
                    SqlCommand cmdInsert = cnn.Procedure(cnn.Connection, "Pr_Proposta_Retorno_PlayList_Processa_TXT");
                    cmdInsert.Parameters.AddWithValue("@Par_Cod_Veiculo", pParam.Cod_Veiculo);
                    cmdInsert.Parameters.AddWithValue("@Par_Data_Inicio", pParam.Data_Inicio.ConvertToDatetime());
                    cmdInsert.Parameters.AddWithValue("@Par_Hora_Inicio", pParam.Hora_Inicio);
                    cmdInsert.Parameters.AddWithValue("@Par_Data_Fim", pParam.Data_Fim.ConvertToDatetime());
                    cmdInsert.Parameters.AddWithValue("@Par_Hora_Fim", pParam.Hora_Fim);
                    cmdInsert.Parameters.AddWithValue("@Par_Linha", lines[i]);
                    cmdInsert.ExecuteNonQuery();

                    // -----fim do for mata a instancia criada dentro do for
                    cmdInsert.Dispose();
                }
                ////---------------------Carrega Conciliacao da baixa
                //SqlCommand cmdBaixa = cnn.Procedure(cnn.Connection, "Sp_Retorno_Play_List");
                //cmdBaixa.Parameters.AddWithValue("@Par_Cod_Veiculo", pParam.Cod_Veiculo);
                //cmdBaixa.Parameters.AddWithValue("@Par_Data_Exibicao", pParam.Data_Exibicao.ConvertToDatetime());
                //cmdBaixa.Parameters.AddWithValue("@Par_Avulso", pParam.Indica_Fitas_Avulsas);
                //cmdBaixa.Parameters.AddWithValue("@Par_Artistico", pParam.Indica_Fitas_Artisticas);
                //SqlDataAdapter adpBaixa = new SqlDataAdapter(cmdBaixa);
                //adpBaixa.Fill(dtbRetorno);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cnn.Close();
            }
            //return dtbRetorno;
        }
        //------------------------ Conciliacao Arquivo Retorno X Roteiro ----------
        public DataTable ProcessaConciliacao(RetornoPlayListModel pParam)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            DataTable dtbRetorno = new DataTable("dtbBaixaAutomatica");
            cnn.Open();
            try
            {
                SqlCommand cmdBaixa = cnn.Procedure(cnn.Connection, "Sp_Retorno_Play_List");
                cmdBaixa.Parameters.AddWithValue("@Par_Cod_Veiculo", pParam.Cod_Veiculo);
                cmdBaixa.Parameters.AddWithValue("@Par_Data_Exibicao", pParam.Data_Exibicao.ConvertToDatetime());
                cmdBaixa.Parameters.AddWithValue("@Par_Avulso", pParam.Indica_Fitas_Avulsas);
                cmdBaixa.Parameters.AddWithValue("@Par_Artistico", pParam.Indica_Fitas_Artisticas);
                SqlDataAdapter adpBaixa = new SqlDataAdapter(cmdBaixa);
                adpBaixa.Fill(dtbRetorno);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cnn.Close();
            }
            return dtbRetorno;
        }
        //---------------------Apaga o Arquivo Anterior
        public void ApagaArquivoRetornoPlayList(String pCodVeiculo)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            String strComando = "Delete From Retorno_PlayList Where Cod_Veiculo = '" + pCodVeiculo + "'";
            SqlCommand cmd = cnn.Text(cnn.Connection, strComando);
            cmd.BeginExecuteNonQuery();
        }
        //---------------------Processa a baixa
        public void BaixarVeiculacao(List<RetornoPlayListBaixaModel> Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            Boolean bolValido;
            SimLib clsLib = new SimLib();
            int dia;
            int mes;
            int ano;
            int hora;
            int minuto;
            int segundo;
            DateTime DataExibicao;
            try
            {
                for (int i = 0; i < Param.Count; i++)
                {
                    if (!String.IsNullOrEmpty(Param[i].Cod_Qualidade))
                    {
                        Param[i].Cod_Qualidade = Param[i].Cod_Qualidade.ToUpper();
                    }


                    Param[i].Indica_Critica = false;
                    Param[i].Mensagem_Critica = "";
                    Param[i].Indica_Processado= false;

                    bolValido = true;
                    if (Param[i].Status == 2)
                    {
                        bolValido = false;
                    }

                    if (Param[i].Status == 1)
                    {
                        if (Param[i].Cod_Qualidade == "VEI" && String.IsNullOrEmpty(Param[i].Horario_Exibicao))
                        {
                            Param[i].Indica_Critica = true;
                            Param[i].Mensagem_Critica = "Não foi informado Horário";
                            bolValido = false;
                        }
                        else
                        {
                            if (!String.IsNullOrEmpty(Param[i].Horario_Exibicao) && String.IsNullOrEmpty(Param[i].Cod_Qualidade)) 
                            {
                                Param[i].Indica_Critica = true;
                                Param[i].Mensagem_Critica = "Não foi informado Qualidade";
                                bolValido = false;
                            }
                        }
                    }
                    if (String.IsNullOrEmpty(Param[i].Cod_Qualidade))
                    {
                        bolValido = false;
                    }
                    if (String.IsNullOrEmpty(Param[i].Numero_Mr ) && Param[i].Status != 2)
                    {
                        Param[i].Indica_Critica = true;
                        Param[i].Mensagem_Critica = "Comercial não é de Mídia";
                        bolValido = false;
                    }
                    if (!String.IsNullOrEmpty(Param[i].Numero_Ce) && Param[i].Status != 2)
                    {
                        Param[i].Indica_Critica = true;
                        Param[i].Mensagem_Critica = "Comprovante Emitido";
                        bolValido = false;
                    }
                    if (!String.IsNullOrEmpty(Param[i].Cod_Qualidade_Anterior) && Param[i].Status != 2)
                    {
                        Param[i].Indica_Critica = true;
                        Param[i].Mensagem_Critica = "Veiculação já estava baixada";
                        bolValido = false;
                    }
                    if (Param[i].Cod_Veiculo != Param[i].Cod_Veiculo_Origem && Param[i].Status!=2)
                    {
                        Param[i].Mensagem_Critica = "Veiculação origem de outro Veículo";
                        bolValido = false;
                    }


                    if (bolValido)
                    {
                        //--- Baixa ----------------
                        SqlDataAdapter Adp = new SqlDataAdapter();
                        DataTable dtb = new DataTable("dtb");
                        SqlCommand cmd = cnn.Procedure(cnn.Connection, "Sp_Baixa");

                        Adp.SelectCommand = cmd;
                        Adp.SelectCommand.Parameters.AddWithValue("@Data_Exibicao", Param[i].Data_Exibicao);
                        Adp.SelectCommand.Parameters.AddWithValue("@Cod_Veiculo", Param[i].Cod_Veiculo);
                        Adp.SelectCommand.Parameters.AddWithValue("@Cod_Programa", Param[i].Cod_Programa_Origem);
                        Adp.SelectCommand.Parameters.AddWithValue("@Chave_Acesso", Param[i].Chave_Acesso);
                        Adp.SelectCommand.Parameters.AddWithValue("@Cod_Qualidade", Param[i].Cod_Qualidade);
                        Adp.SelectCommand.Parameters.AddWithValue("@Documento_Para", "");
                        Adp.SelectCommand.Parameters.AddWithValue("@Indica_Convidado", 0);
                        if (!String.IsNullOrEmpty(Param[i].Horario_Exibicao))
                        {
                            dia = Param[i].Data_Exibicao.Day;
                            mes = Param[i].Data_Exibicao.Month;
                            ano = Param[i].Data_Exibicao.Year;
                            hora = Param[i].Horario_Exibicao.Substring(0, 2).ConvertToInt32();
                            minuto = Param[i].Horario_Exibicao.Substring(3, 2).ConvertToInt32();
                            //segundo = Param[i].Horario_Exibicao.Substring(6, 2).ConvertToInt32();
                            segundo = 0;
                            DataExibicao = new DateTime(ano, mes, dia, hora, minuto, segundo);

                            Adp.SelectCommand.Parameters.AddWithValue("@Data_Limite", DataExibicao);
                        }
                        else
                        {
                            Adp.SelectCommand.Parameters.AddWithValue("@Data_Limite", "");
                        }
                        Adp.SelectCommand.Parameters.AddWithValue("@Cod_Usuario", this.CurrentUser);
                        Adp.Fill(dtb);

                        //--- Salva IEV ----------------
                        if (Param[i].Cod_Qualidade.ToUpper() != "VEI")
                        {
                            this.CriaIev(Param[i]);
                        }
                        //--- Propaga Baixas para Fitas de patrocinio ----------------
                        this.PropagaFitaPatrocinio(Param[i]);
                        //--- Atualiza dados recebidos no Param
                        Param[i].Cod_Qualidade_Anterior = Param[i].Cod_Qualidade;
                        Param[i].Indica_Processado = true;
                        dtb.Dispose();
                        cmd.Dispose();
                        Adp.Dispose();
                    }

                } /// fim do for
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
        //------------------------Cria o Iev
        private void CriaIev(RetornoPlayListBaixaModel Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            DataTable dtb = new DataTable("dtb");
            SqlCommand cmd = cnn.Procedure(cnn.Connection, "prNet_Iev_Insert");
            try
            {
                cmd.Parameters.AddWithValue("@PCod_Empresa", Param.Cod_Empresa);
                cmd.Parameters.AddWithValue("@PNumero_Mr", Param.Numero_Mr);
                cmd.Parameters.AddWithValue("@PSequencia_Mr", Param.Sequencia_Mr);
                cmd.Parameters.AddWithValue("@PIndica_Valora", 2);
                cmd.Parameters.AddWithValue("@PCod_Motivo_Valoracao", 3);
                cmd.Parameters.AddWithValue("@Par_Cod_Usuario", this.CurrentUser);
                cmd.ExecuteNonQuery();
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
        //------------------------Propaga Fita Patrocinio
        private void PropagaFitaPatrocinio(RetornoPlayListBaixaModel Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            DataTable dtb = new DataTable("dtb");
            SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_Proposta_Propaga_Baixa_Patrocinio");
            try
            {
                cmd.Parameters.AddWithValue("@Par_Cod_Empresa", Param.Cod_Empresa);
                cmd.Parameters.AddWithValue("@Par_Numero_Mr", Param.Numero_Mr);
                cmd.Parameters.AddWithValue("@Par_Sequencia_Mr", Param.Sequencia_Mr);
                cmd.Parameters.AddWithValue("@Par_Cod_Comercial", Param.Cod_Comercial);
                cmd.Parameters.AddWithValue("@Par_Cod_Veiculo", Param.Cod_Veiculo);
                cmd.Parameters.AddWithValue("@Par_Data_Exibicao", Param.Data_Exibicao);
                cmd.Parameters.AddWithValue("@Par_Cod_Programa", Param.Cod_Programa);
                cmd.Parameters.AddWithValue("@Par_Chave_Acesso", Param.Chave_Acesso);
                cmd.Parameters.AddWithValue("@Par_Cod_Qualidade", Param.Cod_Qualidade);
                cmd.Parameters.AddWithValue("@Par_Horario_Exibicao", Param.Horario_Exibicao);
                cmd.Parameters.AddWithValue("@Par_Usuario", this.CurrentUser);
                cmd.ExecuteNonQuery();
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

        //------------------------ Apaga Anexos do Upload------------------------------
        public void ApagaAnexos()
        {
            //-----------------Abre o arquivo texto que fez upload
            String sPath = HttpContext.Current.Server.MapPath("~/ANEXOS/RETORNO_PLAYLIST");
            if (sPath.Right(1) != @"\")
            {
                sPath += @"\";
            }
            sPath += this.CurrentUser;
            sPath += @"\";

            DirectoryInfo dir = new DirectoryInfo(sPath);

            foreach (FileInfo fi in dir.GetFiles())
            {
                fi.Delete();
            }
        }
    }
}





