using System;
using System.Web.Http;
using System.Data;
using System.Collections.Generic;

namespace PROPOSTA
{
    public class DashBoardController : ApiController
    {
        //=================================Mode grafico de barra
        [Route("api/DashBoard/ModeloBarra")]
        [HttpPost]
        [ActionName("ModeloBarra")]
        [Authorize()]

        public IHttpActionResult ModeloBarra([FromBody] DashBoard.FiltroModel param)
        {
            SimLib clsLib = new SimLib();
            DashBoard Cls = new DashBoard(User.Identity.Name);
            try
            {
                DashBoard.GraphModel  dtb = Cls.ModeloBarra(param);
                return Ok(dtb);
                
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        //=================================Mode grafico de linha
        [Route("api/DashBoard/ModeloLinha")]
        [HttpPost]
        [ActionName("ModeloBarra")]
        [Authorize()]

        public IHttpActionResult ModeloLinha([FromBody] DashBoard.FiltroModel param)
        {
            SimLib clsLib = new SimLib();
            DashBoard Cls = new DashBoard(User.Identity.Name);
            try
            {
                DashBoard.GraphModel dtb = Cls.ModeloLinha(param);
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

