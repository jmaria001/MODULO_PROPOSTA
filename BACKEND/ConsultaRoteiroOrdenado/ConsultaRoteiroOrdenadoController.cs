using System;
using System.Web.Http;
using System.Data;
using System.Collections.Generic;

namespace PROPOSTA
{
    public class ConsultaRoteiroOrdenadoController : ApiController
    {
        //=================================Lista de Roteiros
        [Route("api/ConsultaRoteiroOrdenado/GuiaProgramacao")]
        [HttpPost]
        [ActionName("ConsultaRoteiroOrdenadoGuiaProgramacao")]
        [Authorize()]
        public IHttpActionResult ConsultaRoteiroOrdenadoGuiaProgramacao([FromBody]ConsultaRoteiroOrdenado.ConsultaRoteiroOrdenadoFiltroModel Filtro)
        {
            SimLib clsLib = new SimLib();
            ConsultaRoteiroOrdenado Cls = new ConsultaRoteiroOrdenado(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.CarregarGuiaProgramacao(Filtro);
                return Ok(dtb);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }


        //=================================Lista de Roteiros
        [Route("api/ConsultaRoteiroOrdenado/CarregarRoteiro")]
        [HttpPost]
        [ActionName("CarregarRoteiro")]
        [Authorize()]
        public IHttpActionResult CarregarRoteiro([FromBody]ConsultaRoteiroOrdenado.ConsultaRoteiroOrdenadoFiltroModel Filtro)
        {
            SimLib clsLib = new SimLib();
            ConsultaRoteiroOrdenado Cls = new ConsultaRoteiroOrdenado(User.Identity.Name);
            try
            {
                List<ConsultaRoteiroOrdenado.ConsultaRoteiroOrdenadoModel> Roteiro = Cls.RoteiroCarregar(Filtro);
                return Ok(Roteiro);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }




    }

}

