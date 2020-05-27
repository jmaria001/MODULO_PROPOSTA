using System;
using System.Web.Http;
using System.Data;
namespace PROPOSTA
{
    public class GeneroController : ApiController
    {
        //=================================Lista de Genero
        [Route("api/GeneroListar")]
        [HttpGet]
        [ActionName("GeneroListar")]
        [Authorize()]
        public IHttpActionResult GeneroListar()
        {
            SimLib clsLib = new SimLib();
            Genero Cls = new Genero(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.GeneroListar(0);
                return Ok(dtb);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        ////=================================Obtem Dados do Genero
        [Route("api/GetGeneroData/{Cod_Genero}")]
        [HttpGet]
        [ActionName("GetGeneroData")]
        [Authorize()]
        public IHttpActionResult GetGeneroData(String Cod_Genero)
        {
            SimLib clsLib = new SimLib();
            Genero Cls = new Genero(User.Identity.Name);
            try
            {
                Genero.GeneroModel Retorno = new Genero.GeneroModel();
                if (Cod_Genero != "0")
                {
                    Retorno = Cls.GetGeneroData(Cod_Genero);

                }
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //===========================Salvar Genero

        [Route("api/SalvarGenero")]
        [HttpPost]
        [ActionName("SalvarGenero")]
        [Authorize()]

        public IHttpActionResult SalvarGenero([FromBody] Genero.GeneroModel pGenero)
        {
            SimLib clsLib = new SimLib();
            Genero Cls = new Genero(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.SalvarGenero(pGenero);
                return Ok(retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //===========================Excluir Genero

        [Route("api/excluirGenero")]
        [HttpPost]
        [ActionName("excluirGenero")]
        [Authorize()]

        public IHttpActionResult excluirGenero([FromBody] Genero.GeneroModel pGenero)
        {
            SimLib clsLib = new SimLib();
            Genero Cls = new Genero(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.excluirGenero(pGenero);
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

