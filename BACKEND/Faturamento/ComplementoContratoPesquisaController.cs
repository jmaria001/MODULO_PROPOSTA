using System;
using System.Web.Http;
using System.Data;
using System.Collections.Generic;

namespace PROPOSTA
{
    public class ComplementoContratoPesquisaController : ApiController
    {
        //=================================Lista de Itens de ComplementosPesquisar
        [Route("api/ComplementosPesquisar")]
        [HttpPost]
        [ActionName("ComplementosPesquisar")]
        [Authorize()]


        public IHttpActionResult ComplementosPesquisar([FromBody]ComplementoContratoPesquisa.FiltroModel pFiltro)
        {
            SimLib clsLib = new SimLib();
            ComplementoContratoPesquisa Cls = new ComplementoContratoPesquisa(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.ComplementosPesquisar(pFiltro);
                return Ok(dtb);

            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        //=================================Dados de Um Complemento
        [Route("api/ComplementosGet/{Id}")]
        [HttpGet]
        [ActionName("ComplementosGet")]
        [Authorize()]


        public IHttpActionResult ComplementosGet(Int32 Id)
        {
            SimLib clsLib = new SimLib();
            ComplementoContratoPesquisa Cls = new ComplementoContratoPesquisa(User.Identity.Name);
            try
            {
                ComplementoContratoPesquisa.ComplementoModel Retorno = Cls.ComplementosGet(Id);
                return Ok(Retorno);

            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //=================================Exclusao de Complemento
        [Route("api/ExcluirComplemento")]
        [HttpPost]
        [ActionName("ExcluirComplemento")]
        [Authorize()]


        public IHttpActionResult ExcluirComplemento([FromBody]ComplementoContratoPesquisa.ComplementoModel pFiltro)
        {
            SimLib clsLib = new SimLib();
            ComplementoContratoPesquisa Cls = new ComplementoContratoPesquisa(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.ExcluirComplemento(pFiltro);
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
