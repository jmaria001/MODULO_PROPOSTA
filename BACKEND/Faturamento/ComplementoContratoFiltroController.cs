using System;
using System.Web.Http;
using System.Data;
using System.Collections.Generic;

namespace PROPOSTA
{
    public class ComplementoContratoFiltroController : ApiController
    {
        //=================================Lista de Itens de ContratosComplementoListar
        [Route("api/ContratosComplementoListar")]
        [HttpPost]
        [ActionName("ContratosComplementoListar")]
        [Authorize()]


        public IHttpActionResult ContratosComplementoListar([FromBody]ComplementoContratoFiltro.FiltroModel pFiltro)
        {
            SimLib clsLib = new SimLib();
            ComplementoContratoFiltro Cls = new ComplementoContratoFiltro(User.Identity.Name);
            try
            {
                DataTable dtb = new DataTable();
                if (pFiltro.Origem==1) /*Midia''*/
                {
                    dtb = Cls.ContratosComplementoMidiaListar(pFiltro);
                }
                if (pFiltro.Origem== 0) /*Antecipado''*/
                {
                    dtb = Cls.ContratosComplementoAntecipadoListar(pFiltro);
                }

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
