using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace PROPOSTA
{

    public partial class Veiculo
    {
        public DataTable VeiculoListar(Int32 pIdVeiculo)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Veiculo_Listar");
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




        //===========================Salvar Itens de Veiculos
        public DataTable SalvarVeiculo(VeiculoModel pVeiculo)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Veiculo_Salvar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Operacao", pVeiculo.Id_Operacao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", pVeiculo.Cod_Veiculo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Nome_Veiculo", pVeiculo.Nome_Veiculo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Sigla_Veiculo", pVeiculo.Sigla_Veiculo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa", pVeiculo.Cod_Empresa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_NomeEmpresaPertence", pVeiculo.NomeEmpresaPertence);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Sigla_JOVE", pVeiculo.Sigla_JOVE);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cidade", pVeiculo.Cidade);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Hora_Inicio_Programacao", pVeiculo.Hora_Inicio_Programacao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Descritivo", pVeiculo.Descritivo);
                if (!String.IsNullOrEmpty(pVeiculo.Data_Inicio))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Inicio", pVeiculo.Data_Inicio.ConvertToDatetime());
                }
                if (!String.IsNullOrEmpty(pVeiculo.Data_Final))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Final", pVeiculo.Data_Final.ConvertToDatetime());
                }
                
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Parceiro", pVeiculo.Indica_Parceiro);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Id_Potencia", pVeiculo.Id_Potencia);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Filiada", pVeiculo.Indica_Filiada);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Afiliada", pVeiculo.Indica_Afiliada);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_NetSim", pVeiculo.NetSim);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_NetNao", pVeiculo.NetNao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo_Net", pVeiculo.Cod_Veiculo_Net);
                //Adp.SelectCommand.Parameters.AddWithValue("@Par_Nome_Veiculo_Net,", pVeiculo.Nome_Veiculo_Net);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Tabela_Net", pVeiculo.Cod_Tabela_Net);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Email_Diretoria", pVeiculo.Email_Diretoria);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Email_Faturamento", pVeiculo.Email_Faturamento);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Email_Opec", pVeiculo.Email_Opec);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Percentual_Contratual", pVeiculo.Percentual_Contratual);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Terceiro", pVeiculo.Cod_Terceiro);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Nome_Terceiro", pVeiculo.Nome_Terceiro);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_RoteiroSim", pVeiculo.Indica_RoteiroSim);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_RoteiroNao", pVeiculo.Indica_RoteiroNao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Ocultar_MMA", pVeiculo.Ocultar_MMA);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Nao_Emite_Ce", pVeiculo.Indica_Nao_Emite_Ce);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Midia_Online", pVeiculo.Indica_Midia_Online);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_RedeId", pVeiculo.RedeId);
                //Adp.SelectCommand.Parameters.AddWithValue("@Par_Nome_Rede", pVeiculo.NomeRede);

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


        //===========================Get Itens Veiculo
        public VeiculoModel GetVeiculoData(String pCodVeiculo)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            VeiculoModel Veiculo = new VeiculoModel();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Veiculo_Get");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", pCodVeiculo);
                Adp.Fill(dtb);
                if (dtb.Rows.Count > 0)
                {
                    Veiculo.Cod_Veiculo             = dtb.Rows[0]["Cod_Veiculo"].ToString();
                    Veiculo.Nome_Veiculo            = dtb.Rows[0]["Nome_Veiculo"].ToString().Trim();
                    Veiculo.Sigla_Veiculo           = dtb.Rows[0]["Sigla_Veiculo"].ToString().Trim();
                    Veiculo.Cod_Empresa             = dtb.Rows[0]["Cod_Empresa"].ToString().Trim();
                    Veiculo.NomeEmpresaPertence     = dtb.Rows[0]["NomeEmpresaPertence"].ToString().Trim();
                    Veiculo.Sigla_JOVE              = dtb.Rows[0]["Sigla_JOVE"].ToString().Trim();
                    Veiculo.Cidade                  = dtb.Rows[0]["Cidade"].ToString().Trim();
                    Veiculo.Hora_Inicio_Programacao = dtb.Rows[0]["Hora_Inicio_Programacao"].ToString().Trim();
                    Veiculo.Descritivo              = dtb.Rows[0]["Descritivo"].ToString().Trim();
                    Veiculo.Data_Inicio             = dtb.Rows[0]["Data_Inicio"].ToString().Trim();
                    Veiculo.Data_Final              = dtb.Rows[0]["Data_Final"].ToString().Trim();
                    Veiculo.Indica_Parceiro         = dtb.Rows[0]["Indica_Parceiro"].ToString().ConvertToBoolean(); 
                    Veiculo.Id_Potencia             = dtb.Rows[0]["Id_Potencia"].ToString().ConvertToInt32();
                    Veiculo.ds_Classe               = dtb.Rows[0]["ds_Classe"].ToString();
                    Veiculo.Indica_Filiada          = dtb.Rows[0]["Indica_Filiada"].ToString().ConvertToBoolean(); 
                    Veiculo.Indica_Afiliada         = dtb.Rows[0]["Indica_Afiliada"].ToString().ConvertToBoolean(); 
                    Veiculo.NetSim                  = dtb.Rows[0]["NetSim"].ToString().ConvertToBoolean(); 
                    Veiculo.NetNao                  = dtb.Rows[0]["NetNao"].ToString().ConvertToBoolean(); 
                    Veiculo.Cod_Veiculo_Net         = dtb.Rows[0]["Cod_Veiculo_Net"].ToString().Trim();
                    Veiculo.Nome_Veiculo_Net        = dtb.Rows[0]["Nome_Veiculo_Net"].ToString().Trim();
                    Veiculo.Cod_Tabela_Net          = dtb.Rows[0]["Cod_Tabela_Net"].ToString().Trim();
                    Veiculo.Email_Diretoria         = dtb.Rows[0]["Email_Diretoria"].ToString().Trim();
                    Veiculo.Email_Faturamento       = dtb.Rows[0]["Email_Faturamento"].ToString().Trim();
                    Veiculo.Email_Opec              = dtb.Rows[0]["Email_Opec"].ToString().Trim();
                    Veiculo.Percentual_Contratual   = dtb.Rows[0]["Percentual_Contratual"].ToString().Trim();
                    Veiculo.Cod_Terceiro            = dtb.Rows[0]["Cod_Terceiro"].ToString().Trim();
                    Veiculo.Nome_Terceiro           = dtb.Rows[0]["Nome_Terceiro"].ToString().Trim();
                    Veiculo.Indica_RoteiroSim       = dtb.Rows[0]["Indica_RoteiroSim"].ToString().ConvertToBoolean(); 
                    Veiculo.Indica_RoteiroNao       = dtb.Rows[0]["Indica_RoteiroNao"].ToString().ConvertToBoolean(); 
                    Veiculo.Ocultar_MMA             = dtb.Rows[0]["Ocultar_MMA"].ToString().ConvertToBoolean(); ;
                    Veiculo.Indica_Nao_Emite_Ce     = dtb.Rows[0]["Indica_Nao_Emite_Ce"].ToString().ConvertToBoolean();
                    Veiculo.Indica_Midia_Online     = dtb.Rows[0]["Indica_Midia_Online"].ToString().ConvertToBoolean();
                    Veiculo.RedeId                  = dtb.Rows[0]["RedeId"].ToString().ConvertToInt32(); 
                    Veiculo.NomeRede                = dtb.Rows[0]["NomeRede"].ToString();
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
            return Veiculo;
        }



        //===========================Excluir Veiculo
        public DataTable ExcluirVeiculo(VeiculoModel pVeiculo)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Veiculo_Excluir");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", pVeiculo.Cod_Veiculo);
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
