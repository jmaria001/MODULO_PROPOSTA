using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace PROPOSTA
{

    public partial class BaixaRoteiro
    {

        //===========================Item de GetRoteiroBaixa
        public BaixaRoteiroModel GetRoteiroBaixa()
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            BaixaRoteiroModel BaixaRoteiro = new BaixaRoteiroModel();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "SP_Baixa_Roteiro_Veiculo");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.Fill(dtb);
                BaixaRoteiro.Data_Inicial = "";
                BaixaRoteiro.Data_Final = "";
                BaixaRoteiro.Cod_Qualidade = "";
                BaixaRoteiro.Cod_Programa = "";
                BaixaRoteiro.Cod_Tipo_Comercial = "";
                BaixaRoteiro.Domingo = true;
                BaixaRoteiro.Segunda = true;
                BaixaRoteiro.Terca = true;
                BaixaRoteiro.Quarta = true;
                BaixaRoteiro.Quinta = true;
                BaixaRoteiro.Sexta = true;
                BaixaRoteiro.Sabado = true;
                BaixaRoteiro.Veiculos = AddVeiculos();
                BaixaRoteiro.DiaSemana = "";
                BaixaRoteiro.Merchandising = 0;
                BaixaRoteiro.Indica_Convidado = 0;
                BaixaRoteiro.Indica_Iem = 1;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cnn.Close();
            }
            return BaixaRoteiro;
        }
        //===========================Item de GetRoteiroBaixa
        public List<BaixaRoteiroVeiculoModel> AddVeiculos()
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            List<BaixaRoteiroVeiculoModel> Veiculos = new List<BaixaRoteiroVeiculoModel>();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "SP_Baixa_Roteiro_Veiculo");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.Fill(dtb);
                foreach (DataRow drw in dtb.Rows)
                {
                    Veiculos.Add(new BaixaRoteiroVeiculoModel()
                    {
                        Cod_Veiculo = drw["Cod_Veiculo"].ToString(),
                        Nome_Veiculo = drw["Nome_Veiculo"].ToString(),
                        Selected = false
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
        public DataTable SalvarRoteiroBaixa(BaixaRoteiroModel pRoteiro)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            String xmlVeiculos = null;
            if (pRoteiro.Veiculos.Count > 0)
            {
                xmlVeiculos = clsLib.SerializeToString(pRoteiro.Veiculos);
            }

            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Processa_Baixa_Roteiro");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Id_Operacao", pRoteiro.Id_Operacao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Veiculos", xmlVeiculos);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Inicial", pRoteiro.Data_Inicial.ConvertToDatetime());

                if (!String.IsNullOrEmpty(pRoteiro.Data_Final))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Final", pRoteiro.Data_Final.ConvertToDatetime());
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Final", DBNull.Value);
                }

                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Qualidade", pRoteiro.Cod_Qualidade);
                
                if (!String.IsNullOrEmpty(pRoteiro.Cod_Programa))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", pRoteiro.Cod_Programa);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", DBNull.Value);
                }

                if (!String.IsNullOrEmpty(pRoteiro.Cod_Tipo_Comercial))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Tipo_Comercial", pRoteiro.Cod_Tipo_Comercial);
                }
                else
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Tipo_Comercial", DBNull.Value);
                }

                Adp.SelectCommand.Parameters.AddWithValue("@Par_Dia_Semana", pRoteiro.DiaSemana);

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