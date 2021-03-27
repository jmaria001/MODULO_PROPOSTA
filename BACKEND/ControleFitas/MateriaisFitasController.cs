using System;
using System.Web.Http;
using System.Data;
using System.Collections.Generic;

namespace PROPOSTA
{
    public class MateriaisFitasController : ApiController
    {
        //=================================Lista de Fitas Avulsos e Artistico
        [Route("api/MateriaisFitasListar")]
        [HttpGet]
        [ActionName("MateriaisFitasListar")]
        [Authorize()]


        public IHttpActionResult MateriaisFitasListar([FromUri]MateriaisFitas.FiltroModel filtro)
        {
            SimLib clsLib = new SimLib();
            MateriaisFitas Cls = new MateriaisFitas(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.MateriaisFitasListar(filtro);
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
        [Route("api/GetMateriaisFitasData/{Numero_Fita}")]
        [HttpGet]
        [ActionName("GetMateriaisFitasData")]
        [Authorize()]
        public IHttpActionResult GetMateriaisFitasData(String Numero_Fita)
        {
            SimLib clsLib = new SimLib();
            MateriaisFitas Cls = new MateriaisFitas(User.Identity.Name);
            try
            {
                MateriaisFitas.MateriaisFitasModel Retorno = new MateriaisFitas.MateriaisFitasModel();
                if (Numero_Fita != "")
                {
                    Retorno = Cls.GetMateriaisFitasData(Numero_Fita);

                }
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }



        [Route("api/RangeFitaMateriais")]
        [HttpPost]
        [Authorize()]
        public IHttpActionResult RangeFitaMateriais([FromBody]  MateriaisFitas.MateriaisFitasModel Param)
        {
            SimLib clsLib = new SimLib();
            try
            {
                MateriaisFitas Cls = new MateriaisFitas(User.Identity.Name);
                DataTable Retorno = Cls.RangeFitaMateriais(Param);
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }



        //===========================Salvar 

        [Route("api/SalvarMateriaisFitas")]
        [HttpPost]
        [ActionName("SalvarMateriaisFitas")]
        [Authorize()]

        public IHttpActionResult SalvarMateriaisFitas([FromBody] MateriaisFitas.MateriaisFitasModel pMateriaisFitas)
        {
            SimLib clsLib = new SimLib();
            MateriaisFitas Cls = new MateriaisFitas(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.SalvarMateriaisFitas(pMateriaisFitas);

                return Ok(retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }


        //===========================Excluir Programa

        [Route("api/ExcluirMateriaisFitas")]
        [HttpPost]
        [ActionName("ExcluirMateriaisFitas")]
        [Authorize()]

        public IHttpActionResult ExcluirMateriaisFitas([FromBody] MateriaisFitas.MateriaisFitasModel pMateriaisFitas)
        {
            SimLib clsLib = new SimLib();
            MateriaisFitas Cls = new MateriaisFitas(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.ExcluirMateriaisFitas(pMateriaisFitas);
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