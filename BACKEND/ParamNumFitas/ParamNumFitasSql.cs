using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace PROPOSTA
{
    public partial class ParamNumFitas
    {
        //----------------------- Conta linhas do grid
        Int32 ContadorLinha = 0;

        //----------------------- Carregar Parâmetros Numeração Fita -------------------------
        public ParamFitaModel CarregarParamFita(string pParam)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            ParamFitaModel Parametros = new ParamFitaModel();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_ParamNumFitas_Carrega");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", pParam);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Tipo_Param", "1");
                Adp.Fill(dtb);
                if (dtb.Rows.Count > 0)
                {
                    Parametros.Cod_Veiculo = dtb.Rows[0]["Cod_Veiculo"].ToString();
                    Parametros.Rg_Comerc_De = dtb.Rows[0]["Rg_Comerc_De"].ToString().ConvertToInt32();
                    Parametros.Rg_Comerc_Ate = dtb.Rows[0]["Rg_Comerc_Ate"].ToString().ConvertToInt32();
                    Parametros.Rg_Artist_De = dtb.Rows[0]["Rg_Artist_De"].ToString().ConvertToInt32();
                    Parametros.Rg_Artist_Ate = dtb.Rows[0]["Rg_Artist_Ate"].ToString().ConvertToInt32();
                    Parametros.Rg_Reserv_De = dtb.Rows[0]["Rg_Reserv_De"].ToString().ConvertToInt32();
                    Parametros.Rg_Reserv_Ate = dtb.Rows[0]["Rg_Reserv_Ate"].ToString().ConvertToInt32();
                    Parametros.Indica_Numerac_Compart = dtb.Rows[0]["Indica_Numerac_Compart"].ToString().ConvertToBoolean();
                    Parametros.Indica_Numerac_Propria = dtb.Rows[0]["Indica_Numerac_Propria"].ToString().ConvertToBoolean();
                    Parametros.Regras = AddRegras(pParam);
                    Parametros.Max_Id_Linha = ContadorLinha;
                }
                else
                {
                    Parametros.Max_Id_Linha = 1;
                    List<ParamFitaRegraModel> Regras = new List<ParamFitaRegraModel>();
                    Regras.Add(new ParamFitaRegraModel()
                    {
                        Id_Linha = 1,
                        Tipo_Midia = "",
                        Tipo_Comercial = "",
                        Num_Fita_De = 0,
                        Num_Fita_Ate = 0,
                    });
                    Parametros.Regras = Regras;
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
            return Parametros;
        }
        //----------------------- Adiciona Regra do Parametros-------------------------
        private List<ParamFitaRegraModel> AddRegras(String pCodVeiculo)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            List<ParamFitaRegraModel> Regras = new List<ParamFitaRegraModel>();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_ParamNumFitas_Carrega");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", pCodVeiculo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Tipo_Param", "2");
                Adp.Fill(dtb);
                foreach (DataRow drw in dtb.Rows)
                {
                    ContadorLinha++;
                    Regras.Add(new ParamFitaRegraModel()
                    {
                        Id_Linha = drw["Id_Linha"].ToString().ConvertToInt32(),
                        Tipo_Midia = drw["Tipo_Midia"].ToString(),
                        Tipo_Comercial = drw["Tipo_Comercial"].ToString(),
                        Num_Fita_De = drw["Num_Fita_De"].ToString().ConvertToInt32(),
                        Num_Fita_Ate = drw["Num_Fita_Ate"].ToString().ConvertToInt32(),
                    }); ;
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
            return Regras;
        }




        //---------------Salvar Parametros----------------
        public DataTable SalvaParametros(ParamFitaModel Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            String xmlRegras = null;
            xmlRegras = clsLib.SerializeToString(Param.Regras);
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_ParamNumFitas_Salva");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", Param.Cod_Veiculo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Rg_Comerc_De", Param.Rg_Comerc_De);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Rg_Comerc_Ate", Param.Rg_Comerc_Ate.ToString().ConvertToInt32());
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Rg_Artist_De", Param.Rg_Artist_De.ToString().ConvertToInt32());
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Rg_Artist_Ate", Param.Rg_Artist_Ate.ToString().ConvertToInt32());
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Rg_Reserv_De", Param.Rg_Reserv_De.ToString().ConvertToInt32());
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Rg_Reserv_Ate", Param.Rg_Reserv_Ate.ToString().ConvertToInt32());
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Numerac_Compart", Param.Indica_Numerac_Compart.ToString().ConvertToBoolean());
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Numerac_Propria", Param.Indica_Numerac_Propria.ToString().ConvertToBoolean());
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Regras", xmlRegras);
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


