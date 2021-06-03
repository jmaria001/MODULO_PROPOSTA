using System;
using System.Web.Http;
using System.Data;
using System.Collections.Generic;
namespace PROPOSTA
{
    public class PropagacaoMapaController : ApiController
    {
        //===========================Salvar Programa

        [Route("api/CarregarPropagacaoMapa")]
        [HttpPost]
        [ActionName("CarregarPropagacaoMapa")]
        [Authorize()]

        public IHttpActionResult CarregarPropagacaoMapa([FromBody] PropagacaoMapa.FiltroModel pPropagacaoMapa)
        {
            SimLib clsLib = new SimLib();
            PropagacaoMapa Cls = new PropagacaoMapa(User.Identity.Name);
            try
            {

                List<PropagacaoMapa.PropagacaoMapaModel> Propagacao_Mapa = new List<PropagacaoMapa.PropagacaoMapaModel> ();
                DataTable dtb = Cls.CarregarPropagacaoMapa(pPropagacaoMapa);

                foreach (DataRow drw in dtb.Rows)
                {
                    Propagacao_Mapa.Add(new  PropagacaoMapa.PropagacaoMapaModel()
                    {
                        Competencia     = drw["Competencia"].ToString(),
                        Mensagem_Status = drw["Mensagem_Status"].ToString(),
                        Indica_Erro     = drw["Indica_Erro"].ToString().ConvertToInt32(),
                        Cod_Empresa     = drw["Cod_Empresa"].ToString(),
                        Numero_Mr       = drw["Numero_Mr"].ToString().ConvertToInt32(),
                        Sequencia_Mr    = drw["Sequencia_Mr"].ToString().ConvertToInt32(),

                    });
                }

                return Ok(Propagacao_Mapa);

            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }






        //[Route("api/CarregarPropagacaoMapa")]
        //[HttpPost]
        //[ActionName("CarregarPropagacaoMapa")]
        //[Authorize()]

        //public IHttpActionResult CarregarPropagacaoMapa([FromBody] PropagacaoMapa.FiltroModel pPropagacaoMapa)
        //{
        //    SimLib clsLib = new SimLib();
        //    PropagacaoMapa Cls = new PropagacaoMapa(User.Identity.Name);
        //    try
        //    {
        //        DataTable retorno = Cls.CarregarPropagacaoMapa(pPropagacaoMapa);
        //        return Ok(retorno);

        //        // return Ok(pPropagacaoMapa);


        //    }
        //    catch (Exception Ex)
        //    {
        //        clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
        //        throw new Exception(Ex.Message);
        //    }
        //}




        ////===========================Carregar Propagacaao Mapa

        //[Route("api/CarregarPropagacaoMapa")]
        //[HttpPost]
        //[ActionName("CarregarPropagacaoMapa")]
        //[Authorize()]

        //public IHttpActionResult CarregarPropagacaoMapa([FromBody] PropagacaoMapa.FiltroModel pPropagacaoMapa)
        //{
        //    SimLib clsLib = new SimLib();
        //    PropagacaoMapa Cls = new PropagacaoMapa(User.Identity.Name);
        //    try
        //    {
        //        PropagacaoMapa.PropagacaoMapaModel retorno = Cls.CarregarPropagacaoMapa(pPropagacaoMapa);
        //        return Ok(retorno);

        //        // return Ok(pPropagacaoMapa);


        //    }
        //    catch (Exception Ex)
        //    {
        //        clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
        //        throw new Exception(Ex.Message);
        //    }
        //}





    }
}