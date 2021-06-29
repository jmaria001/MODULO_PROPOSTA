using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace PROPOSTA
{

    public partial class Apresentadores
    {
        public static string Cod_Apresentador { get; private set; }

        public DataTable ApresentadoresListar(ApresentadoresModel pCod_Apresentador)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Apresentadores_Listar");
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

        public ApresentadoresModel GetApresentadoresData(String pCod_Apresentador)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            ApresentadoresModel Apresentadores = new ApresentadoresModel();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_Proposta_Apresentadores_Get");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Apresentador", pCod_Apresentador);
                Adp.Fill(dtb);
                if (dtb.Rows.Count > 0)
                {
                    Apresentadores.Cod_Apresentador  = dtb.Rows[0]["Cod_Apresentador"].ToString();
                    Apresentadores.Nome_Apresentador = dtb.Rows[0]["Nome_Apresentador"].ToString();
                    Apresentadores.CGC               = dtb.Rows[0]["CGC"].ToString();
                    Apresentadores.Nome_Fantasia     = dtb.Rows[0]["Nome_Fantasia"].ToString();
                    Apresentadores.Razao_Social      = dtb.Rows[0]["Razao_Social"].ToString();
                    Apresentadores.Cod_UF            = dtb.Rows[0]["Cod_UF"].ToString();
                    Apresentadores.Email             = dtb.Rows[0]["Email"].ToString();
                    Apresentadores.Salario           = dtb.Rows[0]["Salario"].ToString().ConvertToDouble();

                    Apresentadores.Programas = AddProgramas(pCod_Apresentador);

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
            return Apresentadores;
        }

        private List<Programa_Model> AddProgramas(String pCod_Apresentador)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            List<Programa_Model> Programas = new List<Programa_Model>();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_Proposta_Apresentadores_Programas_Get");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Apresentador", pCod_Apresentador);
                Adp.Fill(dtb);
                foreach (DataRow drw in dtb.Rows)
                {
                    Programas.Add(new Programa_Model()
                    {
                        Cod_Programa = drw["Cod_Programa"].ToString(),
                        Titulo = drw["Titulo"].ToString(),
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
            return Programas;
        }


                        
        public DataTable SalvarApresentadores(ApresentadoresModel pApresentadores)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();

            String xmlProgramas = null;
 
            ApresentadoresModel Programas = new ApresentadoresModel();

            if (!String.IsNullOrEmpty(xmlProgramas))
            {
                Programas.Cod_Apresentador = pApresentadores.Cod_Apresentador;
            }
            else
            {
                Programas.Cod_Apresentador = pApresentadores.Cod_Apresentador;

            }
            if (!String.IsNullOrEmpty(Programas.Cod_Apresentador))
            {
               
                if (pApresentadores.Programas.Count > 0)
                {
                    xmlProgramas = clsLib.SerializeToString(pApresentadores.Programas);
                }
            }

            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Apresentadores_Salvar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Operacao", pApresentadores.Id_operacao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Apresentador", pApresentadores.Cod_Apresentador);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Nome_Apresentador", pApresentadores.Nome_Apresentador);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_CGC", pApresentadores.CGC);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Nome_Fantasia", pApresentadores.Nome_Fantasia);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Razao_Social", pApresentadores.Razao_Social);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_UF", pApresentadores.Cod_UF);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Email", pApresentadores.Email);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Salario", pApresentadores.Salario.ToString().ConvertToDouble());
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Programas", xmlProgramas);



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

        public DataTable ExcluirApresentadores(ApresentadoresModel pApresentadores)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Apresentadores_Excluir");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Apresentador", pApresentadores.Cod_Apresentador);
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
