using System;
using System.Web.Http;
using System.Data;
using System.Collections.Generic;
namespace PROPOSTA
{
    public class BaixaSiteController : ApiController
    {

        //=================================Lista de Veículos a serem baixados
        [Route("api/BaixaSite/CarregarVeiculacao")]
        [HttpPost]
        [ActionName("CarregarVeiculacao")]
        [Authorize()]


        public IHttpActionResult CarregarVeiculacao([FromBody]BaixaSite.FiltroModel filtro)
        {
            SimLib clsLib = new SimLib();
            BaixaSite Cls = new BaixaSite(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.CarregarVeiculacao(filtro);
                return Ok(dtb);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        //=================================Lista de Veículos a serem baixados
        [Route("api/BaixaSite/BaixarVeiculacao")]
        [HttpPost]
        [ActionName("BaixarVeiculacao")]
        [Authorize()]

        public IHttpActionResult BaixarVeiculacao([FromBody] List<BaixaSite.BaixaModel> Param)
        {
            SimLib clsLib = new SimLib();
            BaixaSite Cls = new BaixaSite(User.Identity.Name);
            try
            {
                List <BaixaSite.BaixaModel> Retorno = Cls.BaixarVeiculacao(Param);
                return Ok(Param);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

    }
}