using System;
using System.Web.Http;
using System.Data;
namespace PROPOSTA
{
    public class TabelaPrecosMolController : ApiController
    {
        //=================================Lista de Itens
        [Route("api/TabelaPrecosMolListar")]
        [HttpGet]
        [ActionName("TabelaPrecosMolListar")]
        [Authorize()]


        public IHttpActionResult TabelaPrecosMolListar([FromUri]TabelaPrecosMol.FiltroMolModel filtro)
        {
            SimLib clsLib = new SimLib();
            TabelaPrecosMol Cls = new TabelaPrecosMol(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.TabelaPrecosMolListar(filtro);
                return Ok(dtb);

            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        ////=================================Obtem dados do Tabela de Precos
        [Route("api/GetTabelaPrecosMolData/{Competencia},{Cod_Programa},{Cod_Veiculo_Mercado}")]
        [HttpGet]
        [ActionName("GetTabelaPrecosMolData")]
        [Authorize()]
        public IHttpActionResult GetTabelaPrecosMolData(String Competencia, String Cod_Programa, String Cod_Veiculo_Mercado)
        {
            SimLib clsLib = new SimLib();
            TabelaPrecosMol Cls = new TabelaPrecosMol(User.Identity.Name);
            try
            {
                TabelaPrecosMol.TabelaPrecosMolModel Retorno = new TabelaPrecosMol.TabelaPrecosMolModel();
                Retorno = Cls.GetTabelaPrecosMolData(Competencia, Cod_Programa, Cod_Veiculo_Mercado);
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //===========================Salvar Tabela de Precos
        [Route("api/SalvarTabelaPrecosMol")]
        [HttpPost]
        [ActionName("SalvarTabelaPrecosMol")]
        [Authorize()]

        public IHttpActionResult SalvarTabelaPrecosMol([FromBody] TabelaPrecosMol.TabelaPrecosMolModel pTabelaPrecosMol)
        {
            SimLib clsLib = new SimLib();
            TabelaPrecosMol Cls = new TabelaPrecosMol(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.SalvarTabelaPrecosMol(pTabelaPrecosMol);
                return Ok(retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }


        //===========================Excluir Itens
        [Route("api/ExcluirTabelaPrecosMol")]
        [HttpPost]
        [ActionName("ExcluirTabelaPrecosMol")]
        [Authorize()]
        public IHttpActionResult ExcluirTabelaPrecosMol([FromBody] TabelaPrecosMol.TabelaPrecosMolModel pTabelaPrecosMol)
        {
            SimLib clsLib = new SimLib();
            TabelaPrecosMol Cls = new TabelaPrecosMol(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.ExcluirTabelaPrecosMol(pTabelaPrecosMol);
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



