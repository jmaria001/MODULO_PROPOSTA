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
        public IHttpActionResult ListSimulacao([FromUri]Simulacao.SimulacaoFiltroParam query)
        {
            SimLib clsLib = new SimLib();
            Simulacao Cls = new Simulacao(User.Identity.Name);

            try
            {
                DataTable dtbRetorno = new DataTable();
                if (query.Processo== "P" || query.Processo== "S")
                {
                    dtbRetorno = Cls.ListSimulacao(query);
                }
                else
                {
                    dtbRetorno = Cls.ListPendenteAprovacao();
                }

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
        [Route("api/GetSimulacao/{Id_Simulacao}/{Processo}")]
        [HttpGet]
        [ActionName("GetSimulacao")]
        [Authorize()]
        public IHttpActionResult GetSimulacao(Int32 Id_Simulacao,String Processo)
        {
            SimLib clsLib = new SimLib();
            Simulacao Cls = new Simulacao(User.Identity.Name);
            try
            {
                Simulacao.SimulacaoModel Retorno = new Simulacao.SimulacaoModel();

                if (Id_Simulacao == 0)
                {
                    Retorno.Esquemas = new List<Simulacao.EsquemaModel>();
                    Retorno.Permite_Editar = true;
                    Retorno.Tipo = Processo;
                }
                else
                {
                    Retorno = Cls.GetSimulacao(Id_Simulacao,false);
                }
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        [Route("api/GetSimulacaoCapa/{Id_Simulacao}/{Processo}")]
        [HttpGet]
        [ActionName("GetSimulacaoCapa")]
        [Authorize()]

        public IHttpActionResult GetSimulacaoCapa(Int32 Id_Simulacao, String Processo)
        {
            SimLib clsLib = new SimLib();
            Simulacao Cls = new Simulacao(User.Identity.Name);
            try
            {
                Simulacao.SimulacaoModel Retorno = new Simulacao.SimulacaoModel();

                
                    Retorno = Cls.GetSimulacao(Id_Simulacao,true);
                
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }



        [Route("api/ImportarSimulacao")]
        [HttpPost]
        [ActionName("ImportarSimulacao")]
        [Authorize()]
        public IHttpActionResult ImportarSimulacao([FromBody] Simulacao.SimulacaoFiltroParam Param)
        {
            SimLib clsLib = new SimLib();
            Simulacao Cls = new Simulacao(User.Identity.Name);
            try
            {
                DataTable Retorno = Cls.ImportarSimulacao(Param);
                return Ok(Retorno);
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
                Midia.Dia_Inicio = FirstDay.Day;
                Midia.Dia_Fim = Lastday.Day;
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
                        Qtd = null,
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


        [Route("api/GetVeiculos")]
        [HttpGet]
        [ActionName("GetVeiculos")]
        [Authorize()]
        public IHttpActionResult GetVeiculos([FromUri]Simulacao.GetVeiculoParam query)
        {
            SimLib clsLib = new SimLib();
            Simulacao Cls = new Simulacao(User.Identity.Name);

            try
            {
                DataTable dtbRetorno = Cls.GetVeiculos(query);
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
            Simulacao.SimulacaoModel Simulacao = new Simulacao.SimulacaoModel();
            try
            {
                DataTable dtb = Cls.SalvarSimulacao(Param);
                if (dtb.Rows[0]["Status"].ToString().ConvertToBoolean())
                {
                    Simulacao = Cls.GetSimulacao(dtb.Rows[0]["Id_Simulacao"].ToString().ConvertToInt32(), false);
                    Simulacao.Critica = null;
                }
                else
                {
                    Simulacao.Critica = dtb.Rows[0]["Mensagem"].ToString();
                }
                return Ok(Simulacao);
                
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        [Route("api/DetalharDesconto/{Id_Midia}")]
        [HttpGet]
        [ActionName("DetalharDesconto")]
        [Authorize()]
        public IHttpActionResult DetalharDesconto(Int32 Id_Midia)
        {
            SimLib clsLib = new SimLib();
            Simulacao Cls = new Simulacao(User.Identity.Name);

            try
            {
                DataTable dtbRetorno = Cls.DetalharDesconto(Id_Midia);
                return Ok(dtbRetorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        [Route("api/DuplicarEsquema/{Id_Esquema}/{Tipo}")]
        [HttpGet]
        [ActionName("DuplicarEsquema")]
        [Authorize()]
        public IHttpActionResult DuplicarEsquema(Int32 Id_Esquema,Int32 Tipo)
        {
            SimLib clsLib = new SimLib();
            Simulacao Cls = new Simulacao(User.Identity.Name);

            try
            {
                DataTable dtbRetorno = Cls.DuplicarEsquema(Id_Esquema,Tipo);
                return Ok(dtbRetorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        
        [Route("api/MostrarAprovadores/{Id_Simulacao}")]
        [HttpGet]
        [Authorize()]
        public IHttpActionResult MostrarAprovadores(Int32 Id_Simulacao)
        {
            SimLib clsLib = new SimLib();
            Simulacao Cls = new Simulacao(User.Identity.Name);
            try
            {
                return Ok(Cls.GetAprovadores(Id_Simulacao));
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
       
        [Route("api/ImprimirMidia/{Id_Simulacao}")]
        [HttpGet]
        [Authorize()]
        public IHttpActionResult ImprimirMidia(Int32 Id_Simulacao)
        {
            SimLib clsLib = new SimLib();
            try
            {
                ImpressaoMidia Cls = new ImpressaoMidia(User.Identity.Name);

                return Ok(Cls.ImprimirMIDIA(Id_Simulacao));
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        [Route("api/ImprimirAnalise/{Id_Simulacao}")]
        [HttpGet]
        [Authorize()]
        public IHttpActionResult ImprimirAnalise(Int32 Id_Simulacao)
        {
            SimLib clsLib = new SimLib();
            try
            {
                ImpressaoAnalise Cls = new ImpressaoAnalise(User.Identity.Name);

                return Ok(Cls.ImprimirAnalise(Id_Simulacao));
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        [Route("api/SolicitarAprovacao")]
        [HttpPost]
        [Authorize()]
        public IHttpActionResult SolicitarAprovacao([FromBody]  Simulacao.Param_Aprovacao_Model Param)
        {
            
            SimLib clsLib = new SimLib();
            try
            {
                Simulacao Cls = new Simulacao(User.Identity.Name);
                DataTable dtbEmail = Cls.SendAprovacao(Param);
                foreach (DataRow drw in dtbEmail.Rows)
                {
                    clsLib.EnviaEmail(drw["Destinatario"].ToString(), null, null, "Módulo Proposta - Solicitação de Aprovação", drw["Texto_Email"].ToString(),"");
                };
                return Ok(true);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        [Route("api/GetAprovacaoData/{token}")]
        [HttpGet]
        //[Authorize()]
        public IHttpActionResult GetAprovacaoData(String token)
        {
            SimLib clsLib = new SimLib();
            try
            {
                Simulacao.SimulacaoModel Retorno = new Simulacao.SimulacaoModel();
                Simulacao Cls = new Simulacao(User.Identity.Name);
                Int32 Id_Simulacao = Cls.GetIdSimulacaoFromAprovacao(token);
                if (Id_Simulacao>0)
                {
                    Retorno = Cls.GetSimulacao(Id_Simulacao,false);
                }
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        
        [Route("api/GetAprovacaoProposta/{Id_Simulacao}")]
        [HttpGet]
        [Authorize()]
        public IHttpActionResult GetAprovacaoProposta(Int32 Id_Simulacao)
        {
            SimLib clsLib = new SimLib();
            try
            {
                Simulacao.SimulacaoModel Retorno = new Simulacao.SimulacaoModel();
                Simulacao Cls = new Simulacao(User.Identity.Name);
                Retorno = Cls.GetSimulacao(Id_Simulacao,false);

                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        

        [Route("api/AprovarProposta")]
        [HttpPost]
        //[Authorize()]
        public IHttpActionResult AprovarProposta([FromBody]  Simulacao.Param_Aprovacao_Model Param)
        {
            SimLib clsLib = new SimLib();
            try
            {
                Simulacao Cls = new Simulacao(User.Identity.Name);
                DataTable Retorno = Cls.AprovarProposta(Param);
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        [Route("api/GerarProposta")]
        [HttpPost]
        [Authorize()]
        public IHttpActionResult GerarProposta([FromBody]  Simulacao.Param_Geracao_Model Param)
        {
            SimLib clsLib = new SimLib();
            try
            {
                Simulacao Cls = new Simulacao(User.Identity.Name);
                Boolean Retorno = Cls.GerarProposta(Param);
                String strPdfName = "";
                if (Retorno)
                {
                    ImpressaoProposta clsProposta = new ImpressaoProposta(User.Identity.Name);
                    string strPath = clsProposta.GetPath();
                    strPdfName =  clsProposta.ImprimirProposta(Param.Id_Simulacao);
                    if (!String.IsNullOrEmpty(Param.Email_Contato))
                    {
                        String strAssinatura = Cls.GetAssinatura();
                        String strBody = "<style>";
                        strBody += "p {font-family:verdana;font-size:12;font-style:italic}";
                        strBody += "}";
                        strBody += "</style>";
                        strBody +=  "<p>Prezado(a) Sr(a) " + Param.Nome_Contato  + "</p>";
                        strBody += "<p>É com satisfação que apresentamos a V.Sa. nossa proposta Comercial.</p>";
                        strBody += "<p>Desde já agradecemos a oportunidade que nos foi concedida e colocamo-nos a disposição para quaisquer esclarecimentos.</p>";
                        strBody += "<br>";
                        strBody += "<p>Atenciosamente,</p>";
                        strBody += "<br>";
                        strBody += "<p>" + strAssinatura + "</p>";
                        strBody += "<br>";
                        strBody += "<br>";
                        strBody += "<p style='font-size=9'>" + "Email enviado automáticamente,favor não responder."+ "</p>";
                        clsLib.EnviaEmail(Param.Email_Contato,Param.Email_Copia,"","Módulo Proposta - Proposta Comercial",strBody,strPath+strPdfName);
                    }
                    
                }
                return Ok(strPdfName);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        [Route("api/ConfirmarVenda")]
        [HttpPost]
        [Authorize()]
        public IHttpActionResult ConfirmarVenda([FromBody]  Simulacao.Param_Geracao_Model Param)
        {
            SimLib clsLib = new SimLib();
            try
            {
                Simulacao Cls = new Simulacao(User.Identity.Name);
                Cls.ConfirmarVenda(Param.Id_Simulacao);
                return Ok(true);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }


        [Route("api/MostrarInconsistencias/{Id_Simulacao}")]
        [HttpGet]
        [Authorize()]
        public IHttpActionResult MostrarInconsistencias(Int32 Id_Simulacao)
        {
            SimLib clsLib = new SimLib();
            Simulacao Cls = new Simulacao(User.Identity.Name);
            try
            {
                return Ok(Cls.MostrarInconsistencias(Id_Simulacao));
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        [Route("api/MockAprovacao/{Id_Simulacao}")]
        [HttpGet]
        //[Authorize()]
        public IHttpActionResult MockAprovacao(Int32 Id_Simulacao)
        {
            SimLib clsLib = new SimLib();
            Simulacao Cls = new Simulacao(User.Identity.Name);
            try
            {
                Cls.MockAprovacao(Id_Simulacao);
                return Ok("Ok. Concluido");
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }


    }
}

