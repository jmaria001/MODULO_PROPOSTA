using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace PROPOSTA
{

    public partial class RegraAprovacao
    {
        Int32 ContadorRange= 0;
        public List<Regra_Aprovacao_Model> RegraAprovacaoListar()
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            List<Regra_Aprovacao_Model> Regra = new List<Regra_Aprovacao_Model>();
            try 
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_RegraAprovacao_Listar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.Fill(dtb);
                foreach (DataRow drw  in dtb.Rows)
                {
                    Regra.Add(new Regra_Aprovacao_Model() {
                        Id_Regra = drw["Id_Regra"].ToString().ConvertToInt32(),
                        Nome_Regra = drw["Nome_Regra"].ToString(),
                        Descricao_Regra = drw["Descricao_Regra"].ToString(),
                        //Desconto_Inicio = drw["Desconto_Inicio"].ToString(),
                        //Desconto_Termino = drw["Desconto_Termino"].ToString(),
                        //Range_Desconto = drw["Range_Desconto"].ToString(),
                        Range = AddRange(drw["Id_Regra"].ToString().ConvertToInt32())
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
            return Regra;
        }
        public Regra_Aprovacao_Model GetRegraAprovacao(Int32 pIdRegraAprovacao)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            Regra_Aprovacao_Model Regra = new Regra_Aprovacao_Model();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_RegraAprovacao_Listar");

                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Id_Regra", pIdRegraAprovacao);
                Adp.Fill(dtb);

                if (dtb.Rows.Count>0)
                {
                    DataRow drw = dtb.Rows[0];
                    Regra.Id_Regra = drw["Id_Regra"].ToString().ConvertToInt32();
                    Regra.Nome_Regra = drw["Nome_Regra"].ToString();
                    Regra.Descricao_Regra = drw["Descricao_Regra"].ToString();
                    Regra.Range = AddRange(drw["Id_Regra"].ToString().ConvertToInt32());
                    Regra.Empresas = AddEmpresas(drw["Id_Regra"].ToString().ConvertToInt32());
                    Regra.Veiculos= AddVeiculos(drw["Id_Regra"].ToString().ConvertToInt32());
                    Regra.Agencias = AddAgencias(drw["Id_Regra"].ToString().ConvertToInt32());
                    Regra.Clientes= AddClientes(drw["Id_Regra"].ToString().ConvertToInt32());
                    Regra.Max_Id_Range = ContadorRange;
                    Regra.SemEmpresa= (Regra.Empresas.Count == 0) ? true : false;
                    Regra.SemVeiculo= (Regra.Veiculos.Count == 0) ? true : false;
                    Regra.SemAgencia= (Regra.Agencias.Count == 0) ? true : false;
                    Regra.SemCliente= (Regra.Clientes.Count == 0) ? true : false;

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
            return Regra;
        }
        private List<Range_Model> AddRange(Int32 pId_Regra)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            List<Range_Model> Range = new List<Range_Model>();
            Int32? varqtd;
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Regra_Aprovacao_Range_Get");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Id_Regra", pId_Regra);
                Adp.Fill(dtb);
                foreach (DataRow drw in dtb.Rows)
                {
                    ContadorRange= drw["Id_Range"].ToString().ConvertToInt32();
                    varqtd = drw["QtdAprovadores"].ToString().ConvertToInt32();
                    if ( varqtd==0){
                        varqtd = null;
                    };

                    Range.Add(new Range_Model()
                    {
                        Id_Range = drw["Id_Range"].ToString().ConvertToInt32(),
                        Id_Regra = drw["Id_Regra"].ToString().ConvertToInt32(),
                        Desconto_De = drw["Desconto_De"].ToString(),
                        Desconto_Ate = drw["Desconto_Ate"].ToString(),
                        Range_Text = drw["Range_Text"].ToString(),
                        QtdAprovadores = varqtd,
                        Aprovadores = AddAprovador(drw["Id_Range"].ToString().ConvertToInt32())
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
            return Range;
        }
        private List<Aprovador_Model> AddAprovador(Int32 pId_Range)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            List<Aprovador_Model> Aprovador= new List<Aprovador_Model>();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_RegraAprovacao_Aprovador_Get");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Id_Range", pId_Range);
                Adp.Fill(dtb);
                foreach (DataRow drw in dtb.Rows)
                {
                    Aprovador.Add(new Aprovador_Model()
                    {
                        Id_Regra_Aprovacao_Usuario = drw["Id_Regra_Aprovacao_Usuario"].ToString().ConvertToInt32(),
                        Id_Range= drw["Id_Range"].ToString().ConvertToInt32(),
                        Id_Usuario = drw["Id_Usuario"].ToString().ConvertToInt32(),
                        Nome_Usuario = drw["Nome_Usuario"].ToString(),
                        Indica_Obrigatorio = drw["Indica_Obrigatorio"].ToString().ConvertToBoolean(),
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
            return Aprovador;
        }
        private List<Regra_Aprovacao_Empresa_Model> AddEmpresas(Int32 pId_Regra)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            List<Regra_Aprovacao_Empresa_Model> Empresas = new List<Regra_Aprovacao_Empresa_Model>();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_RegraAprovacao_Empresas_Get");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Id_Regra", pId_Regra);
                Adp.Fill(dtb);
                foreach (DataRow drw in dtb.Rows)
                {
                    Empresas.Add(new Regra_Aprovacao_Empresa_Model()
                    {
                        Id_Regra_Empresa = drw["Id_Regra_Empresa"].ToString().ConvertToInt32(),
                        Id_Regra = drw["Id_Regra"].ToString().ConvertToInt32(),
                        Cod_Empresa = drw["Cod_Empresa"].ToString(),
                        Nome_Empresa = drw["Nome_Empresa"].ToString(),
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
            return Empresas;
        }
        private List<Regra_Aprovacao_Veiculo_Model> AddVeiculos(Int32 pId_Regra)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            List<Regra_Aprovacao_Veiculo_Model> Veiculos = new List<Regra_Aprovacao_Veiculo_Model>();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_RegraAprovacao_Veiculos_Get");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Id_Regra", pId_Regra);
                Adp.Fill(dtb);
                foreach (DataRow drw in dtb.Rows)
                {
                    Veiculos.Add(new Regra_Aprovacao_Veiculo_Model()
                    {
                        Id_Regra_Veiculo = drw["Id_Regra_Veiculo"].ToString().ConvertToInt32(),
                        Id_Regra = drw["Id_Regra"].ToString().ConvertToInt32(),
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
        private List<Regra_Aprovacao_Agencia_Model> AddAgencias(Int32 pId_Regra)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            List<Regra_Aprovacao_Agencia_Model> Agencias = new List<Regra_Aprovacao_Agencia_Model>();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_RegraAprovacao_Agencias_Get");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Id_Regra", pId_Regra);
                Adp.Fill(dtb);
                foreach (DataRow drw in dtb.Rows)
                {
                    Agencias.Add(new Regra_Aprovacao_Agencia_Model()
                    {
                        Id_Regra_Agencia = drw["Id_Regra_Agencia"].ToString().ConvertToInt32(),
                        Id_Regra = drw["Id_Regra"].ToString().ConvertToInt32(),
                        Cod_Agencia = drw["Cod_Agencia"].ToString(),
                        Nome_Agencia = drw["Nome_Agencia"].ToString(),
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
            return Agencias;
        }
        private List<Regra_Aprovacao_Cliente_Model> AddClientes(Int32 pId_Regra)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            List<Regra_Aprovacao_Cliente_Model> Clientes = new List<Regra_Aprovacao_Cliente_Model>();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_RegraAprovacao_Clientes_Get");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Id_Regra", pId_Regra);
                Adp.Fill(dtb);
                foreach (DataRow drw in dtb.Rows)
                {
                    Clientes.Add(new Regra_Aprovacao_Cliente_Model()
                    {
                        Id_Regra_Cliente = drw["Id_Regra_Cliente"].ToString().ConvertToInt32(),
                        Id_Regra = drw["Id_Regra"].ToString().ConvertToInt32(),
                        Cod_Cliente = drw["Cod_Cliente"].ToString(),
                        Nome_Cliente = drw["Nome_Cliente"].ToString(),
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
            return Clientes;
        }
        public void ExcluirRegraAprovacao(Regra_Aprovacao_Model RegraAprovacao)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SimLib clsLib = new SimLib();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_Excluir_RegraAprovacao");
                cmd.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                cmd.Parameters.AddWithValue("@Par_Id_Regra", RegraAprovacao.Id_Regra);
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cnn.Close();
            }

        }
        public DataTable SalvarRegraAprovacao(Regra_Aprovacao_Model param)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();

            String xmlRange = null;
            String xmlEmpresa= null;
            String xmlVeiculo= null;
            String xmlAgencia= null;
            String xmlCliente= null;
            if (param.Range.Count > 0)
            {
                xmlRange = clsLib.SerializeToString(param.Range);
            }
            if (param.Empresas.Count > 0 && !param.SemEmpresa)
            {
                xmlEmpresa = clsLib.SerializeToString(param.Empresas);
            }
            if (param.Veiculos.Count > 0 && !param.SemVeiculo)
            {
                xmlVeiculo = clsLib.SerializeToString(param.Veiculos);
            }

            if (param.Agencias.Count > 0 && !param.SemAgencia)
            {
                xmlAgencia = clsLib.SerializeToString(param.Agencias);
            }
            if (param.Clientes.Count > 0 && !param.SemCliente)
            {
                xmlCliente = clsLib.SerializeToString(param.Clientes);
            }
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_RegraAprovacao_Salvar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Id_Regra", param.Id_Regra);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Nome_Regra", param.Nome_Regra);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Descricao_Regra", param.Descricao_Regra);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Range", xmlRange);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Empresas", xmlEmpresa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Veiculos", xmlVeiculo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Agencias", xmlAgencia);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Clientes", xmlCliente);
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
