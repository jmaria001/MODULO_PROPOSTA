using System;
using System.Web.Http;
using System.Data;
using System.Collections.Generic;

namespace PROPOSTA
{
    public class ParamRoteiroController : ApiController
    {
        //=================================Filtrar Dados
        [Route("api/ParamRoteiroFiltrar")]
        [HttpGet]
        [ActionName("ParamRoteiroFiltrar")]
        [Authorize()]
        public IHttpActionResult ParamRoteiroFiltrar([FromUri] ParamRoteiro.ParamRoteiroModel Param)
        {
            SimLib clsLib = new SimLib();
            ParamRoteiro Cls = new ParamRoteiro(User.Identity.Name);
            try
            {
                List<ParamRoteiro.ParamRoteiroModel> Retorno = Cls.ParamRoteiroFiltrar(Param);
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }


        //===========================Salvar dados
        [Route("api/ParamRoteiroSalvar")]
        [HttpPost]
        [ActionName("ParamRoteiroSalvar")]
        [Authorize()]
        public IHttpActionResult ParamRoteiroSalvar([FromBody] List<ParamRoteiro.ParamRoteiroModel> pParamRoteiro)
        {
            SimLib clsLib = new SimLib();
            ParamRoteiro Cls = new ParamRoteiro(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.ParamRoteiroSalvar(pParamRoteiro);
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



