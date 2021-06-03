using System;
using System.Web.Http;
using System.Data;
using System.Collections.Generic;

namespace PROPOSTA
{
    public class CalculoValoracaoController : ApiController
    {
        //=================================Validar Contrato
        [Route("api/ValidarContrato")]
        [HttpPost]
        [ActionName("ValidarContrato")]
        [Authorize()]


        public IHttpActionResult ValidarContrato([FromBody]CalculoValoracao.CalculoValoracaoModel pValidarContrato)
        {
            SimLib clsLib = new SimLib();
            CalculoValoracao Cls = new CalculoValoracao(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.ValidarContrato(pValidarContrato);
                return Ok(dtb);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }


        //=================================Validar Contrato
        [Route("api/ValidarNegociacao")]
        [HttpPost]
        [ActionName("ValidarNegociacao")]
        [Authorize()]


        public IHttpActionResult ValidarNegociacao([FromBody]CalculoValoracao.CalculoValoracaoModel pValidarNego)
        {
            SimLib clsLib = new SimLib();
            CalculoValoracao Cls = new CalculoValoracao(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.ValidarNegociacao(pValidarNego);
                return Ok(dtb);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //=================================Salvar Calculo de Valoração
        [Route("api/ValoracaoPendentes")]
        [HttpPost]
        [ActionName("ValoracaoPendentes")]
        [Authorize()]


        public IHttpActionResult SalvarCalculoValoracao([FromBody]CalculoValoracao.CalculoValoracaoModel pNego)
        {
            SimLib clsLib = new SimLib();
            CalculoValoracao Cls = new CalculoValoracao(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.ValoracaoPendentes(pNego);
                return Ok(dtb);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        [Route("api/ValoracaoContratos")]
        [HttpPost]
        [ActionName("ValoracaoContratos")]
        [Authorize()]

        public IHttpActionResult ValoracaoContratos([FromBody]List<CalculoValoracao.CalculoValoracaoModel> pContrato)
        {
            SimLib clsLib = new SimLib();
            CalculoValoracao Cls = new CalculoValoracao(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.ValoracaoContratos(pContrato);
                return Ok(retorno);



            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }


        [Route("api/ValoracaoContratosNego")]
        [HttpPost]
        [ActionName("ValoracaoContratosNego")]
        [Authorize()]

        public IHttpActionResult ValoracaoContratosNego([FromBody]CalculoValoracao.CalculoValoracaoModel pContrato)
        {
            SimLib clsLib = new SimLib();
            CalculoValoracao Cls = new CalculoValoracao(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.ValoracaoContratosNego(pContrato);
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