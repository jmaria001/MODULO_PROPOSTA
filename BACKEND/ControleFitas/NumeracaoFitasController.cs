using System;
using System.Web.Http;
using System.Data;
using System.Collections.Generic;
namespace PROPOSTA
{
    public class NumeracaoFitasController : ApiController
    {

        //=================================Lista de Fitas Avulsos e Artistico
        [Route("api/NumeracaoFitasListar")]
        [HttpGet]
        [ActionName("NumeracaoFitasListar")]
        [Authorize()]


        public IHttpActionResult NumeracaoFitasListar([FromUri]NumeracaoFitas.FiltroModel filtro)
        {
            SimLib clsLib = new SimLib();
            NumeracaoFitas Cls = new NumeracaoFitas(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.NumeracaoFitasListar(filtro);
                return Ok(dtb);
                // return Ok(filtro);

            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }


        [Route("api/ExibirVeiculosFitas")]
        [HttpGet]
        [ActionName("ExibirVeiculosFitas")]
        [Authorize()]
        public IHttpActionResult ExibirVeiculosFitas([FromUri]NumeracaoFitas.FiltroExibirVeiculoModel pfiltro)
        {
            SimLib clsLib = new SimLib();
            NumeracaoFitas Cls = new NumeracaoFitas(User.Identity.Name);
            try
            {
                //DataTable dtb = Cls.ExibirVeiculosFitas(pfiltro);

                List<NumeracaoFitas.FiltroExibirVeiculoModel> Retorno = Cls.ExibirVeiculosFitas(pfiltro);
                return Ok(Retorno);

                //return Ok(dtb);


            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }



        [Route("api/RangeFitaNumeracao")]
        [HttpPost]
        [Authorize()]
        public IHttpActionResult RangeFitaNumeracao([FromBody] NumeracaoFitas.FiltroExibirVeiculoModel Param)
        {
            SimLib clsLib = new SimLib();
            try
            {
                NumeracaoFitas Cls = new NumeracaoFitas(User.Identity.Name);
                DataTable Retorno = Cls.RangeFitaNumeracao(Param);
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        [Route("api/NumeracaoFitasApresentador")]
        [HttpGet]
        [ActionName("NumeracaoFitasApresentador")]
        [Authorize()]
        public IHttpActionResult NumeracaoFitasApresentador([FromUri] NumeracaoFitas.FiltroExibirVeiculoModel Param)
        {
            SimLib clsLib = new SimLib();
            NumeracaoFitas Cls = new NumeracaoFitas(User.Identity.Name);
            try
            {
                DataTable Retorno = Cls.NumeracaoFitasApresentadores(Param.Cod_Apresentador);
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        [Route("api/NumeracaoFitasValidarApresentador")]
        [HttpGet]
        [ActionName("NumeracaoFitasValidarApresentador")]
        [Authorize()]
        public IHttpActionResult NumeracaoFitasValidarApresentador([FromUri] NumeracaoFitas.FiltroExibirVeiculoModel Param)
        {
            SimLib clsLib = new SimLib();
            NumeracaoFitas Cls = new NumeracaoFitas(User.Identity.Name);
            try
            {
                DataTable Retorno = Cls.NumeracaoFitasValidarApresentador(Param.Cod_Apresentador);
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //===========================Salvar Numeração de Fitas

        [Route("api/SalvarNumeracaoFitas")]
        [HttpPost]
        [ActionName("SalvarNumeracaoFitas")]
        [Authorize()]

        public IHttpActionResult SalvarNumeracaoFitas([FromBody] List<NumeracaoFitas.FiltroExibirVeiculoModel> pNumeracaoFitas)
        {
            SimLib clsLib = new SimLib();
            NumeracaoFitas Cls = new NumeracaoFitas(User.Identity.Name);
            try
            {
                List<NumeracaoFitas.FiltroExibirVeiculoModel> retorno = Cls.SalvarNumeracaoFitas(pNumeracaoFitas);
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