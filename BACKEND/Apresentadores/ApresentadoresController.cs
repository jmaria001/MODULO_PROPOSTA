using System;
using System.Web.Http;
using System.Data;
namespace PROPOSTA
{
    public class ApresentadoresController : ApiController
    {
        //=================================Lista de Categorias de Clientes
        [Route("api/ApresentadoresListar")]
        [HttpPost]
        [ActionName("ApresentadoresListar")]
        [Authorize()]
        public IHttpActionResult ApresentadoresListar([FromBody] Apresentadores.ApresentadoresModel pApresentadores)
        {
            SimLib clsLib = new SimLib();
            Apresentadores Cls = new Apresentadores(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.ApresentadoresListar(pApresentadores);
                return Ok(dtb);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }


        //=================================Obtem dados do Apresentadores
        [Route("api/GetApresentadoresData/{Cod_Apresentador}")]
        [HttpGet]
        [ActionName("GetApresentadoresData")]
        [Authorize()]
        public IHttpActionResult GetApresentadoresData(String Cod_Apresentador)
        {
            SimLib clsLib = new SimLib();
            Apresentadores Cls = new Apresentadores(User.Identity.Name);
            try
            {
                Apresentadores.ApresentadoresModel Retorno = new Apresentadores.ApresentadoresModel();
                if (Cod_Apresentador != "0")
                {
                    Retorno = Cls.GetApresentadoresData(Cod_Apresentador);

                }
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }


        //===========================Salvar Apresentadores
        [Route("api/SalvarApresentadores")]
        [HttpPost]
        [ActionName("SalvarApresentadores")]
        [Authorize()]

        public IHttpActionResult SalvarApresentadores([FromBody] Apresentadores.ApresentadoresModel pApresentadores)
        {
            SimLib clsLib = new SimLib();
            Apresentadores Cls = new Apresentadores(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.SalvarApresentadores(pApresentadores);
                return Ok(retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }


        //===========================Excluir Apresentadores
        [Route("api/ExcluirApresentadores")]
        [HttpPost]
        [ActionName("ExcluirApresentadores")]
        [Authorize()]

        public IHttpActionResult ExcluirApresentadores([FromBody] Apresentadores.ApresentadoresModel pApresentadores)
        {
            SimLib clsLib = new SimLib();
            Apresentadores Cls = new Apresentadores(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.ExcluirApresentadores(pApresentadores);
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



