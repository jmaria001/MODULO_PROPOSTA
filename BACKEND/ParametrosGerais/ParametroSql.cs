using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace PROPOSTA
{

    public partial class Parametro
    {
         Int32 iSequenciador { get; set; }

        public DataTable ParametroListar(Int32 pIdParametro)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Parametro_Listar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
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

        public DataTable SalvarParametro(ParametroModel pParametro)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();

            String xmlValores= null;
            if (pParametro.Valores.Count > 0)
            {
                xmlValores = clsLib.SerializeToString(pParametro.Valores);
            }
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Parametro_Salvar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Operacao", pParametro.id_operacao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Parametro", pParametro.Cod_Parametro);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Descricao", pParametro.Descricao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Chave", pParametro.Cod_Chave);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Valor_Individual", pParametro.Indica_Valor_Individual);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Valores", xmlValores);
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

        //public DataTable SalvarValor(ParametroValorModel pValor)
        //{
        //    clsConexao cnn = new clsConexao(this.Credential);
        //    cnn.Open();
        //    SqlDataAdapter Adp = new SqlDataAdapter();
        //    DataTable dtb = new DataTable("dtb");
        //    SimLib clsLib = new SimLib();
        //    try
        //    {
        //        SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Parametro_Valor_Salvar");
        //        Adp.SelectCommand = cmd;
        //        Adp.SelectCommand.Parameters.AddWithValue("@Par_Operacao", pValor.Id_Operacao);
        //        Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
        //        Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Parametro", pValor.Cod_Parametro);
        //        Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa_Faturamento", pValor.Cod_Empresa_Faturamento);
        //        Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa_Venda", pValor.Cod_Empresa_Venda);
        //        Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", pValor.Cod_Veiculo);
        //        Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Chave", pValor.Cod_Chave);
        //        Adp.Fill(dtb);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        cnn.Close();
        //    }
        //    return dtb;
        //}

        public ParametroModel GetParametroData(Int32 pCod_Parametro)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            ParametroModel Parametro = new ParametroModel();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_Proposta_Parametro_Get");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Parametro", pCod_Parametro);
                Adp.Fill(dtb);
                if (dtb.Rows.Count > 0)
                {
                    Parametro.Cod_Parametro = dtb.Rows[0]["Cod_Parametro"].ToString().ConvertToInt32();
                    Parametro.Descricao = dtb.Rows[0]["Descricao"].ToString().TrimEnd();
                    Parametro.Cod_Chave = dtb.Rows[0]["Cod_Chave"].ToString().TrimEnd();
                    Parametro.Indica_Valor_Individual = dtb.Rows[0]["Indica_Valor_Individual"].ToString().ConvertToBoolean();
                    Parametro.Valores = AddValores(pCod_Parametro);
                    Parametro.MaxSequenciador = iSequenciador;
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
            return Parametro;
        }
        public List<ParametroValorModel> AddValores(Int32 pCod_Parametro)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            List<ParametroValorModel> Retorno = new List<ParametroValorModel>();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Parametro_Valor_List");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Parametro", pCod_Parametro);
                Adp.Fill(dtb);
                foreach (DataRow drw in dtb.Rows)
                {
                    iSequenciador++;
                    Retorno.Add(new ParametroValorModel()
                    {
                        Sequenciador = iSequenciador,
                        Cod_Parametro = drw["Cod_Parametro"].ToString().ConvertToInt32(),
                        Cod_Empresa_Faturamento = drw["Cod_Empresa_Faturamento"].ToString().Trim(),
                        Nome_Empresa_Faturamento = drw["Nome_Empresa_Faturamento"].ToString().Trim(),
                        Cod_Empresa_Venda = drw["Cod_Empresa_Venda"].ToString().Trim(),
                        Nome_Empresa_Venda = drw["Nome_Empresa_Venda"].ToString().Trim(),
                        Cod_Veiculo = drw["Cod_Veiculo"].ToString().Trim(),
                        Nome_Veiculo = drw["Nome_Veiculo"].ToString().Trim(),
                        Cod_Chave = drw["Cod_Chave"].ToString().Trim(),
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
            return Retorno;
        }

    }
}
