using System;
using System.Web.Http;
using System.Data;
using System.Collections.Generic;

namespace PROPOSTA
{
    public class PacoteController : ApiController
    {
        //=================================Lista de Pacote
        [Route("api/PacoteListar")]
        [HttpGet]
        [ActionName("PacoteListar")]
        [Authorize()]
        public IHttpActionResult PacoteListar()
        {
            SimLib clsLib = new SimLib();
            Pacote Cls = new Pacote(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.PacoteListar(0);
                return Ok(dtb);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //=================================Obtem dados do Pacote
        [Route("api/GetPacoteData/{Id_Pacote}")]
        [HttpGet]
        [ActionName("GetPacoteData")]
        [Authorize()]
        public IHttpActionResult GetPacoteData(Int32 Id_Pacote)
        {
            SimLib clsLib = new SimLib();
            Pacote Cls = new Pacote(User.Identity.Name);
            try
            {
                Pacote.PacoteModel Retorno = new Pacote.PacoteModel();
                Retorno.Max_Id_Desconto = 0;
                if (Id_Pacote == 0)
                {
                    Retorno.DescontoDetalhe = new List<Pacote.Desconto_DetalheModel>();
                }
                else
                {
                    Retorno = Cls.GetPacoteData(Id_Pacote);
                }
                
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        [Route("api/GetOpcoesDesconto/{p1}")]
        [HttpGet]
        [ActionName("GetOpcoesDesconto")]
        [Authorize()]
        public IHttpActionResult GetOpcoesDesconto(Int32 p1)
        {
            SimLib clsLib = new SimLib();
            Pacote Cls = new Pacote(User.Identity.Name);

            try
            {
                DataTable dtbRetorno = Cls.GetOpcoesDesconto(p1);
                return Ok(dtbRetorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        //===========================Salvar Pacote

        [Route("api/SalvarPacote")]
        [HttpPost]
        [ActionName("SalvarPacote")]
        [Authorize()]

        public IHttpActionResult SalvarPacote([FromBody] Pacote.PacoteModel pPacote)
        {
            SimLib clsLib = new SimLib();
            Pacote Cls = new Pacote(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.SalvarPacote(pPacote);
                return Ok(retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        ////===========================Excluir Pacote

        [Route("api/ExcluirPacote")]
        [HttpPost]
        [ActionName("ExcluirPacote")]
        [Authorize()]

        public IHttpActionResult ExcluirPacote([FromBody] Pacote.PacoteModel pPacote)
        {
            SimLib clsLib = new SimLib();
            Pacote Cls = new Pacote(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.ExcluirPacote(pPacote);
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

