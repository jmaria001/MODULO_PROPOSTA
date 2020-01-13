using System;
using System.Web.Http;
using System.Data;
namespace PROPOSTA
{
    public class CaracVeiculController : ApiController
    {
        //=================================Lista de Caracteristicas de Veiculação
        [Route("api/CaracVeiculListar")]
        [HttpGet]
        [ActionName("CaracVeiculListar")]
        [Authorize()]
        public IHttpActionResult CaracVeiculListar()
        {
            SimLib clsLib = new SimLib();
            CaracVeicul Cls = new CaracVeicul(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.CaracVeiculListar(0);
                return Ok(dtb);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //=================================Obtem dados das caracteristicas da veiculacao
        [Route("api/GetCaracVeiculData/{Cod_Caracteristica}")]
        [HttpGet]
        [ActionName("GetCaracVeiculData")]
        [Authorize()]
        public IHttpActionResult GetCaracVeiculData(String Cod_Caracteristica)
        {
            SimLib clsLib = new SimLib();
            CaracVeicul Cls = new CaracVeicul(User.Identity.Name);
            try
            {
                CaracVeicul.CaracVeiculModel Retorno = new CaracVeicul.CaracVeiculModel();
                if (Cod_Caracteristica != "0")
                {
                    Retorno = Cls.GetCaracVeiculData(Cod_Caracteristica);

                }
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //===========================Salvar Caracteristica da Veiculação
        [Route("api/SalvarCaracVeicul")]
        [HttpPost]
        [ActionName("SalvarCaracVeicul")]
        [Authorize()]

        public IHttpActionResult SalvarCaracVeicul([FromBody] CaracVeicul.CaracVeiculModel pCaracVeicul)
        {
            SimLib clsLib = new SimLib();
            CaracVeicul Cls = new CaracVeicul(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.SalvarCaracVeicul(pCaracVeicul);
                return Ok(retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }


        //===========================Excluir Caracteristica da Veiculação
        [Route("api/ExcluirCaracVeicul")]
        [HttpPost]
        [ActionName("ExcluirCaracVeicul")]
        [Authorize()]

        public IHttpActionResult ExcluirCaracVeicul([FromBody] CaracVeicul.CaracVeiculModel pCaracVeicul)
        {
            SimLib clsLib = new SimLib();
            CaracVeicul Cls = new CaracVeicul(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.ExcluirCaracVeicul(pCaracVeicul);
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



