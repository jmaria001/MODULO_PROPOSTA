using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace PROPOSTA
{

    public partial class FitaPatrocinio
    {
        //===========================Listar Fitas Patrocinio
        public DataTable FitaPatrocinioListar(FiltroModel pFiltro)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open(); 
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Fita_Patrocinio_List");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia_Inicial", clsLib.CompetenciaInt(pFiltro.CompetenciaInicial));
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia_Final", clsLib.CompetenciaInt(pFiltro.CompetenciaFinal));
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", pFiltro.Cod_Programa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", pFiltro.Cod_Veiculo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numerada", pFiltro.Indica_Numerada);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Pendente", pFiltro.Indica_Pendente);
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
        //===========================Gravar Fitas Patrocinio
        public DataTable FitaPatrocinioGravar(FitaPatrocinioModel Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Fita_Patrocinio_Gravar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Id_Fita_Patrocinio",Param.Id_Fita_Patrocinio );
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", Param.Cod_Veiculo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", Param.Cod_Programa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Tipo_Comercial", Param.Cod_Tipo_Comercial);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Fita", Param.Numero_Fita);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia", Param.Competencia);
                if (!String.IsNullOrEmpty(Param.Inicio_Validade))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Inicio_Validade", Param.Inicio_Validade.ConvertToDatetime());
                }
                if (!String.IsNullOrEmpty(Param.Fim_Validade))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Fim_Validade", Param.Fim_Validade.ConvertToDatetime());
                }
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Duracao_Cabeca", Param.Duracao_Cabeca);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Obs_Texto", Param.Obs_Texto);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Titulo_Texto", Param.Titulo_Texto);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Id_Apresentador", Param.Id_Apresentador);

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
        //===========================Procurar Fita Disponivel
        public DataTable FitaPatrocinioProcurarFita(FitaPatrocinioModel Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Sp_Ru_Fita_Disponivel");
                Adp.SelectCommand = cmd;

                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", Param.Cod_Veiculo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Inicio", 1);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Fim", 9999);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Tipo", 0);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Usuario", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Relatorio", 0);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Tipo_Midia", DBNull.Value);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Comercial", Param.Cod_Tipo_Comercial);
                Adp.Fill(dtb);

                SqlCommand cmdDelete  = cnn.Text(cnn.Connection, "Delete From Reserva_Fita Where Cod_Usuario = '" + this.CurrentUser + "'");
                cmdDelete.ExecuteNonQuery();
                
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
        //===========================Desativar Fita Patrocinio
        public void FitaPatrocinioDesativar(FitaPatrocinioModel Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Fita_Patrocinio_Desativar");
                cmd.Parameters.AddWithValue("@Par_Id_Fita_Patrocinio", Param.Id_Fita_Patrocinio);
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
        //===========================Excluir Fita Patrocinio
        public void FitaPatrocinioExcluir(FitaPatrocinioModel Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Fita_Patrocinio_Excluir");
                cmd.Parameters.AddWithValue("@Par_Id_Fita_Patrocinio", Param.Id_Fita_Patrocinio);
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
        public DataTable FitaPatrocinioContratos(FitaPatrocinioModel Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Fita_Patrocinio_Consulta_Agrupamento");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", Param.Cod_Veiculo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Inicio", Param.Inicio_Validade.ConvertToDatetime());
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Fim", Param.Fim_Validade.ConvertToDatetime());
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", Param.Cod_Programa);
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