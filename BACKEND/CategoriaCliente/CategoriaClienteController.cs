using System;
using System.Web.Http;
using System.Data;
namespace PROPOSTA
{
    public class CategoriaClienteController : ApiController
    {
        //=================================Lista de Categorias de Clientes
        [Route("api/CategoriaClienteListar")]
        [HttpGet]
        [ActionName("CategoriaClienteListar")]
        [Authorize()]
        public IHttpActionResult CategoriaClienteListar()
        {
            SimLib clsLib = new SimLib();
            CategoriaCliente Cls = new CategoriaCliente(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.CategoriaClienteListar(0);
                return Ok(dtb);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }


        //=================================Obtem dados da Categoria de Cliente
        [Route("api/GetCategoriaClienteData/{Cod_Categoria}")]
        [HttpGet]
        [ActionName("GetCategoriaClienteData")]
        [Authorize()]
        public IHttpActionResult GetCategoriaClienteData(Int32 Cod_Categoria)
        {
            SimLib clsLib = new SimLib();
            CategoriaCliente Cls = new CategoriaCliente(User.Identity.Name);
            try
            {
                CategoriaCliente.CategoriaClienteModel Retorno = new CategoriaCliente.CategoriaClienteModel();
                if (Cod_Categoria != 0)
                {
                    Retorno = Cls.GetCategoriaClienteData(Cod_Categoria);

                }
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //===========================Salvar Categoria de Cliente
        [Route("api/SalvarCategoriaCliente")]
        [HttpPost]
        [ActionName("SalvarCategoriaCliente")]
        [Authorize()]

        public IHttpActionResult SalvarCategoriaCliente([FromBody] CategoriaCliente.CategoriaClienteModel pCategoriaCliente)
        {
            SimLib clsLib = new SimLib();
            CategoriaCliente Cls = new CategoriaCliente(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.SalvarCategoriaCliente(pCategoriaCliente);
                return Ok(retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }


        //===========================Excluir Categoria de Cliente
        [Route("api/ExcluirCategoriaCliente")]
        [HttpPost]
        [ActionName("ExcluirCategoriaCliente")]
        [Authorize()]

        public IHttpActionResult ExcluirCategoriaCliente([FromBody] CategoriaCliente.CategoriaClienteModel pCategoriaCliente)
        {
            SimLib clsLib = new SimLib();
            CategoriaCliente Cls = new CategoriaCliente(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.ExcluirCategoriaCliente(pCategoriaCliente);
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



