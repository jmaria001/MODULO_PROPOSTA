using System;
using System.Web.Http;
using System.Data;
namespace PROPOSTA
{
    public class ParametroValoracaoController : ApiController
    {
        //=================================Lista de Itens de Permuta
        [Route("api/ParametroValoracaoListar")]
        [HttpGet]
        [ActionName("ParametroValoracaoListar")]
        [Authorize()]


        public IHttpActionResult ParametroValoracaoListar([FromUri]ParametroValoracao.FiltroModel filtro)
        {
            SimLib clsLib = new SimLib();
            ParametroValoracao Cls = new ParametroValoracao(User.Identity.Name);
            try
            {
                ParametroValoracao.ParametroValoracaoModel Retorno= Cls.ParametroValoracaoListar(filtro);
                return Ok(Retorno);

            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //===========================Salvar Parametro
        [Route("api/SalvarParametroValoracao")]
        [HttpPost]
        [ActionName("SalvarParametroValoracao")]
        [Authorize()]

        public IHttpActionResult SalvarParametroValoracao([FromBody] ParametroValoracao.Parametro_Tipo_Comercial_Model pParametroValoracao)
        {
            SimLib clsLib = new SimLib();
            ParametroValoracao Cls = new ParametroValoracao(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.SalvarParametroValoracao(pParametroValoracao);
                return Ok(retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //===========================Salvar Parametro
        [Route("api/SalvarParametroValoracaoDuracao")]
        [HttpPost]
        [ActionName("SalvarParametroValoracaoDuracao")]
        [Authorize()]

        public IHttpActionResult SalvarParametroValoracaoDuracao([FromBody] ParametroValoracao.Parametro_Duracao_Model pParametroValoracao)
        {
            SimLib clsLib = new SimLib();
            ParametroValoracao Cls = new ParametroValoracao(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.SalvarParametroValoracaoDuracao(pParametroValoracao);
                return Ok(retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }


        //===========================Excluir Itens
        [Route("api/ParametroExcluirTipoComercial")]
        [HttpPost]
        [ActionName("ParametroExcluirTipoComercial")]
        [Authorize()]

        public IHttpActionResult ParametroExcluirTipoComercial([FromBody] ParametroValoracao.Parametro_Tipo_Comercial_Model ParametroValoracao)
        {
            SimLib clsLib = new SimLib();
            ParametroValoracao Cls = new ParametroValoracao(User.Identity.Name);
            try
            {
                //Cls.ParametroExcluirTipoComercial(ParametroValoracao);
                //return Ok(true);

                DataTable retorno = Cls.ParametroExcluirTipoComercial(ParametroValoracao);
                return Ok(retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //===========================Excluir Itens
        [Route("api/ParametroExcluirDuracao")]
        [HttpPost]
        [ActionName("ParametroExcluirDuracao")]
        [Authorize()]

        public IHttpActionResult ParametroExcluirDuracao([FromBody] ParametroValoracao.Parametro_Duracao_Model ParametroValoracao)
        {
            SimLib clsLib = new SimLib();
            ParametroValoracao Cls = new ParametroValoracao(User.Identity.Name);
            try
            {

                DataTable retorno = Cls.ParametroExcluirDuracao(ParametroValoracao);
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



