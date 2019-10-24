using System;
using System.Web.Http;
using System.Data;
using System.Collections.Generic;

namespace PROPOSTA
{
    public class RegraAprovacaoController : ApiController
    {
        [Route("api/RegraAprovacaoListar")]
        [HttpGet]
        [ActionName("RegraAprovacaoListar")]
        [Authorize()]
        public IHttpActionResult RegraAprovacaoListar()
        {
            SimLib clsLib = new SimLib();
            RegraAprovacao Cls = new RegraAprovacao(User.Identity.Name);
            try
            {
                List<RegraAprovacao.Regra_Aprovacao_Model> Regra= Cls.RegraAprovacaoListar();
                return Ok(Regra);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        [Route("api/GetRegraAprovacao/{pIdRegra}")]
        [HttpGet]
        [ActionName("GetRegraAprovacao")]
        [Authorize()]
        public IHttpActionResult GetRegraAprovacao(Int32 pIdRegra)
        {
            SimLib clsLib = new SimLib();
            RegraAprovacao Cls = new RegraAprovacao(User.Identity.Name);
            try
            {
                RegraAprovacao.Regra_Aprovacao_Model Regra = new RegraAprovacao.Regra_Aprovacao_Model();
                if (pIdRegra == 0)
                {
                    //RegraAprovacao.Range_Model Range = new RegraAprovacao.Range_Model();
                    //Range.Aprovadores = new List<RegraAprovacao.Aprovador_Model>();
                    Regra.Range =(new List<RegraAprovacao.Range_Model>());
                    Regra.Empresas= (new List<RegraAprovacao.Regra_Aprovacao_Empresa_Model>());
                    Regra.Veiculos = (new List<RegraAprovacao.Regra_Aprovacao_Veiculo_Model>());
                    Regra.Agencias= (new List<RegraAprovacao.Regra_Aprovacao_Agencia_Model>());
                    Regra.Clientes= (new List<RegraAprovacao.Regra_Aprovacao_Cliente_Model>());

                }
                else
                {
                    Regra = Cls.GetRegraAprovacao(pIdRegra);
                }

                return Ok(Regra);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        [Route("api/SalvarRegraAprovacao")]
        [HttpPost]
        [ActionName("SalvarRegraAprovacao")]
        [Authorize()]

        public IHttpActionResult SalvarRegraAprovacao([FromBody] RegraAprovacao.Regra_Aprovacao_Model RegraAprovacao)
        {
            SimLib clsLib = new SimLib();
            RegraAprovacao Cls = new RegraAprovacao(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.SalvarRegraAprovacao(RegraAprovacao);
                return Ok(retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }


        [Route("api/ExcluirRegraAprovacao")]
        [HttpPost]
        [ActionName("ExcluirRegraAprovacao")]
        [Authorize()]
        public IHttpActionResult ExcluirRegraAprovacao([FromBody] RegraAprovacao.Regra_Aprovacao_Model RegraAprovacao)
        {
            SimLib clsLib = new SimLib();
            RegraAprovacao Cls = new RegraAprovacao(User.Identity.Name);
            try
            {
                Cls.ExcluirRegraAprovacao(RegraAprovacao);
                return Ok(true);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

    }

}

