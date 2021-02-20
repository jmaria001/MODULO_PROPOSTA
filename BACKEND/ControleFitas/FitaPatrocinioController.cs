using System;
using System.Web.Http;
using System.Data;
using System.Collections.Generic;
namespace PROPOSTA
{
    public class FitaPatrocinioController : ApiController
    {
        //=================================Lista de Fitas Avulsos e Artistico
        [Route("api/FitaPatrocinioListar")]
        [HttpPost]
        [ActionName("FitaPatrocinioListar")]
        [Authorize()]


        public IHttpActionResult FitaPatrocinioListar([FromBody]FitaPatrocinio.FiltroModel filtro)
        {
            SimLib clsLib = new SimLib();
            FitaPatrocinio Cls = new FitaPatrocinio(User.Identity.Name);
            try
            {
                 DataTable dtb = Cls.FitaPatrocinioListar(filtro);
                 return Ok(dtb);
               
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        //=================================Lista de Fitas Avulsos e Artistico
        [Route("api/FitaPatrocinioGravar")]
        [HttpPost]
        [ActionName("FitaPatrocinioGravar")]
        [Authorize()]


        public IHttpActionResult FitaPatrocinioGravar([FromBody]FitaPatrocinio.FitaPatrocinioModel filtro)
        {
            SimLib clsLib = new SimLib();
            FitaPatrocinio Cls = new FitaPatrocinio(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.FitaPatrocinioGravar(filtro);
                return Ok(dtb);

            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        //=================================Procurar Numero de Fita Disponivel
        [Route("api/FitaPatrocinioProcurarFita")]
        [HttpPost]
        [ActionName("FitaPatrocinioProcurarFita")]
        [Authorize()]


        public IHttpActionResult FitaPatrocinioProcurarFita([FromBody]FitaPatrocinio.FitaPatrocinioModel filtro)
        {
            SimLib clsLib = new SimLib();
            FitaPatrocinio Cls = new FitaPatrocinio(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.FitaPatrocinioProcurarFita(filtro);
                return Ok(dtb);

            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        //=================================Desativar fita patrocinio
        [Route("api/FitaPatrocinioDesativar")]
        [HttpPost]
        [ActionName("FitaPatrocinioDesativar")]
        [Authorize()]
        public IHttpActionResult FitaPatrocinioDesativar([FromBody]FitaPatrocinio.FitaPatrocinioModel filtro)
        {
            SimLib clsLib = new SimLib();
            FitaPatrocinio Cls = new FitaPatrocinio(User.Identity.Name);
            try
            {
                 Cls.FitaPatrocinioDesativar(filtro);
                return Ok(true);

            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        //=================================Excluir fita patrocinio
        [Route("api/FitaPatrocinioExcluir")]
        [HttpPost]
        [ActionName("FitaPatrocinioExcluir")]
        [Authorize()]
        public IHttpActionResult FitaPatrocinioExcluir([FromBody]FitaPatrocinio.FitaPatrocinioModel filtro)
        {
            SimLib clsLib = new SimLib();
            FitaPatrocinio Cls = new FitaPatrocinio(User.Identity.Name);
            try
            {
                Cls.FitaPatrocinioExcluir(filtro);
                return Ok(true);

            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        //=================================Ver Contratos da Fita
        [Route("api/FitaPatrocinioContratos")]
        [HttpPost]
        [ActionName("FitaPatrocinioContratos")]
        [Authorize()]
        public IHttpActionResult FitaPatrocinioContratos([FromBody]FitaPatrocinio.FitaPatrocinioModel filtro)
        {
            SimLib clsLib = new SimLib();
            FitaPatrocinio Cls = new FitaPatrocinio(User.Identity.Name);
            try
            {
                DataTable Retorno= Cls.FitaPatrocinioContratos(filtro);
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