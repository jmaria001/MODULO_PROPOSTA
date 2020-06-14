using System;
using System.Web.Http;
using System.Data;
using System.Collections.Generic;
using System.Globalization;

namespace PROPOSTA
{
    public class GradeController : ApiController
    {
        [Route("api/Grade/List")]
        [HttpGet]
        [ActionName("GradeList")]
        [Authorize()]
        public IHttpActionResult GradeList([FromUri]Grade.GradeFiltroModel Filtro)
        {
            SimLib clsLib = new SimLib();
            Grade Cls = new Grade(User.Identity.Name);
            try
            {
                Grade.GradeListModel Retorno = Cls.GradeList(Filtro);
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

