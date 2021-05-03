using System;
using System.Web.Http;
using System.Data;
using System.Collections.Generic;
namespace PROPOSTA
{
    public class CriticaValoracaoController : ApiController
    {
        //----------------------- Get  -------------------------
        [Route("api/CriticaValoracaoGet")]
        [HttpGet]
        [ActionName("CriticaValoracaoGet")]
        [Authorize()]
        public IHttpActionResult CriticaValoracaoGet([FromUri] CriticaValoracao.FiltroModel pParam)
        {
            SimLib clsLib = new SimLib();
            CriticaValoracao Cls = new CriticaValoracao(User.Identity.Name);
            try
            {
                DataTable Retorno = Cls.CriticaValoracaoGet(pParam);
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



