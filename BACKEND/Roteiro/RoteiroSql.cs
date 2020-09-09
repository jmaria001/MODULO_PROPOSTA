using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace PROPOSTA
{

    public partial class Roteiro
    {
        private Int32 Contador_Programa = 0;
        private Int32 Contador_Item = -1;
        private Int32 Contador_Posicao = 0;
        private Int32 Contador_Break= 0;
        private Int32 Contador_Intervalo= 0;

        public DataTable CarregarGuiaProgramacao(RoteiroFiltroModel Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Sp_Roteiro_Afiliadas_Programa");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", Param.Cod_Veiculo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Exibicao", Param.Data_Exibicao.ConvertToDatetime().ToString("yyyy-MM-dd"));
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", DBNull.Value);
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

        public List<RoteiroModel> RoteiroCarregar(RoteiroFiltroModel Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            List<RoteiroModel> Roteiro = new List<RoteiroModel>();
            try
            {
                String strProgramas = "";
                for (int i = 0; i < Param.Programas.Count; i++)
                {
                    if (Param.Programas[i].Selected)
                    {
                        strProgramas += (Param.Programas[i].Cod_Programa + "    ").Left(4);
                    }
                }
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Sp_Ru_Carrega_Roteiro_V2");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", Param.Cod_Veiculo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Exibicao", Param.Data_Exibicao.ConvertToDatetime().ToString("yyyy-MM-dd"));
                if (strProgramas == "")
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", DBNull.Value);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", strProgramas);
                }
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Relatorio", 0);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Ordenacao", 1);
                Adp.Fill(dtb);
                if (dtb.Rows.Count > 0)
                {
                    String Cod_Programa_Ant = "";
                    Int32 Break_Ant = -1;
                    foreach (DataRow drw in dtb.Rows)
                    {
                        if (Cod_Programa_Ant != drw["Cod_Programa"].ToString())
                        {
                            Contador_Programa++;
                            Contador_Item++;
                            Roteiro.Add(new RoteiroModel()
                            {
                                Cod_Programa = drw["Cod_Programa"].ToString(),
                                Cod_Veiculo= drw["Cod_Veiculo"].ToString(),
                                Data_Exibicao = Param.Data_Exibicao.ConvertToDatetime(),
                                Titulo_Programa = drw["Titulo_Programa"].ToString(),
                                Hora_Inicio_Programa = drw["Inicio_Programa"].ToString().ConvertToDatetime(),
                                Hora_Fim_Programa = drw["Fim_Programa"].ToString().ConvertToDatetime(),
                                Versao = drw["Inicio_Programa"].ToString().ConvertToInt32(),
                                Id_Programa = Contador_Programa,
                                Id_Item = Contador_Item,
                                Indica_Titulo_Programa = true,
                                Indica_Titulo_Break = false,
                                Indica_Titulo_Intervalo = false,
                                Id_Break = Contador_Break,
                                Id_Intervalo = Contador_Intervalo,
                                
                                Show = true,
                            });
                            Cod_Programa_Ant = drw["Cod_Programa"].ToString();
                            Break_Ant = -1;
                        }
                        if (Break_Ant != drw["Breaks"].ToString().ConvertToInt32())
                        {
                            Contador_Posicao = 0;
                            Break_Ant = drw["Breaks"].ToString().ConvertToInt32();
                        }
                        AddItem(Roteiro, drw,Param.Data_Exibicao.ConvertToDatetime());
                    }
                    //Roteiro[0].Contador_Item = Contador_Item;
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
            return Roteiro;
        }
        public List<RoteiroComercialModel> RoteiroCarregarComerciais(RoteiroFiltroModel Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            SimLib clsLib = new SimLib();
            List<RoteiroComercialModel> Comerciais = new List<RoteiroComercialModel>();
            try
            {
                AddComercial(Param, Comerciais);
                AddRotativo(Param, Comerciais);
                AddAvulso(Param, Comerciais);
                AddArtistico(Param, Comerciais);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cnn.Close();
            }
            return Comerciais;
        }

        private void AddItem(List<RoteiroModel> Roteiro, DataRow drw,DateTime Data_Exibicao)
        {
            String strNome_Tipo_Break = "";
            Boolean bolIndica_Comercial = false;
            try
            {
                switch (drw["Tipo_Break"].ToString().ConvertToInt32())
                {
                    case 0:
                        strNome_Tipo_Break = "Local";
                        break;
                    case 1:
                        strNome_Tipo_Break = "Net";
                        break;
                    case 2:
                        strNome_Tipo_Break = "Chamadas Artisticas";
                        break;
                    case 3:
                        strNome_Tipo_Break = "PE";
                        break;
                    default:
                        break;
                }
                if (String.IsNullOrEmpty(drw["Indica_Titulo_Break"].ToString()) && String.IsNullOrEmpty(drw["Indica_Titulo_Intervalo"].ToString()))
                {
                    bolIndica_Comercial = true;
                    Contador_Posicao++;
                }

                String strOrigem = "";
                if (drw["Indica_Midia"].ToString()=="1")
                {
                    strOrigem = "Midia";
                }
                if (drw["Indica_Avulso"].ToString() == "1")
                {
                    strOrigem = "Avulso";
                }
                if (drw["Indica_Artistico"].ToString() == "1")
                {
                    strOrigem = "Artistico";
                }
                if (drw["Indica_Rotativo"].ToString() == "1")
                {
                    strOrigem = "Rotativo";
                }
                Contador_Item++;
                if (drw["Indica_Titulo_Break"].ToString().ConvertToInt32() == 1)
                {
                    Contador_Break++;
                }
                if (drw["Indica_Titulo_Intervalo"].ToString().ConvertToInt32() == 1)
                {
                    Contador_Intervalo++;
                }

                Roteiro.Add(new RoteiroModel()
                {
                    Cod_Programa = drw["Cod_Programa"].ToString(),
                    Cod_Veiculo= drw["Cod_Veiculo"].ToString(),
                    Data_Exibicao = Data_Exibicao,
                    Titulo_Programa = drw["Titulo_Programa"].ToString(),
                    Hora_Inicio_Programa = drw["Inicio_Programa"].ToString().ConvertToDatetime(),
                    Hora_Fim_Programa = drw["Fim_Programa"].ToString().ConvertToDatetime(),
                    Id_Programa = Contador_Programa,
                    Id_Item = Contador_Item,
                    Id_Break = Contador_Break,
                    Id_Intervalo = Contador_Intervalo,
                    Break = drw["Breaks"].ToString().ConvertToInt32(),
                    Titulo_Break = drw["Titulo_Break"].ToString(),
                    Sequencia_Faixa = drw["Sequencia_Faixa"].ToString().ConvertToInt32(),
                    Sequencia_Intervalo = Contador_Posicao,
                    Hora_Inicio_Break = drw["Hora_Inicio_Break"].ToString().ConvertToDatetime().ToString("HH:mm"),
                    Tipo_Break = drw["Tipo_Break"].ToString().ConvertToInt32(),
                    Nome_Tipo_Break = strNome_Tipo_Break,
                    Indica_Titulo_Programa = false,
                    Indica_Titulo_Break = drw["Indica_Titulo_Break"].ToString().ConvertToInt32() == 1,
                    Indica_Titulo_Intervalo = drw["Indica_Titulo_Intervalo"].ToString().ConvertToInt32() == 1,
                    Indica_Comercial = bolIndica_Comercial,
                    Titulo_Comercial = drw["Titulo_Comercial"].ToString(),
                    Cod_Comercial = drw["Cod_Comercial"].ToString(),
                    Numero_Fita = drw["Numero_Fita"].ToString(),
                    Id_Fita = drw["Id_Fita"].ToString().ConvertToInt32(),
                    Duracao = drw["Duracao"].ToString().ConvertToInt32(),
                    Cod_Produto = drw["Cod_Produto"].ToString(),
                    Nome_Produto = drw["Descricao_Produto"].ToString(),
                    Encaixe = drw["Encaixe"].ToString().ConvertToInt32(),
                    Cod_Empresa = drw["Cod_Empresa"].ToString(),
                    Numero_Mr = drw["Numero_Mr"].ToString().ConvertToInt32(),
                    Sequencia_Mr = drw["Sequencia_Mr"].ToString().ConvertToInt32(),
                    Cod_Programa_Origem = drw["Cod_Programa_Origem"].ToString(),
                    Cod_Veiculo_Origem = drw["Cod_Veiculo_Origem"].ToString(),
                    Chave_Acesso = drw["Chave_Acesso"].ToString().ConvertToInt32(),
                    Versao = drw["Inicio_Programa"].ToString().ConvertToInt32(),
                    Origem = strOrigem,
                    Show = true
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void AddComercial(RoteiroFiltroModel Param, List<RoteiroComercialModel> Comerciais)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable();
            SimLib clsLib = new SimLib();
            Int32 Tipo_Break =  - 1;
            String Nome_Tipo_Break = "";
            string strPasta = "";

            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Roteiro_Carrega_Comerciais");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", Param.Cod_Veiculo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Exibicao", Param.Data_Exibicao.ConvertToDatetime().ToString("yyyy-MM-dd"));
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", DBNull.Value);
                Adp.Fill(dtb);

                String strPrograma_Anterior = "";

                foreach (DataRow drw in dtb.Rows)
                {
                    switch (drw["Indica_Grade"].ToString().ConvertToInt32())
                    {
                        case 0:
                            Tipo_Break = 1;
                            Nome_Tipo_Break = "Net";
                            break;
                        case 1 :
                            Tipo_Break = 0;
                            Nome_Tipo_Break = "Local";
                            break;
                        case 2:
                            Tipo_Break = 0;
                            Nome_Tipo_Break = "Local";
                            break;
                        default:
                            break;
                    }

                    if (strPrograma_Anterior != drw["Cod_Programa"].ToString())
                    {
                        strPasta = "Outros";
                        for (int i = 0; i < Param.Programas.Count; i++)
                        {
                            if (drw["Cod_Programa"].ToString()==Param.Programas[i].Cod_Programa && Param.Programas[i].Selected)
                            {
                                strPasta = "Midia";
                            }
                        }
                        Contador_Item++;
                        Comerciais.Add(new RoteiroComercialModel()
                        {
                            Id_Item = Contador_Item,
                            Origem = "Midia",
                            Indica_Titulo_Programa = true,
                            Cod_Programa = drw["Cod_Programa"].ToString(),
                            Cod_Veiculo = drw["Cod_Veiculo"].ToString(),
                            Data_Exibicao = Param.Data_Exibicao.ConvertToDatetime(),
                            Titulo_Programa = drw["Titulo_Programa"].ToString(),
                            Hora_Inicio_Programa = drw["Inicio_Programa"].ToString().ConvertToDatetime(),
                            Hora_Fim_Programa = drw["Fim_Programa"].ToString().ConvertToDatetime(),
                            Pasta = strPasta,
                        });
                        strPrograma_Anterior = drw["Cod_Programa"].ToString();
                    }
                    Contador_Item++;
                    Comerciais.Add(new RoteiroComercialModel()
                    {
                        Id_Item = Contador_Item,
                        Origem = "Midia",
                        Indica_Titulo_Programa = false,
                        Cod_Programa = drw["Cod_Programa"].ToString(),
                        Cod_Veiculo= drw["Cod_Veiculo"].ToString(),
                        Data_Exibicao= Param.Data_Exibicao.ConvertToDatetime(),
                        Titulo_Programa = drw["Titulo_Programa"].ToString(),
                        Hora_Inicio_Programa = drw["Inicio_Programa"].ToString().ConvertToDatetime(),
                        Hora_Fim_Programa = drw["FIm_Programa"].ToString().ConvertToDatetime(),
                        Cod_Empresa = drw["Cod_Empresa"].ToString(),
                        Numero_Mr = drw["Numero_Mr"].ToString().ConvertToInt32(),
                        Sequencia_Mr = drw["Sequencia_Mr"].ToString().ConvertToInt32(),
                        Cod_Comercial = drw["Cod_Comercial"].ToString(),
                        Titulo_Comercial = drw["Titulo_Comercial"].ToString(),
                        Duracao = drw["Duracao"].ToString().ConvertToInt32(),
                        Numero_Fita = drw["Numero_Fita"].ToString(),
                        Tipo_Break=Tipo_Break,
                        Nome_Tipo_Break=Nome_Tipo_Break,
                        Indica_Titulo_Determinar = drw["Indica_Titulo_Determinar"].ToString().ConvertToBoolean(),
                        Cod_Tipo_Comercial = drw["Cod_Tipo_Comercial"].ToString(),
                        Cod_Produto = drw["Cod_Produto"].ToString(),
                        Nome_Produto = drw["Descricao"].ToString(),
                        Indica_Grade = drw["Indica_Grade"].ToString().ConvertToByte(),
                        Chave_Acesso = drw["Chave_Acesso"].ToString().ConvertToInt32(),
                        Obs_Roteiro = drw["Obs_Roteiro"].ToString(),
                        Indica_Ordenado= drw["Indica_Ordenado"].ToString().ConvertToBoolean(),
                        Pasta = strPasta,
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
        }
        private void AddRotativo(RoteiroFiltroModel Param, List<RoteiroComercialModel> Comerciais)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable();
            SimLib clsLib = new SimLib();
            Int32 Tipo_Break = -1;
            String Nome_Tipo_Break = "";
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Roteiro_Carrega_Rotativo");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", Param.Cod_Veiculo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Exibicao", Param.Data_Exibicao.ConvertToDatetime().ToString("yyyy-MM-dd"));
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", DBNull.Value);
                Adp.Fill(dtb);

                String strPrograma_Anterior = "";

                foreach (DataRow drw in dtb.Rows)
                {
                    switch (drw["Indica_Grade"].ToString().ConvertToInt32())
                    {
                        case 0:
                            Tipo_Break = 1;
                            Nome_Tipo_Break = "Net";
                            break;
                        case 1:
                            Tipo_Break = 0;
                            Nome_Tipo_Break = "Local";
                            break;
                        case 2:
                            Tipo_Break = 0;
                            Nome_Tipo_Break = "Local";
                            break;
                        default:
                            break;
                    }

                    if (strPrograma_Anterior != drw["Cod_Programa"].ToString())
                    {
                        Contador_Item++;
                        Comerciais.Add(new RoteiroComercialModel()
                        {
                            Id_Item = Contador_Item,
                            Origem = "Midia",
                            Indica_Titulo_Programa = true,
                            Cod_Programa = drw["Cod_Programa"].ToString(),
                            Cod_Veiculo= drw["Cod_Veiculo"].ToString(),
                            Titulo_Programa = drw["Titulo_Programa"].ToString(),
                            Hora_Inicio_Programa = drw["Hora_Inicio_Programa"].ToString().ConvertToDatetime(),
                            Hora_Fim_Programa = drw["Hora_Fim_Programa"].ToString().ConvertToDatetime(),
                            Pasta = "Rotativo",
                        });
                        strPrograma_Anterior = drw["Cod_Programa"].ToString();
                    }
                    Contador_Item++;
                    Comerciais.Add(new RoteiroComercialModel()
                    {
                        Id_Item = Contador_Item,
                        Origem = "Midia",
                        Indica_Titulo_Programa = false,
                        Cod_Programa = drw["Cod_Programa"].ToString(),
                        Cod_Veiculo = drw["Cod_Veiculo"].ToString(),
                        Data_Exibicao = Param.Data_Exibicao.ConvertToDatetime(),
                        Titulo_Programa = drw["Titulo_Programa"].ToString(),
                        Hora_Inicio_Programa = drw["Hora_Inicio_Programa"].ToString().ConvertToDatetime(),
                        Hora_Fim_Programa = drw["Hora_Fim_Programa"].ToString().ConvertToDatetime(),
                        Cod_Empresa = drw["Cod_Empresa"].ToString(),
                        Numero_Mr = drw["Numero_Mr"].ToString().ConvertToInt32(),
                        Sequencia_Mr = drw["Sequencia_Mr"].ToString().ConvertToInt32(),
                        Cod_Comercial = drw["Cod_Comercial"].ToString(),
                        Titulo_Comercial = drw["Titulo_Comercial"].ToString(),
                        Duracao = drw["Duracao"].ToString().ConvertToInt32(),
                        Numero_Fita = drw["Numero_Fita"].ToString(),
                        Tipo_Break = Tipo_Break,
                        Nome_Tipo_Break = Nome_Tipo_Break,
                        Indica_Titulo_Determinar = drw["Indica_Titulo_Determinar"].ToString().ConvertToBoolean(),
                        Cod_Tipo_Comercial = drw["Cod_Tipo_Comercial"].ToString(),
                        Cod_Produto = drw["Cod_Produto"].ToString(),
                        Nome_Produto = drw["Descricao"].ToString(),
                        Indica_Grade = drw["Indica_Grade"].ToString().ConvertToByte(),
                        Chave_Acesso = drw["Chave_Acesso"].ToString().ConvertToInt32(),
                        Obs_Roteiro = drw["Obs_Roteiro"].ToString(),
                        Indica_Ordenado = drw["Indica_Ordenado"].ToString().ConvertToBoolean(),
                        Pasta = "Rotativo",
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
        }
        private void AddAvulso(RoteiroFiltroModel Param, List<RoteiroComercialModel> Comerciais)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable();
            SimLib clsLib = new SimLib();
            
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Sp_RU_Carrega_Depositorio");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", Param.Cod_Veiculo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Exibicao", Param.Data_Exibicao.ConvertToDatetime().ToString("yyyy-MM-dd"));
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", DBNull.Value);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Tipo", 0);
                Adp.Fill(dtb);

            

                foreach (DataRow drw in dtb.Rows)
                {
                    
                    Contador_Item++;
                    Comerciais.Add(new RoteiroComercialModel()
                    {
                        Id_Item = Contador_Item,
                        Origem = "Avulso",
                        Indica_Titulo_Programa = false,
                        Cod_Programa = drw["Cod_Programa"].ToString(),
                        Cod_Veiculo  = Param.Cod_Veiculo,
                        Data_Exibicao = Param.Data_Exibicao.ConvertToDatetime(),
                        Titulo_Comercial = drw["Titulo_Comercial"].ToString(),
                        Duracao = drw["Duracao"].ToString().ConvertToInt32(),
                        Numero_Fita = drw["Numero_Fita"].ToString(),
                        Id_Fita = drw["IdChave"].ToString().ConvertToInt32(),
                        Cod_Tipo_Comercial = drw["Cod_Tipo_Comercial"].ToString(),
                        Cod_Produto = drw["Cod_Produto"].ToString(),
                        Nome_Produto = drw["Descricao"].ToString(),
                        Pasta = "Avulso",
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
        }
        private void AddArtistico(RoteiroFiltroModel Param, List<RoteiroComercialModel> Comerciais)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable();
            SimLib clsLib = new SimLib();

            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Sp_RU_Carrega_Depositorio");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", Param.Cod_Veiculo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Exibicao", Param.Data_Exibicao.ConvertToDatetime().ToString("yyyy-MM-dd"));
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", DBNull.Value);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Tipo", 1);
                Adp.Fill(dtb);



                foreach (DataRow drw in dtb.Rows)
                {

                    Contador_Item++;
                    Comerciais.Add(new RoteiroComercialModel()
                    {
                        Id_Item = Contador_Item,
                        Origem = "Artistico",
                        Indica_Titulo_Programa = false,
                        Cod_Programa = drw["Cod_Programa"].ToString(),
                        Cod_Veiculo = Param.Cod_Veiculo,
                        Data_Exibicao = Param.Data_Exibicao.ConvertToDatetime(),
                        Titulo_Comercial = drw["Titulo_Comercial"].ToString(),
                        Duracao = drw["Duracao"].ToString().ConvertToInt32(),
                        Numero_Fita = drw["Numero_Fita"].ToString(),
                        Id_Fita= drw["IdChave"].ToString().ConvertToInt32(),
                        Cod_Tipo_Comercial = drw["Cod_Tipo_Comercial"].ToString(),
                        Cod_Produto = drw["Cod_Produto"].ToString(),
                        Nome_Produto = drw["Descricao"].ToString(),
                        Tipo_Break = null,
                        Pasta = "Artistico",
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
        }
        public DataTable RoteiroBaixarVeiculacao(RoteiroModel Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Roteiro_Baixar_Veiculacao");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", Param.Cod_Veiculo); 
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Exibicao", Param.Data_Exibicao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", Param.Cod_Programa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Chave_Acesso", Param.Chave_Acesso);
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
        public void RoteiroExcluir(RoteiroFiltroModel Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                String strsql = "Delete from Roteiro_Tecnico Where Cod_Veiculo = '" + Param.Cod_Veiculo + "'";
                strsql += " And Data_Exibicao = '" + Param.Data_Exibicao.ToString().ConvertToDatetime().ToString("yyyy-MM-dd") + "'";
                SqlCommand cmd = cnn.Text(cnn.Connection, strsql );
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
        public DataTable RoteiroSalvar(List<RoteiroModel> Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();

            String xmlRoteiro = clsLib.SerializeToString(Param);
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Roteiro_Salvar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Roteiro", xmlRoteiro);
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
