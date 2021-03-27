using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace PROPOSTA
{

    public partial class DeParaProgramacao
    {
        public List<VeiculoModel> AddVeiculos()
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            List<VeiculoModel> Veiculos = new List<VeiculoModel>();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Listar_Tabela");
                cmd.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                cmd.Parameters.AddWithValue("@Par_Tabela", "Veiculo");
                Adp.SelectCommand = cmd;
                Adp.Fill(dtb);
                foreach (DataRow drw in dtb.Rows)
                {
                    Veiculos.Add(new VeiculoModel()
                    {
                        Cod_Veiculo = drw["Codigo"].ToString(),
                        Nome_Veiculo = drw["Descricao"].ToString(),
                        Selected = false,
                    });
                };
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cnn.Close();
            }
            return Veiculos;
        }
        public DataTable ProcessaDeParaPeriodo(DeParaPeriodoModel Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            List<VeiculoModel> Veiculos = new List<VeiculoModel>();
            String xmlVeiculos = null;
            if (Param.Veiculos.Count > 0)
            {
                xmlVeiculos = clsLib.SerializeToString(Param.Veiculos);
            }
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_APC_Periodo");
                cmd.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                cmd.Parameters.AddWithValue("@Par_Data_Inicio", Param.Data_Inicio.ConvertToDatetime());
                cmd.Parameters.AddWithValue("@Par_Data_Fim", Param.Data_Termino.ConvertToDatetime());
                cmd.Parameters.AddWithValue("@Par_Dom", Param.Dom);
                cmd.Parameters.AddWithValue("@Par_Seg", Param.Seg);
                cmd.Parameters.AddWithValue("@Par_Ter", Param.Ter);
                cmd.Parameters.AddWithValue("@Par_Qua", Param.Qua);
                cmd.Parameters.AddWithValue("@Par_Qui", Param.Qui);
                cmd.Parameters.AddWithValue("@Par_Sex", Param.Sex);
                cmd.Parameters.AddWithValue("@Par_Sab", Param.Sab);
                cmd.Parameters.AddWithValue("@Par_Programa_De", Param.Cod_Programa_De);
                cmd.Parameters.AddWithValue("@Par_Programa_Para", Param.Cod_Programa_Para);
                cmd.Parameters.AddWithValue("@Par_Veiculos", xmlVeiculos);
                Adp.SelectCommand = cmd;
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
        public DataTable ProcessaDeParaData(DeParaDataModel Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            List<VeiculoModel> Veiculos = new List<VeiculoModel>();
            String xmlVeiculos = null;
            if (Param.Veiculos.Count > 0)
            {
                xmlVeiculos = clsLib.SerializeToString(Param.Veiculos);
            }
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_APC_Data");
                cmd.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                cmd.Parameters.AddWithValue("@Par_Data_De", Param.Data_De.ConvertToDatetime());
                cmd.Parameters.AddWithValue("@Par_Data_Para", Param.Data_Para.ConvertToDatetime());
                cmd.Parameters.AddWithValue("@Par_Programa_De", Param.Cod_Programa_De);
                cmd.Parameters.AddWithValue("@Par_Programa_Para", Param.Cod_Programa_Para);
                cmd.Parameters.AddWithValue("@Par_Veiculos", xmlVeiculos);
                Adp.SelectCommand = cmd;
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