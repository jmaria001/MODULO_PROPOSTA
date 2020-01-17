using System;
using System.Web.Http;
using System.Data;
namespace PROPOSTA
{
    public class MotivoAlterNegocController : ApiController
    {
        //=================================Lista de Dados do Cadastro
        [Route("api/MotivoAlterNegocListar")]
        [HttpGet]
        [ActionName("MotivoAlterNegocListar")]
        [Authorize()]
        public IHttpActionResult MotivoAlterNegocListar()
        {
            SimLib clsLib = new SimLib();
            MotivoAlterNegoc Cls = new MotivoAlterNegoc(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.MotivoAlterNegocListar(0);
                return Ok(dtb);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //=================================Obtem dados do Cadastro na tabela
        [Route("api/GetMotivoAlterNegocData/{Cod_Alteracao}")]
        [HttpGet]
        [ActionName("GetMotivoAlterNegocData")]
        [Authorize()]
        public IHttpActionResult GetMotivoAlterNegocData(String Cod_Alteracao)
        {
            SimLib clsLib = new SimLib();
            MotivoAlterNegoc Cls = new MotivoAlterNegoc(User.Identity.Name);
            try
            {
                MotivoAlterNegoc.MotivoAlterNegocModel Retorno = new MotivoAlterNegoc.MotivoAlterNegocModel();
                if (Cod_Alteracao != "0")
                {
                    Retorno = Cls.GetMotivoAlterNegocData(Cod_Alteracao);

                }
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //===========================Salvar dados do cadastro
        [Route("api/SalvarMotivoAlterNegoc")]
        [HttpPost]
        [ActionName("SalvarMotivoAlterNegoc")]
        [Authorize()]

        public IHttpActionResult SalvarMotivoAlterNegoc([FromBody] MotivoAlterNegoc.MotivoAlterNegocModel pMotivoAlterNegoc)
        {
            SimLib clsLib = new SimLib();
            MotivoAlterNegoc Cls = new MotivoAlterNegoc(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.SalvarMotivoAlterNegoc(pMotivoAlterNegoc);
                return Ok(retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }


        //===========================Excluir cadastro na tabela
        [Route("api/ExcluirMotivoAlterNegoc")]
        [HttpPost]
        [ActionName("ExcluirMotivoAlterNegoc")]
        [Authorize()]

        public IHttpActionResult ExcluirMotivoAlterNegoc([FromBody] MotivoAlterNegoc.MotivoAlterNegocModel pMotivoAlterNegoc)
        {
            SimLib clsLib = new SimLib();
            MotivoAlterNegoc Cls = new MotivoAlterNegoc(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.ExcluirMotivoAlterNegoc(pMotivoAlterNegoc);
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



