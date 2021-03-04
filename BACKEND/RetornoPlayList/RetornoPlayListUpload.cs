using PROPOSTA;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace SIMAGENCIA
{
    public class RetornoPlayListUploadController : ApiController
    {
        [Route("api/RetornPlayListUpload")]
        [HttpPost]
        [ActionName("RetornPlayListUpload")]
        [Authorize()]
        public Task<HttpResponseMessage> Post()
        {
            String message = "";
            String name = "";
            String newFileName = "";
            String FileId = "";
            
            SimLib clsLib = new SimLib();
            var Credential = User.Identity.Name;
            var  CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(Credential, "Name"));
            //========================= Obtem o Id do processo  do header Request
            var re = Request;
            var headers = re.Headers;
            

            //========================= Consiste se o Formulario is Multipart enctype
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            
            //===========Obtem o Diretorio Padrao para armazenamento dos arquivos 
            String tempPath = HttpContext.Current.Server.MapPath("~/ANEXOS/RETORNO_PLAYLIST/TEMP");
            String rootPath = HttpContext.Current.Server.MapPath("~/ANEXOS/RETORNO_PLAYLIST");


            //if (sPath.Right(1) != @"\")
            //{
            //    sPath += @"\";
            //}
            //sPath += this.CurrentUser;
            //sPath += @"\";
            //String strFile = sPath + Arquivo;

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
    }       
}