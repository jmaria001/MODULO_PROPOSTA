using System;
using System.Web.Http;
using System.Data;
using System.Collections.Generic;

namespace PROPOSTA
{
    public class ParRetorPlayListController : ApiController
    {
        //----------------------- Filtrar Dados -------------------------
        [Route("api/ParRetorPlayListFiltrar")]
        [HttpGet]
        [ActionName("ParRetorPlayListFiltrar")]
        [Authorize()]
        public IHttpActionResult ParRetorPlayListFiltrar([FromUri] ParRetorPlayList.RetornoPlayListModel Param)
        {
            SimLib clsLib = new SimLib();
            ParRetorPlayList Cls = new ParRetorPlayList(User.Identity.Name);
            try
            {
                ParRetorPlayList.RetornoPlayListModel Retorno = Cls.ParRetorPlayListFiltrar(Param);
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //------------------------- Salvar dados ------------------------
        [Route("api/ParRetorPlayListSalvar")]
        [HttpPost]
        [ActionName("ParRetorPlayListSalvar")]
        [Authorize()]
        public IHttpActionResult ParRetorPlayListSalvar([FromBody] ParRetorPlayList.RetornoPlayListModel Param)
        {
            SimLib clsLib = new SimLib();
            ParRetorPlayList Cls = new ParRetorPlayList(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.ParRetorPlayListSalvar(Param);
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



