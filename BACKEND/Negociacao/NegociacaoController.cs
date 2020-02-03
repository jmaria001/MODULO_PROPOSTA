using System;
using System.Web.Http;
using System.Data;
using System.Collections.Generic;
using System.Globalization;

namespace PROPOSTA
{
    public class NegociacaoController : ApiController
    {
        [Route("api/Negociacao/List")]
        [HttpGet]
        [ActionName("NegociacaoList")]
        [Authorize()]
        public IHttpActionResult NegociacaoList([FromUri]Negociacao.NegociacaoFiltroParam Param)
        {
            SimLib clsLib = new SimLib();
            Negociacao Cls = new Negociacao(User.Identity.Name);
            try
            {
                List<Negociacao.NegociacaoModel> Retorno = Cls.NegociacaoList(Param);
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        [Route("api/Negociacao/Detalhe")]
        [HttpGet]
        [ActionName("NegociacaoDetalhe")]
        [Authorize()]
        public IHttpActionResult NegociacaoDetalhe([FromUri]Negociacao.NegociacaoFiltroParam Param)
        {
            SimLib clsLib = new SimLib();
            Negociacao Cls = new Negociacao(User.Identity.Name);
            try
            {
                DataTable Retorno = Cls.NegociacaoDetalhe(Param.Numero_Negociacao);
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

