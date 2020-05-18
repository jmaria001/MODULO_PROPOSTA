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
        [Route("api/Negociacao/Contar")]
        [HttpGet]
        [ActionName("NegociacaoContar")]
        [Authorize()]
        public IHttpActionResult NegociacaoContar()
        {
            SimLib clsLib = new SimLib();
            Negociacao Cls = new Negociacao(User.Identity.Name);
            try
            {
                 Negociacao.NegociacaoCountModel Retorno = Cls.NegociacaoContar();
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        [Route("api/ImprimirMapa/{Id_Contrato}")]
        [HttpGet]
        [ActionName("ImprimirMapa")]
        [Authorize()]
        public IHttpActionResult ImprimirMapa(Int32 Id_Contrato)
        {
            SimLib clsLib = new SimLib();
            try
            {
                ImpressaoMapa Cls = new ImpressaoMapa(User.Identity.Name);

                return Ok(Cls.ImprimirMapa(Id_Contrato));
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        [Route("api/Negociacao/Get")]
        [HttpGet]
        [ActionName("NegociacaoGet")]
        [Authorize()]
        public IHttpActionResult NegociacaoGet([FromUri]Negociacao.NegociacaoModel Param)
        {
            SimLib clsLib = new SimLib();
            Negociacao Cls = new Negociacao(User.Identity.Name);
            try
            {
                Negociacao.NegociacaoModel Retorno = new Negociacao.NegociacaoModel();
                if (Param.Numero_Negociacao==0)
                {
                    Retorno.Empresas_Venda = new List<Negociacao.NegociacaoEmpresaVendaModel>();
                    Retorno.Empresas_Faturamento = new List<Negociacao.NegociacaoEmpresaFaturamentoModel>();
                    Retorno.Intermediarios = new List<Negociacao.NegociacaoIntermediarioModel>();
                }
                else
                {
                    Retorno = Cls.NegociacaoGet(Param);
                }
                
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

