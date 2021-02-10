using System;
using System.Web.Http;
using System.Data;
using System.Collections.Generic;

namespace PROPOSTA
{
    public class ComplementoContratoDadosController : ApiController
    {
        //=================================Lista de Itens de GetComplementoData
        [Route("api/GetComplementoData")]
        [HttpPost]
        [ActionName("GetComplementoData")]
        [Authorize()]


        public IHttpActionResult GetComplementoData([FromBody] List<ComplementoContratoDados.ComplementoContratoModel> pData)
        {
            SimLib clsLib = new SimLib();
            ComplementoContratoDados Cls = new ComplementoContratoDados(User.Identity.Name);
            try
            {

                ComplementoContratoDados.ComplementoContratoModel dtb = Cls.GetComplementoData(pData);
                return Ok(dtb);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //===========================Salvar Complemento
        [Route("api/SalvarComplemento")]
        [HttpPost]
        [ActionName("SalvarComplemento")]
        [Authorize()]

        public IHttpActionResult SalvarComplemento([FromBody] ComplementoContratoDados.ComplementoContratoModel Param)
        {
            SimLib clsLib = new SimLib();
            ComplementoContratoDados Cls = new ComplementoContratoDados(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.SalvarComplemento(Param);
                return Ok(retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        //===========================Get Regra Natureza de servicos
        [Route("api/GetNaturezaRegra")]
        [HttpGet]
        [ActionName("GetNaturezaRegra")]
        [Authorize()]

        public IHttpActionResult GetNaturezaRegra([FromUri] ComplementoContratoDados.RegraNaturezaModel Param)
        {
            SimLib clsLib = new SimLib();
            ComplementoContratoDados Cls = new ComplementoContratoDados(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.GetNaturezaRegra(Param);
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
