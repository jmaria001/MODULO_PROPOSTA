using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace PROPOSTA
{

    public partial class Determinacao
    {
        String[] DiaSemana = { "Sab", "Dom", "Seg", "Ter", "Qua", "Qui", "Sex", };
        Int32 Count_Id_Rotate = 0;
        public DeterminacaoModel CarregarDados(FiltroModel Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            DeterminacaoModel Determinacao = new DeterminacaoModel();

            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Determinacao_Dados");
                cmd.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                cmd.Parameters.AddWithValue("@Par_Cod_Empresa", Param.Cod_Empresa);
                cmd.Parameters.AddWithValue("@Par_Numero_Mr", Param.Numero_Mr);
                cmd.Parameters.AddWithValue("@Par_Sequencia_Mr", Param.Sequencia_Mr);
                Adp.SelectCommand = cmd;
                Adp.Fill(dtb);
                if (dtb.Rows.Count > 0)
                {
                    Determinacao.Id_Contrato = dtb.Rows[0]["Id_Contrato"].ToString().ConvertToInt32();
                    Determinacao.Cod_Empresa = dtb.Rows[0]["Cod_Empresa"].ToString();
                    Determinacao.Numero_Mr = dtb.Rows[0]["Numero_Mr"].ToString().ConvertToInt32();
                    Determinacao.Sequencia_Mr = dtb.Rows[0]["Sequencia_Mr"].ToString().ConvertToInt32();
                    Determinacao.Cod_Agencia = dtb.Rows[0]["Cod_Agencia"].ToString();
                    Determinacao.Nome_Agencia = dtb.Rows[0]["Nome_Agencia"].ToString();
                    Determinacao.Cod_Cliente = dtb.Rows[0]["Cod_Cliente"].ToString();
                    Determinacao.Nome_Cliente = dtb.Rows[0]["Nome_Cliente"].ToString();
                    Determinacao.Data_Inicio = dtb.Rows[0]["Data_Inicio"].ToString().ConvertToDatetime().ToString("dd/MM/yyyy");
                    Determinacao.Data_Fim = dtb.Rows[0]["Data_Fim"].ToString().ConvertToDatetime().ToString("dd/MM/yyyy");
                    Determinacao.Competencia = dtb.Rows[0]["Competencia"].ToString().ConvertToInt32();
                    Determinacao.Comerciais = AddComerciais(Param);
                    Determinacao.Veiculos = AddVeiculos(dtb.Rows[0]["Id_Contrato"].ToString().ConvertToInt32());
                    Determinacao.Programas = AddProgramas(Param);
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
            return Determinacao;
        }
        public List<DeterminacaoComercialModel> AddComerciais(FiltroModel Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            List<DeterminacaoComercialModel> Comerciais = new List<DeterminacaoComercialModel>();

            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Determinacao_Comerciais_List");
                cmd.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                cmd.Parameters.AddWithValue("@Par_Cod_Empresa", Param.Cod_Empresa);
                cmd.Parameters.AddWithValue("@Par_Numero_Mr", Param.Numero_Mr);
                cmd.Parameters.AddWithValue("@Par_Sequencia_Mr", Param.Sequencia_Mr);
                Adp.SelectCommand = cmd;
                Adp.Fill(dtb);



                foreach (DataRow drw in dtb.Rows)
                {
                    Comerciais.Add(new DeterminacaoComercialModel()
                    {
                        Cod_Empresa = drw["Cod_Empresa"].ToString(),
                        Numero_Mr = drw["Numero_Mr"].ToString().ConvertToInt32(),
                        Sequencia_Mr = drw["Sequencia_Mr"].ToString().ConvertToInt32(),
                        Cod_Comercial = drw["Cod_Comercial"].ToString(),
                        Titulo_Comercial = drw["Titulo_Comercial"].ToString(),
                        Duracao = drw["Duracao"].ToString().ConvertToInt32(),
                        Cod_Tipo_Comercial = drw["Cod_Tipo_Comercial"].ToString(),
                        Nome_Tipo_Comercial = drw["Nome_Tipo_Comercial"].ToString(),
                        Cod_Red_Produto = drw["Cod_Red_Produto"].ToString().ConvertToInt32(),
                        Nome_Produto = drw["Nome_Produto"].ToString(),
                        Indica_Titulo_Determinar = drw["Indica_Titulo_Determinar"].ToString().ConvertToBoolean(),
                        Tem_Veiculacao = drw["Tem_Veiculacao"].ToString().ConvertToBoolean(),
                        Rotate = new List<DeterminacaoRotateModel>()
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
            return Comerciais;
        }
        public List<DeterminacaoVeiculoModel> AddVeiculos(Int32 pId_Contrato)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            List<DeterminacaoVeiculoModel> Veiculos = new List<DeterminacaoVeiculoModel>();

            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_MapaReserva_Get_Veiculo");
                cmd.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                cmd.Parameters.AddWithValue("@Par_Id_Contrato", pId_Contrato);
                Adp.SelectCommand = cmd;
                Adp.Fill(dtb);
                foreach (DataRow drw in dtb.Rows)
                {
                    Veiculos.Add(new DeterminacaoVeiculoModel()
                    {
                        Codigo = drw["Cod_Veiculo"].ToString(),
                        Descricao = drw["Nome_Veiculo"].ToString(),
                        Selected = true,
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
        public List<DeterminacaoProgramaModel> AddProgramas(FiltroModel Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            List<DeterminacaoProgramaModel> Programas = new List<DeterminacaoProgramaModel>();

            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "[Pr_Proposta_Baixa_Contrato_Get_Programa]");
                cmd.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                cmd.Parameters.AddWithValue("@Par_Cod_Empresa", Param.Cod_Empresa);
                cmd.Parameters.AddWithValue("@Par_Numero_Mr", Param.Numero_Mr);
                cmd.Parameters.AddWithValue("@Par_Sequencia_Mr", Param.Sequencia_Mr);

                Adp.SelectCommand = cmd;
                Adp.Fill(dtb);
                foreach (DataRow drw in dtb.Rows)
                {
                    Programas.Add(new DeterminacaoProgramaModel()
                    {
                        Codigo = drw["Codigo"].ToString(),
                        Descricao = drw["Descricao"].ToString(),
                        Selected = true,
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
        public DataTable SalvarComercial(DeterminacaoComercialModel Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "[Pr_Proposta_Comercial_Insert]");
                cmd.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                cmd.Parameters.AddWithValue("@Par_Cod_Empresa", Param.Cod_Empresa);
                cmd.Parameters.AddWithValue("@Par_Numero_Mr", Param.Numero_Mr);
                cmd.Parameters.AddWithValue("@Par_Sequencia_Mr", Param.Sequencia_Mr);
                cmd.Parameters.AddWithValue("@Par_Cod_Comercial", Param.Cod_Comercial);
                cmd.Parameters.AddWithValue("@Par_Titulo_Comercial", Param.Titulo_Comercial);
                cmd.Parameters.AddWithValue("@Par_Duracao", Param.Duracao);
                cmd.Parameters.AddWithValue("@Par_Cod_Tipo_Comercial", Param.Cod_Tipo_Comercial);
                cmd.Parameters.AddWithValue("@Par_Cod_Red_Produto", Param.Cod_Red_Produto);
                cmd.Parameters.AddWithValue("@Par_Nome_Produto", Param.Nome_Produto);
                cmd.Parameters.AddWithValue("@Par_Indica_Titulo_Determinar", Param.Indica_Titulo_Determinar);
                Adp.SelectCommand = cmd;
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
        public List<AnaliseRotateModel> AnalisarRotate(DeterminacaoModel Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            String xmlVeiculos = null;
            String xmlProgramas = null;
            String xmlComerciais = null;
            List<AnaliseRotateModel> AnaliseRotate = new List<AnaliseRotateModel>();
            if (Param.Veiculos.Count > 0)
            {
                xmlVeiculos = clsLib.SerializeToString(Param.Veiculos);
            }
            if (Param.Programas.Count > 0)
            {
                xmlProgramas = clsLib.SerializeToString(Param.Programas);
            }
            if (Param.Comerciais.Count > 0)
            {
                xmlComerciais = clsLib.SerializeToString(Param.Comerciais);
            }

            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "[Pr_Proposta_Rotate_Simular]");
                cmd.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                cmd.Parameters.AddWithValue("@Par_Cod_Empresa", Param.Cod_Empresa);
                cmd.Parameters.AddWithValue("@Par_Numero_Mr", Param.Numero_Mr);
                cmd.Parameters.AddWithValue("@Par_Sequencia_Mr", Param.Sequencia_Mr);
                if (!String.IsNullOrEmpty(Param.Data_Inicio))
                {
                    cmd.Parameters.AddWithValue("@Par_Data_Inicio", Param.Data_Inicio.ConvertToDatetime());
                }

                if (!String.IsNullOrEmpty(Param.Data_Fim))
                {
                    cmd.Parameters.AddWithValue("@Par_Data_Fim", Param.Data_Fim.ConvertToDatetime());
                }
                cmd.Parameters.AddWithValue("@Par_Comerciais", xmlComerciais);
                cmd.Parameters.AddWithValue("@Par_Veiculos", xmlVeiculos);
                cmd.Parameters.AddWithValue("@Par_Programas", xmlProgramas);
                cmd.Parameters.AddWithValue("@Par_Processo", "SIMULAR");

                Adp.SelectCommand = cmd;
                Adp.Fill(dtb);

                if (dtb.Rows[0]["Status"].ToString().ConvertToBoolean())
                {
                    DataView view = new DataView(dtb);
                    DataTable distinctValues = view.ToTable(true, "Identificador","Cod_Veiculo","Cod_Programa","Cod_Comercial","Duracao","Operacao","Mes","Ano");
                    
                    
                    foreach (DataRow drw in distinctValues.Rows)
                    {


                        Count_Id_Rotate++;
                        AnaliseRotate.Add(new AnaliseRotateModel()
                        {
                            Status = dtb.Rows[0]["Status"].ToString().ConvertToBoolean(),
                            Mensagem = dtb.Rows[0]["Mensagem"].ToString(),
                            Id_Rotate = Count_Id_Rotate,
                            Cod_Veiculo = drw["Cod_Veiculo"].ToString(),
                            Cod_Programa = drw["Cod_Programa"].ToString(),
                            Cod_Comercial = drw["Cod_Comercial"].ToString(),
                            Duracao = drw["Duracao"].ToString().ConvertToInt32(),
                            Operacao = drw["Operacao"].ToString().ConvertToInt32(),
                            Insercoes = AddAnaliseInsercoes(drw, dtb),
                        });
                    }
                }
                else
                {
                    AnaliseRotate.Add(new AnaliseRotateModel()
                    {
                        Status = dtb.Rows[0]["Status"].ToString().ConvertToBoolean(),
                        Mensagem = dtb.Rows[0]["Mensagem"].ToString(),
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
            return AnaliseRotate;
        }
        private List<AnaliseRotateInsercoes> AddAnaliseInsercoes(DataRow drw, DataTable Records)
        {
            List<AnaliseRotateInsercoes> Insercoes = new List<AnaliseRotateInsercoes>();
            DateTime DataInicio = new DateTime(drw["Ano"].ToString().ConvertToInt32(), drw["Mes"].ToString().ConvertToInt32(), 1);
            DateTime DataFim = DataInicio.AddMonths(1).AddDays(-1);
            String Filter = "";
            DataRow[] rows;
            while (DataInicio <= DataFim)
            {
                Filter = "Cod_Veiculo = '" + drw["Cod_Veiculo"].ToString() + "'";
                Filter += " and Cod_Programa = '" + drw["Cod_Programa"].ToString() + "'";
                Filter += " and Cod_Comercial= '" + drw["Cod_Comercial"].ToString() + "'";
                Filter += " and Operacao= '" + drw["Operacao"].ToString() + "'";
                Filter += " and Data_Exibicao= '" + DataInicio + "'";
                rows = Records.Select(Filter);
                if (rows.Length == 0)
                {
                    Insercoes.Add(new AnaliseRotateInsercoes()
                    {
                        Id_Rotate = Count_Id_Rotate,
                        Data_Exibicao = DataInicio,
                        Dia_Semana = DiaSemana[(int)DataInicio.DayOfWeek],
                        Qtd = 0
                    });
                }
                else
                {
                    foreach (DataRow dri in rows)
                    {
                        Insercoes.Add(new AnaliseRotateInsercoes()
                        {
                            Id_Rotate = Count_Id_Rotate,
                            Data_Exibicao = dri["Data_Exibicao"].ToString().ConvertToDatetime(),
                            Dia_Semana = DiaSemana[(int)dri["Data_Exibicao"].ToString().ConvertToDatetime().DayOfWeek],
                            Qtd = dri["Qtd_Insercoes"].ToString().ConvertToInt32(),
                        });
                    };
                };
                DataInicio = DataInicio.AddDays(1);
            };
            return Insercoes;
        }

        public DataTable SalvarDeterminacao(DeterminacaoModel Param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            DataTable dtbDeterminacao = new DataTable("dtbDeterminacao");
            SimLib clsLib = new SimLib();
            String xmlVeiculos = null;
            String xmlProgramas = null;
            String xmlComerciais = null;
            if (Param.Veiculos.Count > 0)
            {
                xmlVeiculos = clsLib.SerializeToString(Param.Veiculos);
            }
            if (Param.Programas.Count > 0)
            {
                xmlProgramas = clsLib.SerializeToString(Param.Programas);
            }
            if (Param.Comerciais.Count > 0)
            {
                xmlComerciais = clsLib.SerializeToString(Param.Comerciais);
            }

            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "[Pr_Proposta_Rotate_Simular]");
                cmd.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                cmd.Parameters.AddWithValue("@Par_Cod_Empresa", Param.Cod_Empresa);
                cmd.Parameters.AddWithValue("@Par_Numero_Mr", Param.Numero_Mr);
                cmd.Parameters.AddWithValue("@Par_Sequencia_Mr", Param.Sequencia_Mr);
                if (!String.IsNullOrEmpty(Param.Data_Inicio))
                {
                    cmd.Parameters.AddWithValue("@Par_Data_Inicio", Param.Data_Inicio.ConvertToDatetime());
                }

                if (!String.IsNullOrEmpty(Param.Data_Fim))
                {
                    cmd.Parameters.AddWithValue("@Par_Data_Fim", Param.Data_Fim.ConvertToDatetime());
                }
                cmd.Parameters.AddWithValue("@Par_Comerciais", xmlComerciais);
                cmd.Parameters.AddWithValue("@Par_Veiculos", xmlVeiculos);
                cmd.Parameters.AddWithValue("@Par_Programas", xmlProgramas);
                cmd.Parameters.AddWithValue("@Par_Processo", "Gravar");

                Adp.SelectCommand = cmd;
                Adp.Fill(dtb);
                /// Se retornou status ok executa sp_determinacao
                if (dtb.Rows[0]["Status"].ToString().ConvertToBoolean())
                {
                    SqlCommand cmdDeterminacao = cnn.Procedure(cnn.Connection, "Sp_Determinacao");
                    cmdDeterminacao.Parameters.AddWithValue("@Par_Cod_Empresa" , Param.Cod_Empresa);
                    cmdDeterminacao.Parameters.AddWithValue("@Par_Numero_MR", Param.Numero_Mr);
                    cmdDeterminacao.Parameters.AddWithValue("@Par_Sequencia_Mr", Param.Sequencia_Mr);
                    cmdDeterminacao.Parameters.AddWithValue("@Par_Cod_Usuario", this.CurrentUser);
                    cmdDeterminacao.Parameters.AddWithValue("@Par_Identificador", dtb.Rows[0]["Id_Determinacao"].ToString());
                    SqlDataAdapter adpDeterminacao = new SqlDataAdapter(cmdDeterminacao);
                    adpDeterminacao.Fill(dtbDeterminacao);
                }
                else
                {
                    return dtb;
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
            return dtbDeterminacao;
        }

    }
}