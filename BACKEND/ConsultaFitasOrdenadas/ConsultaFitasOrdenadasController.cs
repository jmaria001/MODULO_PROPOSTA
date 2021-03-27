using System;
using System.Web.Http;
using System.Data;
using System.Collections.Generic;
namespace PROPOSTA
{
    public class ConsultaFitasOrdenadasController : ApiController
    {
        //=================================Lista de Veículos a serem baixados
        [Route("api/ConsultaFitasOrdenadasListar")]
        [HttpGet]
        [ActionName("ConsultaFitasOrdenadasListar")]
        [Authorize()]


        public IHttpActionResult ConsultaFitasOrdenadasListar([FromUri]ConsultaFitasOrdenadas.FiltroModel filtro)
        {
            SimLib clsLib = new SimLib();
            ConsultaFitasOrdenadas Cls = new ConsultaFitasOrdenadas(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.ConsultaFitasOrdenadasListar(filtro);
                return Ok(dtb);
                // return Ok(filtro);

            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }


    }




}