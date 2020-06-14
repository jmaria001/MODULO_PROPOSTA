using System;
using System.Web.Http;
using System.Data;
using System.Collections.Generic;


namespace PROPOSTA
{
    public class ProgramaController : ApiController
    {
        //=================================Lista de Programas
        [Route("api/ProgramaListar/{Id_Rede}")]
        [HttpGet]
        [ActionName("ProgramaListar")]
        [Authorize()]
        public IHttpActionResult ProgramaListar(Int32 Id_Rede)
        {
            SimLib clsLib = new SimLib();
            Programa Cls = new Programa(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.ProgramaListar(Id_Rede);
                return Ok(dtb);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        //=================================Obtem dados do Programa jOÃO MARIA AJUDOU FAZER ESTA PARTE
        [Route("api/GetProgramaData/{Cod_Programa}")]
        [HttpGet]
        [ActionName("GetProgramaData")]
        [Authorize()]
        public IHttpActionResult GetProgramaData(String Cod_Programa)
        {
            SimLib clsLib = new SimLib();
            Programa Cls = new Programa(User.Identity.Name);
            try
            {
                Programa.ProgramaModel Retorno = new Programa.ProgramaModel();
                if (Cod_Programa != "0")
                {
                    Retorno = Cls.GetProgramaData(Cod_Programa);

                }
                else
                {
                    Retorno.Apresentadores = new List<Programa.Apresentador_Model>();
                    Retorno.Veiculos = new List<Programa.Veiculos_Model>();
                }
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        //Fim da Ajuda
  // Definindo controle apresentadores     
        [Route("api/ApresentadorListar")]
        [HttpGet]
        [ActionName("ApresentadorListar")]
        [Authorize()]
        public IHttpActionResult ApresentadorListar()
        {
            SimLib clsLib = new SimLib();
            Programa Cls = new Programa(User.Identity.Name);
            try
            {
                List<Programa.Apresentador_Model> Apresentador = Cls.ApresentadorListar();
                return Ok(Apresentador);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
// Definindo controle apresentadores
        //[Route("api/GetApresentador/{pIdApresentador}")]
        //[HttpGet]
        //[ActionName("GetApresentador")]
        //[Authorize()]
        //public IHttpActionResult GetApresentador(Int32 pIdApresentador)
        //{
        //    SimLib clsLib = new SimLib();
        //    Programa Cls = new Programa(User.Identity.Name);
        //    try
        //    {
        //        Programa.Apresentador_Model Apresentador = new Programa.Apresentador_Model();
        //        if (pIdApresentador == 0)
        //        {

        //            Apresentador.ApresentadoresSel = (new List<Programa.ApresentadorSel_Model>());
        //        }
        //        else
        //        {
        //            Apresentador = Cls.GetApresentador(pIdApresentador);
        //        }

        //        return Ok(Apresentador);
        //    }
        //    catch (Exception Ex)
        //    {
        //        clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
        //        throw new Exception(Ex.Message);
        //    }
        //}

        // Definindo controle veiculos    
        [Route("api/VeiculosListar")]
        [HttpGet]
        [ActionName("VeiculosListar")]
        [Authorize()]
        public IHttpActionResult VeiculosListar()
        {
            SimLib clsLib = new SimLib();
            Programa Cls = new Programa(User.Identity.Name);
            try
            {
                List<Programa.Veiculos_Model> Veiculos = Cls.VeiculosListar();
                return Ok(Veiculos);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        // Mostra os veiculos do Preograma
        [Route("api/VeiculosMostrar/{Cod_Programa}")]
        [HttpGet]
        [ActionName("VeiculosMostrar")]
        [Authorize()]
        public IHttpActionResult VeiculosMostrar(String Cod_Programa)
        {
            SimLib clsLib = new SimLib();
            Programa Cls = new Programa(User.Identity.Name);
            try
            {
                DataTable Veiculos = Cls.VeiculosMostrar(Cod_Programa);
                return Ok(Veiculos);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }


        //===========================Salvar Programa

        [Route("api/SalvarPrograma")]
        [HttpPost]
        [ActionName("SalvarPrograma")]
        [Authorize()]

        public IHttpActionResult SalvarPrograma([FromBody] Programa.ProgramaModel pPrograma)
        {
            SimLib clsLib = new SimLib();
            Programa Cls = new Programa(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.SalvarPrograma(pPrograma);
                return Ok(retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }


        //===========================Excluir Programa

        [Route("api/ExcluirPrograma")]
        [HttpPost]
        [ActionName("ExcluirPrograma")]
        [Authorize()]

        public IHttpActionResult ExcluirPrograma([FromBody] Programa.ProgramaModel pPrograma)
        {
            SimLib clsLib = new SimLib();
            Programa Cls = new Programa(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.ExcluirPrograma(pPrograma);
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

