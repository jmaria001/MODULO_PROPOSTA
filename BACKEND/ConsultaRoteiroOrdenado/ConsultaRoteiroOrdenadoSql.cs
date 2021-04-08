using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace PROPOSTA
{

    public partial class ConsultaRoteiroOrdenado
    {
        private Int32 Contador_Programa = 0;
        private Int32 Contador_Item = -1;
        private Int32 Contador_Posicao = 0;
        private Int32 Contador_Break = 0;
        private Int32 Contador_Intervalo = 0;

        public DataTable CarregarGuiaProgramacao(ConsultaRoteiroOrdenadoFiltroModel Param)
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

        public List<ConsultaRoteiroOrdenadoModel> RoteiroCarregar(ConsultaRoteiroOrdenadoFiltroModel Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            List<ConsultaRoteiroOrdenadoModel> Roteiro = new List<ConsultaRoteiroOrdenadoModel>();
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
                            Roteiro.Add(new ConsultaRoteiroOrdenadoModel()
                            {
                                Cod_Programa = drw["Cod_Programa"].ToString(),
                                Cod_Veiculo = drw["Cod_Veiculo"].ToString(),
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
                                Permite_Ordenacao = drw["Permite_Ordenacao"].ToString().ConvertToBoolean(),

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
                        AddItem(Roteiro, drw, Param.Data_Exibicao.ConvertToDatetime(), Param.Cod_Veiculo);
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


        private void AddItem(List<ConsultaRoteiroOrdenadoModel> Roteiro, DataRow drw, DateTime Data_Exibicao, String Cod_Veiculo)
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
                if (drw["Indica_Midia"].ToString() == "1")
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

                Roteiro.Add(new ConsultaRoteiroOrdenadoModel()
                {
                    Cod_Programa = drw["Cod_Programa"].ToString(),
                    //Cod_Veiculo= drw["Cod_Veiculo"].ToString(),
                    Cod_Veiculo = Cod_Veiculo,
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
                    Horario_Exibicao = drw["Horario_Exibicao"].ToString().ConvertToDatetime().ToString("HH:mm"),
                    Cod_Tipo_Comercial = drw["Cod_Tipo_Comercial"].ToString(),
                    Versao = drw["Inicio_Programa"].ToString().ConvertToInt32(),
                    Origem = strOrigem,
                    Permite_Ordenacao = drw["Permite_Ordenacao"].ToString().ConvertToBoolean(),
                    Show = true
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
 
  
    }
}
