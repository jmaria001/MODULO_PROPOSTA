using System;
using System.Web.Http;
using System.Data;
using System.Collections.Generic;

namespace PROPOSTA
{
    public class DashBoardController : ApiController
    {
        //=================================Mode grafico de barra
        [Route("api/DashBoard/FunilVendas")]
        [HttpPost]
        [ActionName("FunilVendas")]
        [Authorize()]

        //public IHttpActionResult ModeloBarra([FromBody] DashBoard.FiltroModel param)
        public IHttpActionResult FunilVendas(DashBoard.FiltroFunilVendasModel param)
        {
            SimLib clsLib = new SimLib();
            DashBoard Cls = new DashBoard(User.Identity.Name);
            try
            {
                DashBoard.GraphModel dtb = Cls.FunilVendas(param);
                return Ok(dtb);

            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        //=================================Mode grafico de barra
        [Route("api/DashBoard/ModeloBarra")]
        [HttpPost]
        [ActionName("ModeloBarra")]
        [Authorize()]

        //public IHttpActionResult ModeloBarra([FromBody] DashBoard.FiltroModel param)
        public IHttpActionResult ModeloBarra(DashBoard.FiltroModel param)
        {
            SimLib clsLib = new SimLib();
            DashBoard Cls = new DashBoard(User.Identity.Name);
            try
            {
                DashBoard.GraphModel dtb = Cls.ModeloBarra(param);
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
        [ActionName("ModeloLinha")]
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

        //=================================Mode grafico de Pie
        [Route("api/DashBoard/ModeloPie")]
        [HttpPost]
        [ActionName("ModeloPie")]
        [Authorize()]

        public IHttpActionResult ModeloPie([FromBody] DashBoard.FiltroModel param)
        {
            SimLib clsLib = new SimLib();
            DashBoard Cls = new DashBoard(User.Identity.Name);
            try
            {
                DashBoard.GraphPieModel dtb = Cls.ModeloPie(param);
               return Ok(dtb);
            //return Ok(param);

            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        // Evolução de Vendas

        //=================================Mode grafico de barra
        [Route("api/DashBoard/ModeloBarraEvolucaoVendas")]
        [HttpPost]
        [ActionName("ModeloBarraEvolucaoVendas")]
        [Authorize()]

        //public IHttpActionResult ModeloBarra([FromBody] DashBoard.FiltroModel param)
        public IHttpActionResult ModeloBarraEvolucaoVendas(DashBoard.FiltroModel param)
        {
            SimLib clsLib = new SimLib();
            DashBoard Cls = new DashBoard(User.Identity.Name);
            try
            {
                DashBoard.GraphModel dtb = Cls.ModeloBarraEvolucaoVendas(param);
                return Ok(dtb);

            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        //=================================Mode grafico de linha
        [Route("api/DashBoard/ModeloLinhaEvolucaoVendas")]
        [HttpPost]
        [ActionName("ModeloLinhaEvolucaoVendas")]
        [Authorize()]

        public IHttpActionResult ModeloLinhaEvolucaoVendas([FromBody] DashBoard.FiltroModel param)
        {
            SimLib clsLib = new SimLib();
            DashBoard Cls = new DashBoard(User.Identity.Name);
            try
            {
                DashBoard.GraphModel dtb = Cls.ModeloLinhaEvolucaoVendas(param);
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



