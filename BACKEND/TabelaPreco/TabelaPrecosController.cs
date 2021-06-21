using System;
using System.Web.Http;
using System.Data;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Web;
using System.Threading.Tasks;
using System.Collections.Generic;

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

        //=======================Upload da planilha excel para importacao de precos
        [Route("api/UploadPreco")]
        [HttpPost]
        [ActionName("UploadPreco")]
        [Authorize()]

        public Task<HttpResponseMessage> Post()
        {
            String message = "";
            String name = "";
            String newFileName = "";
            String FileId = "";

            SimLib clsLib = new SimLib();
            var Credential = User.Identity.Name;
            var CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(Credential, "Name"));
            //========================= Obtem o Id do processo  do header Request
            var re = Request;
            var headers = re.Headers;


            //========================= Consiste se o Formulario is Multipart enctype
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            //===========Obtem o Diretorio Padrao para armazenamento dos arquivos 
            String tempPath = HttpContext.Current.Server.MapPath("~/ANEXOS/TABELAPRECO/TEMP");
            String rootPath = HttpContext.Current.Server.MapPath("~/ANEXOS/TABELAPRECO");

            if (rootPath.Right(1) != @"\")
            {
                rootPath += @"\";
            }
            rootPath += CurrentUser;
            rootPath += @"\";
            //============Check if Directory exists  and create
            if (!Directory.Exists(rootPath))
            {
                Directory.CreateDirectory(rootPath);
            }
            if (!Directory.Exists(tempPath))
            {
                Directory.CreateDirectory(tempPath);
            }
            //============Inicia copia e processamento dos arquivos 
            var provider = new MultipartFileStreamProvider(tempPath);
            var task = Request.Content.ReadAsMultipartAsync(provider).
                ContinueWith<HttpResponseMessage>(t =>
                {

                    if (t.IsCanceled || t.IsFaulted)
                    {
                        Request.CreateErrorResponse(HttpStatusCode.InternalServerError, t.Exception);
                    }
                    foreach (MultipartFileData dataitem in provider.FileData)
                    {
                        try
                        {

                            if (String.IsNullOrEmpty(message))
                            {
                                //====Elijmina barras no nome do arquivo
                                name = dataitem.Headers.ContentDisposition.FileName.Replace("\"", "");
                                //=====Rename arquivo para evitar duplicidsade de nome
                                newFileName = Path.GetFileName(name);
                                //=====Se ja existir o arquivo ,apaga 
                                if (File.Exists(Path.Combine(rootPath, newFileName)))
                                {
                                    File.Delete(dataitem.LocalFileName);
                                }
                                else
                                {
                                    File.Move(dataitem.LocalFileName, Path.Combine(rootPath, newFileName));
                                    File.Delete(dataitem.LocalFileName);
                                }
                                //bolEnviou = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            message = ex.ToString();
                        }
                    }
                    if (String.IsNullOrEmpty(message))
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, FileId);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.InternalServerError, message);
                    }
                });
            return task;
        }

        
        //===========================Importar Precos do Excel
        [Route("api/ImportarTabelaPrecos")]
        [HttpPost]
        [ActionName("ImportarTabelaPrecos")]
        [Authorize()]
        public IHttpActionResult ImportarTabelaPrecos([FromBody] TabelaPrecos.TabelaPrecoImportModel pParam)
        {
            SimLib clsLib = new SimLib();
            TabelaPrecos Cls = new TabelaPrecos(User.Identity.Name);
            try
            {
                List<TabelaPrecos.TabelaPrecosModel> retorno = Cls.ImportarTabelaPrecos(pParam);
                return Ok(retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        //===========================Processar Importacao
        [Route("api/ProcessarImportacaoPreco")]
        [HttpPost]
        [ActionName("ProcessarImportacaoPreco")]
        [Authorize()]
        public IHttpActionResult ProcessarImportacaoPreco([FromBody] List<TabelaPrecos.TabelaPrecosModel> pParam)
        {
            SimLib clsLib = new SimLib();
            TabelaPrecos Cls = new TabelaPrecos(User.Identity.Name);
            try
            {
                List<TabelaPrecos.TabelaPrecosModel> retorno = Cls.ProcessarImportacaoPreco(pParam);
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



