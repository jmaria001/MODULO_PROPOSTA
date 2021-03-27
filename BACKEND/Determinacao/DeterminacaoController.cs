using System;
using System.Web.Http;
using System.Data;
using System.Collections.Generic;
namespace PROPOSTA
{
    public class DeterminacaoController : ApiController
    {
        //===========================Validar código de qualidade
        [Route("api/Determinacao/CarregarDados")]
        [HttpPost]
        [ActionName("CarregarDados")]
        [Authorize()]

        public IHttpActionResult CarregarDados([FromBody] Determinacao.FiltroModel Param)
        {
            SimLib clsLib = new SimLib();
            Determinacao Cls = new Determinacao(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.CarregarDados(Param);
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