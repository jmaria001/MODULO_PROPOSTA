using System;
using System.Web.Http;
using System.Data;
using System.Collections.Generic;
namespace PROPOSTA
{
    public class BaixaVeiculacoesController : ApiController
    {

        //=================================Lista de Veículos a serem baixados
        [Route("api/BaixaVeiculacoesListar")]
        [HttpGet]
        [ActionName("BaixaVeiculacoesListar")]
        [Authorize()]


        public IHttpActionResult BaixaVeiculacoesListar([FromUri]BaixaVeiculacoes.FiltroModel filtro)
        {
            SimLib clsLib = new SimLib();
            BaixaVeiculacoes Cls = new BaixaVeiculacoes(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.BaixaVeiculacoesListar(filtro);
                return Ok(dtb);
                // return Ok(filtro);

            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //===========================Validar código de qualidade

        [Route("api/BaixaVeiculacoes/ValidarQualidade")]
        [HttpPost]
        [ActionName("ValidarQualidade")]
        [Authorize()]

        public IHttpActionResult ValidarQualidade([FromBody] BaixaVeiculacoes.BaixaVeiculacoesModel pCodQualidade)
        {
            SimLib clsLib = new SimLib();
            BaixaVeiculacoes Cls = new BaixaVeiculacoes(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.ValidarQualidade(pCodQualidade);
                return Ok(retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //===========================Validar código de qualidade

        [Route("api/BaixaVeiculacoes/DaBaixaVeiculaçoes")]
        [HttpPost]
        [ActionName("DaBaixaVeiculaçoes")]
        [Authorize()]

        public IHttpActionResult DaBaixaVeiculaçoes([FromBody] List<BaixaVeiculacoes.BaixaVeiculacoesModel> pBaixaVeiculacoes)
        {
            SimLib clsLib = new SimLib();
            BaixaVeiculacoes Cls = new BaixaVeiculacoes(User.Identity.Name);
            try
            {
                List<BaixaVeiculacoes.BaixaVeiculacoesModel> retorno = Cls.DaBaixaVeiculaçoes(pBaixaVeiculacoes);
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