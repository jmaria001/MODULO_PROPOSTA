using System;
using System.Web.Http;
using System.Data;
using System.Collections.Generic;
namespace PROPOSTA
{
    public class DeParaProgramacaoController : ApiController
    {
        //===========================Carrega Model do DePara por Periodo
        [Route("api/DeParaProgramacao/CarregarDadosPeriodo")]
        [HttpPost]
        [ActionName("CarregarDadosPeriodo")]
        [Authorize()]

        public IHttpActionResult CarregarDadosPeriodo()
        {
            SimLib clsLib = new SimLib();
            DeParaProgramacao Cls = new DeParaProgramacao(User.Identity.Name);
            DeParaProgramacao.DeParaPeriodoModel DePara = new DeParaProgramacao.DeParaPeriodoModel();
            try
            {
                DePara.Veiculos = Cls.AddVeiculos();
                return Ok(DePara);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        //===========================Carrega Model do DePara por Data
        [Route("api/DeParaProgramacao/CarregarDadosData")]
        [HttpPost]
        [ActionName("CarregarDadosData")]
        [Authorize()]

        public IHttpActionResult CarregarDadosData()
        {
            SimLib clsLib = new SimLib();
            DeParaProgramacao Cls = new DeParaProgramacao(User.Identity.Name);
            DeParaProgramacao.DeParaDataModel DePara = new DeParaProgramacao.DeParaDataModel();
            try
            {
                DePara.Veiculos = Cls.AddVeiculos();
                return Ok(DePara);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //===========================Processa De Para de Periodo
        [Route("api/DeParaProgramacao/ProcessaDeParaPeriodo")]
        [HttpPost]
        [ActionName("ProcessaDeParaPeriodo")]
        [Authorize()]

        public IHttpActionResult ProcessaDeParaPeriodo([FromBody] DeParaProgramacao.DeParaPeriodoModel Param)
        {
            SimLib clsLib = new SimLib();
            DeParaProgramacao Cls = new DeParaProgramacao(User.Identity.Name);
            try
            {
                DataTable Retorno = Cls.ProcessaDeParaPeriodo(Param);
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        //===========================Processa De Para por Data
        [Route("api/DeParaProgramacao/ProcessaDeParaData")]
        [HttpPost]
        [ActionName("ProcessaDeParaData")]
        [Authorize()]

        public IHttpActionResult ProcessaDeParaData([FromBody] DeParaProgramacao.DeParaDataModel Param)
        {
            SimLib clsLib = new SimLib();
            DeParaProgramacao Cls = new DeParaProgramacao(User.Identity.Name);
            try
            {
                DataTable Retorno = Cls.ProcessaDeParaData(Param);
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
    }
}