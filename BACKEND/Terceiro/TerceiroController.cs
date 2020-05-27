using System;
using System.Web.Http;
using System.Data;
using System.Collections.Generic;

namespace PROPOSTA
{
    public class TerceiroController : ApiController
    {
        //=================================Lista de Terceiro
        [Route("api/TerceiroListar")]
        [HttpGet]
        [ActionName("TerceiroListar")]
        [Authorize()]

        public IHttpActionResult TerceiroListar([FromUri] Terceiro.TerceiroFiltroModel param)
        {
            SimLib clsLib = new SimLib();
            Terceiro Cls = new Terceiro(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.TerceiroListar(param);
                return Ok(dtb);
                
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        ////=================================Obtem Dados do Terceiro
        [Route("api/GetTerceiroData/{Cod_Terceiro}")]
        [HttpGet]
        [ActionName("GetTerceiroData")]
        [Authorize()]
        public IHttpActionResult GetTerceiroData(String Cod_Terceiro)
        {
            SimLib clsLib = new SimLib();
            Terceiro Cls = new Terceiro(User.Identity.Name);
            try
            {
                
                Terceiro.TerceiroModel Retorno = new Terceiro.TerceiroModel();
                if (Cod_Terceiro != "0")
                {
                    Retorno = Cls.GetTerceiroData(Cod_Terceiro);

                }
                else
                {
                    Retorno.Permite_Editar = true;
                    //Retorno.Empresas = new List<Terceiro.TerceiroEmpresasModel>();
                    Retorno.Empresas = Cls.AddEmpresas("");
                    Retorno.Enderecos= new List<Terceiro.TerceiroEnderecoModel>() { new Terceiro.TerceiroEnderecoModel()};
                    Retorno.Complementar= new List<Terceiro.TerceiroComplementarModel>() { new Terceiro.TerceiroComplementarModel()} ;
                }
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //===========================Salvar Terceiro
        [Route("api/SalvarTerceiro")]
        [HttpPost]
        [ActionName("SalvarTerceiro")]
        [Authorize()]

        public IHttpActionResult SalvarTerceiro([FromBody] Terceiro.TerceiroModel pCod_Terceiro)
        {
            SimLib clsLib = new SimLib();
            Terceiro Cls = new Terceiro(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.SalvarTerceiro(pCod_Terceiro);
                return Ok(retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //===========================Excluir Terceiro
        [Route("api/ExcluirTerceiro")]
        [HttpPost]
        [ActionName("ExcluirTerceiro")]
        [Authorize()]

        public IHttpActionResult ExcluirTerceiro([FromBody] Terceiro.TerceiroModel pTerceiro)
        {
            SimLib clsLib = new SimLib();
            Terceiro Cls = new Terceiro(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.ExcluirTerceiro( pTerceiro);
                return Ok(retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        //===========================Reativa Terceiro
        [Route("api/ReativarTerceiro")]
        [HttpPost]
        [ActionName("ReativarTerceiro")]
        [Authorize()]
        public IHttpActionResult ReativarTerceiro([FromBody] Terceiro.TerceiroModel Terceiro)
        {
            SimLib clsLib = new SimLib();
            Terceiro Cls = new Terceiro(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.ReativarTerceiro(Terceiro);
                return Ok(dtb);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //===========================Desativa Terceiro
        [Route("api/DesativarTerceiro")]
        [HttpPost]
        [ActionName("DesativarTerceiro")]
        [Authorize()]
        public IHttpActionResult DesativarTerceiro([FromBody] Terceiro.TerceiroModel Terceiro)
        {
            SimLib clsLib = new SimLib();
            Terceiro Cls = new Terceiro(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.DesativarTerceiro(Terceiro);
                return Ok(dtb);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        ////=================================Obtem Dados do Terceiro
        [Route("api/GetCodigoIbge/{Cod_Municipio}")]
        [HttpGet]
        [ActionName("GetCodigoIbge")]
        [Authorize()]
        public IHttpActionResult GetCodigoIbge(String Cod_Municipio)
        {
            SimLib clsLib = new SimLib();
            Terceiro Cls = new Terceiro(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.GetCodigoIbge(Cod_Municipio);
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

