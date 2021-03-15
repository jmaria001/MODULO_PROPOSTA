using System;
using System.Web.Http;
using System.Data;
using System.Collections.Generic;

namespace PROPOSTA
{
    public class ConfirmacaoRoteiroController : ApiController
    {
        //--------------- Listar Veículos - Confirmação do Roteiro ---------------------------
        [Route("api/ConfirmacaoRoteiroListar")]
        [HttpGet]
        [ActionName("ConfirmacaoRoteiroListar")]
        [Authorize()]
        public IHttpActionResult ConfirmacaoRoteiroListar()
        {
            SimLib clsLib = new SimLib();
            ConfirmacaoRoteiro Cls = new ConfirmacaoRoteiro(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.ConfirmacaoRoteiroListar(0);
                return Ok(dtb);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //---------------------------Confirma Roteiro
        [Route("api/ConfirmaRoteiro")]
        [HttpPost]
        [ActionName("ConfirmaRoteiro")]
        [Authorize()]
        public IHttpActionResult ConfirmaRoteiro([FromBody] ConfirmacaoRoteiro.ConfirmacaoRoteiroModel pParam)
        {
            SimLib clsLib = new SimLib();
            ConfirmacaoRoteiro Cls = new ConfirmacaoRoteiro(User.Identity.Name);
            try
            {
                List<ConfirmacaoRoteiro.VeiculosModel> retorno = Cls.ConfirmaRoteiro(pParam);
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



