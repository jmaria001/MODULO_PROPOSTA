using System;
using System.Web.Http;
using System.Data;
using System.Collections.Generic;
namespace PROPOSTA
{
    public class GeracaoCEController : ApiController
    {
        //--------------- Listar Veículos - Geração de CE ---------------------------
        [Route("api/GeracaoCEListaVeiculos")]
        [HttpPost]
        [ActionName("GeracaoCEListaVeiculos")]
        [Authorize()]
        public IHttpActionResult GeracaoCEListaVeiculos()
        {
            SimLib clsLib = new SimLib();
            GeracaoCE Cls = new GeracaoCE(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.GeracaoCEListaVeiculos(0);
                return Ok(dtb);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //----------------------- Geração de Comprovante / Carrega Contratos Pendentes -------------------------
        [Route("api/GeraCE_CarregaPendentes")]
        [HttpPost]
        [ActionName("GeraCE_CarregaPendentes")]
        [Authorize()]
        public IHttpActionResult GeraCE_CarregaPendentes([FromBody] GeracaoCE.GeracaoCEModel pParam)
        {
            SimLib clsLib = new SimLib();
            GeracaoCE Cls = new GeracaoCE(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.GeraCE_CarregaPendentes(pParam);
                return Ok(dtb);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //----------------------- Carrega Criticas -------------------------
        [Route("api/Carrega_Criticas")]
        [HttpPost]
        [ActionName("Carrega_Criticas")]
        [Authorize()]
        public IHttpActionResult Carrega_Criticas([FromBody] String pParam)
        {
            SimLib clsLib = new SimLib();
            GeracaoCE Cls = new GeracaoCE(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.Carrega_Criticas(pParam);
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



