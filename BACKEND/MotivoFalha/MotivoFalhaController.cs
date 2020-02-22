using System;
using System.Web.Http;
using System.Data;
namespace PROPOSTA
{
    public class MotivoFalhaController : ApiController
    {
        //=================================Lista de Dados do Cadastro
        [Route("api/MotivoFalhaListar")]
        [HttpGet]
        [ActionName("MotivoFalhaListar")]
        [Authorize()]
        public IHttpActionResult MotivoFalhaListar()
        {
            SimLib clsLib = new SimLib();
            MotivoFalha Cls = new MotivoFalha(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.MotivoFalhaListar(0);
                return Ok(dtb);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //=================================Obtem dados do Cadastro na tabela
        [Route("api/GetMotivoFalhaData/{Cod_Motivo_Falha}")]
        [HttpGet]
        [ActionName("GetMotivoFalhaData")]
        [Authorize()]
        public IHttpActionResult GetMotivoFalhaData(String Cod_Motivo_Falha)
        {
            SimLib clsLib = new SimLib();
            MotivoFalha Cls = new MotivoFalha(User.Identity.Name);
            try
            {
                MotivoFalha.MotivoFalhaModel Retorno = new MotivoFalha.MotivoFalhaModel();
                if (Cod_Motivo_Falha != "0")
                {
                    Retorno = Cls.GetMotivoFalhaData(Cod_Motivo_Falha);

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
        [Route("api/SalvarMotivoFalha")]
        [HttpPost]
        [ActionName("SalvarMotivoFalha")]
        [Authorize()]

        public IHttpActionResult SalvarMotivoFalha([FromBody] MotivoFalha.MotivoFalhaModel pMotivoFalha)
        {
            SimLib clsLib = new SimLib();
            MotivoFalha Cls = new MotivoFalha(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.SalvarMotivoFalha(pMotivoFalha);
                return Ok(retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }


        //===========================Excluir cadastro na tabela
        [Route("api/ExcluirMotivoFalha")]
        [HttpPost]
        [ActionName("ExcluirMotivoFalha")]
        [Authorize()]

        public IHttpActionResult ExcluirMotivoFalha([FromBody] MotivoFalha.MotivoFalhaModel pMotivoFalha)
        {
            SimLib clsLib = new SimLib();
            MotivoFalha Cls = new MotivoFalha(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.ExcluirMotivoFalha(pMotivoFalha);
                return Ok(retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }



        //===================================Desativar/Reativar
        [Route("api/DesativarReativarMotivoFalha")]
        [HttpPost]
        [ActionName("DesativarReativarMotivoFalha")]
        [Authorize()]

        public IHttpActionResult DesativarReativarMotivoFalha([FromBody] MotivoFalha.MotivoFalhaModel pMotivoFalha)
        {
            SimLib clsLib = new SimLib();
            MotivoFalha Cls = new MotivoFalha(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.DesativarReativarMotivoFalha(pMotivoFalha);
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



