using System;
using System.Web.Http;
using System.Data;
using System.Collections.Generic;

namespace PROPOSTA
{
    public class RoteiroController : ApiController
    {
        //=================================Lista de Roteiros
        [Route("api/Roteiro/GuiaProgramacao")]
        [HttpPost]
        [ActionName("RoteiroGuiaProgramacao")]
        [Authorize()]
        public IHttpActionResult RoteiroGuiaProgramacao([FromBody]Roteiro.RoteiroFiltroModel Filtro)
        {
            SimLib clsLib = new SimLib();
            Roteiro Cls = new Roteiro(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.CarregarGuiaProgramacao(Filtro);
                return Ok(dtb);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        //=================================Lista de Roteiros
        [Route("api/Roteiro/CarregarRoteiro")]
        [HttpPost]
        [ActionName("CarregarRoteiro")]
        [Authorize()]
        public IHttpActionResult CarregarRoteiro([FromBody]Roteiro.RoteiroFiltroModel Filtro)
        {
            SimLib clsLib = new SimLib();
            Roteiro Cls = new Roteiro(User.Identity.Name);
            try
            {
                List<Roteiro.RoteiroModel> Roteiro = Cls.RoteiroCarregar(Filtro);
                return Ok(Roteiro);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        //=================================Lista de Comerciais
        [Route("api/Roteiro/CarregarComerciais")]
        [HttpPost]
        [ActionName("RoteiroCarregarComerciais")]
        [Authorize()]
        public IHttpActionResult RoteiroCarregarComerciais([FromBody]Roteiro.RoteiroFiltroModel Filtro)
        {
            SimLib clsLib = new SimLib();
            Roteiro Cls = new Roteiro(User.Identity.Name);
            try
            {
                List<Roteiro.RoteiroComercialModel>   Comerciais = Cls.RoteiroCarregarComerciais(Filtro);
                return Ok(Comerciais);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        //=================================Baixa de comerciais no roteiro
        [Route("api/Roteiro/BaixarVeiculacao")]
        [HttpPost]
        [ActionName("RoteiroBaixarVeiculacao")]
        [Authorize()]
        public IHttpActionResult RoteiroBaixarVeiculacao([FromBody]Roteiro.RoteiroModel pRoteiro)
        {
            SimLib clsLib = new SimLib();
            Roteiro Cls = new Roteiro(User.Identity.Name);
            try
            {
                DataTable Retorno = Cls.RoteiroBaixarVeiculacao(pRoteiro);
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        //=================================Exclusao do Roteiro
        [Route("api/Roteiro/Excluir")]
        [HttpPost]
        [ActionName("RoteiroExcluir")]
        [Authorize()]
        public IHttpActionResult RoteiroExcluir([FromBody]Roteiro.RoteiroFiltroModel pRoteiro)
        {
            SimLib clsLib = new SimLib();
            Roteiro Cls = new Roteiro(User.Identity.Name);
            try
            {
                Cls.RoteiroExcluir(pRoteiro);
                return Ok(true);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //=================================Salvar Ordenacao
        [Route("api/Roteiro/Salvar")]
        [HttpPost]
        [ActionName("RoteiroSalvar")]
        [Authorize()]
        public IHttpActionResult RoteiroSalvar ([FromBody]List<Roteiro.RoteiroModel> pRoteiro)
        {
            SimLib clsLib = new SimLib();
            Roteiro Cls = new Roteiro(User.Identity.Name);
            try
            {
                DataTable Retorno =  Cls.RoteiroSalvar(pRoteiro);
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        //=================================Salvar Ordenacao
        [Route("api/Roteiro/ListarBreak")]
        [HttpGet]
        [ActionName("RoteiroListarBreak")]
        [Authorize()]
        public IHttpActionResult RoteiroListarBreak([FromUri] Roteiro.RoteiroFiltroModel Param)
        {
            SimLib clsLib = new SimLib();
            Roteiro Cls = new Roteiro(User.Identity.Name);
            try
            {
                Roteiro.BreakModel Retorno = Cls.RoteiroListarBreak(Param);
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }


        [Route("api/Roteiro/GravarBreak")]
        [HttpPost]
        [ActionName("RoteiroGravarBreak")]
        [Authorize()]
        public IHttpActionResult RoteiroGravarBreak([FromBody] Roteiro.BreakModel Param)
        {
            SimLib clsLib = new SimLib();
            Roteiro Cls = new Roteiro(User.Identity.Name);
            try
            {
                DataTable Retorno = Cls.RoteiroGravarBreak(Param);
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        [Route("api/Roteiro/ProgramasBreak")]
        [HttpGet]
        [ActionName("RoteiroProgramasBreak")]
        [Authorize()]
        public IHttpActionResult RoteiroProgramasBreak([FromUri] Roteiro.RoteiroFiltroModel Param)
        {
            SimLib clsLib = new SimLib();
            Roteiro Cls = new Roteiro(User.Identity.Name);
            try
            {
                DataTable Retorno = Cls.RoteiroProgramasBreak(Param);
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        [Route("api/Roteiro/PreOrdenar")]
        [HttpPost]
        [ActionName("PreOrdenar")]
        [Authorize()]
        public IHttpActionResult PreOrdenar([FromBody] Roteiro.FiltroPreOrdModel Roteiro)
        {
            SimLib clsLib = new SimLib();
            Roteiro Cls = new Roteiro(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.PreOrdenar(Roteiro);
                return Ok(retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        [Route("api/Roteiro/ExisteRoteiroOrdenado")]
        [HttpGet]
        [ActionName("ExisteRoteiroOrdenado")]
        [Authorize()]
        public IHttpActionResult ExisteRoteiroOrdenado([FromUri] Roteiro.FiltroPreOrdModel Roteiro)
        {
            SimLib clsLib = new SimLib();
            Roteiro Cls = new Roteiro(User.Identity.Name);
            try
            {
                Boolean Retorno = Cls.ExisteRoteiroOrdenado(Roteiro);
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

