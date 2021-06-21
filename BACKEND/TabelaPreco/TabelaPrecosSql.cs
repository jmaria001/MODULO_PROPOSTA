using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web;
using System.IO;

namespace PROPOSTA
{

    public partial class TabelaPrecos
    {

        //===========================Listar Tabela de Precos
        public DataTable TabelaPrecosListar(FiltroModel pFiltro)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_TabelaPrecos_Listar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia", clsLib.CompetenciaInt(pFiltro.Competencia));
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Veiculo", pFiltro.Veiculo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Programa", pFiltro.Programa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Vigente", pFiltro.Indica_Vigente);
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


        //===========================Salvar Itens de Permuta
        public DataTable SalvarTabelaPrecos(TabelaPrecosModel pTabelaPrecos)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_TabelaPrecos_Salvar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Operacao", pTabelaPrecos.Id_Operacao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia", pTabelaPrecos.Competencia);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Sequencia", pTabelaPrecos.Sequencia);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Tipo_Preco", pTabelaPrecos.Tipo_Preco.ToUpper());
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", pTabelaPrecos.Cod_Programa.ToUpper());
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Valor", pTabelaPrecos.Valor);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo_Mercado", pTabelaPrecos.Cod_Veiculo_Mercado.ToUpper());
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


        //===========================Get Tabela Preços

        public TabelaPrecosModel GetTabelaPrecosData(String pCompetencia, String pCod_Programa, String pCod_Veiculo_Mercado)
        //public TabelaPrecosModel GetTabelaPrecosData(String pCompetencia)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            TabelaPrecosModel TabelaPrecos = new TabelaPrecosModel();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_TabelaPrecos_Get");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia", pCompetencia);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", pCod_Programa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo_Mercado", pCod_Veiculo_Mercado);
                Adp.Fill(dtb);
                if (dtb.Rows.Count > 0)
                {
                    TabelaPrecos.Competencia = dtb.Rows[0]["Competencia"].ToString();
                    TabelaPrecos.Sequencia = dtb.Rows[0]["Sequencia"].ToString().ConvertToInt32();
                    TabelaPrecos.Tipo_Preco = dtb.Rows[0]["Tipo_Preco"].ToString();
                    TabelaPrecos.Cod_Programa = dtb.Rows[0]["Cod_Programa"].ToString();
                    TabelaPrecos.Titulo = dtb.Rows[0]["Titulo"].ToString();
                    TabelaPrecos.Cod_Veiculo_Mercado = dtb.Rows[0]["Cod_Veiculo_Mercado"].ToString().Trim();
                    TabelaPrecos.Nome_Veiculo = dtb.Rows[0]["Nome_Veiculo"].ToString();
                    TabelaPrecos.Valor = dtb.Rows[0]["Valor"].ToString();


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
            return TabelaPrecos;
        }



        //===========================Excluir Tabela de Precos
        public DataTable ExcluirTabelaPrecos(TabelaPrecosModel pTabelaPrecos)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_TabelaPrecos_Excluir");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia", pTabelaPrecos.Competencia);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", pTabelaPrecos.Cod_Programa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo_Mercado", pTabelaPrecos.Cod_Veiculo_Mercado);
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
        //===========================Importar Tabela de Precos  
        public List<TabelaPrecosModel> ImportarTabelaPrecos(TabelaPrecoImportModel pParam)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            List<TabelaPrecosModel> Tabela = new List<TabelaPrecosModel>();
            SimLib clsLib = new SimLib();
            String sPath = HttpContext.Current.Server.MapPath("~/ANEXOS/TABELAPRECO");
            try
            {
                //-----------------Abre o arquivo texto que fez upload


                if (sPath.Right(1) != @"\")
                {
                    sPath += @"\";
                }
                sPath += this.CurrentUser;
                sPath += @"\";
                String strFile = sPath + pParam.File;
                //-----------------Ler o Arquivo de retorno e gravar no sql 
                var lines = File.ReadAllLines(strFile);

                for (var i = 1; i < lines.Length; i += 1)
                {
                    var values = lines[i].Split(';');
                    if (values.Length < 3)
                    {
                        Tabela.Add(new TabelaPrecosModel()
                        {
                            Competencia = "",
                            Tipo_Preco = "",
                            Cod_Programa = "",
                            Titulo = "",
                            Cod_Veiculo_Mercado = "",
                            Nome_Veiculo = "",
                            Valor_Dec = 0,
                            Critica = "O Arquivo CSV não está em um formato válido"
                        });
                        break;
                    }

                    if (!String.IsNullOrEmpty(values[0]) && !String.IsNullOrEmpty(values[1]))
                    {


                        SqlDataAdapter Adp = new SqlDataAdapter();
                        DataTable dtb = new DataTable("dtb");
                        //-------------------------------------Executa a procedure para cada linha
                        SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_PROPOSTA_Tabela_Preco_Excel_Processa_CSV");
                        cmd.Parameters.AddWithValue("@Par_Cod_Veiculo_Mercado", values[0]);
                        cmd.Parameters.AddWithValue("@Par_Cod_Programa", values[1]);
                        Adp.SelectCommand = cmd;
                        var newValor = values[2].Replace(".", ",");
                        Adp.Fill(dtb);
                        Tabela.Add(new TabelaPrecosModel()
                        {
                            Competencia = pParam.Competencia,
                            Tipo_Preco = pParam.Tipo_Preco,
                            Cod_Programa = dtb.Rows[0]["Cod_Programa"].ToString(),
                            Titulo = dtb.Rows[0]["Titulo_Programa"].ToString(),
                            Cod_Veiculo_Mercado = dtb.Rows[0]["Cod_Veiculo_Mercado"].ToString(),
                            Nome_Veiculo = dtb.Rows[0]["Nome_Veiculo_Mercado"].ToString(),
                            Valor_Dec = newValor.ConvertToDouble(),
                            Critica = dtb.Rows[0]["Critica"].ToString(),
                        });

                        dtb.Dispose();
                        Adp.Dispose();
                        cmd.Dispose();
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
                //=========================Apaga todos os arquivos da pasta  
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
            }
            return Tabela;
        }
        //===========================Processar Importacao  Tabela de Precos  
        public List<TabelaPrecosModel> ProcessarImportacaoPreco(List<TabelaPrecosModel> pParam)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SimLib clsLib = new SimLib();
            String sPath = HttpContext.Current.Server.MapPath("~/ANEXOS/TABELAPRECO");
            try
            {
                for (int i = 0; i < pParam.Count; i++)
                {
                    if (String.IsNullOrEmpty(pParam[i].Critica))
                    {
                        SqlDataAdapter Adp = new SqlDataAdapter();
                        DataTable dtb = new DataTable("dtb");
                        //-------------------------------------Executa a procedure para cada linha
                        SqlCommand cmd= cnn.Procedure(cnn.Connection, "[Pr_Tabela_Preco_Gravar]");
                        cmd.Parameters.AddWithValue("@Par_Competencia",clsLib.CompetenciaInt( pParam[i].Competencia));
                        cmd.Parameters.AddWithValue("@Par_Sequencia", 1);
                        cmd.Parameters.AddWithValue("@Par_Tipo_Preco", pParam[i].Tipo_Preco);
                        cmd.Parameters.AddWithValue("@Par_Cod_Programa", pParam[i].Cod_Programa);
                        cmd.Parameters.AddWithValue("@Par_Valor", pParam[i].Valor_Dec);
                        cmd.Parameters.AddWithValue("@Par_Cod_Veiculo_Mercado", pParam[i].Cod_Veiculo_Mercado);
                        cmd.Parameters.AddWithValue("@Par_Usuario", this.CurrentUser); 
                        Adp.SelectCommand = cmd;
                        Adp.Fill(dtb);
                        pParam[i].Critica = dtb.Rows[0]["Mensagem"].ToString();
                        pParam[i].Indica_Processado = true;
                        dtb.Dispose();
                        Adp.Dispose();
                        cmd.Dispose();
                    }
                };
            }

            finally
            {
                cnn.Close();
                //=========================Apaga todos os arquivos da pasta  
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
            }
            return pParam;
        }

    }
}
