using System;
using System.Web.Http;
using System.Data;
namespace PROPOSTA
{
    public class TiposComercializacaoController : ApiController
    {
        //=================================Lista de Comercializacao
        [Route("api/TiposComercializacaoListar")]
        [HttpGet]
        [ActionName("TiposComercializacaoListar")]
        [Authorize()]
        public IHttpActionResult TiposComercializacaoListar()
        {
            SimLib clsLib = new SimLib();
            TiposComercializacao Cls = new TiposComercializacao(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.TiposComercializacaoListar();
                return Ok(dtb);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        ////=================================Obtem Dados do Tipos Comercializacao
        [Route("api/GetTiposComercializacaoData/{Cod_Tipo_Comercializacao}")]
        [HttpGet]
        [ActionName("GetTiposComercializacaoData")]
        [Authorize()]
        public IHttpActionResult GetTiposComercializacaoData(String Cod_Tipo_Comercializacao)
        {
            SimLib clsLib = new SimLib();
            TiposComercializacao Cls = new TiposComercializacao(User.Identity.Name);
            try
            {
                TiposComercializacao.TiposComercializacaoModel Retorno = new TiposComercializacao.TiposComercializacaoModel();

                Retorno = Cls.GetTiposComercializacaoData(Cod_Tipo_Comercializacao);

         
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //===========================Salvar Tipos Comercializacao

        [Route("api/SalvarTiposComercializacao")]
        [HttpPost]
        [ActionName("SalvarTiposComercializacao")]
        [Authorize()]

        public IHttpActionResult SalvarTiposComercializacao([FromBody] TiposComercializacao.TiposComercializacaoModel pTiposComercializacao)
        {
            SimLib clsLib = new SimLib();
            TiposComercializacao Cls = new TiposComercializacao(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.SalvarTiposComercializacao(pTiposComercializacao);
                return Ok(retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }


        //===========================Excluir Tipos Comercializacao

        [Route("api/excluirTiposComercializacao")]
        [HttpPost]
        [ActionName("excluirTiposComercializacao")]
        [Authorize()]

        public IHttpActionResult excluirTiposComercializacao([FromBody]TiposComercializacao.TiposComercializacaoModel pTiposComercializacao)
        {
            SimLib clsLib = new SimLib();
            TiposComercializacao Cls = new TiposComercializacao(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.excluirTiposComercializacao(pTiposComercializacao);
                return Ok(retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //===================================Desativar/Reativar
                   
        [Route("api/DesativarReativarTiposComercializacao")]
        [HttpPost]
        [ActionName("DesativarReativarTiposComercializacao")]
        [Authorize()]

        public IHttpActionResult DesativarReativarTiposComercializacao([FromBody] TiposComercializacao.TiposComercializacaoModel pTiposComercializacao)
        {
            SimLib clsLib = new SimLib();
            TiposComercializacao Cls = new TiposComercializacao(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.DesativarReativarTiposComercializacao(pTiposComercializacao);
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