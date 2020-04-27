using System;
using System.Web.Http;
using System.Data;
namespace PROPOSTA
{
    public class TabelaPrecosController : ApiController
    {
        //=================================Lista de Itens de Permuta
        [Route("api/TabelaPrecosListar")]
        [HttpGet]
        [ActionName("TabelaPrecosListar")]
        [Authorize()]

        
        public IHttpActionResult TabelaPrecosListar([FromUri]TabelaPrecos.FiltroModel filtro)
        {
            SimLib clsLib = new SimLib();
            TabelaPrecos Cls = new TabelaPrecos(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.TabelaPrecosListar(filtro);
                return Ok(dtb);

            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }


        ////=================================Obtem dados do Tabela de Precos
        [Route("api/GetTabelaPrecosData/{Competencia},{Cod_Programa},{Cod_Veiculo_Mercado}")]
        //[Route("api/GetTabelaPrecosData/{Competencia}")]
        [HttpGet]
        [ActionName("GetTabelaPrecosData")]
        [Authorize()]
        public IHttpActionResult GetTabelaPrecosData(String Competencia, String Cod_Programa, String Cod_Veiculo_Mercado)
       
        {
            SimLib clsLib = new SimLib();
            TabelaPrecos Cls = new TabelaPrecos(User.Identity.Name);
            try
            {
                TabelaPrecos.TabelaPrecosModel Retorno = new TabelaPrecos.TabelaPrecosModel();
                if (Competencia != "0")
                {

                    
                    Retorno = Cls.GetTabelaPrecosData(Competencia, Cod_Programa, Cod_Veiculo_Mercado);
                    

                }
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //===========================Salvar Tabela de Precos
        [Route("api/SalvarTabelaPrecos")]
        [HttpPost]
        [ActionName("SalvarTabelaPrecos")]
        [Authorize()]

        public IHttpActionResult SalvarTabelaPrecos([FromBody] TabelaPrecos.TabelaPrecosModel pTabelaPrecos)
        {
            SimLib clsLib = new SimLib();
            TabelaPrecos Cls = new TabelaPrecos(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.SalvarTabelaPrecos(pTabelaPrecos);
                return Ok(retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }


        //===========================Excluir Itens
        [Route("api/ExcluirTabelaPrecos")]
        [HttpPost]
        [ActionName("ExcluirTabelaPrecos")]
        [Authorize()]

        public IHttpActionResult ExcluirTabelaPrecos([FromBody] TabelaPrecos.TabelaPrecosModel pTabelaPrecos)
        {
            SimLib clsLib = new SimLib();
            TabelaPrecos Cls = new TabelaPrecos(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.ExcluirTabelaPrecos(pTabelaPrecos);
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



