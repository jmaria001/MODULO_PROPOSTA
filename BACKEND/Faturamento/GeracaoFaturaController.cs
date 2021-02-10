using System;
using System.Web.Http;
using System.Data;
using System.Collections.Generic;

namespace PROPOSTA
{
    public class GeracaoFaturaController : ApiController
    {
        //=================================Lista de Itens de ContratosFaturaLista
        [Route("api/ContratosFaturaLista")]
        [HttpGet]
        [ActionName("ContratosFaturaLista")]
        [Authorize()]


        public IHttpActionResult ContratosFaturaLista([FromUri]GeracaoFatura.FiltroModel pFiltro)
        {
            SimLib clsLib = new SimLib();
            GeracaoFatura Cls = new GeracaoFatura(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.ContratosFaturaLista(pFiltro);
                return Ok(dtb);

            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }


        //===========================Incluir solicitação de fatura
        [Route("api/IncluirSolicitacao")]
        [HttpPost]
        [ActionName("IncluirSolicitacao")]
        [Authorize()]

        // aqui se vc ta passando uma lista, tem que receber a lista
        public IHttpActionResult IncluirSolicitacao([FromBody] List<GeracaoFatura.SolicitacaoFaturaModel> pContratos)
        {
            SimLib clsLib = new SimLib();
            GeracaoFatura Cls = new GeracaoFatura(User.Identity.Name);
            try
            {
                //aqui ta dando erro porque o incluirsolicitacao ta esperando um model SolicitacaoFaturaModel entao tem que mudar la tambem para lista
                DataTable retorno = Cls.IncluirSolicitacao(pContratos);
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
