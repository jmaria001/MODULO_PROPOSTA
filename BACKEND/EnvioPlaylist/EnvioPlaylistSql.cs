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
    public partial class EnvioPlayList
    {
        //------------------------- Filtrar Dados da Playlist---------------------------
        public EnvioPlayListModel EnvioPlayListFiltrar(EnvioPlayListModel Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            EnvioPlayListModel Retorno = new EnvioPlayListModel();
            try
            {
                Retorno.Cod_Veiculo = Param.Cod_Veiculo;
                Retorno.Nome_Veiculo = Param.Nome_Veiculo;
                Retorno.Data_Programacao = Param.Data_Programacao;

                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Playlist_Filtrar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", Param.Cod_Veiculo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Prog", Param.Data_Programacao.ConvertToDatetime());
                Adp.Fill(dtb);

                if (dtb.Rows.Count > 0)
                {
                    Retorno.Exibidor = dtb.Rows[0]["Exibidor"].ToString().Trim();
                    Retorno.Nome_Arquivo = dtb.Rows[0]["Nome_Arquivo"].ToString().Trim();
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



        //------------------------- Filtrar Parâmetros---------------------------
        public EnvioPlayListModel EnvioPlayListFiltrarParametros(EnvioPlayListModel Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            EnvioPlayListModel Retorno = new EnvioPlayListModel();
            try
            {
                Retorno.Cod_Veiculo = Param.Cod_Veiculo;
                Retorno.Nome_Veiculo = Param.Nome_Veiculo;
                //Retorno.Data_Programacao = Param.Data_Programacao;
                //Retorno.Exibidor = Param.Exibidor;
                //Retorno.Nome_Arquivo = Param.Nome_Arquivo;
                //Retorno.Sistema_Exibicao_Digital = Param.Exibidor;

                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Playlist_Parametros_Salvar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Funcao", 'F');
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", Param.Cod_Veiculo);
                Adp.Fill(dtb);
                if (dtb.Rows.Count > 0)
                {
                    Retorno.Sistema_Exibicao_Digital = dtb.Rows[0]["Sistema_Exibicao_Digital"].ToString().Trim();
                    Retorno.Nome_Arquivo_Integracao = dtb.Rows[0]["Nome_Arquivo_Integracao"].ToString().Trim();
                    Retorno.Posicao_Num_Fita = dtb.Rows[0]["Posicao_Num_Fita"].ToString().Trim();
                    Retorno.Tamanho_Num_Fita = dtb.Rows[0]["Tamanho_Num_Fita"].ToString().Trim();

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

        //------------------------ Salvar Parâmetros -----------------------------
        public DataTable EnvioPlayListSalvarParametros(EnvioPlayListModel Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Playlist_Parametros_Salvar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Funcao", 'S');
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", Param.Cod_Veiculo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Sistema_Exibicao_Digital", Param.Sistema_Exibicao_Digital);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Nome_Arquivo_Integracao", Param.Nome_Arquivo_Integracao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Posicao_Num_Fita", Param.Posicao_Num_Fita);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Tamanho_Num_Fita", Param.Tamanho_Num_Fita);
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

        //------------------------ Verifica se Existe Roteiro Encerrado-----------------------------
        public Boolean ExisteRoteiroEncerrado(EnvioPlayListModel Param)
        {
            Boolean Retorno = true;
            //Descomentar abaixo quando fizer o encerramento do roteiro
            //clsConexao cnn = new clsConexao(this.Credential);
            //cnn.Open();
            //DataTable dtb = new DataTable("dtb");
            //SimLib clsLib = new SimLib();
            //try
            //{
            //    String sSql = "Select * From Roteiro_Fechamento with (Nolock)";

            //    sSql += " Where Cod_Veiculo = '" + Param.Cod_Veiculo + "'";
            //    sSql += " And Data = '" + Param.Data_Programacao.ConvertToDatetime().ToString("yyyy-MM-dd") + "'";
            //    SqlCommand cmd = cnn.Text(cnn.Connection, sSql);
            //    SqlDataAdapter Adp = new SqlDataAdapter(cmd);
            //    Adp.Fill(dtb);
            //    if (dtb.Rows.Count==0)
            //    {
            //        Retorno = false;
            //    }
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
            //finally
            //{
            //    cnn.Close();
            //}
            return Retorno;
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------- GERAR ARQUIVO FLORIPA -----------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------------------------------------
        public String GerarFloripa(EnvioPlayListModel Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            String Retorno = "";
            Boolean bolValido = false;
            Boolean bolGerouArquivo = false;
            String strLinha = "";
            //---------------------------Cria uma pasta no servidor para gerar o arquivo
            String sPath = HttpContext.Current.Server.MapPath("~/PLAYLIST");
            if (sPath.Right(1) != @"\")
            {
                sPath += @"\";
            }
            sPath += this.CurrentUser;
            sPath += @"\";
            if (!System.IO.Directory.Exists(sPath))
            {
                System.IO.Directory.CreateDirectory(sPath);
            }
            //-----------------------Apaga todos os arquivos da pasta antes da geracao , para nao acumular arquivos.
            var list = System.IO.Directory.GetFiles(sPath, "*.*");
            try
            {
                foreach (var item in list)
                {
                    System.IO.File.Delete(item);
                }
            }
            catch (Exception)
            {
            }
            //---------------------------Criar o Arquivo Texto na pasta
            String strFile = sPath + Param.Nome_Arquivo;
            var PlayListFile = File.Create(strFile);
            try
            {
                //---------------------------Le Parametro de Configruacao de Fitas
                Int32 nPosicaoFita = 1;//  default
                Int32 nTamanhaFita = 6;  //  default

                DataTable dtbParam = new DataTable();
                String strSql = "Select Cod_Chave From Parametro_Valor Where Cod_Parametro = 76 and Cod_Veiculo = '" + Param.Cod_Veiculo + "'";
                SqlCommand cmdParam = cnn.Text(cnn.Connection, strSql);
                SqlDataAdapter AdpParam = new SqlDataAdapter(cmdParam);
                AdpParam.Fill(dtbParam);
                if (dtbParam.Rows.Count > 0)
                {
                    nPosicaoFita = dtbParam.Rows[0]["Cod_Chave"].ToString().Left(2).ConvertToInt32();
                    nTamanhaFita = dtbParam.Rows[0]["Cod_Chave"].ToString().TrimEnd().Right(2).ConvertToInt32();
                }
                //----------------------------------Consulta o Roteiro Tecnico[
                DataTable dtbRoteiro = new DataTable();
                SqlCommand cmdRoteiro = cnn.Procedure(cnn.Connection, "Sp_RU_Carrega_Roteiro");
                cmdRoteiro.Parameters.AddWithValue("@Par_Cod_Veiculo", Param.Cod_Veiculo);
                cmdRoteiro.Parameters.AddWithValue("@Par_Data_Exibicao", Param.Data_Programacao.ConvertToDatetime().ToString("yyyy-MM-dd"));
                SqlDataAdapter adpRoteiro = new SqlDataAdapter(cmdRoteiro);
                adpRoteiro.Fill(dtbRoteiro);
                using (StreamWriter writetext = new StreamWriter(PlayListFile))
                {
                    //----------------------------------Preenche o Arquivo Texto
                    foreach (DataRow drw in dtbRoteiro.Rows)
                    {
                        bolValido = true;
                        //---------------------------------Linha titulo do intervalo nao é valido
                        if (drw["Indica_Titulo_Intervalo"].ToString() == "1")
                        {
                            bolValido = false;
                        }
                        //===============Linhas cujo tipo do break o usuario nao pode ordenar
                        if (drw["Indica_Ordenacao"].ToString() == "0" && (drw["Indica_Titulo_Break"].ToString() == "0" || String.IsNullOrEmpty(drw["Indica_Titulo_Break"].ToString())))
                        {
                            bolValido = false;
                        }

                        if (bolValido)
                        {
                            //===============Monta Linha de Titulo do Break
                            if (drw["Indica_Titulo_Break"].ToString() == "1")
                            {
                                strLinha = "*";
                                strLinha += drw["Cod_Programa"].ToString().Substring(0, 4);
                                strLinha += " ";
                                strLinha += drw["Titulo_Break"].ToString().Substring(0, 9);
                                strLinha += " BREAK:";
                                strLinha += "  ";
                                if (String.IsNullOrEmpty(drw["Sequencia_Break"].ToString()))
                                {
                                    strLinha += drw["Breaks"].ToString().PadLeft(2, '0');
                                }
                                else
                                {
                                    strLinha += drw["Sequencia_Break"].ToString().PadLeft(2, '0');
                                }
                                strLinha += "       ";
                                strLinha += "00:00:00";
                            }
                            else
                            //===============Monta Linha do Comercial
                            {
                                if (!String.IsNullOrEmpty(drw["Numero_Fita"].ToString()))
                                {
                                    strLinha = drw["Numero_Fita"].ToString().Substring(nPosicaoFita - 1, nTamanhaFita);
                                }

                            }

                            writetext.WriteLine(strLinha);
                            bolGerouArquivo = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                if (bolGerouArquivo)
                {
                    Retorno = "PLAYLIST/" + this.CurrentUser.ToUpper() + "/" + Param.Nome_Arquivo.ToUpper();
                }
                
                PlayListFile.Close();
            }
            return Retorno;
        }


        //--------------------------------------------------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------- GERAR ARQUIVO LOUTH -------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------------------------------------
        public String GerarLouth(EnvioPlayListModel Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            String Retorno = "";
            Boolean bolValido = false;
            Boolean bolGerouArquivo = false;
            String strLinha = "";
            //---------------------------Cria uma pasta no servidor para gerar o arquivo
            String sPath = HttpContext.Current.Server.MapPath("~/PLAYLIST");
            if (sPath.Right(1) != @"\")
            {
                sPath += @"\";
            }
            sPath += this.CurrentUser;
            sPath += @"\";
            if (!System.IO.Directory.Exists(sPath))
            {
                System.IO.Directory.CreateDirectory(sPath);
            }
            //-----------------------Apaga todos os arquivos da pasta antes da geracao , para nao acumular arquivos.
            var list = System.IO.Directory.GetFiles(sPath, "*.*");
            try
            {
                foreach (var item in list)
                {
                    System.IO.File.Delete(item);
                }
            }
            catch (Exception)
            {
            }
            //---------------------------Criar o Arquivo Texto na pasta
            String strFile = sPath + Param.Nome_Arquivo;
            var PlayListFile = File.Create(strFile);
            try
            {
                String sProgramaAnterior = "";
                String sBreakAnterior = "";
                String sTituloAnterior = "";
                String sCodProgramaAnterior = "";
                String sTituloAtual = "";
                Int32 iHora = 0;
                Int32 iMinuto = 0;
                Int32 iSegundo = 0;
                String iDuracao = "";
                //----------------------------------Consulta o Roteiro Tecnico
                DataTable dtbRoteiro = new DataTable();
                SqlCommand cmdRoteiro = cnn.Procedure(cnn.Connection, "Sp_RU_Carrega_Roteiro");
                cmdRoteiro.Parameters.AddWithValue("@Par_Cod_Veiculo", Param.Cod_Veiculo);
                cmdRoteiro.Parameters.AddWithValue("@Par_Data_Exibicao", Param.Data_Programacao.ConvertToDatetime().ToString("yyyy-MM-dd"));
                SqlDataAdapter adpRoteiro = new SqlDataAdapter(cmdRoteiro);
                adpRoteiro.Fill(dtbRoteiro);
                using (StreamWriter writetext = new StreamWriter(PlayListFile))
                {
                    //----------------------------------Preenche o Arquivo Texto
                    foreach (DataRow drw in dtbRoteiro.Rows)
                    {
                        bolValido = true;
                        //---------------------------------Linha titulo do intervalo nao é valido
                        if (drw["Indica_Titulo_Intervalo"].ToString() == "1")
                        {
                            bolValido = false;
                        }
                        //---------------------------------Montagem
                        if (bolValido)
                        {
                            //===============Monta Linha de Titulo do Break (Mudança de Programa/Break)
                            if (drw["Indica_Titulo_Break"].ToString() == "1")
                            {
                                //----
                                if (sBreakAnterior != "")
                                {
                                    if (sBreakAnterior.ConvertToInt32() > -1)
                                    {
                                        strLinha = "SWT     ";        //--8
                                        strLinha += "BLACK-5   ";     //--10
                                        strLinha += "BLACK 5 SEGUNDOS     ";     //--21
                                        strLinha += "00:00:05.00";     //--11
                                        strLinha += "00:00:00.00";     //--11
                                        strLinha += Param.Data_Programacao.ConvertToDatetime().ToString("dd/MM/yyyy");      //--10
                                        writetext.WriteLine(strLinha);
                                        //----
                                        strLinha = "PAUSA             ";      //--18
                                        strLinha += "FINAL " + sCodProgramaAnterior + " " + sBreakAnterior.PadLeft(2, '0');     //--13
                                        strLinha += "                                        ";     //--40
                                        writetext.WriteLine(strLinha);
                                    }
                                }
                                //----
                                if (sProgramaAnterior != drw["Cod_Programa"].ToString() + drw["Titulo_Break"].ToString())
                                {
                                    strLinha = "REM               ";      //--18
                                    strLinha += drw["Titulo_Programa"].ToString().Substring(0, 16);     //--16
                                    strLinha += "                                     ";     //--37
                                    writetext.WriteLine(strLinha);
                                    //----
                                    strLinha = "REM               ";      //--18
                                    strLinha += "Horario : " + drw["Cod_Faixa"].ToString();     //--16
                                    strLinha += "                                      ";     //--38
                                    writetext.WriteLine(strLinha);
                                }
                                //----
                                if (drw["Breaks"].ToString().ConvertToInt32() > -1)     //--Programas que tem Composicao de Break
                                {
                                    strLinha = "REM               ";      //--18
                                    strLinha += "INICIO  " + drw["Cod_Programa"].ToString().Substring(0, 4) + " " + drw["Breaks"].ToString().PadLeft(2, '0');     //--15
                                    strLinha += "                                      ";     //--38
                                    writetext.WriteLine(strLinha);
                                }
                                //----
                                sTituloAtual = drw["Titulo_Break"].ToString();
                                sProgramaAnterior = drw["Cod_Programa"].ToString() + drw["Titulo_Break"].ToString();
                                sTituloAnterior = drw["Titulo_Break"].ToString();
                                sCodProgramaAnterior = drw["Cod_Programa"].ToString();
                                sBreakAnterior = drw["Breaks"].ToString();
                            }
                            //----
                            if (drw["Breaks"].ToString().ConvertToInt32() > -1)
                            {
                                strLinha = "INT     ";      //--8
                                if (drw["Numero_Fita"].ToString() == "")
                                {
                                    strLinha += "          ";   //--10
                                }
                                else
                                {
                                    strLinha += drw["Numero_Fita"].ToString();     //--10
                                }
                                if (drw["Titulo_Comercial"].ToString() == "")
                                {
                                    strLinha += "                ";     //--16
                                }
                                else
                                {
                                    strLinha += drw["Titulo_Comercial"].ToString().Substring(0, 16);     //--16
                                }
                                strLinha += "     ";      //--5
                                iHora = drw["Duracao"].ToString().ConvertToInt32() / 3600;
                                iMinuto = (drw["Duracao"].ToString().ConvertToInt32() % 3600) / 60;      //-- operador % pega o resto da divisão
                                iSegundo = (drw["Duracao"].ToString().ConvertToInt32() % 3600) % 60;     //-- operador % pega o resto da divisão
                                iDuracao = iHora.ToString().PadLeft(2, '0') + ":" + iMinuto.ToString().PadLeft(2, '0') + ":" + iSegundo.ToString().PadLeft(2, '0');
                                strLinha += iDuracao + ".00";     //--14
                                strLinha += "00:00:00.00";     //--11  
                                strLinha += Param.Data_Programacao.ConvertToDatetime().ToString("dd/MM/yyyy");      //--10
                                writetext.WriteLine(strLinha);
                            }
                        }   //--fecha bolvalido
                    }       //--fecha loop
                    //--Linhas após o fim do loop
                    if (sBreakAnterior != "")
                    {
                        strLinha = "SWT     ";      //--8
                        strLinha += "BLACK-5   ";     //--10
                        strLinha += "BLACK 5 SEGUNDOS     ";     //--16
                        strLinha += "00:00:00.00";     //--11
                        strLinha += "00:00:00.00";     //--11
                        strLinha += Param.Data_Programacao.ConvertToDatetime().ToString("dd/MM/yyyy");      //--10
                        writetext.WriteLine(strLinha);
                        //----
                        strLinha = "PAUSA             ";      //--18
                        strLinha += "FINAL " + sCodProgramaAnterior + " " + sBreakAnterior.PadLeft(2, '0');     //--13
                        strLinha += "                                        ";     //--40
                        writetext.WriteLine(strLinha);
                    }
                    bolGerouArquivo = true;
                }   //--fecha StreamWriter
            }   //--fecha file create
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (bolGerouArquivo)
                {
                    Retorno = "PLAYLIST/" + this.CurrentUser.ToUpper() + "/" + Param.Nome_Arquivo.ToUpper();
                }
                PlayListFile.Close();
            }
            return Retorno;
        }




        //--------------------------------------------------------------------------------------------------------------------------------------------------
        //------------------------------------------------------------- GERAR ARQUIVO 4S -------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------------------------------------
        public String Gerar4S(EnvioPlayListModel Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            String Retorno = "";
            Boolean bolValido = false;
            Boolean bolGerouArquivo = false;
            String strLinha = "";
            //---------------------------Cria uma pasta no servidor para gerar o arquivo
            String sPath = HttpContext.Current.Server.MapPath("~/PLAYLIST");
            if (sPath.Right(1) != @"\")
            {
                sPath += @"\";
            }
            sPath += this.CurrentUser;
            sPath += @"\";
            if (!System.IO.Directory.Exists(sPath))
            {
                System.IO.Directory.CreateDirectory(sPath);
            }
            //-----------------------Apaga todos os arquivos da pasta antes da geracao , para nao acumular arquivos.
            var list = System.IO.Directory.GetFiles(sPath, "*.*");
            try
            {
                foreach (var item in list)
                {
                    System.IO.File.Delete(item);
                }
            }
            catch (Exception)
            {
            }
            //---------------------------Criar o Arquivo Texto na pasta
            String strFile = sPath + Param.Nome_Arquivo;
            var PlayListFile = File.Create(strFile);
            try
            {
                String aTipoPrograma0 = "SAT";
                String aTipoPrograma1 = "VT1";
                String aTipoPrograma2 = "PRD";
                Boolean bPrimeiro = true;
                Boolean bPrimeiraVez = true;
                String sProgramaAnterior = "";
                String sTituloAnterior = "";
                Int32 nTipoBreak = 0;
                Int32 nTipoAnterior = 0;
                Int32 nContComercial = 0;
                Int32 nTipoPrograma = 0;    //-- Tipo do Programa 0=Rede 1=Local 2=Local ao Vivo
                Int32 nUltimoBreak = 0;
                String sTitulo = "";
                String sBreak = "";
                Int32 iHora = 0;
                Int32 iMinuto = 0;
                Int32 iSegundo = 0;
                String sDuracao = "";


                //----------------------------------Consulta o Roteiro Tecnico
                DataTable dtbRoteiro = new DataTable();
                SqlCommand cmdRoteiro = cnn.Procedure(cnn.Connection, "Sp_RU_Carrega_Roteiro");
                cmdRoteiro.Parameters.AddWithValue("@Par_Cod_Veiculo", Param.Cod_Veiculo);
                cmdRoteiro.Parameters.AddWithValue("@Par_Data_Exibicao", Param.Data_Programacao.ConvertToDatetime().ToString("yyyy-MM-dd"));
                SqlDataAdapter adpRoteiro = new SqlDataAdapter(cmdRoteiro);
                adpRoteiro.Fill(dtbRoteiro);
                using (StreamWriter writetext = new StreamWriter(PlayListFile))
                {
                    //----------------------------------Preenche o Arquivo Texto
                    foreach (DataRow drw in dtbRoteiro.Rows)
                    {
                        bolValido = true;
                        //----
                        if (bPrimeiraVez)   //--Se for a primeira vez que entra no loop de registros
                        {
                            bPrimeiraVez = false;
                            sProgramaAnterior = drw["Cod_Programa"].ToString().Substring(0, 4);
                            if (drw["Titulo_Break"].ToString() != "")
                            {
                                sTituloAnterior = drw["Titulo_Break"].ToString();
                            }
                            else
                            {
                                sTituloAnterior = drw["Cod_Programa"].ToString();
                            }
                        }
                        //===============Se For Titulo do Break
                        if (drw["Indica_Titulo_Break"].ToString().ConvertToInt32() == 1)
                        {
                            nTipoBreak = -1;
                        }
                        //===============Despreza Linhas Titulos de Intervalo
                        if (drw["Indica_Titulo_Intervalo"].ToString().ConvertToInt32() == 1)
                        {
                            nTipoBreak = drw["Tipo_Break"].ToString().ConvertToInt32();
                            bolValido = false;
                        }
                        //===============Não envia comerciais que usuario não pode ordenar
                        if (drw["Indica_Ordenacao"].ToString().ConvertToInt32() == 0 && drw["Indica_Titulo_Break"].ToString().ConvertToInt32() == 0)     //-- && = and
                        {
                            bolValido = false;
                        }
                        //------------------------------------  Montagem  -----------------------------------------------
                        if (bolValido)  //--1
                        {
                            if (sProgramaAnterior.ToString() != drw["Cod_Programa"].ToString().Substring(0, 4))
                            {
                                if (!bPrimeiro)
                                {
                                    //----
                                    if (nTipoAnterior == 0 && nContComercial > 0)       //-- Quando termina um break da rede, Linha de Fade
                                    {
                                        strLinha = "F         ";     //--10
                                        strLinha += "FADE                               ";   //--35
                                        strLinha += "00:01:00";     //--8
                                        strLinha += "VDR           ";      //--14
                                        strLinha += "1";    //--1
                                        writetext.WriteLine(strLinha);
                                    }
                                    //----
                                    if (nTipoAnterior != 1)
                                    {
                                        strLinha = "PGM       ";     //--10
                                        strLinha += "===   P R O G R A M   ===          ";   //--35
                                        strLinha += "00:00:01";     //--8
                                        if (nTipoPrograma == 0)
                                        {
                                            strLinha += aTipoPrograma0;      //--
                                        }
                                        else if (nTipoPrograma == 1)
                                        {
                                            strLinha += aTipoPrograma1;      //--
                                        }
                                        else if (nTipoPrograma == 2)
                                        {
                                            strLinha += aTipoPrograma2;      //--
                                        }
                                        strLinha += "           1";    //--12
                                        writetext.WriteLine(strLinha);
                                    }
                                    //----
                                    if (nTipoAnterior != 1)
                                    {
                                        strLinha = "PAUSE     ";    //--10
                                    }
                                    else
                                    {
                                        strLinha = "PGM       ";       //--10
                                    }
                                    nUltimoBreak += 1;
                                    strLinha += (sTituloAnterior.Substring(0, 20).Trim() + " - " + nUltimoBreak.ToString() + " BL").PadRight(27, ' ');  //--27
                                    strLinha += "        00:00:00"; //--16
                                    if (nTipoAnterior == 1)
                                    {
                                        if (nTipoPrograma == 0)
                                        {
                                            strLinha += aTipoPrograma0;      //--
                                        }
                                        else if (nTipoPrograma == 1)
                                        {
                                            strLinha += aTipoPrograma1;      //--
                                        }
                                        else if (nTipoPrograma == 2)
                                        {
                                            strLinha += aTipoPrograma2;      //--
                                        }
                                    }
                                    else
                                    {
                                        strLinha += "   ";
                                    }
                                    strLinha += "           1"; //--12
                                    writetext.WriteLine(strLinha);
                                    //----
                                    sProgramaAnterior = drw["Cod_Programa"].ToString().Substring(0, 4);
                                    if (drw["Titulo_Break"].ToString() != "")
                                    {
                                        sTituloAnterior = drw["Titulo_Break"].ToString();
                                    }
                                    else
                                    {
                                        sTituloAnterior = drw["Cod_Programa"].ToString();
                                    }
                                    //----
                                    nTipoPrograma = 0;
                                    DataTable dtbTipoProg1 = new DataTable();
                                    String strSql1 = "Select Indica_Local From Programa Where Cod_Programa = '" + drw["Cod_Programa"].ToString() + "'";
                                    SqlCommand cmdTipoProg1 = cnn.Text(cnn.Connection, strSql1);
                                    SqlDataAdapter AdpTipoProg1 = new SqlDataAdapter(cmdTipoProg1);
                                    AdpTipoProg1.Fill(dtbTipoProg1);
                                    if (dtbTipoProg1.Rows.Count > 0)
                                    {
                                        nTipoPrograma = dtbTipoProg1.Rows[0]["Indica_Local"].ToString().ConvertToInt32();
                                    }
                                    if (nTipoPrograma.ToString().ConvertToInt32() == 1)
                                    {
                                        DataTable dtbTipoProg2 = new DataTable();
                                        String strSql2 = "Select Indica_AoVivo From Programa_Veiculo Where Cod_Programa = '" + drw["Cod_Programa"].ToString() + "' And Cod_Veiculo = '" + Param.Cod_Veiculo + "'";
                                        SqlCommand cmdTipoProg2 = cnn.Text(cnn.Connection, strSql2);
                                        SqlDataAdapter AdpTipoProg2 = new SqlDataAdapter(cmdTipoProg2);
                                        AdpTipoProg2.Fill(dtbTipoProg2);
                                        if (dtbTipoProg2.Rows.Count > 0)
                                        {
                                            if (dtbTipoProg2.Rows[0]["Indica_AoVivo"].ToString().ConvertToInt32() == 1)
                                            {
                                                nTipoPrograma = 2;
                                            }
                                        }

                                    }




                                }   //--fecha bPrimeiro
                            }   //--fecha sProgramaAnterior

                            //--===============Linha do Inter Programa
                            if (drw["Indica_Titulo_Break"].ToString().ConvertToInt32() == 1 && drw["Breaks"].ToString().ConvertToInt32() == 0)     //-- && = And
                            {
                                if (!bPrimeiro)
                                {
                                    strLinha = "INTERPGM  ";    //--10
                                    strLinha += "INTER PROGRAMA                     ";   //--35
                                    strLinha += "00:00:00";     //--8
                                    if (nTipoPrograma == 0)
                                    {
                                        strLinha += aTipoPrograma0;      //--
                                    }
                                    else if (nTipoPrograma == 1)
                                    {
                                        strLinha += aTipoPrograma1;      //--
                                    }
                                    else if (nTipoPrograma == 2)
                                    {
                                        strLinha += aTipoPrograma2;      //--
                                    }
                                    strLinha += "           1";    //--12
                                    writetext.WriteLine(strLinha);
                                }
                                nContComercial = 0;
                                bolValido = false;
                            }

                            //----
                            if (bolValido)  //--2
                            {
                                //===============Linha Titulo do Programa / Break
                                if (drw["Indica_Titulo_Break"].ToString().ConvertToInt32() == 1 && drw["Breaks"].ToString().ConvertToInt32() != 0)
                                {
                                    //----
                                    if (nTipoAnterior == 0 && !bPrimeiro && nContComercial > 0)     //-- Quando termina um break da rede, Linha de Fade
                                    {
                                        strLinha = "F         ";
                                        strLinha += "FADE                               ";
                                        strLinha += "00:01:00";
                                        strLinha += "VDR           ";
                                        strLinha += "1";
                                        writetext.WriteLine(strLinha);
                                    }
                                    //----
                                    if (nTipoPrograma != 1)
                                    {
                                        strLinha = "PGM       ";
                                        strLinha += "===   P R O G R A M   ===          ";
                                        strLinha += "00:00:01";
                                        if (nTipoPrograma == 0)
                                        {
                                            strLinha += aTipoPrograma0;      //--
                                        }
                                        else if (nTipoPrograma == 1)
                                        {
                                            strLinha += aTipoPrograma1;      //--
                                        }
                                        else if (nTipoPrograma == 2)
                                        {
                                            strLinha += aTipoPrograma2;      //--
                                        }
                                        strLinha += "           1";    //--12
                                        writetext.WriteLine(strLinha);
                                    }
                                    //----
                                    //--====================Linha titulo do Bloco
                                    sTitulo = drw["Titulo_Break"].ToString().Substring(0, 20);
                                    if (drw["Sequencia_Break"].ToString() == "")
                                    {
                                        sBreak = drw["Breaks"].ToString();
                                    }
                                    else
                                    {
                                        sBreak = drw["Sequencia_Break"].ToString();
                                    }
                                    //----
                                    if (nTipoPrograma != 1)
                                    {
                                        strLinha = "PAUSE     ";
                                    }
                                    else
                                    {
                                        strLinha = "PGM       ";
                                    }
                                    strLinha += (sTitulo.Substring(0, 20).Trim() + " - " + sBreak + " BL").PadRight(27, ' ');   //--27
                                    strLinha += "        00:00:00"; //--16
                                    if (nTipoPrograma == 1)
                                    {
                                        strLinha += aTipoPrograma1;      //--
                                    }
                                    else
                                    {
                                        strLinha += "   ";
                                    }
                                    strLinha += "           1"; //--12
                                    writetext.WriteLine(strLinha);
                                    //----
                                    nTipoAnterior = nTipoPrograma;
                                    nUltimoBreak = sBreak.ToString().ConvertToInt32();
                                    nContComercial = 0;
                                }
                                //----
                                else    //--====2. Linha do Comercial
                                {
                                    strLinha = drw["Numero_Fita"].ToString().Substring(4, 4);   //--4
                                    strLinha += "      ";   //--6
                                    strLinha += drw["Titulo_Comercial"].ToString().Substring(0, 30);    //--30
                                    strLinha += "     ";    //--5
                                    iHora = drw["Duracao"].ToString().ConvertToInt32() / 3600;
                                    iMinuto = (drw["Duracao"].ToString().ConvertToInt32() % 3600) / 60;      //-- operador % pega o resto da divisão
                                    iSegundo = (drw["Duracao"].ToString().ConvertToInt32() % 3600) % 60;     //-- operador % pega o resto da divisão
                                    sDuracao = iHora.ToString().PadLeft(2, '0') + ":" + iMinuto.ToString().PadLeft(2, '0') + ":" + iSegundo.ToString().PadLeft(2, '0');
                                    strLinha += sDuracao;     //--8
                                    strLinha += "VDR";
                                    strLinha += "           ";  //--11
                                    strLinha += "0";
                                    writetext.WriteLine(strLinha);
                                    //----
                                    bPrimeiro = false;
                                    nContComercial = nContComercial + 1;
                                }


                            }   //--fecha bolValido 2

                        }   //--fecha bolValido 1

                    }   //--fecha loop

                    //--Linhas após o fim do loop

                    //================Grava o Ultimo Bloco do Ultimo programa
                    if (!bPrimeiro)
                    {
                        if (nTipoAnterior == 0 && nContComercial > 0)   //-- Quando termina um break da rede, Linha de Fade
                        {
                            strLinha = "F         ";
                            strLinha += "FADE                               ";
                            strLinha += "00:01:00";
                            strLinha += "VDR            ";
                            strLinha += "1";
                            writetext.WriteLine(strLinha);
                        }

                        //----
                        if (nTipoAnterior != 1)
                        {
                            strLinha = "PGM       ";
                            strLinha += "===   P R O G R A M   ===          ";
                            strLinha += "00:00:01";
                            if (nTipoPrograma == 0)
                            {
                                strLinha += aTipoPrograma0;      //--
                            }
                            else if (nTipoPrograma == 1)
                            {
                                strLinha += aTipoPrograma1;      //--
                            }
                            else if (nTipoPrograma == 2)
                            {
                                strLinha += aTipoPrograma2;      //--
                            }
                            strLinha += "           1";    //--12
                            writetext.WriteLine(strLinha);
                        }

                        //----
                        if (nTipoAnterior != 1)
                        {
                            strLinha = "PAUSE     ";
                        }
                        else
                        {
                            strLinha = "PGM       ";
                        }
                        nUltimoBreak += 1;
                        strLinha += (sTituloAnterior.Substring(0, 20).Trim() + " - " + nUltimoBreak.ToString() + " BL").PadRight(27, ' ');  //--27
                        strLinha += "        00:00:00"; //--16
                        strLinha += "              1"; //--15
                        writetext.WriteLine(strLinha);
                    }




                    bolGerouArquivo = true;
                }   //--fecha StreamWriter
            }   //--fecha file create
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (bolGerouArquivo)
                {
                    Retorno = "PLAYLIST/" + this.CurrentUser.ToUpper() + "/" + Param.Nome_Arquivo.ToUpper();
                }
                PlayListFile.Close();
            }
            return Retorno;
        }





        //--------------------------------------------------------------------------------------------------------------------------------------------------
        //------------------------------------------------------------- GERAR ARQUIVO VICTOR -------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------------------------------------
        public String GerarVICTOR(EnvioPlayListModel Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            //------------
            String Retorno = "";
            Boolean bolValido = false;
            Boolean bolGerouArquivo = false;
            String strLinha = "";
            //---------------------------Cria uma pasta no servidor para gerar o arquivo
            String sPath = HttpContext.Current.Server.MapPath("~/PLAYLIST");
            if (sPath.Right(1) != @"\")
            {
                sPath += @"\";
            }
            sPath += this.CurrentUser;
            sPath += @"\";
            if (!System.IO.Directory.Exists(sPath))
            {
                System.IO.Directory.CreateDirectory(sPath);
            }
            //-----------------------Apaga todos os arquivos da pasta antes da geracao , para nao acumular arquivos.
            var list = System.IO.Directory.GetFiles(sPath, "*.*");
            try
            {
                foreach (var item in list)
                {
                    System.IO.File.Delete(item);
                }
            }
            catch (Exception)
            {
            }
            //---------------------------Criar o Arquivo Texto na pasta
            String strFile = sPath + Param.Nome_Arquivo;
            var PlayListFile = File.Create(strFile);
            try
            {

                Int32 iHora = 0;
                Int32 iMinuto = 0;
                Int32 iSegundo = 0;
                String sDuracao = "";
                String sAcento = "";
                String sSubstituto = "";
                String sCaracter = "";


                //---------------------------Lê Parâmetro de Configuração de Fitas
                Int32 nPosicaoFita = 1;//  default
                Int32 nTamanhoFita = 6;  //  default

                DataTable dtbParam = new DataTable();
                String strSql = "Select Cod_Chave From Parametro_Valor Where Cod_Parametro = 76 and Cod_Veiculo = '" + Param.Cod_Veiculo + "'";
                SqlCommand cmdParam = cnn.Text(cnn.Connection, strSql);
                SqlDataAdapter AdpParam = new SqlDataAdapter(cmdParam);
                AdpParam.Fill(dtbParam);
                if (dtbParam.Rows.Count > 0)
                {
                    nPosicaoFita = dtbParam.Rows[0]["Cod_Chave"].ToString().Left(2).ConvertToInt32();
                    nTamanhoFita = dtbParam.Rows[0]["Cod_Chave"].ToString().TrimEnd().Right(2).ConvertToInt32();
                }


                //----------------------------------Consulta o Roteiro Tecnico
                DataTable dtbRoteiro = new DataTable();
                SqlCommand cmdRoteiro = cnn.Procedure(cnn.Connection, "Sp_RU_Carrega_Roteiro");
                cmdRoteiro.Parameters.AddWithValue("@Par_Cod_Veiculo", Param.Cod_Veiculo);
                cmdRoteiro.Parameters.AddWithValue("@Par_Data_Exibicao", Param.Data_Programacao.ConvertToDatetime().ToString("yyyy-MM-dd"));
                SqlDataAdapter adpRoteiro = new SqlDataAdapter(cmdRoteiro);
                adpRoteiro.Fill(dtbRoteiro);
                using (StreamWriter writetext = new StreamWriter(PlayListFile))
                {
                    //----------------------------------Preenche o Arquivo Texto
                    foreach (DataRow drw in dtbRoteiro.Rows)
                    {
                        bolValido = true;

                        //-----------------Linha titulo do intervalo nao é valido
                        if (drw["Indica_Titulo_Intervalo"].ToString() == "1")
                        {
                            bolValido = false;
                        }

                        //-----------------Não envia comerciais que usuario não pode ordenar
                        if (drw["Indica_Ordenacao"].ToString().ConvertToInt32() == 0 && drw["Indica_Titulo_Break"].ToString().ConvertToInt32() == 0)     //-- && = and
                        {
                            bolValido = false;
                        }
                        if (drw["Tipo_Break"].ToString().ConvertToInt32() == 1)
                        {
                            bolValido = false;
                        }

                        //------------------------------------  Montagem  -----------------------------------------------
                        if (bolValido)
                        {
                            //------------------- Linha Titulo do Programa / Break
                            if (drw["Indica_Titulo_Break"].ToString().ConvertToInt32() == 1)
                            {
                                strLinha = "BREAK     LIVE 05 00 00 Bl" + drw["Breaks"].ToString().PadLeft(2, '0') + " - " + drw["Titulo_Programa"];


                                sAcento = "ÀÁÂÃÄÅÇÈÉÊËÌÍÎÏÑÒÓÔÕÖÙÚÛÜÝàáâãäåçèéêëìíîïñòóôõöùúûü&ªº";
                                sSubstituto = "AAAAAACEEEEIIIINOOOOOUUUUYaaaaaaceeeeiiiinooooouuuuEao";
                                for (int iPosicaoLinha = 0; iPosicaoLinha < (strLinha.Length - 1); iPosicaoLinha += 1)
                                {
                                    //Pega um caracter do strLinha
                                    sCaracter = strLinha.Substring(iPosicaoLinha, 1);

                                    //Verifica se o caracter é igual à algum do sAcento
                                    for (int iPosicaoAcento = 0; iPosicaoAcento < (54 - 1); iPosicaoAcento += 1)
                                    {
                                        if (sCaracter == sAcento.Substring(iPosicaoAcento, 1))
                                        {
                                            //Troca o novo caracter no strLinha
                                            if (iPosicaoLinha == 0)
                                            {
                                                strLinha = sSubstituto.Substring(iPosicaoAcento, 1) + strLinha.Substring(iPosicaoLinha + 1, ((strLinha.Length - 1) - iPosicaoLinha));
                                            }
                                            else if (iPosicaoLinha == (strLinha.Length - 1))
                                            {
                                                strLinha = strLinha.Substring(0, iPosicaoLinha) + sSubstituto.Substring(iPosicaoAcento, 1);
                                            }
                                            else
                                            {
                                                strLinha = strLinha.Substring(0, iPosicaoLinha) + sSubstituto.Substring(iPosicaoAcento, 1) + strLinha.Substring(iPosicaoLinha + 1, ((strLinha.Length - 1) - iPosicaoLinha));
                                            }

                                            //sai do loop
                                            break;
                                        }
                                    }
                                }

                                writetext.WriteLine(strLinha);

                            }
                            //------------------- Linha do Comercial
                            else
                            {
                                if (drw["Numero_Fita"].ToString() == "")
                                {
                                    strLinha = "      ";
                                }
                                else
                                {
                                    //strLinha = drw["Numero_Fita"].ToString().Substring(nPosicaoFita - 1, nTamanhoFita);
                                    strLinha = drw["Numero_Fita"].ToString().Substring((nPosicaoFita + 2) - 1, nTamanhoFita);
                                }
                                strLinha += "    ";
                                strLinha += drw["Titulo_Comercial"].ToString().Substring(0, 35);
                                iHora = drw["Duracao"].ToString().ConvertToInt32() / 3600;
                                iMinuto = (drw["Duracao"].ToString().ConvertToInt32() % 3600) / 60;      //-- operador % pega o resto da divisão
                                iSegundo = (drw["Duracao"].ToString().ConvertToInt32() % 3600) % 60;     //-- operador % pega o resto da divisão
                                sDuracao = iHora.ToString().PadLeft(2, '0') + ":" + iMinuto.ToString().PadLeft(2, '0') + ":" + iSegundo.ToString().PadLeft(2, '0');
                                strLinha += sDuracao;     //--8
                                if (drw["Breaks"].ToString().ConvertToInt32() == 0)
                                {
                                    strLinha += "INTE";
                                }
                                else
                                {
                                    strLinha += "BREA";
                                }
                                strLinha += "          0";


                                sAcento = "ÀÁÂÃÄÅÇÈÉÊËÌÍÎÏÑÒÓÔÕÖÙÚÛÜÝàáâãäåçèéêëìíîïñòóôõöùúûü&ªº";
                                sSubstituto = "AAAAAACEEEEIIIINOOOOOUUUUYaaaaaaceeeeiiiinooooouuuuEao";
                                for (int iPosicaoLinha = 0; iPosicaoLinha < (strLinha.Length - 1); iPosicaoLinha += 1)
                                {
                                    //Pega um caracter do strLinha
                                    sCaracter = strLinha.Substring(iPosicaoLinha, 1);

                                    //Verifica se o caracter é igual à algum do sAcento
                                    for (int iPosicaoAcento = 0; iPosicaoAcento < (54 - 1); iPosicaoAcento += 1)
                                    {
                                        if (sCaracter == sAcento.Substring(iPosicaoAcento, 1))
                                        {
                                            //Troca o novo caracter no strLinha
                                            if (iPosicaoLinha == 0)
                                            {
                                                strLinha = sSubstituto.Substring(iPosicaoAcento, 1) + strLinha.Substring(iPosicaoLinha + 1, ((strLinha.Length - 1) - iPosicaoLinha));
                                            }
                                            else if (iPosicaoLinha == (strLinha.Length - 1))
                                            {
                                                strLinha = strLinha.Substring(0, iPosicaoLinha) + sSubstituto.Substring(iPosicaoAcento, 1);
                                            }
                                            else
                                            {
                                                strLinha = strLinha.Substring(0, iPosicaoLinha) + sSubstituto.Substring(iPosicaoAcento, 1) + strLinha.Substring(iPosicaoLinha + 1, ((strLinha.Length - 1) - iPosicaoLinha));
                                            }

                                            //sai do loop
                                            break;
                                        }
                                    }
                                }


                                writetext.WriteLine(strLinha);

                            }


                        }   //--fecha bolValido

                    }   //--fecha loop

                    bolGerouArquivo = true;
                }   //--fecha StreamWriter
            }   //--fecha file create
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (bolGerouArquivo)
                {
                    Retorno = "PLAYLIST/" + this.CurrentUser.ToUpper() + "/" + Param.Nome_Arquivo.ToUpper();
                }
                PlayListFile.Close();
            }
            return Retorno;
        }
        //--------------------------------- FIM ----------------------------------------------------




        //--------------------------------------------------------------------------------------------------------------------------------------------------
        //------------------------------------------------------------- GERAR ARQUIVO VSN ------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------------------------------------
        public String GerarVSN(EnvioPlayListModel Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            //------------
            String Retorno = "";
            Boolean bolValido = false;
            Boolean bolGerouArquivo = false;
            String strLinha = "";
            //---------------------------Cria uma pasta no servidor para gerar o arquivo
            String sPath = HttpContext.Current.Server.MapPath("~/PLAYLIST");
            if (sPath.Right(1) != @"\")
            {
                sPath += @"\";
            }
            sPath += this.CurrentUser;
            sPath += @"\";
            if (!System.IO.Directory.Exists(sPath))
            {
                System.IO.Directory.CreateDirectory(sPath);
            }
            //-----------------------Apaga todos os arquivos da pasta antes da geracao , para nao acumular arquivos.
            var list = System.IO.Directory.GetFiles(sPath, "*.*");
            try
            {
                foreach (var item in list)
                {
                    System.IO.File.Delete(item);
                }
            }
            catch (Exception)
            {
            }
            //---------------------------Criar o Arquivo Texto na pasta
            String strFile = sPath + Param.Nome_Arquivo;
            var PlayListFile = File.Create(strFile);
            try
            {

                Int32 iHora = 0;
                Int32 iMinuto = 0;
                Int32 iSegundo = 0;
                String sDuracao = "";
                String sAcento = "";
                String sSubstituto = "";
                String sCaracter = "";



                //---------------------------Lê Parâmetro de Configuração de Fitas
                Int32 nPosicaoFita = 1;//  default
                Int32 nTamanhoFita = 6;  //  default

                DataTable dtbParam = new DataTable();
                String strSql = "Select Cod_Chave From Parametro_Valor Where Cod_Parametro = 76 and Cod_Veiculo = '" + Param.Cod_Veiculo + "'";
                SqlCommand cmdParam = cnn.Text(cnn.Connection, strSql);
                SqlDataAdapter AdpParam = new SqlDataAdapter(cmdParam);
                AdpParam.Fill(dtbParam);
                if (dtbParam.Rows.Count > 0)
                {
                    nPosicaoFita = dtbParam.Rows[0]["Cod_Chave"].ToString().Left(2).ConvertToInt32();
                    nTamanhoFita = dtbParam.Rows[0]["Cod_Chave"].ToString().TrimEnd().Right(2).ConvertToInt32();
                }


                //----------------------------------Consulta o Roteiro Tecnico
                DataTable dtbRoteiro = new DataTable();
                SqlCommand cmdRoteiro = cnn.Procedure(cnn.Connection, "Sp_RU_Carrega_Roteiro");
                cmdRoteiro.Parameters.AddWithValue("@Par_Cod_Veiculo", Param.Cod_Veiculo);
                cmdRoteiro.Parameters.AddWithValue("@Par_Data_Exibicao", Param.Data_Programacao.ConvertToDatetime().ToString("yyyy-MM-dd"));
                SqlDataAdapter adpRoteiro = new SqlDataAdapter(cmdRoteiro);
                adpRoteiro.Fill(dtbRoteiro);
                using (StreamWriter writetext = new StreamWriter(PlayListFile))
                {
                    //----------------------------------Preenche o Arquivo Texto
                    foreach (DataRow drw in dtbRoteiro.Rows)
                    {
                        bolValido = true;

                        //-----------------Linha titulo do intervalo nao é valido
                        if (drw["Indica_Titulo_Intervalo"].ToString() == "1")
                        {
                            bolValido = false;
                        }

                        //-----------------Não envia comerciais que usuario não pode ordenar
                        if (drw["Indica_Ordenacao"].ToString().ConvertToInt32() == 0 && drw["Indica_Titulo_Break"].ToString().ConvertToInt32() == 0)     //-- && = and
                        {
                            bolValido = false;
                        }
                        if (drw["Tipo_Break"].ToString().ConvertToInt32() == 1)
                        {
                            bolValido = false;
                        }

                        //------------------------------------  Montagem  -----------------------------------------------
                        if (bolValido)
                        {
                            //----Linha Titulo do Programa / Break
                            if (drw["Indica_Titulo_Break"].ToString().ConvertToInt32() == 1)
                            {
                                strLinha = "BK| |.|00:00:00|BREAK " + drw["Breaks"].ToString().PadLeft(2, '0') + " " + drw["Cod_Programa"].ToString();
                                strLinha += "----------------------|00:00:20";


                                sAcento = "ÀÁÂÃÄÅÇÈÉÊËÌÍÎÏÑÒÓÔÕÖÙÚÛÜÝàáâãäåçèéêëìíîïñòóôõöùúûü&ªº";
                                sSubstituto = "AAAAAACEEEEIIIINOOOOOUUUUYaaaaaaceeeeiiiinooooouuuuEao";
                                for (int iPosicaoLinha = 0; iPosicaoLinha < (strLinha.Length - 1); iPosicaoLinha += 1)
                                {
                                    //Pega um caracter do strLinha
                                    sCaracter = strLinha.Substring(iPosicaoLinha, 1);

                                    //Verifica se o caracter é igual à algum do sAcento
                                    for (int iPosicaoAcento = 0; iPosicaoAcento < (54 - 1); iPosicaoAcento += 1)
                                    {
                                        if (sCaracter == sAcento.Substring(iPosicaoAcento, 1))
                                        {
                                            //Troca o novo caracter no strLinha
                                            if (iPosicaoLinha == 0)
                                            {
                                                strLinha = sSubstituto.Substring(iPosicaoAcento, 1) + strLinha.Substring(iPosicaoLinha + 1, ((strLinha.Length - 1) - iPosicaoLinha));
                                            }
                                            else if (iPosicaoLinha == (strLinha.Length - 1))
                                            {
                                                strLinha = strLinha.Substring(0, iPosicaoLinha) + sSubstituto.Substring(iPosicaoAcento, 1);
                                            }
                                            else
                                            {
                                                strLinha = strLinha.Substring(0, iPosicaoLinha) + sSubstituto.Substring(iPosicaoAcento, 1) + strLinha.Substring(iPosicaoLinha + 1, ((strLinha.Length - 1) - iPosicaoLinha));
                                            }

                                            //sai do loop
                                            break;
                                        }
                                    }
                                }

                                writetext.WriteLine(strLinha);
                            }
                            //----Linha do Comercial
                            else
                            {
                                strLinha = "EV| |.|00:00:20||";
                                iSegundo = drw["Duracao"].ToString().ConvertToInt32() % 60;     //-- operador % pega o resto da divisão
                                iMinuto = (drw["Duracao"].ToString().ConvertToInt32() / 60) % 60;      //-- operador % pega o resto da divisão
                                iHora = drw["Duracao"].ToString().ConvertToInt32() / 3600;
                                sDuracao = iHora.ToString().PadLeft(2, '0') + ":" + iMinuto.ToString().PadLeft(2, '0') + ":" + iSegundo.ToString().PadLeft(2, '0');
                                strLinha += sDuracao;     //--8
                                strLinha += "|VSR1|.|.|.| | |";
                                if (drw["Numero_Fita"].ToString() == "")
                                {
                                    strLinha += "      ";
                                }
                                else
                                {
                                    strLinha += drw["Numero_Fita"].ToString().Substring((nPosicaoFita + 2) - 1, nTamanhoFita);
                                }
                                strLinha += "|||||";
                                strLinha += drw["Titulo_Comercial"].ToString();


                                sAcento = "ÀÁÂÃÄÅÇÈÉÊËÌÍÎÏÑÒÓÔÕÖÙÚÛÜÝàáâãäåçèéêëìíîïñòóôõöùúûü&ªº";
                                sSubstituto = "AAAAAACEEEEIIIINOOOOOUUUUYaaaaaaceeeeiiiinooooouuuuEao";
                                for (int iPosicaoLinha = 0; iPosicaoLinha < (strLinha.Length - 1); iPosicaoLinha += 1)
                                {
                                    //Pega um caracter do strLinha
                                    sCaracter = strLinha.Substring(iPosicaoLinha, 1);

                                    //Verifica se o caracter é igual à algum do sAcento
                                    for (int iPosicaoAcento = 0; iPosicaoAcento < (54 - 1); iPosicaoAcento += 1)
                                    {
                                        if (sCaracter == sAcento.Substring(iPosicaoAcento, 1))
                                        {
                                            //Troca o novo caracter no strLinha
                                            if (iPosicaoLinha == 0)
                                            {
                                                strLinha = sSubstituto.Substring(iPosicaoAcento, 1) + strLinha.Substring(iPosicaoLinha + 1, ((strLinha.Length - 1) - iPosicaoLinha));
                                            }
                                            else if (iPosicaoLinha == (strLinha.Length - 1))
                                            {
                                                strLinha = strLinha.Substring(0, iPosicaoLinha) + sSubstituto.Substring(iPosicaoAcento, 1);
                                            }
                                            else
                                            {
                                                strLinha = strLinha.Substring(0, iPosicaoLinha) + sSubstituto.Substring(iPosicaoAcento, 1) + strLinha.Substring(iPosicaoLinha + 1, ((strLinha.Length - 1) - iPosicaoLinha));
                                            }

                                            //sai do loop
                                            break;
                                        }
                                    }
                                }


                                writetext.WriteLine(strLinha);
                            }


                        }   //--fecha bolValido

                    }   //--fecha loop

                    bolGerouArquivo = true;
                }   //--fecha StreamWriter
            }   //--fecha file create
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (bolGerouArquivo)
                {
                    Retorno = "PLAYLIST/" + this.CurrentUser.ToUpper() + "/" + Param.Nome_Arquivo.ToUpper();
                }
                PlayListFile.Close();
            }
            return Retorno;
        }
        //--------------------------------- FIM ----------------------------------------------------






        //--------------------------------------------------------------------------------------------------------------------------------------------------
        //------------------------------------------------------------- GERAR ARQUIVO DAD ------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------------------------------------
        public String GerarDAD(EnvioPlayListModel Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            //----
            String Retorno = "";
            Boolean bolValido = false;
            Boolean bolGerouArquivo = false;
            //---------------------------Cria uma pasta no servidor para gerar o arquivo
            String sPath = HttpContext.Current.Server.MapPath("~/PLAYLIST");
            if (sPath.Right(1) != @"\")
            {
                sPath += @"\";
            }
            sPath += this.CurrentUser;
            sPath += @"\";
            if (!System.IO.Directory.Exists(sPath))
            {
                System.IO.Directory.CreateDirectory(sPath);
            }
            //-----------------------Apaga todos os arquivos da pasta antes da geracao , para nao acumular arquivos.
            var list = System.IO.Directory.GetFiles(sPath, "*.*");
            try
            {
                foreach (var item in list)
                {
                    System.IO.File.Delete(item);
                }
            }
            catch (Exception)
            {
            }
            //---------------------------Criar o Arquivo DBF na pasta
            try
            {
                Boolean bolImportado = false;
                String strProgramaAnterior = "";
                String strBreakAnterior = "";
                String strNumeroFita = "";
                String strKey = "";
                String sDia_Semana_Extenso = "";
                String sMes_Extenso = "";
                //--
                String strProximoArquivo = "";
                String strProximaData = Param.Data_Programacao.ConvertToDatetime().AddDays(1).ToString("ddMM");
                String strDataAtual = Param.Data_Programacao.ConvertToDatetime().ToString("ddMM");
                strProximoArquivo = Param.Nome_Arquivo.Replace(sPath, "");
                strProximoArquivo = strProximoArquivo.Replace(strDataAtual, strProximaData);
                strProximoArquivo = strProximoArquivo.Replace(".DBF", "");
                //---------------------------Lê Parâmetro de Configuração de Fitas
                Int32 nPosicaoFita = 1; //--default
                Int32 nTamanhoFita = 6; //--default
                DataTable dtbParam = new DataTable();
                String strSql = "Select Cod_Chave From Parametro_Valor Where Cod_Parametro = 76 and Cod_Veiculo = '" + Param.Cod_Veiculo + "'";
                SqlCommand cmdParam = cnn.Text(cnn.Connection, strSql);
                SqlDataAdapter AdpParam = new SqlDataAdapter(cmdParam);
                AdpParam.Fill(dtbParam);
                if (dtbParam.Rows.Count > 0)
                {
                    nPosicaoFita = dtbParam.Rows[0]["Cod_Chave"].ToString().Left(2).ConvertToInt32();
                    nTamanhoFita = dtbParam.Rows[0]["Cod_Chave"].ToString().TrimEnd().Right(2).ConvertToInt32();
                }
                //----------------------------------Consulta o Roteiro Tecnico
                DataTable dtbRoteiro = new DataTable();
                SqlCommand cmdRoteiro = cnn.Procedure(cnn.Connection, "Sp_RU_Carrega_Roteiro_V2");
                cmdRoteiro.Parameters.AddWithValue("@Par_Cod_Veiculo", Param.Cod_Veiculo);
                cmdRoteiro.Parameters.AddWithValue("@Par_Data_Exibicao", Param.Data_Programacao.ConvertToDatetime().ToString("yyyy-MM-dd"));
                SqlDataAdapter adpRoteiro = new SqlDataAdapter(cmdRoteiro);
                adpRoteiro.Fill(dtbRoteiro);
                //--
                string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sPath + ";Extended Properties=dBase IV";
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                using (OleDbCommand command = connection.CreateCommand())
                {
                    //----------------------------------Preenche o Arquivo DBF
                    foreach (DataRow drw in dtbRoteiro.Rows)
                    {
                        connection.Open();
                        bolValido = true;
                        //--------------------- Cria Arquivo DBF (Estrutura) -------------------------------
                        command.CommandText =   "CREATE TABLE " + Param.Nome_Arquivo +
                                                " (CUT varchar(5), FUNCTION varchar(1), DELAY varchar(1), PLAYS varchar(2), SEC varchar(1), TER varchar(1), SEGUE varchar(1)," +
                                                " TIME varchar(1), BEGEND varchar(1), CHAIN varchar(100), ROTATE varchar(1), TYPE varchar(1), COMMENT varchar(30), LINEID varchar(1)," +
                                                " STARTTIME varchar(1), ENDTIME varchar(1), FOSTART varchar(1), FOLENGTH varchar(1), FISTART varchar(1), FILENGTH varchar(1)," +
                                                " LIBLOC varchar(1), LIBNAME varchar(1), GUID varchar(1), ORDERID varchar(1))";
                        command.ExecuteNonQuery();
                        //----------------------------- Monta o Cabeçalho ----------------------------------
                        command.CommandText = "INSERT INTO " + Param.Nome_Arquivo +
                                                " (CUT, FUNCTION, PLAYS, SEC, TER, SEGUE, TYPE, COMMENT)" +
                                                " SELECT '0', 'L', '01', '0', '0', '1', 'P', '" + 
                                                Param.Cod_Veiculo.ToString() + "*" + Param.Data_Programacao.ConvertToDatetime().ToString("dd/MM/yyyy") + "'";
                        command.ExecuteNonQuery();
                        //----------------------------- Monta a Identificação da Emissora (9 Linhas) --------------------------
                        //---------- LINHA 1 ----------
                        command.CommandText =   "INSERT INTO " + Param.Nome_Arquivo +
                                                " (TYPE, COMMENT)" +
                                                " SELECT 'C', '******************************'";
                        command.ExecuteNonQuery();
                        //---------- LINHA 2 ----------
                        command.CommandText =   "INSERT INTO " + Param.Nome_Arquivo +
                                                " (TYPE, COMMENT)" +
                                                " SELECT 'C', '*'";
                        command.ExecuteNonQuery();
                        //---------- LINHA 3 ----------
                        command.CommandText =   "INSERT INTO " + Param.Nome_Arquivo +
                                                " (TYPE, COMMENT)" +
                                                " SELECT 'C', '*      " + Param.Nome_Veiculo.Substring(0, 30) + "'";
                        command.ExecuteNonQuery();
                        //---------- LINHA 4 ----------
                        command.CommandText =   "INSERT INTO " + Param.Nome_Arquivo +
                                                " (TYPE, COMMENT)" +
                                                " SELECT 'C', '*'";
                        command.ExecuteNonQuery();
                        //---------- LINHA 5 ----------
                        if (Param.Data_Programacao.ConvertToDatetime().DayOfWeek.ToString().ConvertToInt32() == 1)
                        {
                            sDia_Semana_Extenso = "Domingo";
                        }
                        else if (Param.Data_Programacao.ConvertToDatetime().DayOfWeek.ToString().ConvertToInt32() == 2)
                        {
                            sDia_Semana_Extenso = "Segunda";
                        }
                        else if (Param.Data_Programacao.ConvertToDatetime().DayOfWeek.ToString().ConvertToInt32() == 3)
                        {
                            sDia_Semana_Extenso = "Terça";
                        }
                        else if (Param.Data_Programacao.ConvertToDatetime().DayOfWeek.ToString().ConvertToInt32() == 4)
                        {
                            sDia_Semana_Extenso = "Quarta";
                        }
                        else if (Param.Data_Programacao.ConvertToDatetime().DayOfWeek.ToString().ConvertToInt32() == 5)
                        {
                            sDia_Semana_Extenso = "Quinta";
                        }
                        else if (Param.Data_Programacao.ConvertToDatetime().DayOfWeek.ToString().ConvertToInt32() == 6)
                        {
                            sDia_Semana_Extenso = "Sexta";
                        }
                        else if (Param.Data_Programacao.ConvertToDatetime().DayOfWeek.ToString().ConvertToInt32() == 7)
                        {
                            sDia_Semana_Extenso = "Sábado";
                        }
                        command.CommandText =   "INSERT INTO " + Param.Nome_Arquivo +
                                                " (TYPE, COMMENT)" +
                                                " SELECT 'C', '*      " + sDia_Semana_Extenso + "'";
                        command.ExecuteNonQuery();
                        //---------- LINHA 6 ----------
                        command.CommandText =   "INSERT INTO " + Param.Nome_Arquivo +
                                                " (TYPE, COMMENT)" +
                                                " SELECT 'C', '*'";
                        command.ExecuteNonQuery();
                        //---------- LINHA 7 ----------
                        if (Param.Data_Programacao.ConvertToDatetime().Month.ToString().ConvertToInt32() == 1)
                        {
                            sMes_Extenso = "Janeiro";
                        }
                        else if (Param.Data_Programacao.ConvertToDatetime().Month.ToString().ConvertToInt32() == 2)
                        {
                            sMes_Extenso = "Fevereiro";
                        }
                        else if (Param.Data_Programacao.ConvertToDatetime().Month.ToString().ConvertToInt32() == 3)
                        {
                            sMes_Extenso = "Março";
                        }
                        else if (Param.Data_Programacao.ConvertToDatetime().Month.ToString().ConvertToInt32() == 4)
                        {
                            sMes_Extenso = "Abril";
                        }
                        else if (Param.Data_Programacao.ConvertToDatetime().Month.ToString().ConvertToInt32() == 5)
                        {
                            sMes_Extenso = "Maio";
                        }
                        else if (Param.Data_Programacao.ConvertToDatetime().Month.ToString().ConvertToInt32() == 6)
                        {
                            sMes_Extenso = "Junho";
                        }
                        else if (Param.Data_Programacao.ConvertToDatetime().Month.ToString().ConvertToInt32() == 7)
                        {
                            sMes_Extenso = "Julho";
                        }
                        else if(Param.Data_Programacao.ConvertToDatetime().Month.ToString().ConvertToInt32() == 8)
                        {
                            sMes_Extenso = "Agosto";
                        }
                        else if (Param.Data_Programacao.ConvertToDatetime().Month.ToString().ConvertToInt32() == 9)
                        {
                            sMes_Extenso = "Setembro";
                        }
                        else if (Param.Data_Programacao.ConvertToDatetime().Month.ToString().ConvertToInt32() == 10)
                        {
                            sMes_Extenso = "Outubro";
                        }
                        else if (Param.Data_Programacao.ConvertToDatetime().Month.ToString().ConvertToInt32() == 11)
                        {
                            sMes_Extenso = "Novembro";
                        }
                        else if (Param.Data_Programacao.ConvertToDatetime().Month.ToString().ConvertToInt32() == 12)
                        {
                            sMes_Extenso = "Dezembro";
                        }
                        command.CommandText =   "INSERT INTO " + Param.Nome_Arquivo +
                                                " (TYPE, COMMENT)" +
                                                " SELECT 'C', '*      " + Param.Data_Programacao.ConvertToDatetime().Day.ToString("dd") + 
                                                "/" + sMes_Extenso + "/" + Param.Data_Programacao.ConvertToDatetime().Year.ToString("yyyy") + "'";
                        command.ExecuteNonQuery();
                        //---------- LINHA 8 ----------
                        command.CommandText =   "INSERT INTO " + Param.Nome_Arquivo +
                                                " (TYPE, COMMENT)" +
                                                " SELECT 'C', '*'";
                        command.ExecuteNonQuery();
                        //---------- LINHA 9 ----------
                        command.CommandText =   "INSERT INTO " + Param.Nome_Arquivo +
                                                " (TYPE, COMMENT)" +
                                                " SELECT 'C', '******************************'";
                        command.ExecuteNonQuery();
                        //-----------------Linha titulo do intervalo nao é valido
                        if (drw["Indica_Titulo_Intervalo"].ToString() == "1")
                        {
                            bolValido = false;
                        }
                        //-----------------Não envia comerciais que usuario não pode ordenar
                        bolImportado = false;
                        if (drw["Indica_Ordenacao"].ToString().ConvertToInt32() == 0 && drw["Indica_Titulo_Break"].ToString().ConvertToInt32() == 0)     //-- && = and
                        {
                            bolImportado = true;
                            bolValido = false;
                        }
                        //-------------------------------------------------------------  Montagem  -------------------------------------------------------------------------
                        if (bolValido)
                        {
                            //----------------------------- Comecou um Programa Novo ------------------------
                            if (drw["Cod_Programa"].ToString() != strProgramaAnterior)
                            {
                                command.CommandText =   "INSERT INTO " + Param.Nome_Arquivo +
                                                        " (CUT, TYPE, COMMENT)" +
                                                        " SELECT '" + drw["Cod_Programa"].ToString() + "', 'C', '" + drw["Titulo_Programa"].ToString().Substring(0, 30) + "'";
                                command.ExecuteNonQuery();
                                strProgramaAnterior = drw["Cod_Programa"].ToString();
                            }
                            //----------------------------- Linha Titulo do Programa / Break ----------------------
                            if (drw["Indica_Titulo_Break"].ToString().ConvertToInt32() == 1)
                            {
                                if (strBreakAnterior != "")
                                {
                                    command.CommandText = "INSERT INTO " + Param.Nome_Arquivo +
                                                            " (CUT, TYPE, COMMENT)" +
                                                            " SELECT '" + drw["Hora_Inicio_Break"].ToString().ConvertToDatetime().ToString("dd/MM/yyyy HH:mm").Substring(12, 5) +
                                                            "', 'C', 'Fim do Break " + strBreakAnterior + "'";
                                    command.ExecuteNonQuery();
                                    strProgramaAnterior = drw["Cod_Programa"].ToString();
                                }
                                command.CommandText = "INSERT INTO " + Param.Nome_Arquivo +
                                                        " (CUT, TYPE, COMMENT)" +
                                                        " SELECT '" +
                                                        drw["Hora_Inicio_Break"].ToString().ConvertToDatetime().ToString("dd/MM/yyyy HH:mm").Substring(12, 5) +
                                                        "', 'C', 'Inicio do Break '" +
                                                        drw["Hora_Inicio_Break"].ToString().ConvertToDatetime().ToString("dd/MM/yyyy HH:mm").Substring(12, 5) + "'";
                                command.ExecuteNonQuery();
                                strBreakAnterior = drw["Hora_Inicio_Break"].ToString().ConvertToDatetime().ToString("dd/MM/yyyy HH:mm").Substring(12, 5);
                            }
                            //------------------------- Linha do Comercial --------------------
                            else
                            {
                                if (bolImportado)
                                {
                                    strNumeroFita = "";
                                }
                                else
                                {
                                    if (drw["Numero_Fita"].ToString() == "")
                                    {
                                        strNumeroFita = "      ";
                                    }
                                    else
                                    {
                                        strNumeroFita = drw["Numero_Fita"].ToString().Substring(nPosicaoFita - 1, nTamanhoFita);
                                    }
                                }
                                strKey = drw["Titulo_Comercial"].ToString();
                                //--
                                String sCut = "";
                                if (strNumeroFita.ToString().ConvertToInt32() > 99999)
                                {
                                    sCut = "TEXTO";
                                }
                                else
                                {
                                    sCut = strNumeroFita;
                                }
                                String sFunction = "";
                                if (bolImportado)
                                {
                                    sFunction = "C";
                                }
                                else
                                {
                                    sFunction = drw["Letra_Dad"].ToString();
                                }
                                String sType = "";
                                if (bolImportado || drw["Letra_Dad"].ToString() == "C")     //-- || = OR
                                {
                                    sType = "C"; 
                                }
                                else
                                {
                                    sType = "P";
                                }
                                command.CommandText = "INSERT INTO " + Param.Nome_Arquivo +
                                                        " (CUT, FUNCTION, PLAYS, SEC, TER, SEGUE, TYPE, COMMENT)" +
                                                        " SELECT '" + sCut + "','" + sFunction + "', '01', '0', '0', '1', '" + sType + "', '" + strKey.ToString().Substring(0, 30) + "'";
                                command.ExecuteNonQuery();
                            } //fecha Else
                        } //--fim bolValido
                        connection.Close();
                    } //--fim foreach
                    //---------------------------------- Ultima Linha ----------------------------------
                    command.CommandText =   "INSERT INTO " + Param.Nome_Arquivo +
                                            " (CUT, FUNCTION, PLAYS, SEGUE, CHAIN, TYPE)" +
                                            " SELECT 'CHAIN', 'L', '01', '0', '" + strProximoArquivo.Substring(0, 8) + "', 'H'";
                    command.ExecuteNonQuery();
                    bolGerouArquivo = true;
                } //--fim using
            } //--fim try
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally
            {
                if (bolGerouArquivo)
                {
                    Retorno = "PLAYLIST/" + this.CurrentUser.ToUpper() + "/" + Param.Nome_Arquivo.ToUpper();
                }
            }
            return Retorno;

        }
        //--------------------------------- FIM ----------------------------------------------------








    }
}





