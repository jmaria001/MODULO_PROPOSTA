using System;
using System.Web.Http;
using System.Data;
using System.Collections.Generic;
namespace PROPOSTA
{
    public class EnvioPlayListController : ApiController
    {
        //----------------------- Filtrar Dados da Playlist-------------------------
        [Route("api/EnvioPlayListFiltrar")]
        [HttpGet]
        [ActionName("EnvioPlayListFiltrar")]
        [Authorize()]
        public IHttpActionResult EnvioPlayListFiltrar([FromUri] EnvioPlayList.EnvioPlayListModel Param)
        {
            SimLib clsLib = new SimLib();
            EnvioPlayList Cls = new EnvioPlayList(User.Identity.Name);
            try
            {
                EnvioPlayList.EnvioPlayListModel Retorno = Cls.EnvioPlayListFiltrar(Param);
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }


        //----------------------- Filtrar Parametros-------------------------
        [Route("api/EnvioPlayListFiltrarParametros")]
        [HttpGet]
        [ActionName("EnvioPlayListFiltrarParametros")]
        [Authorize()]
        public IHttpActionResult EnvioPlayListFiltrarParametros([FromUri] EnvioPlayList.EnvioPlayListModel Param)
        {
            SimLib clsLib = new SimLib();
            EnvioPlayList Cls = new EnvioPlayList(User.Identity.Name);
            try
            {
                EnvioPlayList.EnvioPlayListModel Retorno = Cls.EnvioPlayListFiltrarParametros(Param);
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }


        //------------------------- Salvar Parametros ------------------------
        [Route("api/EnvioPlayListSalvarParametros")]
        [HttpPost]
        [ActionName("EnvioPlayListSalvarParametros")]
        [Authorize()]
        public IHttpActionResult EnvioPlayListSalvarParametros([FromBody] EnvioPlayList.EnvioPlayListModel Param)
        {
            SimLib clsLib = new SimLib();
            EnvioPlayList Cls = new EnvioPlayList(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.EnvioPlayListSalvarParametros(Param);
                return Ok(retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //------------------------------Gerar Arquivos 
        [Route("api/EnvioPlayGerarArquivo")]
        [HttpPost]
        [ActionName("EnvioPlayGerarArquivo")]
        [Authorize()]
        public IHttpActionResult EnvioPlayGerarArquivo([FromBody] EnvioPlayList.EnvioPlayListModel Param)
        {
            SimLib clsLib = new SimLib();
            EnvioPlayList Cls = new EnvioPlayList(User.Identity.Name);
            try
            {
                EnvioPlayList.GeracaoPlayListModel Retorno = new EnvioPlayList.GeracaoPlayListModel();
                String strUrl = "";

                //-------Verifica se tem Roteiro Encerrado
                if (!Cls.ExisteRoteiroEncerrado(Param))
                {
                    Retorno.Status= false;
                    Retorno.Mensagem = "Não existe Roteiro Encerrado para esse Veiculo / Data.";
                    Retorno.Url = null;
                }

                //-------Gera o Arquivo para o exibidor 
                switch (Param.Exibidor.ToUpper())
                {
                case "4SCV":
                    break;
                case "4SIT":
                    break;
                case "4SCAMP":
                    break;
                case "4S":
                    strUrl = Cls.Gerar4S(Param);
                    break;
                case "VR":
                    break;
                case "VR400":
                    break;
                case "VR420":
                    break;
                case "FLORIPA" :
                    strUrl = Cls.GerarFloripa(Param);
                    break;
                case "LOUTH":
                    strUrl = Cls.GerarLouth(Param);
                    break;
                case "OMNEON PRODRIVE":
                    break;
                case "LEITCH":
                    break;
                case "4S VR300":
                    break;
                case "VICTOR":
                    strUrl = Cls.GerarVICTOR(Param);
                    break;
                case "VSN":
                    strUrl = Cls.GerarVSN(Param);
                    break;
                case "DAD":
                        strUrl = Cls.GerarDAD(Param);
                        //strUrl = Cls.DataSetExample();
                        break;
                case "INFORMA":
                    break;
                default:
                    break;
                }

                //-------Retorna
                if (!String.IsNullOrEmpty(strUrl))
                {
                    Retorno.Status = true;
                    Retorno.Url = strUrl;
                    Retorno.Mensagem = "Arquivo Gerado com Sucesso.";

                }
                else
                {
                    Retorno.Status = false;
                    Retorno.Url = "";
                    Retorno.Mensagem = "Nenhum Arquivo foi Gerado.";
                }

                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
    }
}






