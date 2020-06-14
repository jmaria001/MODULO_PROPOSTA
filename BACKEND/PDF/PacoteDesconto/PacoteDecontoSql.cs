using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace PROPOSTA
{

    public partial class Pacote
    {
        public DataTable PacoteListar(Int32 pIdPacote)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Pacote_Listar");
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
        public DataTable GetOpcoesDesconto(Int32 pTipoDesconto)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Opcoes_Desconto");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@pTipoDesconto", pTipoDesconto);
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
        public DataTable SalvarPacote(PacoteModel pPacote)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            String xmlDescontoDetalhe= null;
            if (pPacote.DescontoDetalhe.Count > 0)
            {
                xmlDescontoDetalhe = clsLib.SerializeToString(pPacote.DescontoDetalhe);
            }

            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Pacote_Salvar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Id_Pacote", pPacote.Id_Pacote);
                if (!String.IsNullOrEmpty(pPacote.Validade_Inicio))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Validade_Inicio", pPacote.Validade_Inicio.ConvertToDatetime());
                }
                if (!String.IsNullOrEmpty(pPacote.Validade_Termino))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Validade_Termino", pPacote.Validade_Termino.ConvertToDatetime());
                }

                Adp.SelectCommand.Parameters.AddWithValue("@Par_Descricao", pPacote.Descricao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_DescontoDetalhe", xmlDescontoDetalhe);
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

        public DataTable ExcluirPacote(PacoteModel pPacote)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Pacote_Excluir");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Id_Pacote", pPacote.Id_Pacote);
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

        public PacoteModel GetPacoteData(Int32 pId_Pacote)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            PacoteModel Pacote = new PacoteModel();
            List<Desconto_DetalheModel> Detalhe = new List<Desconto_DetalheModel>();
            Int32 ContadorDetalhe = 0;
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_Proposta_Pacote_Get");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Id_Pacote", pId_Pacote);
                Adp.Fill(dtb);
                if (dtb.Rows.Count > 0)
                {
                    Pacote.Id_Pacote = dtb.Rows[0]["Id_Pacote"].ToString().ConvertToInt32();
                    Pacote.Descricao= dtb.Rows[0]["Descricao"].ToString();
                    if (String.IsNullOrEmpty(dtb.Rows[0]["Validade_Inicio"].ToString()))
                    {
                        Pacote.Validade_Inicio = "";
                    }
                    else
                    {
                        Pacote.Validade_Inicio = dtb.Rows[0]["Validade_Inicio"].ToString().ConvertToDatetime().ToString("dd/MM/yyyy");
                    }
                    if (String.IsNullOrEmpty(dtb.Rows[0]["Validade_Termino"].ToString()))
                    {
                        Pacote.Validade_Termino = "";
                    }
                    else
                    {
                        Pacote.Validade_Termino = dtb.Rows[0]["Validade_Termino"].ToString().ConvertToDatetime().ToString("dd/MM/yyyy");
                    }

                    DataTable dtbDetalhe = new DataTable();
                    SqlCommand cmdDetalhe = cnn.Procedure(cnn.Connection, "PR_Proposta_Pacote_Detalhe_Get");
                    Adp.SelectCommand = cmdDetalhe;
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Id_Pacote", pId_Pacote);
                    Adp.Fill(dtbDetalhe);

                    foreach (DataRow drw in dtbDetalhe.Rows)
                    {
                        ContadorDetalhe++;
                        Detalhe.Add(new Desconto_DetalheModel()
                        {
                            Id_Pacote_Detalhe = ContadorDetalhe,
                            Cod_Desconto = drw["Cod_Desconto"].ToString().ConvertToInt32(),
                            Descricao = drw["Descricao"].ToString(),
                            Conteudo = drw["Conteudo"].ToString(),
                            Data_Inicio=drw["Data_Inicio"].ToString().ConvertToDatetime(),
                            Data_Termino = drw["Data_Termino"].ToString().ConvertToDatetime(),
                            Chave= drw["Chave"].ToString(),
                            Desconto= drw["Desconto"].ToString()
                        });
                    }
                    Pacote.Max_Id_Desconto = ContadorDetalhe;
                    Pacote.DescontoDetalhe = Detalhe;
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
            return Pacote;
        }
    }
}
