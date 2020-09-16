using System;
using System.Web.Http;
using System.Data;
using System.Collections.Generic;
using System.Globalization;


namespace PROPOSTA
{
    public class AMController : ApiController
    {

        [Route("api/AM/List")]
        [HttpGet]
        [ActionName("AMList")]
        [Authorize()]
        public IHttpActionResult AMList([FromUri]AM.AMFiltroModel Param)
        {
            SimLib clsLib = new SimLib();
            AM Cls = new AM(User.Identity.Name);
            try
            {
                DataTable Retorno = Cls.AMList(Param);
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        [Route("api/AM/AmReencaixe")]
        [HttpGet]
        [ActionName("AmReencaixe")]
        [Authorize()]
        public IHttpActionResult AmReencaixe([FromUri]AM.Reencaixe_Model Param)
        {
            SimLib clsLib = new SimLib();
            AM Cls = new AM(User.Identity.Name);
            try
            {
                DataTable Retorno = Cls.AmReencaixe(Param);
                return Ok(Retorno);



            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }


        [Route("api/Am/ReabrirAM")]
        [HttpPost]
        [Authorize()]
        public IHttpActionResult ReabrirAM([FromBody]  AM.Reencaixe_Model Param)
        {
            SimLib clsLib = new SimLib();
            try
            {
                AM Cls = new AM(User.Identity.Name);
                DataTable Retorno = Cls.ReabrirAM(Param);
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

                     

        [Route("api/AMFalhas")]
        [HttpGet]
        [ActionName("AMFalhas")]
        [Authorize()]
        //public IHttpActionResult AMFalhas(String Cod_Empresa, Int32 Numero_Mr, Int32 Sequencia_Mr, String Documento_Para)
        public IHttpActionResult AMFalhas([FromUri] AM.AMModel Param)
        {
            SimLib clsLib = new SimLib();
            AM Cls = new AM(User.Identity.Name);
            try
            {

                AM.AMModel Retorno = new AM.AMModel();
                if (Param.Cod_Empresa != "0")
                {
                    Retorno = Cls.AMFalhas(Param.Cod_Empresa, Param.Numero_Mr, Param.Sequencia_Mr, Param.Documento_Para);
                }
                else
                {
                    Retorno.Falhas = new List<AM.Falhas_Model>();
                }
                return Ok(Retorno);
                
        }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        [Route("api/AM/PesquisaComerciais")]
        [HttpGet]
        [ActionName("AMPesquisaComerciais")]
        [Authorize()]
        //public IHttpActionResult AMFalhas(String Cod_Empresa, Int32 Numero_Mr, Int32 Sequencia_Mr, String Documento_Para)
        public IHttpActionResult AMPesquisaComerciais([FromUri] AM.AMModel Param)
        {
            SimLib clsLib = new SimLib();
            AM Cls = new AM(User.Identity.Name);
            try
            {
                DataTable Retorno = Cls.AMFalhasPesquisaComerciais(Param.Cod_Empresa, Param.Numero_Mr, Param.Sequencia_Mr, Param.Cod_Comercial);
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        [Route("api/AM/ListarGrade")]
        [HttpGet]
        [ActionName("AMListarGrade")]
        [Authorize()]
        //public IHttpActionResult AMFalhas(String Cod_Empresa, Int32 Numero_Mr, Int32 Sequencia_Mr, String Documento_Para)
        public IHttpActionResult AMListarGrade([FromUri] AM.ParamGradeModel Param)
        {
            SimLib clsLib = new SimLib();
            AM Cls = new AM(User.Identity.Name);
            try
            {
                DataTable Retorno = Cls.AMFalhaListarGrade(Param.Cod_Veiculo,Param.Competencia,Param.Cod_Empresa, Param.Numero_Mr, Param.Sequencia_Mr,Param.Cod_Programa);
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        [Route("api/AM/SalvarCompensacao")]
        [HttpPost]
        [ActionName("AMSalvarCompensacao")]
        [Authorize()]
        public IHttpActionResult AMSalvarCompensacao([FromBody] AM.Reencaixe_Model Param)
        {
            SimLib clsLib = new SimLib();
            AM Cls = new AM(User.Identity.Name);
            try
            {
                DataTable Retorno = Cls.AMFalhaSalvar(Param);
                return Ok(Retorno);
                //return Ok(Param);
            }
            
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }


        [Route("api/Am/Solucao")]
        [HttpPost]
        [ActionName("AmSolucao")]
        [Authorize()]
        public IHttpActionResult AmSolucao([FromBody] AM.Reencaixe_Model Param)
        {
            SimLib clsLib = new SimLib();
            AM Cls = new AM(User.Identity.Name);
            try
            {
                DataTable Retorno = Cls.GravarSolucaoCompensacaoFalhaSalvar(Param);
                return Ok(Retorno);
            }

            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }




        //===========================Excluir Programa

        [Route("api/ExcluirCompensacao")]
        [HttpPost]
        [ActionName("ExcluirCompensacao")]
        [Authorize()]

        public IHttpActionResult ExcluirPrograma([FromBody] AM.Reencaixe_Model pCompensacao)
        {
            SimLib clsLib = new SimLib();
            AM Cls = new AM(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.ExcluirCompensacao(pCompensacao);
                return Ok(retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }


        //===========================Excluir Programa

        [Route("api/AM/EfetuarReencaixe")]
        [HttpPost]
        [ActionName("EfetuarReencaixe")]
        [Authorize()]

        public IHttpActionResult EfetuarReencaixe([FromBody] List<AM.Reencaixe_Model> pReencaixe)
        {
            SimLib clsLib = new SimLib();
            AM Cls = new AM(User.Identity.Name);
            try
            {
                Boolean retorno = Cls.EfetuarReencaixe(pReencaixe);
                return Ok(retorno);

            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }


    }
 }

