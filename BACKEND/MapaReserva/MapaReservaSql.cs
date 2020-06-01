using CLASSDB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PROPOSTA
{
    public partial class MapaReserva

    {
        public DataTable MapaReservaList(MapaReservaFiltroModel Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            List<MapaReservaModel> Negociacoes = new List<MapaReservaModel>();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_MapaReserva_List");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Negociacao", Param.Numero_Negociacao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Mr", Param.Numero_Mr);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Pi", Param.Numero_Pi);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa_Venda", Param.Cod_Empresa_Venda);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa_Faturamento", Param.Cod_Empresa_Faturamento);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia_Inicio", clsLib.CompetenciaInt(Param.Competencia_Inicio));
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia_Termino", clsLib.CompetenciaInt(Param.Competencia_Fim));
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Agencia", Param.Agencia);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cliente", Param.Cliente);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Contato", Param.Contato);
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
        public DataTable MapaReservaDetalheContrato(Int32 pIdContrato)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_MapaReserva_Get_Contrato");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Id_Contrato", pIdContrato);
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
        public DataTable MapaReservaDetalheComercial(Int32 pIdContrato)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_MapaReserva_Get_Comercial");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Id_Contrato", pIdContrato);
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
        public DataTable MapaReservaDetalheCompetencia(Int32 pIdContrato)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_MapaReserva_Get_Competencia");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Id_Contrato", pIdContrato);
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
        public DataTable MapaReservaDetalheVeiculo(Int32 pIdContrato)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();

            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_MapaReserva_Get_Veiculo");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Id_Contrato", pIdContrato);
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
        public List<MapaReservaMidiaModel> MapaReservaDetalheMidia(MapaReservaMidiaFiltroModel Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            List<MapaReservaMidiaModel> Midias = new List<MapaReservaMidiaModel>();

            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_MapaReserva_Get_Midia");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Id_Contrato", Param.Id_Contrato);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia", Param.Competencia);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", Param.Cod_Veiculo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Display", Param.Display);
                Adp.Fill(dtb);
                

                DataView viewMidia = new DataView(dtb);
                DataTable dtbMidia = viewMidia.ToTable(true, "Cod_Veiculo","Tipo_Linha","Cod_Programa","Cod_Caracteristica","Cod_Comercial","Indica_Exibido");
                Int32 Qtd_Total = 0;
                Double Valor_Negociado = 0;
                Double Valor_Tabela = 0;
                foreach (DataRow drw in dtbMidia.Rows)
                {
                    Qtd_Total = 0;
                    Valor_Negociado = 0;
                    Valor_Tabela = 0;
                    MapaReservaMidiaModel MidiaTemp = new MapaReservaMidiaModel();
                    MidiaTemp.Tipo_Linha= drw["Tipo_Linha"].ToString().ConvertToInt32();
                    MidiaTemp.Cod_Veiculo = drw["Cod_Veiculo"].ToString();
                    MidiaTemp.Cod_Programa = drw["Cod_Programa"].ToString();
                    MidiaTemp.Cod_Caracteristica = drw["Cod_Caracteristica"].ToString();
                    MidiaTemp.Cod_Comercial = drw["Cod_Comercial"].ToString();
                    MidiaTemp.Indica_Exibido =drw["Indica_Exibido"].ToString().ConvertToBoolean();
                    MidiaTemp.Insercoes = new List<MapaReservaInsercoesModel>();
                    String strSql = "Cod_Veiculo = '" + drw["Cod_Veiculo"].ToString() + "'";
                    strSql += " And Tipo_Linha= '" + drw["Tipo_Linha"].ToString() + "'";
                    strSql += " And Cod_Programa = '" + drw["Cod_Programa"].ToString() + "'";
                    strSql += " And Cod_Caracteristica = '" + drw["Cod_Caracteristica"].ToString() + "'";
                    strSql += " And Cod_Comercial= '" + drw["Cod_Comercial"].ToString() + "'";
                    strSql += " And Indica_Exibido= " + drw["Indica_Exibido"].ToString();

                    DataRow[] rows = dtb.Select(strSql,"Data_Exibicao");
                    for (int i = 0; i < rows.Length; i++)
                    {
                        Valor_Tabela += rows[i]["Valor_Tabela"].ToString().ConvertToDouble();
                        Valor_Negociado += rows[i]["Valor_Negociado"].ToString().ConvertToDouble();
                        Qtd_Total += rows[i]["Qtd"].ToString().ConvertToInt32();

                        MidiaTemp.Insercoes.Add(new MapaReservaInsercoesModel()
                        {
                            Data_Exibicao = rows[i]["Data_Exibicao"].ToString(),
                            Dia = rows[i]["Dia"].ToString(),
                            Dia_Semana = rows[i]["Dia_Semana"].ToString(),
                            Qtd = rows[i]["Qtd"].ToString().ConvertToInt32(),
                        });

                    }
                    MidiaTemp.Valor_Negociado = Valor_Negociado;
                    MidiaTemp.Valor_Tabela = Valor_Tabela;
                    if (Valor_Tabela>0)
                    {
                        MidiaTemp.Desconto = (1 - (Valor_Negociado / Valor_Tabela)) * 100;
                    }
                    else
                    {
                        MidiaTemp.Desconto = 0;
                    }
                    MidiaTemp.Qtd_Total = Qtd_Total;


                    Midias.Add(MidiaTemp);
                }
            }
            catch (Exception )
            {
                throw ;
            }
            finally
            {
                cnn.Close();
            }
            return Midias;
        }
        public DataTable MapaReservaDetalheVeiculacoes(MapaReservaMidiaFiltroModel Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();

            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_MapaReserva_Get_Veiculacoes");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Id_Contrato", Param.Id_Contrato);
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
        public DataTable MapaReservaDetalheResumo(MapaReservaMidiaFiltroModel Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();

            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_MapaReserva_Get_Resumo");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Id_Contrato", Param.Id_Contrato);
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
