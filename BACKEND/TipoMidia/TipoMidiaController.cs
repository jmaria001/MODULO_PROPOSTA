using System;
using System.Web.Http;
using System.Data;
namespace PROPOSTA
{
    public class TipoMidiaController : ApiController
    {
        //=================================Lista de Tipo Midia
        [Route("api/TipoMidiaListar")]
        [HttpGet]
        [ActionName("TipoMidiaListar")]
        [Authorize()]
        public IHttpActionResult TipoMidiaListar()
        {
            SimLib clsLib = new SimLib();
            TipoMidia Cls = new TipoMidia(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.TipoMidiaListar(0);
                return Ok(dtb);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        ////=================================Obtem dados do tipo_midia
        [Route("api/GetTipoMidiaData/{Cod_Tipo_Midia}")]
        [HttpGet]
        [ActionName("GetTipoMidiaData")]
        [Authorize()]
        public IHttpActionResult GetTipoMidiaData(String Cod_Tipo_Midia)
        {
            SimLib clsLib = new SimLib();
            TipoMidia Cls = new TipoMidia(User.Identity.Name);
            try
            {
                TipoMidia.TipoMidiaModel Retorno = new TipoMidia.TipoMidiaModel();
                if (Cod_Tipo_Midia != "0")
                {
                    Retorno = Cls.GetTipoMidiaData(Cod_Tipo_Midia);

                }
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //===========================Salvar TipoMidia

        [Route("api/SalvarTipoMidia")]
        [HttpPost]
        [ActionName("SalvarTipoMidia")]
        [Authorize()]

        public IHttpActionResult SalvarTipoMidia([FromBody] TipoMidia.TipoMidiaModel pTipoMidia)
        {
            SimLib clsLib = new SimLib();
            TipoMidia Cls = new TipoMidia(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.SalvarTipoMidia(pTipoMidia);
                return Ok(retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //===========================Excluir Tipo Midia

        [Route("api/excluirtipomidia")]
        [HttpPost]
        [ActionName("excluirtipomidia")]
        [Authorize()]

        public IHttpActionResult excluirtipomidia([FromBody] TipoMidia.TipoMidiaModel pTipoMidia)
        {
            SimLib clsLib = new SimLib();
            TipoMidia Cls = new TipoMidia(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.excluirtipomidia(pTipoMidia);
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

