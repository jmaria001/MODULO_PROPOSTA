using System;
using System.Web.Http;
using System.Data;
using System.Collections.Generic;
namespace PROPOSTA
{
    public class ImpressaoCEController : ApiController
    {
        //=====================Listar Comprovante
        [Route("api/ImpressaoCe")]
        [HttpPost]
        [ActionName("ImpressaoCe")]
        [Authorize()]
        public IHttpActionResult ImpressaoCe([FromBody] ImpressaoCE.ImpressaoCeFiltroModel Param)
        {
            SimLib clsLib = new SimLib();
            ImpressaoCE Cls = new ImpressaoCE(User.Identity.Name);
            try
            {
                DataTable dtb = Cls.ImpressaoCeList(Param);
                ImpressaoComprovante clsCe = new ImpressaoComprovante(User.Identity.Name);

                if (dtb.Rows[0]["Status"].ToString().ConvertToBoolean()==false)
                {
                    return Ok(new ImpressaoCE.RetornoImpressaoCeModel()
                    {
                        Status = false,
                        Mensagem = dtb.Rows[0]["Mensagem"].ToString(),
                        pdfFileName = ""
                    }
                    );
                }
                else
                {
                    String PdfComprovante = clsCe.ImprimirComprovante(dtb);
                    return Ok(new ImpressaoCE.RetornoImpressaoCeModel()
                    {
                        Status = true,
                        Mensagem = "",
                        pdfFileName = PdfComprovante
                    }
                    );
                }
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

    }
}



