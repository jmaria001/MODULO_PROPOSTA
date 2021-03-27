using System;
using System.Web.Http;
using System.Data;
using System.Collections.Generic;

namespace PROPOSTA
{
    public class ReabreCEController : ApiController
    {
        //=================================Lista de Itens de Roteiro Exibir
        [Route("api/ExecutarReabreCE")]
        [HttpGet]
        [ActionName("ExecutarReabreCE")]
        [Authorize()]


        public IHttpActionResult RateioConsultaGet([FromUri]ReabreCE.FiltroReabreCEModel pFiltro)
        {
            SimLib clsLib = new SimLib();
            ReabreCE Cls = new ReabreCE(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.ExecutarReabreCE(pFiltro);
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



