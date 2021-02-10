using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace PROPOSTA
{

    public partial class DepositoFitas
    {
        //===========================Listar Fitas Avulsos
        public DataTable DepositoFitasListar(FiltroModel pFiltro)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Dep_Avulso_Artistico_Listar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", pFiltro.Cod_Veiculo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Situacao", pFiltro.Situacao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Fita_Inicio", pFiltro.Numero_Fita_Inicio);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Fita_Fim", pFiltro.Numero_Fita_Fim);
                if (String.IsNullOrEmpty(pFiltro.Data_Inicio))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Inicio", DBNull.Value);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Inicio", pFiltro.Data_Inicio.ConvertToDatetime());
                }
                if (String.IsNullOrEmpty(pFiltro.Data_Final))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Final", DBNull.Value);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Final", pFiltro.Data_Final.ConvertToDatetime());
                }

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

        public DepositoFitasModel GetDepositorioFitasData(String pNumero_Fita)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            DepositoFitasModel  DepositorioFitas = new DepositoFitasModel();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_Proposta_DepositorioFitas_Get");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Fita", pNumero_Fita);

                Adp.Fill(dtb);
                if (dtb.Rows.Count > 0)
                {
                    DepositorioFitas.Tipo_Fita             = dtb.Rows[0]["Tipo_Fita"].ToString();
                    DepositorioFitas.Data_Inicio           = dtb.Rows[0]["Data_Inicio"].ToString();
                    DepositorioFitas.Data_Final            = dtb.Rows[0]["Data_Final"].ToString();
                    DepositorioFitas.Quantidade            = dtb.Rows[0]["Quantidade"].ToString().ConvertToInt32();
                    DepositorioFitas.Duracao               = dtb.Rows[0]["Duracao"].ToString().ConvertToInt32();
                    DepositorioFitas.Titulo_Comercial      = dtb.Rows[0]["Titulo_Comercial"].ToString();
                    DepositorioFitas.Indica_Chamada        = dtb.Rows[0]["Indica_Chamada"].ToString().ConvertToBoolean();
                    DepositorioFitas.Cod_Tipo_Comercial    = dtb.Rows[0]["Cod_Tipo_Comercial"].ToString();
                    DepositorioFitas.Descricao_Comercial   = dtb.Rows[0]["Descricao_Comercial"].ToString();
                    DepositorioFitas.Cod_Veiculo           = dtb.Rows[0]["Cod_Veiculo"].ToString();
                    DepositorioFitas.Nome_Veiculo          = dtb.Rows[0]["Nome_Veiculo"].ToString();
                    DepositorioFitas.Cod_Programa          = dtb.Rows[0]["Cod_Programa"].ToString();
                    DepositorioFitas.Titulo_Programa       = dtb.Rows[0]["Titulo_Programa"].ToString();
                    DepositorioFitas.Cod_Programa_Antes    = dtb.Rows[0]["Cod_Programa_Antes"].ToString();
                    DepositorioFitas.Titulo_Programa_Antes = dtb.Rows[0]["Titulo_Programa_Antes"].ToString();
                    DepositorioFitas.Cod_Programa_Apos     = dtb.Rows[0]["Cod_Programa_Apos"].ToString();
                    DepositorioFitas.Titulo_Programa_Apos  = dtb.Rows[0]["Titulo_Programa_Apos"].ToString();
                    DepositorioFitas.Cod_Red_Produto       = dtb.Rows[0]["Cod_Red_Produto"].ToString().ConvertToInt32();
                    DepositorioFitas.Descricao_Produto     = dtb.Rows[0]["Descricao_Produto"].ToString();
                    DepositorioFitas.Cod_Apresentador      = dtb.Rows[0]["Cod_Apresentador"].ToString();
                    DepositorioFitas.Nome_Apresentador     = dtb.Rows[0]["Nome_Apresentador"].ToString();
                    DepositorioFitas.Arquivo_Midia         = dtb.Rows[0]["Arquivo_Midia"].ToString();
                    DepositorioFitas.Numero_Fita           = dtb.Rows[0]["Numero_Fita"].ToString();
                    DepositorioFitas.Indica_DiaDom         = dtb.Rows[0]["Indica_DiaDom"].ToString().ConvertToBoolean();
                    DepositorioFitas.Indica_DiaSeg         = dtb.Rows[0]["Indica_DiaSeg"].ToString().ConvertToBoolean();
                    DepositorioFitas.Indica_DiaTer         = dtb.Rows[0]["Indica_DiaTer"].ToString().ConvertToBoolean();
                    DepositorioFitas.Indica_DiaQua         = dtb.Rows[0]["Indica_DiaQua"].ToString().ConvertToBoolean();
                    DepositorioFitas.Indica_DiaQui         = dtb.Rows[0]["Indica_DiaQui"].ToString().ConvertToBoolean();
                    DepositorioFitas.Indica_DiaSex         = dtb.Rows[0]["Indica_DiaSex"].ToString().ConvertToBoolean();
                    DepositorioFitas.Indica_DiaSab         = dtb.Rows[0]["Indica_DiaSab"].ToString().ConvertToBoolean();
                    //DepositorioFitas.Veiculos = AddVeiculos(pNumero_Fita);
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
            return DepositorioFitas;
        }

         // Definindo range fita

        public DataTable RangeFita(DepositoFitas.DepositoFitasModel Param)
        {
            DataTable dtb = new DataTable();
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Range_Fita");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", Param.Cod_Veiculo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Tipo_Fita", Param.Tipo_Fita);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Tipo_Midia", Param.Tipo_Midia);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Tipo_Comercial", Param.Cod_Tipo_Comercial);
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

        public DataTable SalvarDepositorioFitas(DepositoFitasModel pDepositorioFitas)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();

            //String xmlVeiculos = null;

            DepositoFitasModel DepositorioFitas= new DepositoFitasModel();

            //DepositorioFitas.Cod_Veiculo = pDepositorioFitas.Cod_Veiculo;


            //if (!String.IsNullOrEmpty(DepositorioFitas.Cod_Veiculo))
            //{
            //    if (pDepositorioFitas.Veiculos.Count > 0)
            //    {
            //        xmlVeiculos = clsLib.SerializeToString(pDepositorioFitas.Veiculos);
            //    }
            //}
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_DepositorioFitas_Salvar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Operacao", pDepositorioFitas.Id_operacao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Tipo_Fita", pDepositorioFitas.Tipo_Fita);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Inicio", pDepositorioFitas.Data_Inicio.ConvertToDatetime());
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Final", pDepositorioFitas.Data_Final.ConvertToDatetime());
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Quantidade", pDepositorioFitas.Quantidade);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Duracao", pDepositorioFitas.Duracao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Titulo_Comercial", pDepositorioFitas.Titulo_Comercial);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Chamada", pDepositorioFitas.Indica_Chamada);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Tipo_Comercial", pDepositorioFitas.Cod_Tipo_Comercial);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", pDepositorioFitas.Cod_Veiculo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", pDepositorioFitas.Cod_Programa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa_Ar", pDepositorioFitas.Cod_Programa_Antes);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa_Ar_Apos", pDepositorioFitas.Cod_Programa_Apos);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Red_Produto", pDepositorioFitas.Cod_Red_Produto);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Apresentador", pDepositorioFitas.Cod_Apresentador);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Localizacao", pDepositorioFitas.Arquivo_Midia);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Fita", pDepositorioFitas.Numero_Fita);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_DiaSeg", pDepositorioFitas.Indica_DiaSeg);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_DiaTer", pDepositorioFitas.Indica_DiaTer);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_DiaQua", pDepositorioFitas.Indica_DiaQua);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_DiaQui", pDepositorioFitas.Indica_DiaQui);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_DiaSex", pDepositorioFitas.Indica_DiaSex);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_DiaSab", pDepositorioFitas.Indica_DiaSab);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_DiaDom", pDepositorioFitas.Indica_DiaDom);
                //Adp.SelectCommand.Parameters.AddWithValue("@Par_Veiculos", xmlVeiculos);

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


        public DataTable ExcluirDepositorioFitas(DepositoFitasModel pDepositorioFitas)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_DepositorioFitas_Excluir");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Fita", pDepositorioFitas.Numero_Fita);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Tipo_Fita", pDepositorioFitas.Tipo_Fita);
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




        //Definindo Veiculo
        //public  List<Veiculos_Model> AddVeiculos(String pNumero_Fita)
        //{
        //    clsConexao cnn = new clsConexao(this.Credential);
        //    cnn.Open();
        //    SqlDataAdapter Adp = new SqlDataAdapter();
        //    DataTable dtb = new DataTable("dtb");
        //    SimLib clsLib = new SimLib();
        //    List<Veiculos_Model> Veiculos = new List<Veiculos_Model>();
        //    try
        //    {
        //        SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_DepositorioFitas_Veiculo_Listar");
        //        Adp.SelectCommand = cmd;
        //        Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
        //        Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Fita", pNumero_Fita);
        //        Adp.Fill(dtb);
        //        foreach (DataRow drw in dtb.Rows)
        //        {
        //            Veiculos.Add(new Veiculos_Model()
        //            {
        //                Cod_Veiculo = drw["Cod_Veiculo"].ToString(),
        //                Nome_Veiculo = drw["Nome_Veiculo"].ToString(),

        //            });
        //        }

        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        cnn.Close();
        //    }
        //    return Veiculos;
        //}



    }
}