using System;
using System.Web.Http;
using System.Data;
namespace PROPOSTA
{
    public class MercadoController : ApiController
    {
        //=================================Lista de Mercado
        [Route("api/MercadoListar")]
        [HttpGet]
        [ActionName("MercadoListar")]
        [Authorize()]
        public IHttpActionResult MercadoListar()
        {
            SimLib clsLib = new SimLib();
            Mercado Cls = new Mercado(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.MercadoListar(0);
                return Ok(dtb);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //=================================Obtem dados do Mercado
        [Route("api/GetMercadoData/{Cod_Mercado}")]
        [HttpGet]
        [ActionName("GetMercadoData")]
        [Authorize()]
        public IHttpActionResult GetMercadoData(String Cod_Mercado)
        {
            SimLib clsLib = new SimLib();
            Mercado Cls = new Mercado(User.Identity.Name);
            try
            {
                Mercado.MercadoModel Retorno = new Mercado.MercadoModel();
                if (Cod_Mercado != "0")
                {
                    Retorno = Cls.GetMercadoData(Cod_Mercado);

                }
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //===========================Salvar Mercado

        [Route("api/SalvarMercado")]
        [HttpPost]
        [ActionName("SalvarMercado")]
        [Authorize()]

        public IHttpActionResult SalvarMercado([FromBody] Mercado.MercadoModel pMercado)
        {
            SimLib clsLib = new SimLib();
            Mercado Cls = new Mercado(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.SalvarMercado(pMercado);
                return Ok(retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        //===========================Excluir Mercado

        [Route("api/ExcluirMercado")]
        [HttpPost]
        [ActionName("ExcluirMercado")]
        [Authorize()]

        public IHttpActionResult ExcluirMercado([FromBody] Mercado.MercadoModel pMercado)
        {
            SimLib clsLib = new SimLib();
            Mercado Cls = new Mercado(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.ExcluirMercado(pMercado);
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

