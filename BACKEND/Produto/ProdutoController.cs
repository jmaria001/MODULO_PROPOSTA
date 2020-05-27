using System;
using System.Web.Http;
using System.Data;
using System.Collections.Generic;

namespace PROPOSTA
{
    public class ProdutoController : ApiController
    {
        //=================================Lista de produto
        [Route("api/ProdutoListar")]
        [HttpGet]
        [ActionName("ProdutoListar")]
        [Authorize()]

        public IHttpActionResult ProdutoListar([FromUri]Produto.ProdutoModel Param)
        {
            SimLib clsLib = new SimLib();
            Produto Cls = new Produto(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.ProdutoListar(Param);
                return Ok(dtb);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        //=================================Obtem dados do produto
        [Route("api/GetProdutoData/{Cod_Red_Produto}")]
        [HttpGet]
        [ActionName("GetProdutoData")]
        [Authorize()]

        public IHttpActionResult SetorListar(Int16 Cod_Red_Produto)
        {
            SimLib clsLib = new SimLib();
            Produto Cls = new Produto(User.Identity.Name);
            try
            {
                Produto.ProdutoModel Retorno = new Produto.ProdutoModel();
                if (Cod_Red_Produto != 0)
                {
                    Retorno = Cls.GetProdutoData(Cod_Red_Produto);
                }
                else
                {
                    Retorno.Clientes = new List<Produto.ProdutoClienteModel>();
                }
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        //=================================Obtem dados do produto
        [Route("api/SetorListar/{Cod_Segmento}")]
        [HttpGet]
        [ActionName("SetorListar")]
        [Authorize()]
        public IHttpActionResult GetProdutoData(Int16 Cod_Segmento)
        {
            SimLib clsLib = new SimLib();
            Produto Cls = new Produto(User.Identity.Name);
            try
            {
                
                DataTable Retorno = Cls.SetorListar(Cod_Segmento);
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }



        //===========================Salvar Produto
        [Route("api/SalvarProduto")]
        [HttpPost]
        [ActionName("SalvarProduto")]
        [Authorize()]

        public IHttpActionResult SalvarProduto([FromBody] Produto.ProdutoModel pProduto)
        {
            SimLib clsLib = new SimLib();
            Produto Cls = new Produto(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.SalvarProduto(pProduto);
                return Ok(retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //===========================Excluir Produto

        [Route("api/ExcluirProduto")]
        [HttpPost]
        [ActionName("ExcluirProduto")]
        [Authorize()]

        public IHttpActionResult ExcluirProduto([FromBody] Produto.ProdutoModel pProduto)
        {
            SimLib clsLib = new SimLib();
            Produto Cls = new Produto(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.ExcluirProduto(pProduto);
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