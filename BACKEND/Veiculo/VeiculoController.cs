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

        //=================================Obtem dados do Itens de Permuta
        [Route("api/GetVeiculoData/{Cod_Veiculo}")]
        [HttpGet]
        [ActionName("GetVeiculoData")]
        [Authorize()]
        public IHttpActionResult GetVeiculotaData(String Cod_Veiculo)
        {
            SimLib clsLib = new SimLib();
            Veiculo Cls = new Veiculo(User.Identity.Name);
            try
            {
                Veiculo.VeiculoModel Retorno = new Veiculo.VeiculoModel();
                if (Cod_Veiculo != "0")
                {
                    Retorno = Cls.GetVeiculoData(Cod_Veiculo);

                }
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //===========================Salvar Itens de Veiculo
        [Route("api/SalvarVeiculo")]
        [HttpPost]
        [ActionName("SalvarVeiculo")]
        [Authorize()]

        public IHttpActionResult SalvarVeiculo([FromBody] Veiculo.VeiculoModel pVeiculo)
        {
            SimLib clsLib = new SimLib();
            Veiculo Cls = new Veiculo(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.SalvarVeiculo(pVeiculo);
                return Ok(retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }


        //===========================Excluir Itens Veiculo
        [Route("api/ExcluirVeiculo")]
        [HttpPost]
        [ActionName("ExcluirVeiculo")]
        [Authorize()]

        public IHttpActionResult ExcluirVeiculo([FromBody] Veiculo.VeiculoModel pVeiculo)
        {
            SimLib clsLib = new SimLib();
            Veiculo Cls = new Veiculo(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.ExcluirVeiculo(pVeiculo);
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

