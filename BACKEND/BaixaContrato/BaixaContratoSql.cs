using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace PROPOSTA
{

    public partial class BaixaContrato
    {
        //===========================Item de GetContratoBaixa
        public BaixaContratoModel GetContratoBaixa(FiltroModel pFiltro)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            BaixaContratoModel BaixaContrato = new BaixaContratoModel();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Baixa_Contrato_Get_Contrato");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa", pFiltro.Cod_Empresa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Mr", pFiltro.Numero_Mr);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Sequencia_Mr", pFiltro.Sequencia_Mr);
                Adp.Fill(dtb);
                if (dtb.Rows.Count > 0)
                {
                    BaixaContrato.Cod_Empresa = dtb.Rows[0]["Cod_Empresa"].ToString();
                    BaixaContrato.Numero_Mr = dtb.Rows[0]["Numero_Mr"].ToString().ConvertToInt32();
                    BaixaContrato.Sequencia_Mr = dtb.Rows[0]["Sequencia_Mr"].ToString().ConvertToInt32();
                    BaixaContrato.Cod_Agencia = dtb.Rows[0]["Cod_Agencia"].ToString();
                    BaixaContrato.Nome_Agencia = dtb.Rows[0]["Nome_Agencia"].ToString();
                    BaixaContrato.Cod_Cliente = dtb.Rows[0]["Cod_Cliente"].ToString();
                    BaixaContrato.Nome_Cliente = dtb.Rows[0]["Nome_Cliente"].ToString();
                    BaixaContrato.Cod_Qualidade_Cancelamento= dtb.Rows[0]["Cod_Qualidade_Cancelamento"].ToString();
                    BaixaContrato.Descricao_Qualidade_Cancelamento = dtb.Rows[0]["Descricao_Qualidade_Cancelamento"].ToString();
                    BaixaContrato.Data_Inicial = dtb.Rows[0]["Data_Inicial"].ToString().ConvertToDatetime().ToString("dd/MM/yyyy");
                    BaixaContrato.Data_Final= dtb.Rows[0]["Data_Final"].ToString().ConvertToDatetime().ToString("dd/MM/yyyy");
                    BaixaContrato.Domingo = true;
                    BaixaContrato.Segunda = true;
                    BaixaContrato.Terca = true;
                    BaixaContrato.Quarta = true;
                    BaixaContrato.Quinta = true;
                    BaixaContrato.Sexta = true;
                    BaixaContrato.Sabado = true;
                    BaixaContrato.Data_Help = "";
                    BaixaContrato.Indica_Cancelamento = false;
                    BaixaContrato.Indica_Cancelar_Am = false;
                    BaixaContrato.Motivo_Cancelamento = "";
                    BaixaContrato.Veiculos = AddVeiculos(pFiltro);
                    BaixaContrato.Tipo_Operacao = "";
                    BaixaContrato.DiaSemana = "";
                    BaixaContrato.Loaded = true;
                }
                else
                {
                    BaixaContrato.Loaded = false;
                    BaixaContrato.Veiculos = new List<BaixaContratoVeiculoModel>();
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
            return BaixaContrato;
        }
        //===========================Item de GetContratoBaixa
        public List<BaixaContratoVeiculoModel> AddVeiculos(FiltroModel pFiltro)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            List<BaixaContratoVeiculoModel> Veiculos = new List<BaixaContratoVeiculoModel>();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "SP_Baixa_Contrato_Veiculo");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa", pFiltro.Cod_Empresa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Mr", pFiltro.Numero_Mr);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Sequencia_Mr", pFiltro.Sequencia_Mr);
                Adp.Fill(dtb);
                foreach (DataRow drw in dtb.Rows)
                {
                    Veiculos.Add(new BaixaContratoVeiculoModel()
                    {
                        Cod_Veiculo = drw["Cod_Veiculo"].ToString(),
                        Nome_Veiculo = drw["Nome_Veiculo"].ToString(),
                        Selected = true
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
            return Veiculos;
        }
        public DataTable SalvarContratoBaixa(BaixaContratoModel pContrato)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            String xmlVeiculos = null;
            if (pContrato.Veiculos.Count > 0)
            {
                xmlVeiculos = clsLib.SerializeToString(pContrato.Veiculos);
            }

            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Processa_Baixa_Contrato");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Id_Operacao", pContrato.Id_Operacao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Veiculos", xmlVeiculos);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa", pContrato.Cod_Empresa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Mr", pContrato.Numero_Mr);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Sequencia_Mr", pContrato.Sequencia_Mr);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Inicial", pContrato.Data_Inicial.ConvertToDatetime());

                if (!String.IsNullOrEmpty(pContrato.Data_Final))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Final", pContrato.Data_Final.ConvertToDatetime());
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Final", DBNull.Value);
                }

                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Qualidade", pContrato.Cod_Qualidade);

                if (!String.IsNullOrEmpty(pContrato.Cod_Veiculo))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", pContrato.Cod_Veiculo);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", DBNull.Value);
                }

                if (!String.IsNullOrEmpty(pContrato.Cod_Programa))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", pContrato.Cod_Programa);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", DBNull.Value);
                }

                if (!String.IsNullOrEmpty(pContrato.Cod_Comercial))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Comercial", pContrato.Cod_Comercial);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Comercial", DBNull.Value);
                }

                Adp.SelectCommand.Parameters.AddWithValue("@Par_Dia_Semana", pContrato.DiaSemana);

                if (!String.IsNullOrEmpty(pContrato.Data_Help))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Help", pContrato.Data_Help.ConvertToDatetime());
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Help", DBNull.Value);
                }

                if (pContrato.Tipo_Operacao.ToUpper()=="CANCELAMENTO")
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Cancelamento", true);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Cancelamento", true);
                }
                
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Cancelar_Am", pContrato.Indica_Cancelar_Am).ToString().ConvertToBoolean();

                if (!String.IsNullOrEmpty(pContrato.Motivo_Cancelamento))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Motivo_Cancelamento", pContrato.Motivo_Cancelamento);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Motivo_Cancelamento", DBNull.Value);
                }

                Adp.SelectCommand.Parameters.AddWithValue("@Par_Tipo_Operacao", pContrato.Tipo_Operacao);
                Adp.Fill(dtb);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cnn.Close();
            }
            return dtb;
        }
        //===========================Buscar Programas do Contrato
        public DataTable GetProgramaContrato(FiltroModel pFiltro)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            BaixaContratoModel BaixaContrato = new BaixaContratoModel();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Baixa_Contrato_Get_Programa");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa", pFiltro.Cod_Empresa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Mr", pFiltro.Numero_Mr);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Sequencia_Mr", pFiltro.Sequencia_Mr);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", pFiltro.Cod_Programa);
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
        //===========================Buscar Comercials do Contrato
        public DataTable GetComercialContrato(FiltroModel pFiltro)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            BaixaContratoModel BaixaContrato = new BaixaContratoModel();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Baixa_Contrato_Get_Comercial");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa", pFiltro.Cod_Empresa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Numero_Mr", pFiltro.Numero_Mr);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Sequencia_Mr", pFiltro.Sequencia_Mr);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Comercial", pFiltro.Cod_Comercial);
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