using System;
using System.Web.Http;
using System.Data;
namespace PROPOSTA
{
    public class EmpresaController : ApiController
    {
        //=================================Lista de Empresas
        [Route("api/EmpresaListar")]
        [HttpGet]
        [ActionName("EmpresaListar")]
        [Authorize()]
        public IHttpActionResult EmpresaListar()
        {
            SimLib clsLib = new SimLib();
            Empresa Cls = new Empresa(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.EmpresaListar(0);
                return Ok(dtb);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        //=================================Obtem dados das Empresas
        [Route("api/GetEmpresaData/{Cod_Empresa}")]
        [HttpGet]
        [ActionName("GetEmpresaData")]
        [Authorize()]
        public IHttpActionResult GetEmpresaData(String Cod_Empresa)
        {
            SimLib clsLib = new SimLib();
            Empresa Cls = new Empresa(User.Identity.Name);
            try
            {
                Empresa.EmpresaModel Retorno = new Empresa.EmpresaModel();
                if (Cod_Empresa != "0")
                {
                    Retorno = Cls.GetEmpresaData(Cod_Empresa);

                }
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //===========================Salvar Veiculo

        [Route("api/SalvarEmpresa")]
        [HttpPost]
        [ActionName("SalvarEmpresa")]
        [Authorize()]

        public IHttpActionResult SalvarEmpresa([FromBody] Empresa.EmpresaModel pEmpresa)
        {
            SimLib clsLib = new SimLib();
            Empresa Cls = new Empresa(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.SalvarEmpresa(pEmpresa);
                return Ok(retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }


       //===========================Excluir Empresa

        [Route("api/excluirEmpresa")]
        [HttpPost]
        [ActionName("excluirEmpresa")]
        [Authorize()]

        public IHttpActionResult excluirEmpresa([FromBody] Empresa.EmpresaModel pEmpresa)
        {
            SimLib clsLib = new SimLib();
            Empresa Cls = new Empresa(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.excluirEmpresa(pEmpresa);
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

