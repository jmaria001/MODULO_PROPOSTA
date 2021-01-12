using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace PROPOSTA
{
    public partial class ParamRoteiro
    {

        //===========================Filtrar Dados
        public List<ParamRoteiroModel> ParamRoteiroFiltrar(ParamRoteiroModel Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            List<ParamRoteiroModel> ParamRoteiros = new List<ParamRoteiroModel>();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_ParamRoteiro_Filtrar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", Param.Cod_Veiculo);
                Adp.Fill(dtb);
                foreach (DataRow drw in dtb.Rows)
                {
                    ParamRoteiros.Add(new ParamRoteiroModel()
                    {
                        Cod_Veiculo = Param.Cod_Veiculo,
                        Intervalo = drw["Intervalo"].ToString(),
                        Descricao  = drw["Descricao"].ToString(),
                        Origem_Break= drw["Origem_Break"].ToString(),
                        Origem_Roteiro =drw["Origem_Roteiro"].ToString(),
                        Permite_Ordenacao =drw["Permite_Ordenacao"].ToString().ConvertToBoolean()
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
            return ParamRoteiros;
        }


        //===========================Salvar Dados
        public DataTable ParamRoteiroSalvar(List<ParamRoteiroModel> pParamRoteiro)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            String xmlParametro= clsLib.SerializeToString(pParamRoteiro);
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_ParamRoteiro_Salvar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Parametro", xmlParametro);  //--contém todos os campos
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


