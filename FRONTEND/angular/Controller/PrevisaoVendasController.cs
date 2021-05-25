using System;
using System.Web.Http;
using System.Data;
using System.Collections.Generic;

namespace PROPOSTA
{
    public class PrevisaoVendasController : ApiController
    {
        //=================================Carregar Previsao por Agencia
        [Route("api/CarregarPrevisaoVendasAgencia")]
        [HttpPost]
        [ActionName("CarregarPrevisaoVendasAgencia")]
        [Authorize()]


        public IHttpActionResult CarregarPrevisaoVendasAgencia([FromBody]PrevisaoVendas.FiltroModel filtro)
        {
            SimLib clsLib = new SimLib();
            PrevisaoVendas Cls = new PrevisaoVendas((User.Identity.Name));
            try
            {
                DataTable Retorno = Cls.CarregarPrevisaoVendasAgencia(filtro);
                return Ok(Retorno);

            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //=================================Carregar Previsao por Veiculo
        [Route("api/CarregarPrevisaoVendasVeiculo")]
        [HttpPost]
        [ActionName("CarregarPrevisaoVendasVeiculo")]
        [Authorize()]


        public IHttpActionResult CarregarPrevisaoVendasVeiculo([FromBody]PrevisaoVendas.FiltroModel filtro)
        {
            SimLib clsLib = new SimLib();
            PrevisaoVendas Cls = new PrevisaoVendas((User.Identity.Name));
            try
            {
                DataTable Retorno = Cls.CarregarPrevisaoVendasVeiculo(filtro);
                return Ok(Retorno);

            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //=================================Carregar Previsao por Mensal
        [Route("api/CarregarPrevisaoVendasMensal")]
        [HttpPost]
        [ActionName("CarregarPrevisaoVendasMensal")]
        [Authorize()]


        public IHttpActionResult CarregarPrevisaoVendasMensal([FromBody]PrevisaoVendas.FiltroModel filtro)
        {
            SimLib clsLib = new SimLib();
            PrevisaoVendas Cls = new PrevisaoVendas((User.Identity.Name));
            try
            {
                DataTable Retorno = Cls.CarregarPrevisaoVendasMensal(filtro);
                return Ok(Retorno);

            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //=================================Carregar Previsao por Mensal
        [Route("api/CarregarPrevisaoCadastroMensal")]
        [HttpPost]
        [ActionName("CarregarPrevisaoCadastroMensal")]
        [Authorize()]


        public IHttpActionResult CarregarPrevisaoCadastroMensal([FromBody]PrevisaoVendas.FiltroModel filtro)
        {
            SimLib clsLib = new SimLib();
            PrevisaoVendas Cls = new PrevisaoVendas((User.Identity.Name));
            try
            {
                List<PrevisaoVendas.PrevisaVendasMensalModel> Previsao = new List<PrevisaoVendas.PrevisaVendasMensalModel>();
                DataTable dtb = Cls.CarregarPrevisaoVendasMensal(filtro);
                foreach (DataRow drw in dtb.Rows)
                {
                    Previsao.Add(new PrevisaoVendas.PrevisaVendasMensalModel()
                    {
                        Cod_Contato = filtro.Cod_Contato,
                        Tipo_Linha = drw["Tipo_Linha"].ToString().ConvertToByte(),
                        Competencia = drw["Competencia"].ToString().ConvertToInt32(),
                        Ano = drw["Ano"].ToString().ConvertToInt32(),
                        Mes = drw["Competencia_Text"].ToString(),
                        Valor_Previsao = drw["Valor_Total"].ToString().ConvertToMoney(),
                    });
                }

                return Ok(Previsao);

            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //=================================Carregar Historico Previsao por Mensal
        [Route("api/CarregarHistoricoMensal")]
        [HttpPost]
        [ActionName("CarregarHistoricoMensal")]
        [Authorize()]

        public IHttpActionResult CarregarHistoricoMensal([FromBody] List<PrevisaoVendas.PrevisaVendasMensalModel> Param)
        {
            SimLib clsLib = new SimLib();
            PrevisaoVendas Cls = new PrevisaoVendas((User.Identity.Name));
            try
            {
                List<PrevisaoVendas.PrevisaVendasMensalModel> Previsao = Cls.CarregarHistoricoMensal(Param);
                return Ok(Previsao);

            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }


        //===========================Salvar Previsao de Vendas Mensal


        [Route("api/SalvarPrevisaoVendasMensal")]
        [HttpPost]
        [ActionName("SalvarPrevisaoVendasMensal")]
        [Authorize()]

        public IHttpActionResult SalvarPrevisaoVendasMensal([FromBody] List<PrevisaoVendas.PrevisaVendasMensalModel> pPrevisao)
        {
            SimLib clsLib = new SimLib();
            PrevisaoVendas Cls = new PrevisaoVendas(User.Identity.Name);

            try
            {
                //PrevisaoVendas.GravarPrevisaVendasMensalMModel retorno = Cls.SalvarPrevisaoVendasMensal(pPrevisao);
                Boolean retorno = Cls.SalvarPrevisaoVendasMensal(pPrevisao);
                return Ok(retorno);

            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //=================================Carregar Previsao por Agencia - Cadastro
        [Route("api/CarregarPrevisaoCadastroAgencia")]
        [HttpPost]
        [ActionName("CarregarPrevisaoCadastroAgencia")]
        [Authorize()]


        public IHttpActionResult CarregarPrevisaoCadastroAgencia([FromBody]PrevisaoVendas.FiltroModel filtro)
        {
            SimLib clsLib = new SimLib();
            PrevisaoVendas Cls = new PrevisaoVendas((User.Identity.Name));
            try
            {
                List<PrevisaoVendas.PrevisaoVendasAgenciaModel> Previsao = new List<PrevisaoVendas.PrevisaoVendasAgenciaModel>();
                DataTable dtb = Cls.CarregarPrevisaoVendasAgencia(filtro);

                foreach (DataRow drw in dtb.Rows)
                {
                    Previsao.Add(new PrevisaoVendas.PrevisaoVendasAgenciaModel()
                    {
                        Tipo_Linha = drw["Tipo_Linha"].ToString().ConvertToByte(),
                        Cod_Contato = drw["Cod_Contato"].ToString(),
                        Cod_Agencia = drw["Cod_Agencia"].ToString(),
                        Nome_Agencia = drw["Nome_Agencia"].ToString(),
                        Cod_Cliente = drw["Cod_Cliente"].ToString(),
                        Nome_Cliente = drw["Nome_Cliente"].ToString(),
                        Ano = filtro.Competencia.ConvertToInt32(),
                        Valor_Jan = drw["Valor_Jan"].ToString().ConvertToMoney(),
                        Valor_Fev = drw["Valor_Fev"].ToString().ConvertToMoney(),
                        Valor_Mar = drw["Valor_Mar"].ToString().ConvertToMoney(),
                        Valor_Abr = drw["Valor_Abr"].ToString().ConvertToMoney(),
                        Valor_Mai = drw["Valor_Mai"].ToString().ConvertToMoney(),
                        Valor_Jun = drw["Valor_Jun"].ToString().ConvertToMoney(),
                        Valor_Jul = drw["Valor_Jul"].ToString().ConvertToMoney(),
                        Valor_Ago = drw["Valor_Ago"].ToString().ConvertToMoney(),
                        Valor_Set = drw["Valor_Set"].ToString().ConvertToMoney(),
                        Valor_Out = drw["Valor_Out"].ToString().ConvertToMoney(),
                        Valor_Nov = drw["Valor_Nov"].ToString().ConvertToMoney(),
                        Valor_Dez = drw["Valor_Dez"].ToString().ConvertToMoney(),
                        Valor_Total = drw["Valor_Total"].ToString().ConvertToMoney(),
                    });
                }

                return Ok(Previsao);

            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        //=================================Carregar Historico Previsao por Agencia
        [Route("api/CarregarHistoricoAgencia")]
        [HttpPost]
        [ActionName("CarregarHistoricoAgencia")]
        [Authorize()]

        public IHttpActionResult CarregarHistoricoAgencia([FromBody] List<PrevisaoVendas.PrevisaoVendasAgenciaModel> Param)
        {
            SimLib clsLib = new SimLib();
            PrevisaoVendas Cls = new PrevisaoVendas((User.Identity.Name));
            try
            {
                List<PrevisaoVendas.PrevisaoVendasAgenciaModel> Previsao = Cls.CarregarHistoricoAgencia(Param);
                return Ok(Previsao);

            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //===========================Excluir Previsao AgenciaCliente
        [Route("api/PrevisaoExcluirAgenciaCliente")]
        [HttpPost]
        [ActionName("PrevisaoExcluirAgenciaCliente")]
        [Authorize()]

        public IHttpActionResult PrevisaoExcluirAgenciaCliente([FromBody] PrevisaoVendas.PrevisaoVendasAgenciaModel Param)
        {
            SimLib clsLib = new SimLib();
            PrevisaoVendas Cls = new PrevisaoVendas(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.PrevisaoExcluirAgenciaCliente(Param);
                return Ok(retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //===========================Salvar Previsao de Vendas Agencia/Cliente


        [Route("api/SalvarPrevisaoVendasAgencia")]
        [HttpPost]
        [ActionName("SalvarPrevisaoVendasAgencia")]
        [Authorize()]

        public IHttpActionResult SalvarPrevisaoVendasAgencia([FromBody] List<PrevisaoVendas.PrevisaoVendasAgenciaModel> pPrevisao)
        {
            SimLib clsLib = new SimLib();
            PrevisaoVendas Cls = new PrevisaoVendas(User.Identity.Name);

            try
            {
                //PrevisaoVendas.GravarPrevisaVendasMensalMModel retorno = Cls.SalvarPrevisaoVendasMensal(pPrevisao);
                Boolean retorno = Cls.SalvarPrevisaoVendasAgencia(pPrevisao);
                return Ok(retorno);

            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        [Route("api/CarregarPrevisaoConsisteCompetencia")]
        [HttpPost]
        [ActionName("CarregarPrevisaoConsisteCompetencia")]
        [Authorize()]

        public IHttpActionResult CarregarPrevisaoConsisteCompetencia([FromBody]PrevisaoVendas.FiltroModel filtro)
        {
            SimLib clsLib = new SimLib();
            PrevisaoVendas Cls = new PrevisaoVendas((User.Identity.Name));
            try
            {
                Boolean Retorno = Cls.CarregarPrevisaoConsisteCompetencia(filtro);
                return Ok(Retorno);

            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }


        [Route("api/PrevisaVendaNewAgencia")]
        [HttpPost]
        [ActionName("PrevisaVendaNewAgencia")]
        [Authorize()]

        public IHttpActionResult PrevisaVendaNewAgencia([FromBody]PrevisaoVendas.FiltroModel filtro)
        {
            SimLib clsLib = new SimLib();
            PrevisaoVendas Cls = new PrevisaoVendas((User.Identity.Name));
            try
            {
                PrevisaoVendas.PrevisaoVendasAgenciaModel Retorno = new PrevisaoVendas.PrevisaoVendasAgenciaModel();
                Retorno.Tipo_Linha = 1;
                Retorno.Cod_Agencia = "";
                Retorno.Cod_Cliente= "";
                Retorno.Ano = filtro.Competencia.ConvertToInt32();
                Retorno.Cod_Contato = filtro.Cod_Contato;
                return Ok(Retorno);

            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }


        /// Procedure definido para Veículo ///////////////////////////////////////////////////////////////////////////////////////////////////



        //===========================Salvar Previsao de Vendas Veiculo


        //=================================Carregar Previsao por Veiculo - Cadastro
        [Route("api/CarregarPrevisaoCadastroVeiculo")]
        [HttpPost]
        [ActionName("CarregarPrevisaoCadastroVeiculo")]
        [Authorize()]


        public IHttpActionResult CarregarPrevisaoCadastroVeiculo([FromBody]PrevisaoVendas.FiltroModel filtro)
        {
            SimLib clsLib = new SimLib();
            PrevisaoVendas Cls = new PrevisaoVendas((User.Identity.Name));
            try
            {
                List<PrevisaoVendas.PrevisaoVendasVeiculoModel> Previsao = new List<PrevisaoVendas.PrevisaoVendasVeiculoModel>();
                DataTable dtb = Cls.CarregarPrevisaoVendasVeiculo(filtro);

                foreach (DataRow drw in dtb.Rows)
                {
                    Previsao.Add(new PrevisaoVendas.PrevisaoVendasVeiculoModel()
                    {
                        Tipo_Linha = drw["Tipo_Linha"].ToString().ConvertToByte(),
                        Cod_Contato = drw["Cod_Contato"].ToString(),
                        Cod_Veiculo = drw["Cod_Veiculo"].ToString(),
                        Nome_Veiculo = drw["Nome_Veiculo"].ToString(),
                        Ano = filtro.Competencia.ConvertToInt32(),
                        Valor_Jan = drw["Valor_Jan"].ToString().ConvertToMoney(),
                        Valor_Fev = drw["Valor_Fev"].ToString().ConvertToMoney(),
                        Valor_Mar = drw["Valor_Mar"].ToString().ConvertToMoney(),
                        Valor_Abr = drw["Valor_Abr"].ToString().ConvertToMoney(),
                        Valor_Mai = drw["Valor_Mai"].ToString().ConvertToMoney(),
                        Valor_Jun = drw["Valor_Jun"].ToString().ConvertToMoney(),
                        Valor_Jul = drw["Valor_Jul"].ToString().ConvertToMoney(),
                        Valor_Ago = drw["Valor_Ago"].ToString().ConvertToMoney(),
                        Valor_Set = drw["Valor_Set"].ToString().ConvertToMoney(),
                        Valor_Out = drw["Valor_Out"].ToString().ConvertToMoney(),
                        Valor_Nov = drw["Valor_Nov"].ToString().ConvertToMoney(),
                        Valor_Dez = drw["Valor_Dez"].ToString().ConvertToMoney(),
                        Valor_Total = drw["Valor_Total"].ToString().ConvertToMoney(),
                    });
                }

                return Ok(Previsao);

            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        //=================================Carregar Historico Previsao por Veiculo
        [Route("api/CarregarHistoricoVeiculo")]
        [HttpPost]
        [ActionName("CarregarHistoricoVeiculo")]
        [Authorize()]

        public IHttpActionResult CarregarHistoricoVeiculo([FromBody] List<PrevisaoVendas.PrevisaoVendasVeiculoModel> Param)
        {
            SimLib clsLib = new SimLib();
            PrevisaoVendas Cls = new PrevisaoVendas((User.Identity.Name));
            try
            {
                List<PrevisaoVendas.PrevisaoVendasVeiculoModel> Previsao = Cls.CarregarHistoricoVeiculo(Param);
                return Ok(Previsao);

            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //===========================Excluir Previsao Veiculo
        [Route("api/PrevisaoExcluirVeiculo")]
        [HttpPost]
        [ActionName("PrevisaoExcluirVeiculo")]
        [Authorize()]

        public IHttpActionResult PrevisaoExcluirVeiculo([FromBody] PrevisaoVendas.PrevisaoVendasVeiculoModel Param)
        {
            SimLib clsLib = new SimLib();
            PrevisaoVendas Cls = new PrevisaoVendas(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.PrevisaoExcluirVeiculo(Param);
                return Ok(retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //===========================Salvar Previsao de Vendas Veiculo
                    
        [Route("api/SalvarPrevisaoVendasVeiculo")]
        [HttpPost]
        [ActionName("SalvarPrevisaoVendasVeiculo")]
        [Authorize()]

        public IHttpActionResult SalvarPrevisaoVendasVeiculo([FromBody] List<PrevisaoVendas.PrevisaoVendasVeiculoModel> pPrevisao)
        {
            SimLib clsLib = new SimLib();
            PrevisaoVendas Cls = new PrevisaoVendas(User.Identity.Name);

            try
            {
                //PrevisaoVendas.GravarPrevisaVendasMensalMModel retorno = Cls.SalvarPrevisaoVendasMensal(pPrevisao);
                Boolean retorno = Cls.SalvarPrevisaoVendasVeiculo(pPrevisao);
                return Ok(retorno);

            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        [Route("api/PrevisaVendaNewVeiculo")]
        [HttpPost]
        [ActionName("PrevisaVendaNewVeiculo")]
        [Authorize()]

        public IHttpActionResult PrevisaVendaNewVeiculo([FromBody]PrevisaoVendas.FiltroModel filtro)
        {
            SimLib clsLib = new SimLib();
            PrevisaoVendas Cls = new PrevisaoVendas((User.Identity.Name));
            try
            {
                PrevisaoVendas.PrevisaoVendasVeiculoModel Retorno = new PrevisaoVendas.PrevisaoVendasVeiculoModel();
                Retorno.Tipo_Linha = 1;
                Retorno.Cod_Veiculo = "";
                Retorno.Ano = filtro.Competencia.ConvertToInt32();
                Retorno.Cod_Contato = filtro.Cod_Contato;
                return Ok(Retorno);

            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }








    }





}



