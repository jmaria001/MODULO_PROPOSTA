using System;
using System.Web.Http;
using System.Data;
using System.Collections.Generic;

namespace PROPOSTA
{
    public class BaixaContratoController : ApiController
    {
        //=================================Item de GetContratoBaixa
        [Route("api/BaixaContrato/GetContratoBaixa")]
        [HttpPost]
        [ActionName("GetContratoBaixa")]
        [Authorize()]


        public IHttpActionResult GetContratoBaixa([FromBody]BaixaContrato.FiltroModel pFiltro)
        {
            SimLib clsLib = new SimLib();
            BaixaContrato Cls = new BaixaContrato(User.Identity.Name);
            try
            {
                BaixaContrato.BaixaContratoModel dtb = Cls.GetContratoBaixa(pFiltro);
                return Ok(dtb);

            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //===========================Salvar ContratoBaixa
        [Route("api/BaixaContrato/Baixar")]
        [HttpPost]
        [ActionName("SalvarContratoBaixa")]
        [Authorize()]

        public IHttpActionResult SalvarContratoBaixa([FromBody] BaixaContrato.BaixaContratoModel pContrato)
        {
            SimLib clsLib = new SimLib();
            BaixaContrato Cls = new BaixaContrato(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.SalvarContratoBaixa(pContrato);
                return Ok(retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        //===========================Buscar Programas do Contrato
        [Route("api/BaixaContrato/GetProgramaContrato")]
        [HttpPost]
        [ActionName("GetProgramaContrato")]
        [Authorize()]

        public IHttpActionResult GetProgramaContrato([FromBody] BaixaContrato.FiltroModel pFiltro)
        {
            SimLib clsLib = new SimLib();
            BaixaContrato Cls = new BaixaContrato(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.GetProgramaContrato(pFiltro);
                return Ok(retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        //===========================Buscar Comercials do Contrato
        [Route("api/BaixaContrato/GetComercialContrato")]
        [HttpPost]
        [ActionName("GetComercialContrato")]
        [Authorize()]

        public IHttpActionResult GetComercialContrato([FromBody] BaixaContrato.FiltroModel pFiltro)
        {
            SimLib clsLib = new SimLib();
            BaixaContrato Cls = new BaixaContrato(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.GetComercialContrato(pFiltro);
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
