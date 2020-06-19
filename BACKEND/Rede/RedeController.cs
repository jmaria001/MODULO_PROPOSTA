using System;
using System.Web.Http;
using System.Data;
namespace PROPOSTA
{
    public class RedeController : ApiController
    {
        //=================================Lista de Rede
        [Route("api/RedeListar")]
        [HttpGet]
        [ActionName("RedeListar")]
        [Authorize()]
        public IHttpActionResult RedeListar()
        {
            SimLib clsLib = new SimLib();
            Rede Cls = new Rede(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.RedeListar(0);
                return Ok(dtb);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        ////=================================Obtem dados do tipo_midia
        [Route("api/GetRedeData/{RedeID}")]
        [HttpGet]
        [ActionName("GetRedeData")]
        [Authorize()]
        public IHttpActionResult GetRedeData(Int32 RedeID)
        {
            SimLib clsLib = new SimLib();
            Rede Cls = new Rede(User.Identity.Name);
            try
            {
                Rede.RedeModel Retorno = new Rede.RedeModel();
                if (RedeID != 0)
                {
                    Retorno = Cls.GetRedeData(RedeID);

                }
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //===========================Salvar Rede

        [Route("api/SalvarRede")]
        [HttpPost]
        [ActionName("SalvarRede")]
        [Authorize()]

        public IHttpActionResult SalvarRede([FromBody] Rede.RedeModel pRede)
        {
            SimLib clsLib = new SimLib();
            Rede Cls = new Rede(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.SalvarRede(pRede);
                return Ok(retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //===========================Excluir Tipo Midia

        [Route("api/ExcluirRede")]
        [HttpPost]
        [ActionName("ExcluirRede")]
        [Authorize()]

        public IHttpActionResult ExcluirRede([FromBody] Rede.RedeModel pRede)
        {
            SimLib clsLib = new SimLib();
            Rede Cls = new Rede(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.ExcluirRede(pRede);
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

