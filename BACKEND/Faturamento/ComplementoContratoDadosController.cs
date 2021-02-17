using System;
using System.Web.Http;
using System.Data;
using System.Collections.Generic;

namespace PROPOSTA
{
    public class ComplementoContratoDadosController : ApiController
    {
        //=================================Lista de Itens de GetComplementoData
        [Route("api/GetComplementoData")]
        [HttpPost]
        [ActionName("GetComplementoData")]
        [Authorize()]


        public IHttpActionResult GetComplementoData([FromBody] List<ComplementoContratoDados.ComplementoContratoModel> pData)
        {
            SimLib clsLib = new SimLib();
            ComplementoContratoDados Cls = new ComplementoContratoDados(User.Identity.Name);
            try
            {

                ComplementoContratoDados.ComplementoContratoModel dtb = Cls.GetComplementoData(pData);
                return Ok(dtb);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }

        //===========================Salvar Complemento
        [Route("api/SalvarComplemento")]
        [HttpPost]
        [ActionName("SalvarComplemento")]
        [Authorize()]

        public IHttpActionResult SalvarComplemento([FromBody] ComplementoContratoDados.ComplementoContratoModel Param)
        {
            SimLib clsLib = new SimLib();
            ComplementoContratoDados Cls = new ComplementoContratoDados(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.SalvarComplemento(Param);
                return Ok(retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        //===========================Get Regra Natureza de servicos
        [Route("api/GetNaturezaRegra")]
        [HttpGet]
        [ActionName("GetNaturezaRegra")]
        [Authorize()]

        public IHttpActionResult GetNaturezaRegra([FromUri] ComplementoContratoDados.RegraNaturezaModel Param)
        {
            SimLib clsLib = new SimLib();
            ComplementoContratoDados Cls = new ComplementoContratoDados(User.Identity.Name);
            try
            {
                DataTable retorno = Cls.GetNaturezaRegra(Param);
                return Ok(retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        //=================================Lista de Itens de GetComplementoData
        [Route("api/GetEmptyOutrasReceitas")]
        [HttpPost]
        [ActionName("GetEmptyOutrasReceitas")]
        [Authorize()]


        public IHttpActionResult GetEmptyOutrasReceitas()
        {
            SimLib clsLib = new SimLib();
            ComplementoContratoDados Cls = new ComplementoContratoDados(User.Identity.Name);
            try
            {

                ComplementoContratoDados.ComplementoContratoModel Retorno = new ComplementoContratoDados.ComplementoContratoModel();
                Retorno.Rateios = new List<ComplementoContratoDados.RateioModel>();
                List<ComplementoContratoDados.RateioModel> Rateios = new List<ComplementoContratoDados.RateioModel>();
                List<ComplementoContratoDados.DuplicataModel> Duplicatas = new List<ComplementoContratoDados.DuplicataModel>();
                List<ComplementoContratoDados.ComplementoMapasModel> ComplementoMapas = new List<ComplementoContratoDados.ComplementoMapasModel>();
                Rateios.Add(new ComplementoContratoDados.RateioModel
                {
                    Id_Rateio = 0,
                    Numero_Rateio = 1,
                    Cod_Cliente = "",
                    Cod_Agencia = "",
                    Data_Emissao = DateTime.Now.ToString("dd/MM/yyyy"),
                    Vlr_A_Faturar = "" ,
                    Cod_Condicao = "15DFM",
                    Cod_Veiculo = "",
                    Indica_Log_Agencia = 1,
                    Indica_Log_Cliente = 1,
                    Referencia = "",
                    Perc_Rateio = "100"

                });

                var _dtemissao = Rateios[0].Data_Emissao.ConvertToDatetime();
                var _vencimento_year = _dtemissao.Year;
                var _vencimento_month = _dtemissao.Month;
                DateTime _Vencimento_base = new DateTime(_vencimento_year, _vencimento_month, DateTime.DaysInMonth(_vencimento_year, _vencimento_month));
                _Vencimento_base.AddDays(-1);
                var _Vencimento = _Vencimento_base.AddDays(15);
                Duplicatas.Add(new ComplementoContratoDados.DuplicataModel()
                {
                    Id_Rateio = 0,
                    Id_Parcela = 1,
                    Parcela = 1,
                    Vencimento = _Vencimento.ToString("dd/MM/yyyy"),
                    Dia_Semana = ((int)_Vencimento.DayOfWeek).ToString(),
                    Valor = Rateios[0].Vlr_A_Faturar,

                });

                Rateios[0].Duplicatas = Duplicatas;
                Retorno.Rateios = Rateios;
                Retorno.ComplementoMapas = ComplementoMapas;
                Retorno.Origem = 3;
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
