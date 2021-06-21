using CLASSDB;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace PROPOSTA
{

    public partial class ParametroValoracao
    {

        //===========================Listar Parametro Valoracao
        public ParametroValoracaoModel ParametroValoracaoListar(FiltroModel pFiltro)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            ParametroValoracaoModel Retorno = new ParametroValoracaoModel();
            try
            {
                Retorno.Competencia = pFiltro.Competencia;
                Retorno.Cod_Programa = pFiltro.Cod_Programa;
                Retorno.Titulo = pFiltro.Titulo_Programa;
                Retorno.TipoParametroValoracao = pFiltro.TipoParametroValoracao;
                Retorno.Tipo_Comercial = AddListaTipoComercial(pFiltro);
                Retorno.Duracao = AddListaDuracao(pFiltro);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cnn.Close();
            }
            return Retorno;
        }

        //=====================Adicionar Lista Tipo Comercial
        public List<Parametro_Tipo_Comercial_Model> AddListaTipoComercial(FiltroModel pFiltro)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            List<Parametro_Tipo_Comercial_Model> Tipo_Comercial = new List<Parametro_Tipo_Comercial_Model>();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_ParametroValoracao_TC_Listar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia", clsLib.CompetenciaInt(pFiltro.Competencia));
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", pFiltro.Cod_Programa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_TipoParametroValoracao", pFiltro.TipoParametroValoracao);
                Adp.Fill(dtb);
                foreach (DataRow drw in dtb.Rows)
                {
                    Tipo_Comercial.Add(new Parametro_Tipo_Comercial_Model()
                    {
                        Cod_Tipo_Comercial = drw["Cod_Tipo_Comercial"].ToString(),
                        Descricao = drw["Descricao"].ToString(),
                        Vlr_Parametro= drw["Vlr_Parametro"].ToString(),
                        Indica_Vlr_Duracao = drw["Indica_Vlr_Duracao"].ToString().ConvertToBoolean(),
                        Indica_Vlr_Proporcional = drw["Indica_Vlr_Proporcional"].ToString().ConvertToBoolean(),
                        Tipo_Valoracao= drw["Tipo_Valoracao"].ToString(),
                        Indica_Desativado = drw["Indica_Desativado"].ToString().ConvertToBoolean(),
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
            return Tipo_Comercial;
        }
        //=====================Adicionar Lista Tipo Comercial
        public List<Parametro_Duracao_Model> AddListaDuracao(FiltroModel pFiltro)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            List<Parametro_Duracao_Model> Duracao = new List<Parametro_Duracao_Model>();
            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_ParametroValoracao_Duracao_Listar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia", clsLib.CompetenciaInt(pFiltro.Competencia));
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", pFiltro.Cod_Programa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_TipoParametroValoracao", pFiltro.TipoParametroValoracao);
                Adp.Fill(dtb);
                foreach (DataRow drw in dtb.Rows)
                {
                    Duracao.Add(new Parametro_Duracao_Model()
                    {
                        Duracao = drw["Duracao"].ToString().ConvertToInt32(),
                        Vlr_Parametro= drw["Vlr_Parametro"].ToString(),
                        Indica_Desativado = drw["Indica_Desativado"].ToString().ConvertToBoolean()
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
            return Duracao;
        }
        //===========================Salvar parametro
        public DataTable SalvarParametroValoracao(Parametro_Tipo_Comercial_Model pParametroValoracao)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {

                Byte btIndica_Vlr_Duracao = 0;
                Byte btIndica_Vlr_Proporcional = 0;

                if (pParametroValoracao.Tipo_Valoracao == "1")
                {
                    btIndica_Vlr_Duracao = 1;
                }
                if (pParametroValoracao.Tipo_Valoracao == "2")
                {
                    btIndica_Vlr_Proporcional = 1;
                }

                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Parametro_Valoracao_TipoComercial_Salvar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Usuario", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia", clsLib.CompetenciaInt(pParametroValoracao.Competencia));
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa", pParametroValoracao.Cod_Empresa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", pParametroValoracao.Cod_Programa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Tipo_Comercial", pParametroValoracao.Cod_Tipo_Comercial);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Vlr_Parametro", pParametroValoracao.Vlr_Parametro);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Vlr_Duracao", btIndica_Vlr_Duracao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Vlr_Proporcional", btIndica_Vlr_Proporcional);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Desativado", pParametroValoracao.Indica_Desativado);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_TipoParametroValoracao", pParametroValoracao.TipoParametroValoracao);
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
            //return dtb;
            return dtb;
        }

        //===========================Salvar parametro
        public DataTable SalvarParametroValoracaoDuracao(Parametro_Duracao_Model pParametroValoracao)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {

                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Parametro_Valoracao_Duracao_Salvar");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Usuario", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia", clsLib.CompetenciaInt(pParametroValoracao.Competencia));
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa", pParametroValoracao.Cod_Empresa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", pParametroValoracao.Cod_Programa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Duracao", pParametroValoracao.Duracao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Vlr_Parametro", pParametroValoracao.Vlr_Parametro);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Indica_Desativado", pParametroValoracao.Indica_Desativado);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_TipoParametroValoracao", pParametroValoracao.TipoParametroValoracao);
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


        public DataTable ParametroExcluirTipoComercial(Parametro_Tipo_Comercial_Model pExcluirTipoComercial)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {
                //SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_ParametroValoracao_Excluir_TipoComercial");
                //cmd.Parameters.AddWithValue("@Par_Competencia", clsLib.CompetenciaInt(pExcluirTipoComercial.Competencia));
                //cmd.Parameters.AddWithValue("@Par_Cod_Empresa", pExcluirTipoComercial.Cod_Empresa);
                //cmd.Parameters.AddWithValue("@Par_Cod_Programa", pExcluirTipoComercial.Cod_Programa);
                //cmd.Parameters.AddWithValue("@Par_Cod_Tipo_Comercial", pExcluirTipoComercial.Cod_Tipo_Comercial);

                //cmd.ExecuteNonQuery();

                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_ParametroValoracao_Excluir_TipoComercial");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia", clsLib.CompetenciaInt(pExcluirTipoComercial.Competencia));
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa", pExcluirTipoComercial.Cod_Empresa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", pExcluirTipoComercial.Cod_Programa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Tipo_Comercial", pExcluirTipoComercial.Cod_Tipo_Comercial);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_TipoParametroValoracao", pExcluirTipoComercial.TipoParametroValoracao);

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

        public DataTable ParametroExcluirDuracao(Parametro_Duracao_Model pExcluirDuracao)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {

                SqlCommand cmd = cnn.Procedure(cnn.Connection, "PR_PROPOSTA_ParametroValoracao_Excluir_Duracao");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia", clsLib.CompetenciaInt(pExcluirDuracao.Competencia));
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Empresa", pExcluirDuracao.Cod_Empresa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Programa", pExcluirDuracao.Cod_Programa);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Duracao", pExcluirDuracao.Duracao);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_TipoParametroValoracao", pExcluirDuracao.TipoParametroValoracao);
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
        public DataTable ParametroValoracaoExportar(Exportar_Model pParam)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            try
            {

                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Exporta_Parametro_Valoracao");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login",this. CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia_Origem", clsLib.CompetenciaInt(pParam.Competencia_Origem));
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia_Destino", clsLib.CompetenciaInt(pParam.Competencia_Destino));
                Adp.SelectCommand.Parameters.AddWithValue("@Par_NOR", pParam.NOR);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_MER", pParam.MER);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_MOL", pParam.MOL);
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
