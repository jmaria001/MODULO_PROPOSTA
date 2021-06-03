using System;
using System.Web.Http;
using System.Data;
using System.Collections.Generic;

namespace PROPOSTA
{
    public class PesquisaFaturamentoController : ApiController
    {
        //=================================Lista de Itens de FaturasListar
        [Route("api/FaturasListar")]
        [HttpPost]
        [ActionName("FaturasListar")]
        [Authorize()]


        public IHttpActionResult FaturasListar([FromBody]PesquisaFaturamento.FiltroModel pFiltro)
        {
            SimLib clsLib = new SimLib();
            PesquisaFaturamento Cls = new PesquisaFaturamento(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.FaturasListar(pFiltro);
                return Ok(dtb);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        //=================================Get Dados da Fatura
        [Route("api/FaturaGet")]
        [HttpPost]
        [ActionName("FaturaGet")]
        [Authorize()]


        public IHttpActionResult FaturaGet([FromBody]PesquisaFaturamento.FaturaModel pFiltro)
        {
            SimLib clsLib = new SimLib();
            PesquisaFaturamento Cls = new PesquisaFaturamento(User.Identity.Name);
            try
            {  
                PesquisaFaturamento.FaturaModel Retorno = Cls.FaturaGet(pFiltro);
                return Ok(Retorno);
                
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }


        //=================================Cancela Fatura
        [Route("api/FaturaCancelar")]
        [HttpPost]
        [ActionName("FaturaCancelar")]
        [Authorize()]
        public IHttpActionResult FaturaCancelar([FromBody]PesquisaFaturamento.FaturaModel pFiltro)
        {
            SimLib clsLib = new SimLib();
            PesquisaFaturamento Cls = new PesquisaFaturamento(User.Identity.Name);
            try
            {
                DataTable Retorno = Cls.FaturaCancelar(pFiltro);
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
