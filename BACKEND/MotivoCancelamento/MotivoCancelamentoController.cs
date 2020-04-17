using System;
using System.Web.Http;
using System.Data;
namespace PROPOSTA
{
    public class MotivoCancelamentoController : ApiController
    {
        //=================================Lista de MotivoCancelamento
        [Route("api/MotivoCancelamentoListar")]
        [HttpGet]
        [ActionName("MotivoCancelamentoListar")]
        [Authorize()]
        public IHttpActionResult MotivoCancelamentoListar()
        {
            SimLib clsLib = new SimLib();
            MotivoCancelamento Cls = new MotivoCancelamento(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.MotivoCancelamentoListar("0");
                return Ok(dtb);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        ////=================================Obtem Dados do MotivoCancelamento
        [Route("api/GetMotivoCancelamentoData/{Cod_Cancelamento}")]
        [HttpGet]
        [ActionName("GetMotivoCancelamentoData")]
        [Authorize()]
        public IHttpActionResult GetMotivoCancelamentoData(string Cod_Cancelamento)
        {
            SimLib clsLib = new SimLib();
            MotivoCancelamento Cls = new MotivoCancelamento(User.Identity.Name);
            try
            {
                MotivoCancelamento.MotivoCancelamentoModel Retorno = new MotivoCancelamento.MotivoCancelamentoModel();
                if (Cod_Cancelamento != "0")
                {
                    Retorno = Cls.GetMotivoCancelamentoData(Cod_Cancelamento);

                }
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //===========================Salvar MotivoCancelamento

        [Route("api/SalvarMotivoCancelamento")]
        [HttpPost]
        [ActionName("SalvarMotivoCancelamento")]
        [Authorize()]

        public IHttpActionResult SalvarMotivoCancelamento([FromBody] MotivoCancelamento.MotivoCancelamentoModel pCod_Cancelamento)
        {
            SimLib clsLib = new SimLib();
            MotivoCancelamento Cls = new MotivoCancelamento(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.SalvarMotivoCancelamento(pCod_Cancelamento);
                return Ok(retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //===========================Excluir Tipo Midia

        //[Route("api/ExcluirRede")]
        //[HttpPost]
        //[ActionName("ExcluirRede")]
        //[Authorize()]

        //public IHttpActionResult ExcluirRede([FromBody] Rede.RedeModel pRede)
        //{
        //    SimLib clsLib = new SimLib();
        //    Rede Cls = new Rede(User.Identity.Name);
        //    try
        //    {
        //        DataTable retorno = Cls.ExcluirRede(pRede);
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

