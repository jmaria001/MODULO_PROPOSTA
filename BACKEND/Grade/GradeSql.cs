using CLASSDB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PROPOSTA
{
    public partial class Grade

    {
        public GradeListModel GradeList(GradeFiltroModel Filtro)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            GradeListModel Grades = new GradeListModel();
            List<GradeListDiaModel> Dias = new List<GradeListDiaModel>();
            List<GradeListProgramaModel> Programas = new List<GradeListProgramaModel>();

            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Grade_List");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", Filtro.Cod_Veiculo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia", clsLib.CompetenciaInt(Filtro.Competencia));
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", Filtro.Cod_Programa);
                Adp.Fill(dtb);
                DateTime dtUltimoDia = DateTime.MinValue;
                if (dtb.Rows.Count > 0)
                {
                    dtUltimoDia = dtb.Rows[0]["Data_Exibicao"].ToString().ConvertToDatetime();
                }
                foreach (DataRow drw in dtb.Rows)
                {
                    if (drw["Data_Exibicao"].ToString().ConvertToDatetime() != dtUltimoDia)
                    {
                        Dias.Add(new GradeListDiaModel() { Data_Exibicao = dtUltimoDia, Programas = Programas, Dia_Semana = dtUltimoDia.ToString("ddd").ToUpper() });
                        Programas = new List<GradeListProgramaModel>();
                        dtUltimoDia = drw["Data_Exibicao"].ToString().ConvertToDatetime();
                    }
                    Programas.Add(new GradeListProgramaModel()
                    {
                        Cod_Programa = drw["Cod_Programa"].ToString(),
                        Nome_Programa = drw["Nome_Programa"].ToString(),
                        Hora_Inicio = drw["Hora_Inicio"].ToString(),
                        Hora_Termino = drw["Hora_Termino"].ToString(),
                        Nome_Genero = drw["Nome_Genero"].ToString(),
                        Indica_Desativado = drw["Indica_Desativado"].ToString().ConvertToBoolean(),
                        Dispo_Net = drw["Dispo_Net"].ToString().ConvertToInt32(),
                        Dispo_Local = drw["Dispo_Local"].ToString().ConvertToInt32(),
                        Absorvido_Net = drw["Absorvido_Net"].ToString().ConvertToInt32(),
                        Absorvido_Local = drw["Absorvido_Local"].ToString().ConvertToInt32(),
                        Saldo_Net = drw["Saldo_Net"].ToString().ConvertToInt32(),
                        Saldo_Local = drw["Saldo_Local"].ToString().ConvertToInt32(), 

                    });
                }
                if (dtUltimoDia != DateTime.MinValue)
                {
                    Dias.Add(new GradeListDiaModel() { Data_Exibicao = dtUltimoDia, Programas = Programas, Dia_Semana = dtUltimoDia.ToString("ddd").ToUpper() });
                }

                Grades.Dias = Dias;
            }

            catch (Exception)
            {
                throw;
            }
            finally
            {
                cnn.Close();
            }
            return Grades;
        }
        public GradeModel GradeGetData(GradeGetDataModel param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            GradeModel Grade = new GradeModel();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Grade_Get");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", param.Cod_Veiculo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Exibicao", param.Data_Exibicao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", param.Cod_Programa);
                Adp.Fill(dtb);

                if (dtb.Rows.Count > 0)
                {
                    Grade.Cod_Veiculo = dtb.Rows[0]["Cod_Veiculo"].ToString();
                    Grade.Data_Exibicao = dtb.Rows[0]["Data_Exibicao"].ToString();
                    Grade.Cod_Programa = dtb.Rows[0]["Cod_Programa"].ToString().TrimEnd();
                    Grade.Dispo_Net = dtb.Rows[0]["Dispo_Net"].ToString().ConvertToInt32();
                    Grade.Dispo_Local = dtb.Rows[0]["Dispo_Local"].ToString().ConvertToInt32();
                    Grade.Dispo_Programa = dtb.Rows[0]["Dispo_Programa"].ToString().ConvertToInt32();
                    Grade.Faixa_Horaria = dtb.Rows[0]["Faixa_Horaria"].ToString().ConvertToByte();
                    Grade.Horario_Final = dtb.Rows[0]["Horario_Final"].ToString();
                    Grade.Horario_Inicial = dtb.Rows[0]["Horario_Inicial"].ToString();
                    Grade.RedeId = dtb.Rows[0]["RedeId"].ToString().ConvertToInt32();
                    Grade.Nome_Veiculo= dtb.Rows[0]["Nome_Veiculo"].ToString();
                    Grade.Nome_Programa = dtb.Rows[0]["Nome_Programa"].ToString();
                    Grade.Inicio_Validade = dtb.Rows[0]["Data_Exibicao"].ToString().ConvertToDatetime().ToString("dd/MM/yyyy");
                    Grade.Termino_Validade = dtb.Rows[0]["Data_Exibicao"].ToString().ConvertToDatetime().ToString("dd/MM/yyyy");
                    Grade.Domingo= dtb.Rows[0]["Dia_Semana"].ToString().ConvertToByte() == 1;
                    Grade.Segunda= dtb.Rows[0]["Dia_Semana"].ToString().ConvertToByte() == 2;
                    Grade.Terca= dtb.Rows[0]["Dia_Semana"].ToString().ConvertToByte() == 3;
                    Grade.Quarta= dtb.Rows[0]["Dia_Semana"].ToString().ConvertToByte() == 4;
                    Grade.Quinta= dtb.Rows[0]["Dia_Semana"].ToString().ConvertToByte() == 5;
                    Grade.Sexta= dtb.Rows[0]["Dia_Semana"].ToString().ConvertToByte() == 6;
                    Grade.Sabado= dtb.Rows[0]["Dia_Semana"].ToString().ConvertToByte() == 7;
                    Grade.Veiculos = AddVeiculos(param);
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
            return Grade;
        }
        public DataTable GradeGetProgramas(GradeFiltroModel param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Grade_GetPrograma");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_RedeId", param.RedeId);
                if (!String.IsNullOrEmpty(param.Cod_Programa))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", param.Cod_Programa);
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
        public List<VeiculoModel> AddVeiculos(GradeGetDataModel param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            List<VeiculoModel> Veiculos = new List<VeiculoModel>();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Grade_GetVeiculo");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Exibicao", param.Data_Exibicao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", param.Cod_Programa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_RedeId", DBNull.Value);
                Adp.Fill(dtb);
                foreach (DataRow drw  in dtb.Rows)
                {
                    Veiculos.Add(new VeiculoModel()
                    {
                        Cod_Veiculo = drw["Cod_Veiculo"].ToString(),
                        Nome_Veiculo = drw["Nome_Veiculo"].ToString(),
                        Selected = drw["Selected"].ToString().ConvertToBoolean()
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
        public String GetUltimoDiaGrade(GradeGetDataModel param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            string dtUltimoDia = "";
            try
            {
                String strSql = "Select Max(Data_Exibicao) as Data_Exibicao From Grade G with (Nolock) WHere G.Cod_Programa = '" + param.Cod_Programa + "'";
                SqlCommand cmd = cnn.Text(cnn.Connection, strSql);
                Adp.SelectCommand = cmd;
                Adp.Fill(dtb);
                dtUltimoDia = dtb.Rows[0]["Data_Exibicao"].ToString().ConvertToDatetime().ToString("dd/MM/yyyy");
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cnn.Close();
            }
            return dtUltimoDia;
        }
        public List<VeiculoModel> GetVeiculosRede(Int32 pRedeId)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            List<VeiculoModel> Veiculos = new List<VeiculoModel>();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Grade_GetVeiculo");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Exibicao", DBNull.Value);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", DBNull.Value);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_RedeId", pRedeId);

                Adp.Fill(dtb);
                foreach (DataRow drw in dtb.Rows)
                {
                    Veiculos.Add(new VeiculoModel()
                    {
                        Cod_Veiculo = drw["Cod_Veiculo"].ToString(),
                        Nome_Veiculo = drw["Nome_Veiculo"].ToString(),
                        Selected = drw["Selected"].ToString().ConvertToBoolean()
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
        public DataTable SalvarGrade(GradeModel param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            String xmlVeiculo = null;
            if (param.Veiculos!=null)
            {
                xmlVeiculo = clsLib.SerializeToString(param.Veiculos);
            }
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "[PR_Proposta_Grade_Salvar]");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Operacao", param.Action.ToUpper());
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", param.Cod_Programa.ToUpper());
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Dispo_Net", param.Dispo_Net);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Dispo_Local", param.Dispo_Local);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Dispo_Programa", param.Dispo_Programa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Faixa_Horaria", param.Faixa_Horaria);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Horario_Final", param.Horario_Final);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Horario_Inicial", param.Horario_Inicial);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Domingo", param.Domingo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Segunda", param.Segunda);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Terca", param.Terca);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Quarta", param.Quarta);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Quinta", param.Quinta);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Sexta", param.Sexta);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Sabado", param.Sabado);
                if (!String.IsNullOrEmpty(param.Inicio_Validade))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Inicio_Validade", param.Inicio_Validade.ConvertToDatetime());
                }
                if (!String.IsNullOrEmpty(param.Termino_Validade))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Termino_Validade", param.Termino_Validade.ConvertToDatetime());
                }
                
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Veiculos", xmlVeiculo);
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
        public DataTable ExcluirGrade(GradeModel param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            String xmlVeiculo = null;
            if (param.Veiculos != null)
            {
                xmlVeiculo = clsLib.SerializeToString(param.Veiculos);
            }
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "[PR_Proposta_Grade_Excluir]");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", param.Cod_Programa.ToUpper());
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Domingo", param.Domingo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Segunda", param.Segunda);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Terca", param.Terca);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Quarta", param.Quarta);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Quinta", param.Quinta);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Sexta", param.Sexta);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Sabado", param.Sabado);
                if (!String.IsNullOrEmpty(param.Inicio_Validade))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Inicio_Validade", param.Inicio_Validade.ConvertToDatetime());
                }
                if (!String.IsNullOrEmpty(param.Termino_Validade))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Termino_Validade", param.Termino_Validade.ConvertToDatetime());
                }
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Veiculos", xmlVeiculo);
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
        public DataTable DesativarGrade(GradeModel param,String Operacao)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            String xmlVeiculo = null;
            if (param.Veiculos != null)
            {
                xmlVeiculo = clsLib.SerializeToString(param.Veiculos);
            }
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "[PR_Proposta_Grade_Desativar]");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", param.Cod_Programa.ToUpper());
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Domingo", param.Domingo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Segunda", param.Segunda);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Terca", param.Terca);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Quarta", param.Quarta);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Quinta", param.Quinta);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Sexta", param.Sexta);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Sabado", param.Sabado);
                if (!String.IsNullOrEmpty(param.Inicio_Validade))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Inicio_Validade", param.Inicio_Validade.ConvertToDatetime());
                }
                if (!String.IsNullOrEmpty(param.Termino_Validade))
                {
                    Adp.SelectCommand.Parameters.AddWithValue("@Par_Termino_Validade", param.Termino_Validade.ConvertToDatetime());
                }
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Veiculos", xmlVeiculo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Operacao", Operacao);
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




        //--mmm INICIO

        //--Carrega Lista de Veiculos
        public List<ListarVeiculoModel> CarregaVeiculo()
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            List<ListarVeiculoModel> ListadeVeiculos = new List<ListarVeiculoModel>();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Grade_Propagacao_Lista_Vei");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.Fill(dtb);
                foreach (DataRow drw in dtb.Rows)
                {
                    ListadeVeiculos.Add(new ListarVeiculoModel()
                    {
                        Cod_Veiculo = drw["Cod_Veiculo"].ToString(),
                        Nome_Veiculo = drw["Nome_Veiculo"].ToString()
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
            return ListadeVeiculos;
        }
        //--Carrega Lista de Programas
        public List<ListarProgramaModel> CarregaPrograma()
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            List<ListarProgramaModel> ListadeProgramas = new List<ListarProgramaModel>();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Grade_Propagacao_Lista_Prog");
                Adp.SelectCommand = cmd;
                Adp.Fill(dtb);
                foreach (DataRow drw in dtb.Rows)
                {
                    ListadeProgramas.Add(new ListarProgramaModel()
                    {
                        Cod_Programa = drw["Cod_Programa"].ToString(),
                        Titulo = drw["Titulo"].ToString()
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
            return ListadeProgramas;
        }
        //--Salva a Propagação
        public Boolean  SalvarPropagacaoGrade(PropagacaoGradeModel Grade)
        {
            Boolean Retorno = true;
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            String strVeiculos = "";
            for (var i = 0; i < Grade.Veiculos.Count; i++)
            {
                if (Grade.Veiculos[i].Selected)
                {
                    strVeiculos += Grade.Veiculos[i].Cod_Veiculo;
                }
            }
            String strProgramas = "";
            for (var i = 0; i < Grade.Programas.Count; i++)
            {
                if (Grade.Programas[i].Selected)
                {
                    strProgramas += Grade.Programas[i].Cod_Programa;
                }
            }
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Exporta_Grade");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia_Base", clsLib.CompetenciaInt(Grade.Competencia_Base));
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Inicio", clsLib.FirstDay(Int32.Parse(Grade.Data_Inicio.Substring(0, 2)), Int32.Parse(Grade.Data_Inicio.Substring(3, 4))));
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Data_Fim", clsLib.LastDay(Int32.Parse(Grade.Data_Fim.Substring(0, 2)), Int32.Parse(Grade.Data_Fim.Substring(3, 4))));
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Veiculo", strVeiculos);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Programa", strProgramas);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Veiculo_Origem", Grade.Cod_Veiculo_Origem);
                Adp.Fill(dtb);
            }
            catch (Exception)
            {
                Retorno = false;
                throw;
            }
            finally
            {
                cnn.Close();
            }
            return Retorno;
        }

        //--mmm FIM

    }
}
