using System;
using System.Web.Http;
using System.Data;
using System.Collections.Generic;
using System.Globalization;

namespace PROPOSTA
{
    public class SimulacaoController : ApiController
    {
        [Route("api/ListSimulacao")]
        [HttpGet]
        [ActionName("ListSimulacao")]
        [Authorize()]
        public IHttpActionResult ListSimulacao()
        {
            SimLib clsLib = new SimLib();
            Simulacao Cls = new Simulacao(User.Identity.Name);

            try
            {
                DataTable dtbRetorno = Cls.ListSimulacao();
                return Ok(dtbRetorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        [Route("api/SimulacaoDestroy")]
        [HttpPost]
        [ActionName("SimulacaoDestroy")]
        [Authorize()]
        public IHttpActionResult SimulacaoDestroy([FromBody] Simulacao.SimulacaoModel Param)
        {
            SimLib clsLib = new SimLib();
            Simulacao Cls = new Simulacao(User.Identity.Name);
            try
            {
                DataTable Retorno = Cls.SimulacaoDestroy(Param);
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        [Route("api/GetSimulacao/{Id_Simulacao}")]
        [HttpGet]
        [ActionName("GetSimulacao")]
        [Authorize()]
        public IHttpActionResult GetSimulacao(Int32 Id_Simulacao)
        {
            SimLib clsLib = new SimLib();
            Simulacao Cls = new Simulacao(User.Identity.Name);
            try
            {
                Simulacao.SimulacaoModel Retorno = new Simulacao.SimulacaoModel();

                if (Id_Simulacao == 0)
                {
                    Retorno.Esquemas = new List<Simulacao.EsquemaModel>();
                    Retorno.Validade_Inicio = null;
                }
                else
                {
                    Retorno = Cls.GetSimulacao(Id_Simulacao);
                }
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        [Route("api/GetOpcoesDesconto/{p1}")]
        [HttpGet]
        [ActionName("GetOpcoesDesconto")]
        [Authorize()]
        public IHttpActionResult GetOpcoesDesconto(Int32 p1)
        {
            SimLib clsLib = new SimLib();
            Simulacao Cls = new Simulacao(User.Identity.Name);

            try
            {
                DataTable dtbRetorno = Cls.GetOpcoesDesconto(p1);
                return Ok(dtbRetorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        
        [Route("api/GetNewEsquema")]
        [HttpGet]
        [ActionName("GetNewEsquema")]
        [Authorize()]
        public IHttpActionResult GetNewEsquema()
        {
            SimLib clsLib = new SimLib();
            Simulacao Cls = new Simulacao(User.Identity.Name);

            try
            {
                Simulacao.EsquemaModel Esquema = new Simulacao.EsquemaModel();
                Esquema.Veiculos = new List<Simulacao.VeiculoModel>();
                Esquema.Midias= new List<Simulacao.MidiaModel>();
                return Ok(Esquema);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        [Route("api/GetNewMidia/{pCompetencia}")]
        [HttpGet]
        [ActionName("GetNewMidia")]
        [Authorize()]
        public IHttpActionResult GetNewMidia(Int32 pCompetencia)
        {
            SimLib clsLib = new SimLib();
            Simulacao Cls = new Simulacao(User.Identity.Name);

            try
            {
                Simulacao.MidiaModel Midia = new Simulacao.MidiaModel();
                Int32 mes = pCompetencia.ToString().Substring(4, 2).ConvertToInt32();
                Int32 ano = pCompetencia.ToString().Substring(0, 4).ConvertToInt32();
                DateTime Lastday = clsLib.LastDay(mes, ano);
                DateTime FirstDay = clsLib.FirstDay(mes, ano);
                List<Simulacao.InsercaoModel> Insercao = new List<Simulacao.InsercaoModel>();
                var strDW = new string[7] { "DOM", "SEG", "TER", "QUA", "QUI", "SEX", "SAB" };
                while (FirstDay.Month == Lastday.Month)
                {
                    Insercao.Add(new Simulacao.InsercaoModel()
                    {
                        Id_Insercao = 0,
                        Id_Midia = 0,
                        Data_Exibicao = FirstDay,
                        Dia = FirstDay.Day.ToString().PadLeft(2, '0'),
                        Dia_Semana = strDW[FirstDay.DayOfWeek.ToString("d").ConvertToInt32()],
                        Qtd = 0,
                        Valor_Tabela_Unitario = "",
                        Valor_Negociado_Unitario = "",
                        Valor_Negociado_Total= "",
                        Desconto_Aplicado = "",
                        Tipo_Desconto = "",
                        Tem_Grade = false
                    });
                    FirstDay = FirstDay.AddDays(1);
                }
                Midia.Insercoes = Insercao;

                return Ok(Midia);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        [Route("api/GetVeiculos/{Abrangencia}/{Mercado}/{Empresa}/{EmpresaFaturamento}")]
        [Route("api/GetVeiculos")]
        [HttpGet]
        [ActionName("GetVeiculos")]
        [Authorize()]
        //public IHttpActionResult GetVeiculos(Int32 Abrangencia, String Mercado=null, String Empresa= null, String EmpresaFaturamento)
        public IHttpActionResult GetVeiculos([FromUri]Simulacao.GetVeiculoParam query)
        {
            SimLib clsLib = new SimLib();
            Simulacao Cls = new Simulacao(User.Identity.Name);

            try
            {
                DataTable dtbRetorno = Cls.GetVeiculos(query.Abrangencia, query.Mercado, query.Empresa, query.EmpresaFaturamento);
                return Ok(dtbRetorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        [Route("api/GetProgramasGrade")]
        [HttpPost]
        [ActionName("GetProgramasGrade")]
        [Authorize()]
        public IHttpActionResult GetProgramasGrade([FromBody]  Simulacao.GetProgramasGradeParam Param)
        {
            SimLib clsLib = new SimLib();
            Simulacao Cls = new Simulacao(User.Identity.Name);

            try
            {
                DataTable dtb = Cls.GetProgramasGrade(Param);
                return Ok(dtb);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        [Route("api/DistribuirInsercoes")]
        [HttpPost]
        [ActionName("DistribuirInsercoes")]
        [Authorize()]
        public IHttpActionResult DistribuirInsercoes([FromBody]  Simulacao.DistribuicaoInsecoesParam Param)
        {
            SimLib clsLib = new SimLib();
            Simulacao Cls = new Simulacao(User.Identity.Name);

            try
            {
                List<Simulacao.InsercaoModel> Insercoes = Cls.DistribuirInsercoes(Param);
                return Ok(Insercoes);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }



        [Route("api/SalvarSimulacao")]
        [HttpPost]
        [ActionName("SalvarSimulacao")]
        [Authorize()]
        public IHttpActionResult SalvarSimulacao([FromBody]  Simulacao.SimulacaoModel Param)
        {
            SimLib clsLib = new SimLib();
            Simulacao Cls = new Simulacao(User.Identity.Name);

            try
            {
                DataTable dtb = Cls.SalvarSimulacao(Param);
                return Ok(dtb);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        //[Route("api/CalcularSimulacao")]
        //[HttpPost]
        //[ActionName("CalcularSimulacao")]
        //[Authorize()]
        //public IHttpActionResult CalcularSimulacao([FromBody]  Simulacao.SalvarSimulacaoParam Param)
        //{
        //    SimLib clsLib = new SimLib();
        //    Simulacao Cls = new Simulacao(User.Identity.Name);

        //    try
        //    {
        //        DataTable dtb = Cls.CalcularSimulacao(Param);
        //        return Ok(dtb);
        //    }
        //    catch (Exception Ex)
        //    {
        //        clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
        //        throw new Exception(Ex.Message);
        //    }
        //}
        //[Route("api/GetSimulacao/{IdSimulacao}")]
        //[HttpGet]
        //[ActionName("GetSimulacao")]
        //[Authorize()]
        //public IHttpActionResult GetSimulacao(Int32 IdSimulacao)
        //{
        //    SimLib clsLib = new SimLib();
        //    Simulacao Cls = new Simulacao(User.Identity.Name);

        //    try
        //    {
        //        Simulacao.GetSimulacaoModel Retorno = Cls.GetSimulacao(IdSimulacao);
        //        return Ok(Retorno);
        //    }
        //    catch (Exception Ex)
        //    {
        //        clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
        //        throw new Exception(Ex.Message);
        //    }
        //}

        //[Route("api/Testex")]
        //[HttpGet]
        //[ActionName("Testex")]
        ////[Authorize()]
        //public IHttpActionResult Testex()
        //{

        //    try
        //    {

        //        return Ok("testex");
        //    }
        //    catch (Exception Ex)
        //    {
        //        throw new Exception(Ex.Message);
        //    }
        //}

    }
}

