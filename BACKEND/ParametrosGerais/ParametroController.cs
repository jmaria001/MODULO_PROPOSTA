using System;
using System.Web.Http;
using System.Data;
using System.Collections.Generic;

namespace PROPOSTA
{
    public class ParametroController : ApiController
    {
        //=================================Lista de Parametro
        [Route("api/ParametroListar")]
        [HttpGet]
        [ActionName("ParametroListar")]
        [Authorize()]
        public IHttpActionResult ParametroListar()
        {
            SimLib clsLib = new SimLib();
            Parametro Cls = new Parametro(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.ParametroListar(0);
                return Ok(dtb);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        ////=================================Obtem Dados do Parametro
        [Route("api/GetParametroData/{Cod_Parametro}")]
        [HttpGet]
        [ActionName("GetParametroData")]
        [Authorize()]
        public IHttpActionResult GetParametroData(Int32 Cod_Parametro)
        {
            SimLib clsLib = new SimLib();
            Parametro Cls = new Parametro(User.Identity.Name);
            try
            {
                Parametro.ParametroModel Retorno = new Parametro.ParametroModel();
                if (Cod_Parametro != 0)
                {
                    Retorno = Cls.GetParametroData(Cod_Parametro);
                }
                else
                {
                    Retorno.Valores = new List<Parametro.ParametroValorModel>() { new Parametro.ParametroValorModel() };
                    Retorno.MaxSequenciador = 0;
                }
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //===========================Salvar Parametro
        [Route("api/SalvarParametro")]
        [HttpPost]
        [ActionName("SalvarParametro")]
        [Authorize()]

        public IHttpActionResult SalvarParametro([FromBody] Parametro.ParametroModel pParametro)
        {
            SimLib clsLib = new SimLib();
            Parametro Cls = new Parametro(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.SalvarParametro(pParametro);
                return Ok(retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        ////===========================Salvar Valor
        //[Route("api/SalvarValor")]
        //[HttpPost]
        //[ActionName("SalvarValor")]
        //[Authorize()]

        //public IHttpActionResult SalvarValor([FromBody] Parametro.ParametroValorModel pValor)
        //{
        //    SimLib clsLib = new SimLib();
        //    Parametro Cls = new Parametro(User.Identity.Name);
        //    try
        //    {
        //        DataTable retorno = Cls.SalvarValor(pValor);
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

