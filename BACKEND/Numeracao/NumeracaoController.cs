using System;
using System.Web.Http;
using System.Data;
using System.Collections.Generic;

namespace PROPOSTA
{
    public class NumeracaoController : ApiController
    {
        //=================================Lista de Itens de ContratosFaturaLista
        [Route("api/Numeracao/Listar")]
        [HttpGet]
        [ActionName("NumeracaoListar")]
        [Authorize()]


        public IHttpActionResult NumeracaoListar()
        {
            SimLib clsLib = new SimLib();
            Numeracao Cls = new Numeracao(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.NumeracaoListar();
                return Ok(dtb);

            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        [Route("api/Numeracao/GetData/{Cod_Empresa}")]
        [HttpGet]
        [ActionName("GetNumeracaoData")]
        [Authorize()]
        public IHttpActionResult GetNumeracaoData( String Cod_Empresa)
        {
            SimLib clsLib = new SimLib();
            Numeracao Cls = new Numeracao(User.Identity.Name);
            try
            {

                Numeracao.NumeracaoModel Retorno = new Numeracao.NumeracaoModel();
                Retorno = Cls.GetNumeracaoData(Cod_Empresa);
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }



        //===========================Salvar Numeracao
        [Route("api/Numeracao/Salvar")]
        [HttpPost]
        [ActionName("SalvarNumeracao")]
        [Authorize()]

        public IHttpActionResult SalvarNumeracao([FromBody] Numeracao.NumeracaoModel pNumeracao)
        {
            SimLib clsLib = new SimLib();
            Numeracao Cls = new Numeracao(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.SalvarNumeracao(pNumeracao);
                return Ok(retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //===========================Confirmar Fechamento
        [Route("api/Numeracao/Confirmar")]
        [HttpPost]
        [ActionName("ConfirmarNumeracao")]
        [Authorize()]

        public IHttpActionResult ConfirmarNumeracao([FromBody] List<Numeracao.NumeracaoModel> pNumeracao)
        {
            SimLib clsLib = new SimLib();
            Numeracao Cls = new Numeracao(User.Identity.Name);
            try
            {
                List<Numeracao.NumeracaoModel> retorno = Cls.ConfirmarFechamento(pNumeracao);
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
