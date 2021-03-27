using System;
using System.Web.Http;
using System.Data;
using System.Collections.Generic;
namespace PROPOSTA
{
    public class HorarioExibicaoController : ApiController
    {
        //=================================Lista de Veículos a serem baixados
        [Route("api/HorarioExibicaoListar")]
        [HttpGet]
        [ActionName("HorarioExibicaoListar")]
        [Authorize()]


        public IHttpActionResult HorarioExibcaoListar([FromUri]HorarioExibicao.FiltroModel filtro)
        {
            SimLib clsLib = new SimLib();
            HorarioExibicao Cls = new HorarioExibicao(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.HorarioExibicaoListar(filtro);
                return Ok(dtb);
                // return Ok(filtro);

            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //[Route("api/VeiculosListar")]
        //[HttpGet]
        //[ActionName("VeiculosListar")]
        //[Authorize()]
        //public IHttpActionResult VeiculosListar([FromUri]HorarioExibicao.HorarioExibicaoModel filtro)
        //{
        //    SimLib clsLib = new SimLib();
        //    HorarioExibicao Cls = new HorarioExibicao(User.Identity.Name);
        //    try
        //    {
        //        DataTable Veiculos = Cls.VeiculosListar(filtro);
        //        return Ok(Veiculos);
        //    }
        //    catch (Exception Ex)
        //    {
        //        clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
        //        throw new Exception(Ex.Message);
        //    }
        //}

        //===========================Salvar Horario Exibicao
 

        [Route("api/SalvarHorarioExibicao")]
        [HttpPost]
        [ActionName("SalvarHorarioExibicao")]
        [Authorize()]

        public IHttpActionResult SalvarHorarioExibicao([FromBody] HorarioExibicao.GravarModel pHorarioExibicao)
        {
            SimLib clsLib = new SimLib();
            HorarioExibicao Cls = new HorarioExibicao(User.Identity.Name);

            try
            {



                HorarioExibicao.GravarModel  retorno = Cls.SalvarHorarioExibicao(pHorarioExibicao);
                return Ok(retorno);


            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }


        //===========================Replicar Horario Exibicao


        [Route("api/ReplicarHorarioExibicao")]
        [HttpPost]
        [ActionName("ReplicarHorarioExibicao")]
        [Authorize()]

        public IHttpActionResult ReplicarHorarioExibicao([FromBody] List<HorarioExibicao.HorarioExibicaoModel> pReplicar)
        {
            SimLib clsLib = new SimLib();
            HorarioExibicao Cls = new HorarioExibicao(User.Identity.Name);

            try
            {



                List<HorarioExibicao.HorarioExibicaoModel> retorno = Cls.ReplicarHorarioExibicao(pReplicar);
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