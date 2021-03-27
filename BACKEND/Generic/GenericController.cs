using System;
using System.Web.Http;
using System.Data;
using System.Globalization;
using System.Data.OleDb;

namespace PROPOSTA
{
    public class GenericController : ApiController
    {

        [Route("api/TesteApi")]
        [HttpGet]
        [ActionName("TesteApi")]
        public IHttpActionResult TesteApi()
        {
            try
            {
                

                return Ok("teste ok");

            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }


        [Route("api/GetDataBaseName")]
        [HttpGet]
        [ActionName("GetDataBaseName")]
        public IHttpActionResult GetDataBaseName()
        {
            try
            {

                SimLib clsLib = new SimLib();
                Generic Cls = new Generic(User.Identity.Name);
                try
                {
                    DataTable dtb = Cls.GetDataBaseName();
                    return Ok(dtb);
                }
                catch (Exception Ex)
                {
                    clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                    throw new Exception(Ex.Message);
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        [Route("api/GetMensagem")]
        [HttpPost]
        [ActionName("GetMensagem")]
        [Authorize()]
        public IHttpActionResult GetMensagem()
        {
            SimLib clsLib = new SimLib();
            Generic Cls = new Generic(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.GetMensagem(clsLib.GetJsonItem(User.Identity.Name, "Name"));
                return Ok(dtb);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }


        [Route("api/VistoMensagem")]
        [HttpPost]
        [ActionName("VistoMensagem")]
        [Authorize()]
        public IHttpActionResult VistoMensagem([FromBody] Generic.VistoMensagemParam Param)
        {
            SimLib clsLib = new SimLib();
            Generic Cls = new Generic(User.Identity.Name);

            try
            {
                Cls.MarcarMensagem(Param.Id_Mensagem);
                return Ok();
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        [Route("api/EnviarMensagem")]
        [HttpPost]
        [ActionName("EnviarMensagem")]
        [Authorize()]
        public IHttpActionResult EnviarMensagem([FromBody] Generic.Mensagem Param)
        {
            SimLib clsLib = new SimLib();
            Generic Cls = new Generic(User.Identity.Name);

            try
            {
                Cls.EnviarMensagem(Param);
                return Ok(true);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        [Route("api/RemoverMensagem")]
        [HttpPost]
        [ActionName("RemoverMensagem")]
        [Authorize()]
        public IHttpActionResult RemoverMensagem([FromBody] Generic.VistoMensagemParam Param)
        {
            SimLib clsLib = new SimLib();
            Generic Cls = new Generic(User.Identity.Name);

            try
            {
                Cls.RemoverMensagem(Param.Id_Mensagem);
                return Ok();
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        

        [Route("api/ListarTabela/{pTabela}")]
        [HttpGet]
        [ActionName("ListarTabela")]
        [Authorize()]
        public IHttpActionResult ListarTabela(String pTabela)
        {
            SimLib clsLib = new SimLib();
            Generic Cls = new Generic(User.Identity.Name);
            try
            {
                DataTable dtbRetorno = Cls.ListarTabela(pTabela );
                return Ok(dtbRetorno);

            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }

        }

        [Route("api/ListarTabela/{pTabela}/{pFilter}")]
        [HttpGet]
        [ActionName("ListarTabela")]
        [Authorize()]
        public IHttpActionResult ListarTabela(String pTabela,String pFilter)
        {
            SimLib clsLib = new SimLib();
            Generic Cls = new Generic(User.Identity.Name);
            try
            {
                DataTable dtbRetorno = Cls.ListarTabela(pTabela,pFilter);
                return Ok(dtbRetorno);

            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }

        }

        [Route("api/ValidarTabela/{pTabela}/{pCodigo}")]
        [HttpGet]
        [ActionName("ValidarTabela")]
        [Authorize()]
        public IHttpActionResult ValidarTabela(String pTabela, String pCodigo)
        {
            SimLib clsLib = new SimLib();
            Generic Cls = new Generic(User.Identity.Name);
            try
            {
                DataTable dtbRetorno = Cls.ValidarTabela(pTabela, pCodigo);
                return Ok(dtbRetorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }

        }
        [Route("api/GetNivelAcesso")]
        [HttpGet]
        [ActionName("GetMensagem")]
        [Authorize()]
        public IHttpActionResult GetNivelAcesso()
        {
            SimLib clsLib = new SimLib();
            Generic Cls = new Generic(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.GetNivelAcesso();
                return Ok(dtb);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
    }

}

