using System;
using System.Web.Http;
using System.Data;
using System.Collections.Generic;
namespace PROPOSTA
{
    public class DeterminacaoController : ApiController
    {
        //===========================Carregar Dados do Contrato
        [Route("api/Determinacao/CarregarDados")]
        [HttpPost]
        [ActionName("CarregarDados")]
        [Authorize()]

        public IHttpActionResult CarregarDados([FromBody] Determinacao.FiltroModel Param)
        {
            SimLib clsLib = new SimLib();
            Determinacao Cls = new Determinacao(User.Identity.Name);
            try
            {
                Determinacao.DeterminacaoModel retorno = Cls.CarregarDados(Param);
                return Ok(retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        //===========================Gravar novo Comercial
        [Route("api/Determinacao/SalvarComercial")]
        [HttpPost]
        [ActionName("SalvarComercial")]
        [Authorize()]

        public IHttpActionResult SalvarComercial([FromBody] Determinacao.DeterminacaoComercialModel Param)
        {
            SimLib clsLib = new SimLib();
            Determinacao Cls = new Determinacao(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.SalvarComercial(Param);
                return Ok(retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //===========================Analisar Rotate
        [Route("api/Determinacao/AnalisarRotate")]
        [HttpPost]
        [ActionName("AnalisarRotate")]
        [Authorize()]
        public IHttpActionResult AnalisarRotate([FromBody] Determinacao.DeterminacaoModel Param)
        {
            SimLib clsLib = new SimLib();
            Determinacao Cls = new Determinacao(User.Identity.Name);
            try
            {
                List<Determinacao.AnaliseRotateModel> retorno = Cls.AnalisarRotate(Param);
                return Ok(retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        //===========================Salvar Determinacao
        [Route("api/Determinacao/SalvarDeterminacao")]
        [HttpPost]
        [ActionName("SalvarDeterminacao")]
        [Authorize()]
        public IHttpActionResult SalvarDeterminacao([FromBody] Determinacao.DeterminacaoModel Param)
        {
            SimLib clsLib = new SimLib();
            Determinacao Cls = new Determinacao(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.SalvarDeterminacao(Param);
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