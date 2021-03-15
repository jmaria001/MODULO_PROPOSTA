using System;
using System.Web.Http;
using System.Data;
namespace PROPOSTA
{
    public class CondPgtoController : ApiController
    {
        //=================================Lista de Condição de Pagamento
        [Route("api/CondPgtoListar")]
        [HttpGet]
        [ActionName("CondPgtoListar")]
        [Authorize()]
        public IHttpActionResult CondPgtoListar()
        {
            SimLib clsLib = new SimLib();
            CondPgto Cls = new CondPgto(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.CondPgtoListar(0);
                return Ok(dtb);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        //=================================Obtem dados do Tipo Comercial
        [Route("api/GetCondPgtodata/{Cod_Condicao}")]
        [HttpGet]
        [ActionName("GetCondPgtodata")]
        [Authorize()]
        public IHttpActionResult GetCondPgtodata(String Cod_Condicao)
        {
            SimLib clsLib = new SimLib();
            CondPgto Cls = new CondPgto(User.Identity.Name);
            try
            {
                CondPgto.CondPgtoModel Retorno = new CondPgto.CondPgtoModel();
                if (Cod_Condicao != "0")
                {
                    Retorno = Cls.GetCondPgtoData(Cod_Condicao);

                }
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }


        //===========================Salvar Veiculo

        [Route("api/SalvarCondPgto")]
        [HttpPost]
        [ActionName("SalvarCondPgto")]
        [Authorize()]

        public IHttpActionResult SalvarCondPgto([FromBody] CondPgto.CondPgtoModel pCondPgto)
        {
            SimLib clsLib = new SimLib();
            CondPgto Cls = new CondPgto(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.SalvarCondPgto(pCondPgto);
                return Ok(retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }


        //===========================Excluir Condicao de Pagamento

        [Route("api/ExcluirCondPgto")]
        [HttpPost]
        [ActionName("ExcluirCondPgto")]
        [Authorize()]

        public IHttpActionResult ExcluirCondPgto([FromBody] CondPgto.CondPgtoModel pCondPgto)
        {
            SimLib clsLib = new SimLib();
            CondPgto Cls = new CondPgto(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.ExcluirCondPgto(pCondPgto);
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

