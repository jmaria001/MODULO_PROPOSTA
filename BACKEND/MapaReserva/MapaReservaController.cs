using System;
using System.Web.Http;
using System.Data;
using System.Collections.Generic;
using System.Globalization;

namespace PROPOSTA
{
    public class MapaReservaController : ApiController
    {
        [Route("api/MapaReserva/List")]
        [HttpGet]
        [ActionName("MapaReservaList")]
        [Authorize()]
        public IHttpActionResult MapaReservaList([FromUri]MapaReserva.MapaReservaFiltroModel Param)
        {
            SimLib clsLib = new SimLib();
            MapaReserva Cls = new MapaReserva(User.Identity.Name);
            try
            {
                DataTable Retorno = Cls.MapaReservaList(Param);
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        [Route("api/MapaReserva/DetalheContrato/{Id}")]
        [HttpGet]
        [ActionName("DetalheContrato")]
        [Authorize()]
        public IHttpActionResult DetalheContrato(Int32 Id)
        {
            SimLib clsLib = new SimLib();
            MapaReserva Cls = new MapaReserva(User.Identity.Name);
            try
            {
                DataTable Retorno = Cls.MapaReservaDetalheContrato(Id);
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        [Route("api/MapaReserva/DetalheComercial/{Id}")]
        [HttpGet]
        [ActionName("DetalheComercial")]
        [Authorize()]
        public IHttpActionResult DetalheComercial(Int32 Id)
        {
            SimLib clsLib = new SimLib();
            MapaReserva Cls = new MapaReserva(User.Identity.Name);
            try
            {
                DataTable Retorno = Cls.MapaReservaDetalheComercial(Id);
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        [Route("api/MapaReserva/DetalheCompetencia/{Id}")]
        [HttpGet]
        [ActionName("DetalheCompetencia")]
        [Authorize()]
        public IHttpActionResult DetalheCompetencia(Int32 Id)
        {
            SimLib clsLib = new SimLib();
            MapaReserva Cls = new MapaReserva(User.Identity.Name);
            try
            {
                DataTable Retorno = Cls.MapaReservaDetalheCompetencia(Id);
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        [Route("api/MapaReserva/DetalheVeiculo/{Id}")]
        [HttpGet]
        [ActionName("DetalheVeiculo")]
        [Authorize()]
        public IHttpActionResult DetalheVeiculo(Int32 Id)
        {
            SimLib clsLib = new SimLib();
            MapaReserva Cls = new MapaReserva(User.Identity.Name);
            try
            {
                DataTable Retorno = Cls.MapaReservaDetalheVeiculo(Id);
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        [Route("api/MapaReserva/DetalheMidia")]
        [HttpGet]
        [ActionName("DetalheMidia")]
        [Authorize()]
        public IHttpActionResult DetalheMidia([FromUri] MapaReserva.MapaReservaMidiaFiltroModel Param)
        {
            SimLib clsLib = new SimLib();
            MapaReserva Cls = new MapaReserva(User.Identity.Name);
            try
            {
                List<MapaReserva.MapaReservaMidiaModel> Retorno = Cls.MapaReservaDetalheMidia(Param);
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        [Route("api/MapaReserva/DetalheVeiculacao")]
        [HttpGet]
        [ActionName("DetalheVeiculacao")]
        [Authorize()]
        public IHttpActionResult DetalheVeiculacao([FromUri] MapaReserva.MapaReservaMidiaFiltroModel Param)
        {
            SimLib clsLib = new SimLib();
            MapaReserva Cls = new MapaReserva(User.Identity.Name);
            try
            {
                DataTable Retorno = Cls.MapaReservaDetalheVeiculacoes(Param);
                return Ok(Retorno);
            }
            catch (Exception Ex)
            {
                clsLib.EmailErrorToSuporte(User.Identity.Name, Ex.Message.ToString(), Ex.Source, Ex.StackTrace);
                throw new Exception(Ex.Message);
            }
        }
        [Route("api/MapaReserva/DetalheResumo")]
        [HttpGet]
        [ActionName("DetalheResumo")]
        [Authorize()]
        public IHttpActionResult DetalheResumo([FromUri] MapaReserva.MapaReservaMidiaFiltroModel Param)
        {
            SimLib clsLib = new SimLib();
            MapaReserva Cls = new MapaReserva(User.Identity.Name);
            try
            {
                DataTable Retorno = Cls.MapaReservaDetalheResumo(Param);
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

