using System;
using System.Web.Http;
using System.Data;
namespace PROPOSTA
{
    public class ConsultaProgramacaoDiariaController : ApiController
    {
        //=================================Lista de Dados
        [Route("api/ConsultaProgramacaoDiariaListar")]
        [HttpGet]
        [ActionName("ConsultaProgramacaoDiariaListar")]
        [Authorize()]

        public IHttpActionResult ConsultaProgramacaoDiariaListar([FromUri]ConsultaProgramacaoDiaria.FiltroModel filtro)
        {
            SimLib clsLib = new SimLib();
            ConsultaProgramacaoDiaria Cls = new ConsultaProgramacaoDiaria(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.ConsultaProgramacaoDiariaListar(filtro);
                return Ok(dtb);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //=================================Lista dados do Detalhe
        [Route("api/ListarConsultaProgramacaoDiariaDetalhe")]
        [HttpGet]
        [ActionName("ListarConsultaProgramacaoDiariaDetalhe")]
        [Authorize()]
        public IHttpActionResult ListarConsultaProgramacaoDiariaDetalhe([FromUri]ConsultaProgramacaoDiaria.FiltroDetalheModel filtro2)
        {
            SimLib clsLib = new SimLib();
            ConsultaProgramacaoDiaria Cls = new ConsultaProgramacaoDiaria(User.Identity.Name);
            try
            {
                DataTable Retorno = Cls.ListarConsultaProgramacaoDiariaDetalhe(filtro2);
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

    }
}



