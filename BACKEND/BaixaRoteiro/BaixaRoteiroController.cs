using System;
using System.Web.Http;
using System.Data;
using System.Collections.Generic;

namespace PROPOSTA
{
    public class BaixaRoteiroController : ApiController
    {
        //=================================Item de 'GetRoteiroBaixa
        [Route("api/GetRoteiroBaixa")]
        [HttpGet]
        [ActionName("GetRoteiroBaixa")]
        [Authorize()]


        public IHttpActionResult GetRoteiroBaixa()
        {
            SimLib clsLib = new SimLib();
            BaixaRoteiro Cls = new BaixaRoteiro(User.Identity.Name);
            try
            {
                BaixaRoteiro.BaixaRoteiroModel dtb = Cls.GetRoteiroBaixa();
                return Ok(dtb);

            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //===========================Salvar ContratoBaixa
        [Route("api/SalvarRoteiroBaixa")]
        [HttpPost]
        [ActionName("SalvarRoteiroBaixa")]
        [Authorize()]

        public IHttpActionResult SalvarRoteiroBaixa([FromBody] BaixaRoteiro.BaixaRoteiroModel pBaixaRoteiro)
        {
            SimLib clsLib = new SimLib();
            BaixaRoteiro Cls = new BaixaRoteiro(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.SalvarRoteiroBaixa(pBaixaRoteiro);
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
