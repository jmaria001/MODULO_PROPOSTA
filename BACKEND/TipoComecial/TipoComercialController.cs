using System;
using System.Web.Http;
using System.Data;
namespace PROPOSTA
{
    public class TipoComercialController : ApiController
    {
        //=================================Lista de Tipo Comercial
        [Route("api/TipoComercialListar")]
        [HttpGet]
        [ActionName("TipoComercialListar")]
        [Authorize()]
        public IHttpActionResult TipoComercialListar()
        {
            SimLib clsLib = new SimLib();
            TipoComercial Cls = new TipoComercial(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.TipoComercialListar(0);
                return Ok(dtb);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        //=================================Obtem dados do Tipo Comercial
        [Route("api/GetTipoComercialData/{Cod_Tipo_Comercial}")]
        [HttpGet]
        [ActionName("GetTipoComercialData")]
        [Authorize()]
        public IHttpActionResult GetTipoComercialData(String Cod_Tipo_Comercial)
        {
            SimLib clsLib = new SimLib();
            TipoComercial Cls = new TipoComercial(User.Identity.Name);
            try
            {
                TipoComercial.TipoComercialModel Retorno = new TipoComercial.TipoComercialModel();
                if (Cod_Tipo_Comercial != "0")
                {
                    Retorno = Cls.GetTipoComercialData(Cod_Tipo_Comercial);

                }
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //===========================Salvar Veiculo

        //[Route("api/SalvarTipoComercial")]
        //[HttpPost]
        //[ActionName("SalvarTipoComercial")]
        //[Authorize()]

        //public IHttpActionResult SalvarVeiculo([FromBody] TipoComercial.TipoComercialModel pTipoComercial)
        //{
        //    SimLib clsLib = new SimLib();
        //    TipoComercial.TipoComercialModel Cls = new TipoComercial(User.Identity.Name);
        //    try
        //    {
        //        DataTable retorno = Cls.SalvarVeiculo(pTipoComercial);
        //        return Ok(retorno);
        //    }
        //    catch (Exception Ex)
        //    {
        //        clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
        //        throw new Exception(Ex.Message);
        //    }
        //}

    }

}

