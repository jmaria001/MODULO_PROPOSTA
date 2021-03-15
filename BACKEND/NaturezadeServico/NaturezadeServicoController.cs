using System;
using System.Web.Http;
using System.Data;
using System.Collections.Generic;

namespace PROPOSTA
{
    public class NaturezadeServicoController : ApiController
    {
        //=================================Lista de Fitas Avulsos e Artistico
        [Route("api/NaturezadeServicoListar")]
        [HttpGet]
        [ActionName("NaturezadeServicoListar")]
        [Authorize()]


        public IHttpActionResult NaturezadeServicoListar([FromUri]NaturezadeServico.FiltroModel filtro)
        {
            SimLib clsLib = new SimLib();
            NaturezadeServico Cls = new NaturezadeServico(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.NaturezadeServicoListar(filtro);
                return Ok(dtb);


            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }


        [Route("api/GetNaturezadeServicoData")]
        [HttpGet]
        [ActionName("GetNaturezadeServicoData")]
        [Authorize()]
        public IHttpActionResult GetNaturezadeServicoData( String Cod_Natureza, String Cod_Empresa)
        {
            SimLib clsLib = new SimLib();
            NaturezadeServico Cls = new NaturezadeServico(User.Identity.Name);
            try
            {

                NaturezadeServico.NaturezadeServicoModel Retorno = new NaturezadeServico.NaturezadeServicoModel();
                Retorno = Cls.GetNaturezadeServicoData(Cod_Natureza,Cod_Empresa);
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //===========================Salvar Natureza de Serviço

        [Route("api/SalvarNaturezadeServico")]
        [HttpPost]
        [ActionName("SalvarNaturezadeServico")]
        [Authorize()]

        public IHttpActionResult SalvarNaturezadeServico([FromBody] NaturezadeServico.NaturezadeServicoModel pNaturezadeServico)
        {
            SimLib clsLib = new SimLib();
            NaturezadeServico Cls = new NaturezadeServico(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.SalvarNaturezadeServico(pNaturezadeServico);

                return Ok(retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //===========================Excluir Natureza de Serviço
                    
        [Route("api/ExcluirNaturezadeServico")]
        [HttpPost]
        [ActionName("ExcluirNaturezadeServico")]
        [Authorize()]

        public IHttpActionResult ExcluirNaturezadeServico([FromBody] NaturezadeServico.NaturezadeServicoModel pNaturezadeServico)
        {
            SimLib clsLib = new SimLib();
            NaturezadeServico Cls = new NaturezadeServico(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.ExcluirNaturezadeServico(pNaturezadeServico);
                return Ok(retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //===================================Desativar/Reativar
        [Route("api/DesativarReativarNaturezadeServico")]
        [HttpPost]
        [ActionName("DesativarReativarNaturezadeServico")]
        [Authorize()]

        public IHttpActionResult DesativarReativarNaturezadeServico([FromBody] NaturezadeServico.NaturezadeServicoModel pNaturezadeServico)
        {
            SimLib clsLib = new SimLib();
            NaturezadeServico Cls = new NaturezadeServico(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.DesativarReativarNaturezadeServico(pNaturezadeServico);
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