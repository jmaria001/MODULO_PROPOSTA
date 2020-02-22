using System;
using System.Web.Http;
using System.Data;
namespace PROPOSTA
{
    public class QualidadeController : ApiController
    {
        //=================================Lista de qualidade
        [Route("api/QualidadeListar")]
        [HttpGet]
        [ActionName("QualidadeListar")]
        [Authorize()]
        public IHttpActionResult QualidadeListar()
        {
            SimLib clsLib = new SimLib();
            Qualidade Cls = new Qualidade(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.QualidadeListar(0);
                return Ok(dtb);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        ////=================================Obtem dados da qualidade
        [Route("api/GetQualidadeData/{Cod_Qualidade}")]
        [HttpGet]
        [ActionName("GetQualidadeData")]
        [Authorize()]
        public IHttpActionResult GetQualidadeData(String Cod_Qualidade)
        {
            SimLib clsLib = new SimLib();
            Qualidade Cls = new Qualidade(User.Identity.Name);
            try
            {
                Qualidade.QualidadeModel Retorno = new Qualidade.QualidadeModel();
                if (Cod_Qualidade != "0")
                {
                    Retorno = Cls.GetQualidadeData(Cod_Qualidade);

                }
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        //===========================Salvar Qualidde

        [Route("api/SalvarQualidade")]
        [HttpPost]
        [ActionName("SalvarQualidade")]
        [Authorize()]

        public IHttpActionResult SalvarQualidade([FromBody] Qualidade.QualidadeModel pQualidade)
        {
            SimLib clsLib = new SimLib();
            Qualidade Cls = new Qualidade(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.SalvarQualidade(pQualidade);
                return Ok(retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //===========================Excluir Qualidade

        [Route("api/ExcluirQualidade")]
        [HttpPost]
        [ActionName("ExcluirQualidade")]
        [Authorize()]

        public IHttpActionResult ExcluirQualidade([FromBody] Qualidade.QualidadeModel pQualidade)
        {
            SimLib clsLib = new SimLib();
            Qualidade Cls = new Qualidade(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.ExcluirQualidade(pQualidade);
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