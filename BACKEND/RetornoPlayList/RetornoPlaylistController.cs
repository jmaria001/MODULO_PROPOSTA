using System;
using System.Web.Http;
using System.Data;
using System.Collections.Generic;
using System.IO;

namespace PROPOSTA
{
    public class RetornoPlayListController : ApiController
    {

        //----------------------- Carrega Dados -------------------------
        [Route("api/RetornoPlayListDados")]
        [HttpGet]
        [ActionName("RetornoPlayListDados")]
        [Authorize()]
        public IHttpActionResult RetornoPlayListDados([FromUri] RetornoPlayList.RetornoPlayListModel Param)
        {
            SimLib clsLib = new SimLib();
            RetornoPlayList Cls = new RetornoPlayList(User.Identity.Name);
            try
            {
                RetornoPlayList.RetornoPlayListModel Retorno = Cls.RetornoPlayListDados(Param);
                Retorno.Anexos = new List<RetornoPlayList.AnexoModel>();
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }


        //----------------------- Processa Importação ------------------------
        [Route("api/RetornoPlayListConsiste")]
        [HttpPost]
        [ActionName("RetornoPlayListConsiste")]
        [Authorize()]
        public IHttpActionResult RetornoPlayListConsiste([FromBody] RetornoPlayList.RetornoPlayListModel Param)
        {
            SimLib clsLib = new SimLib();
            RetornoPlayList Cls = new RetornoPlayList(User.Identity.Name);
            try
            {
                //----Faz Consistencias

                DataTable retorno = Cls.RetornoPlayListConsistencias(Param);
                return Ok(retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //----------------------- Processa Importação ------------------------
        [Route("api/RetornoPlayListProcessa")]
        [HttpPost]
        [ActionName("RetornoPlayListProcessa")]
        [Authorize()]
        public IHttpActionResult RetornoPlayListProcessa([FromBody] RetornoPlayList.RetornoPlayListModel Param)
        {
            SimLib clsLib = new SimLib();
            RetornoPlayList Cls = new RetornoPlayList(User.Identity.Name);
            try
            {
                Cls.ApagaArquivoRetornoPlayList(Param.Cod_Veiculo);
                DataTable dtbBaixa = new DataTable("dtbBaixa");
                for (int i = 0; i < Param.Anexos.Count; i++)
                {
                    if (Param.Tipo_Arquivo.ToUpper() == "CSV")
                    {
                        //-----Processa Arquivo CSV
                        Cls.RetornoPlayListProcessaCSV(Param,Param.Anexos[i].AnexoName);
                    }
                    if (Param.Tipo_Arquivo.ToUpper() == "TXT")
                    {
                        //-----Processa Arquivo TXT
                        Cls.RetornoPlayListProcessaTXT(Param, Param.Anexos[i].AnexoName);
                    }
                }
                dtbBaixa = Cls.ProcessaConciliacao(Param);
                Cls.ApagaArquivoRetornoPlayList(Param.Cod_Veiculo);
                Cls.ApagaAnexos();
                return Ok(dtbBaixa);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }


        //------------ Faz a Baixa ----------------
        [Route("api/RetornoPlayListBaixa")]
        [HttpPost]
        [ActionName("RetornoPlayListBaixa")]
        [Authorize()]
        public IHttpActionResult RetornoPlayListBaixa([FromBody] List<RetornoPlayList.RetornoPlayListBaixaModel> Param)
        {
            SimLib clsLib = new SimLib();
            RetornoPlayList Cls = new RetornoPlayList(User.Identity.Name);
            try
            {
                Cls.BaixarVeiculacao(Param);
                return Ok(Param);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        //------------ Remove Anexo----------------
        [Route("api/RetornoPlayListRemoveAnexo")]
        [HttpPost]
        [ActionName("RetornoPlayListRemoveAnexo")]
        [Authorize()]
        public IHttpActionResult RetornoPlayListRemoveAnexo([FromBody] RetornoPlayList.AnexoModel Param)
        {
            SimLib clsLib = new SimLib();
            String strCod_Usuario = clsLib.Decriptografa(clsLib.GetJsonItem(User.Identity.Name, "Name"));
            try
            {

                String PathDoc = System.Web.HttpContext.Current.Server.MapPath("~/ANEXOS/RETORNO_PLAYLIST");
                
                if (Directory.Exists(PathDoc))
                {
                    if (File.Exists(PathDoc + @"\" + Param.AnexoName))
                    {
                        File.Delete(PathDoc + @"\" + Param.AnexoName);
                    }
                }

                return Ok(PathDoc + @"\" + Param.AnexoName);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
    }
}






