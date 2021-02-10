using System;
using System.Web.Http;
using System.Data;
using System.Collections.Generic;
namespace PROPOSTA
{
    public class DepositoFitasController : ApiController
    {

        //=================================Lista de Fitas Avulsos e Artistico
        [Route("api/DepositoFitasListar")]
        [HttpGet]
        [ActionName("DepositoFitasListar")]
        [Authorize()]


        public IHttpActionResult DepositoFitasListar([FromUri]DepositoFitas.FiltroModel filtro)
        {
            SimLib clsLib = new SimLib();
            DepositoFitas Cls = new DepositoFitas(User.Identity.Name);
            try
            {
                 DataTable dtb = Cls.DepositoFitasListar(filtro);
                 return Ok(dtb);
               // return Ok(filtro);

            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //=================================Obtem dados do Programa jOÃO MARIA AJUDOU FAZER ESTA PARTE
        [Route("api/GetDepositorioFitasData/{Numero_Fita}")]
        [HttpGet]
        [ActionName("GetDepositorioFitasData")]
        [Authorize()]
        public IHttpActionResult GetDepositorioFitasData(String Numero_Fita)
        {
            SimLib clsLib = new SimLib();
            DepositoFitas Cls = new DepositoFitas(User.Identity.Name);
            try
            {
                DepositoFitas.DepositoFitasModel Retorno = new DepositoFitas.DepositoFitasModel();
                if (Numero_Fita != "")
                {
                    Retorno = Cls.GetDepositorioFitasData(Numero_Fita);

                }
                else
                {
                  
                    Retorno.Veiculos = new List<DepositoFitas.Veiculos_Model>();
                }
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        [Route("api/RangeFita")]
        [HttpPost]
        [Authorize()]
        public IHttpActionResult RangeFita([FromBody]  DepositoFitas.DepositoFitasModel Param)
        {
            SimLib clsLib = new SimLib();
            try
            {
                DepositoFitas Cls = new DepositoFitas(User.Identity.Name);
                DataTable Retorno = Cls.RangeFita(Param);
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //===========================Salvar Programa

        [Route("api/SalvarDepositorioFitas")]
        [HttpPost]
        [ActionName("SalvarDepositorioFitas")]
        [Authorize()]

        public IHttpActionResult SalvarDepositorioFitas([FromBody] DepositoFitas.DepositoFitasModel pDepositorioFitas)
        {
            SimLib clsLib = new SimLib();
            DepositoFitas Cls = new DepositoFitas(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.SalvarDepositorioFitas(pDepositorioFitas);
                             
                return Ok(retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }


        //===========================Excluir Programa

        [Route("api/ExcluirDepositorioFitas")]
        [HttpPost]
        [ActionName("ExcluirDepositorioFitas")]
        [Authorize()]

        public IHttpActionResult ExcluirDepositorioFitas([FromBody] DepositoFitas.DepositoFitasModel pDepositorioFitas)
        {
            SimLib clsLib = new SimLib();
            DepositoFitas Cls = new DepositoFitas(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.ExcluirDepositorioFitas(pDepositorioFitas);
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