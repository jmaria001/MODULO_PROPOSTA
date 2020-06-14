using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace PROPOSTA
{

    public partial class Programa
    {
        //private readonly string pCod_Veiculo;

        public static string Cod_Programa { get; private set; }
        

        public DataTable ProgramaListar(Int32 pId_Rede)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Programa_Listar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Id_Rede", pId_Rede);
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

        public ProgramaModel GetProgramaData(String pCod_Programa)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            ProgramaModel Programa = new ProgramaModel();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection,"PR_Proposta_Programa_Get");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", pCod_Programa);

                Adp.Fill(dtb);
                if (dtb.Rows.Count > 0)
                {
                    Programa.RedeId             = dtb.Rows[0]["RedeID"].ToString().ConvertToInt32();
                    Programa.NomeRede           = dtb.Rows[0]["NomeRede"].ToString();
                    Programa.Cod_Programa       = dtb.Rows[0]["Cod_Programa"].ToString();
                    Programa.Titulo             = dtb.Rows[0]["Titulo"].ToString();
                    Programa.Sub_Titulo         = dtb.Rows[0]["Sub_Titulo"].ToString();
                    Programa.Cod_Genero         = dtb.Rows[0]["Cod_Genero"].ToString();
                    Programa.Genero             = dtb.Rows[0]["Descricao"].ToString();
                    Programa.Indica_evento      = dtb.Rows[0]["Indica_evento"].ToString().ConvertToBoolean();
                    Programa.Indica_Rotativo    = dtb.Rows[0]["Indica_Rotativo"].ToString().ConvertToBoolean();
                    Programa.Indica_Local       = dtb.Rows[0]["Indica_Local"].ToString().ConvertToBoolean();
                    Programa.Indica_Desativado  = dtb.Rows[0]["Indica_Desativado"].ToString().ConvertToBoolean();
                    Programa.Indica_Programet   = dtb.Rows[0]["Indica_Programet"].ToString().ConvertToBoolean();
                    Programa.Indica_Boletim     = dtb.Rows[0]["Indica_Boletim"].ToString().ConvertToBoolean();
                    Programa.Indica_Internet    = dtb.Rows[0]["Indica_Internet"].ToString().ConvertToBoolean();
                    Programa.Indica_Faixa       = dtb.Rows[0]["Indica_Faixa"].ToString().ConvertToBoolean();
                    Programa.Cod_A_JOVE         = dtb.Rows[0]["Cod_A_JOVE"].ToString();
                    Programa.Cod_N_JOVE         = dtb.Rows[0]["Cod_N_JOVE"].ToString().ConvertToInt32();
                    Programa.Indica_Patrocinio  = dtb.Rows[0]["Indica_Patrocinio"].ToString().ConvertToBoolean();
                    Programa.Qtd_Cotas          = dtb.Rows[0]["Qtd_Cotas"].ToString().ConvertToInt32();
                    Programa.Horario_Exibicao   = dtb.Rows[0]["Horario_Exibicao"].ToString();
                    Programa.DiaSeg             = dtb.Rows[0]["DiaSeg"].ToString().ConvertToBoolean();
                    Programa.DiaTer             = dtb.Rows[0]["DiaTer"].ToString().ConvertToBoolean();
                    Programa.DiaQua             = dtb.Rows[0]["DiaQua"].ToString().ConvertToBoolean();
                    Programa.DiaQui             = dtb.Rows[0]["DiaQui"].ToString().ConvertToBoolean();
                    Programa.DiaSex             = dtb.Rows[0]["DiaSex"].ToString().ConvertToBoolean();
                    Programa.DiaSab             = dtb.Rows[0]["DiaSab"].ToString().ConvertToBoolean();
                    Programa.DiaDom             = dtb.Rows[0]["DiaDom"].ToString().ConvertToBoolean();
                    Programa.Sinopse            = dtb.Rows[0]["Sinopse"].ToString();
                    Programa.Apresentadores = AddApresentador(pCod_Programa);
                    Programa.Veiculos = AddVeiculos(pCod_Programa);

          
                }            }
            
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cnn.Close();
            }
            return Programa;
        }

      //  Int32 ContadorApresentador = 0;
        public List<Apresentador_Model> ApresentadorListar()
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            List<Apresentador_Model> Apresentador = new List<Apresentador_Model>();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Programa_Apresentador_Listar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.Fill(dtb);
                foreach (DataRow drw in dtb.Rows)
                {
                    Apresentador.Add(new Apresentador_Model()
                    {
                        Id_Apresentador = drw["Id_Apresentador"].ToString().ConvertToInt32(),
                        Nome_Apresentador = drw["Nome_Apresentador"].ToString(),
                        Cod_Programa = drw["Cod_Programa"].ToString(),
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
            return Apresentador;
        }


        private List<Apresentador_Model> AddApresentador(String pCod_Programa)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            List<Apresentador_Model> Apresentadores = new List<Apresentador_Model>();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_Proposta_Programa_Apresentadores_Get");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", pCod_Programa);
                Adp.Fill(dtb);
                foreach (DataRow drw in dtb.Rows)
                {
                    Apresentadores.Add(new Apresentador_Model()
                    {
                        Id_Apresentador = drw["Id_Apresentador"].ToString().ConvertToInt32(),
                        Nome_Apresentador = drw["Nome_Apresentador"].ToString(),
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
            return Apresentadores;
        }


        //Definindo Veiculo
        public List<Veiculos_Model> VeiculosListar()
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            List<Veiculos_Model> Veiculos = new List<Veiculos_Model>();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Programa_Veiculo_Listar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.Fill(dtb);
                foreach (DataRow drw in dtb.Rows)
                {
                    Veiculos.Add(new Veiculos_Model()
                    {
                        Cod_Veiculo = drw["Cod_Veiculo"].ToString(),
                        Nome_Veiculo = drw["Nome_Veiculo"].ToString(),
                       
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
        public DataTable VeiculosMostrar(String pCod_Programa)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Programa_Veiculo_Mostrar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", pCod_Programa    );
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

        private List<Veiculos_Model> AddVeiculos(String pCod_Programa)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            List<Veiculos_Model> Veiculos = new List<Veiculos_Model>();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_Proposta_Programa_Veiculo_Get");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", pCod_Programa);
                Adp.Fill(dtb);
                foreach (DataRow drw in dtb.Rows)
                {
                    Veiculos.Add(new Veiculos_Model()
                    {
                        Cod_Veiculo = drw["Cod_Veiculo"].ToString(),
                        Nome_Veiculo = drw["Nome_Veiculo"].ToString(),
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

        //Fim 



        public DataTable SalvarPrograma(ProgramaModel pPrograma)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();

            String xmlApresentadores = null;
            String xmlVeiculos = null;

            ProgramaModel Programa = new ProgramaModel();

            Programa.Cod_Programa = pPrograma.Cod_Programa;

            if (!String.IsNullOrEmpty(Programa.Cod_Programa))
            {
                if (pPrograma.Apresentadores.Count > 0)
                {
                    xmlApresentadores = clsLib.SerializeToString(pPrograma.Apresentadores);
                }
            }


            if (!String.IsNullOrEmpty(Programa.Cod_Programa))
            {
                if (pPrograma.Veiculos.Count > 0)
                {
                    xmlVeiculos = clsLib.SerializeToString(pPrograma.Veiculos);
                }
            }
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Programa_Salvar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Operacao", pPrograma.Id_operacao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_RedeId", pPrograma.RedeId);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", pPrograma.Cod_Programa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Titulo", pPrograma.Titulo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Sub_Titulo", pPrograma.Sub_Titulo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Genero", pPrograma.Cod_Genero);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_evento", pPrograma.Indica_evento);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Rotativo", pPrograma.Indica_Rotativo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Local", pPrograma.Indica_Local);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Desativado", pPrograma.Indica_Desativado);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Programet", pPrograma.Indica_Programet);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Boletim", pPrograma.Indica_Boletim);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Internet", pPrograma.Indica_Internet);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Faixa", pPrograma.Indica_Faixa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Qtd_Cotas", pPrograma.Qtd_Cotas);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Horario_Exibicao", pPrograma.Horario_Exibicao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Patrocinio", pPrograma.Indica_Patrocinio);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_A_JOVE", pPrograma.Cod_A_JOVE);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_N_JOVE", pPrograma.Cod_N_JOVE);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_DiaSeg", pPrograma.DiaSeg);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_DiaTer", pPrograma.DiaTer);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_DiaQua", pPrograma.DiaQua);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_DiaQui", pPrograma.DiaQui);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_DiaSex", pPrograma.DiaSex);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_DiaSab", pPrograma.DiaSab);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_DiaDom", pPrograma.DiaDom);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Sinopse", pPrograma.Sinopse);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Apresentadores", xmlApresentadores);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Veiculos", xmlVeiculos);

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

        public DataTable ExcluirPrograma(ProgramaModel pPrograma)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Programa_Excluir");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa",  pPrograma.Cod_Programa);
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
