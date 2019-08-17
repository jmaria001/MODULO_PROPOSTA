
using System;
using System.Data;
using System.Net.Http;
using System.Web.Http;

namespace PROPOSTA
{
    public class CredentialController : ApiController
    {

        [Route("api/Credential/{pRouteId}")]
        [HttpGet]
        [ActionName("Credential")]
        [Authorize()]
        //public IHttpActionResult Permissao([FromBody] apiCredential.ParamCredential Param)
        public IHttpActionResult Permissao(String pRouteId)
        {
            apiCredential Cls = new apiCredential(User.Identity.Name);
            SimLib clsLib = new SimLib();
            Boolean retorno = false;
            try
            {
                retorno = Cls.Permissao(pRouteId);
                return Ok(retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        [Route("api/GetUserData")]
        [HttpGet]
        [ActionName("GetUserData")]
        [Authorize()]
        public IHttpActionResult GetUserData()
        {
            apiCredential Cls = new apiCredential(User.Identity.Name);
            SimLib clsLib = new SimLib();
            try
            {
                DataTable dtbRetorno = Cls.GetUserData();
                return Ok(dtbRetorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }

        }

        [Route("api/NewPassword")]
        [HttpPost]
        [ActionName("NewPassword")]
        //[Authorize()]
        public IHttpActionResult NewPassword([FromBody] apiCredential.RememberPassword Param)
        {

            apiCredential Cls = new apiCredential(User.Identity.Name);
            try
            {
                String Retorno = Cls.EsqueceuSenha(Param);

                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        [Route("api/SetPassword")]
        [HttpPost]
        [ActionName("SetPassword")]
        //[Authorize()]
        public IHttpActionResult SetPassword([FromBody] apiCredential.RememberPassword Param)
        {

            apiCredential Cls = new apiCredential(User.Identity.Name);
            try
            {
                String Retorno = Cls.AlterarSenha(Param);

                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }


    }
}
   