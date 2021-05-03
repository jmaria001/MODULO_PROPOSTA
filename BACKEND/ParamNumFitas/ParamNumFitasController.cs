using System;
using System.Web.Http;
using System.Data;
using System.Collections.Generic;
namespace PROPOSTA
{
    public class ParamNumFitasController : ApiController
    {

        //----------------------- Carregar Parâmetros Numeração Fita -------------------------
        [Route("api/CarregarParamFita/{pCodVeiculo}")]
        [HttpGet]
        [ActionName("CarregarParamFita")]
        [Authorize()]
        public IHttpActionResult CarregarParamFita(String pCodVeiculo)
        {
            SimLib clsLib = new SimLib();
            ParamNumFitas Cls = new ParamNumFitas(User.Identity.Name);
            try
            {
                ParamNumFitas.ParamFitaModel Retorno = Cls.CarregarParamFita(pCodVeiculo);
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //----------------------- Carregar Parâmetros Numeração Fita sem o veiculo-------------------------
        [Route("api/CarregarParamFita")]
        [HttpGet]
        [ActionName("CarregarParamFita2")]
        [Authorize()]
        public IHttpActionResult CarregarParamFita2()
        {
            SimLib clsLib = new SimLib();
            ParamNumFitas Cls = new ParamNumFitas(User.Identity.Name);
            try
            {
                ParamNumFitas.ParamFitaModel Retorno = new ParamNumFitas.ParamFitaModel();
                Retorno.Regras = new List<ParamNumFitas.ParamFitaRegraModel>();
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }


        //----------------------- Salva Parametros -------------------------
        [Route("api/SalvaParametros")]
        [HttpPost]
        [ActionName("SalvaParametros")]
        [Authorize()]
        public IHttpActionResult SalvaParametros([FromBody] ParamNumFitas.ParamFitaModel pParam)
        {
            SimLib clsLib = new SimLib();
            ParamNumFitas Cls = new ParamNumFitas(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.SalvaParametros(pParam);
                return Ok(dtb);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }




    }
}



