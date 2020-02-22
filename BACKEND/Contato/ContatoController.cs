using System;
using System.Web.Http;
using System.Data;
namespace PROPOSTA
{
    public class ContatoController : ApiController
    {
        //=================================Lista de contato
        [Route("api/ContatoListar")]
        [HttpGet]
        [ActionName("ContatoListar")]
        [Authorize()]
        public IHttpActionResult ContatoListar()
        {
            SimLib clsLib = new SimLib();
            Contato Cls = new Contato(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.ContatoListar(0);
                return Ok(dtb);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        ////=================================Obtem dados do contato
        [Route("api/GetContatoData/{Cod_Contato}")]
        [HttpGet]
        [ActionName("GetContatoData")]
        [Authorize()]
        public IHttpActionResult GetContatoData(String Cod_Contato)
        {
            SimLib clsLib = new SimLib();
            Contato Cls = new Contato(User.Identity.Name);
            try
            {
                Contato.ContatoModel Retorno = new Contato.ContatoModel();
                if (Cod_Contato != "0")
                {
                    Retorno = Cls.GetContatoData(Cod_Contato);

                }
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //===========================Salvar Contato

        [Route("api/SalvarContato")]
        [HttpPost]
        [ActionName("SalvarContato")]
        [Authorize()]

        public IHttpActionResult SalvarContato([FromBody] Contato.ContatoModel pContato)
        {
            SimLib clsLib = new SimLib();
            Contato Cls = new Contato(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.SalvarContato(pContato);
                return Ok(retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //===========================Excluir Contato

        [Route("api/ExcluirContato")]
        [HttpPost]
        [ActionName("ExcluirContato")]
        [Authorize()]

        public IHttpActionResult ExcluirContato([FromBody] Contato.ContatoModel pContato)
        {
            SimLib clsLib = new SimLib();
            Contato Cls = new Contato(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.ExcluirContato(pContato);
                return Ok(retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //===========================Reativa Contato

        [Route("api/ReativarContato")]
        [HttpPost]
        [ActionName("ReativarContato")]
        [Authorize()]
        public IHttpActionResult ReativarContato([FromBody] Contato.ContatoModel Contato)
        {
            SimLib clsLib = new SimLib();
            Contato Cls = new Contato(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.ReativarContato(Contato.Cod_Contato);
                return Ok(dtb);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //===========================Desativa Contato

        [Route("api/DesativarContato")]
        [HttpPost]
        [ActionName("DesativarContato")]
        [Authorize()]
        public IHttpActionResult DesativarContato([FromBody] Contato.ContatoModel Contato)
        {
            SimLib clsLib = new SimLib();
            Contato Cls = new Contato(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.DesativarContato(Contato.Cod_Contato);
                return Ok(dtb);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
    }
}

