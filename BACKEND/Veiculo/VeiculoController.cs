using System;
using System.Web.Http;
using System.Data;
namespace PROPOSTA
{
    public class VeiculoController : ApiController
    {
        [Route("api/VeiculoListar")]
        [HttpGet]
        [ActionName("VeiculoListar")]
        [Authorize()]
        public IHttpActionResult VeiculoListar()
        {
            SimLib clsLib = new SimLib();
            Veiculo Cls = new Veiculo(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.VeiculoListar(0);
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

