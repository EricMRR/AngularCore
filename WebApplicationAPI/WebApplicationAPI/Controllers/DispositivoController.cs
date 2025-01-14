
using ClassLibrary.Solucion;
using ClassLibrary.Transporte;
using ClassLibraryDAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using System.Text.Json.Serialization;

namespace WebApplicationAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.Unspecified)]
    public class DispositivoController : ControllerBase
    {
        private readonly ILogger<DispositivoController> logger;
        private readonly Contexto contexto;

        public DispositivoController(ILogger<DispositivoController> logger, Contexto contexto)
        {
            this.logger = logger;
            this.contexto = contexto;
        }

        [EnableCors]
        [HttpPost]
        [NoPropertyNamingPolicy]
        public RespuestaAPI<Dispositivo> Agregar(PeticionAPI<Dispositivo> peticionAPI) {
            RespuestaAPI<Dispositivo> r = new RespuestaAPI<Dispositivo>();
            try
            {
                //Obtencion de datos de sesion... me falta cachar esa parte
                Dispositivo d = new Dispositivo() { Id = peticionAPI.Data.Id, CuentaId = peticionAPI.Data.CuentaId, Eliminado = peticionAPI.Data.Eliminado, Modificacion = peticionAPI.Data.Modificacion, Nombre = peticionAPI.Data.Nombre };
                contexto.Dispositivos.Add(d);
                contexto.SaveChangesAsync();

                r.Data = d;
                r.Code = 0;
                r.Mensaje = "OK";
            }
            catch (Exception ee)
            {
                r.Code = 100;
                r.Mensaje = ee.Message;
            }
            return r;
        }

        [EnableCors]
        [HttpPut]
        [NoPropertyNamingPolicy]
        public RespuestaAPI<Dispositivo> Actualizar(PeticionAPI<Dispositivo> peticionAPI)
        {
            RespuestaAPI<Dispositivo> r = new RespuestaAPI<Dispositivo>();
            try
            {
                //Obtencion de datos de sesion... me falta cachar esa parte
                Dispositivo d = new Dispositivo() { Id = peticionAPI.Data.Id, CuentaId = peticionAPI.Data.CuentaId, Eliminado = peticionAPI.Data.Eliminado, Modificacion = peticionAPI.Data.Modificacion, Nombre = peticionAPI.Data.Nombre };
                contexto.Dispositivos.Update(d);
                contexto.SaveChangesAsync();

                r.Data = d;
                r.Code = 0;
                r.Mensaje = "OK";
            }
            catch (Exception ee)
            {
                r.Code = 100;
                r.Mensaje = ee.Message;
            }
            return r;
        }

        [EnableCors]
        [HttpDelete("{Id}")]
        [NoPropertyNamingPolicy]
        public RespuestaAPI<bool> Borrar(Int32 Id)
        {
            RespuestaAPI<bool> r = new RespuestaAPI<bool>();
            try
            {
                //Obtencion de datos de sesion... me falta cachar esa parte
                Dispositivo d = new Dispositivo() { Id = Id };
                contexto.Dispositivos.Remove(d);
                contexto.SaveChangesAsync();

                r.Data = true;
                r.Code = 0;
                r.Mensaje = "OK";
            }
            catch (Exception ee)
            {
                r.Code = 100;
                r.Mensaje = ee.Message;
            }
            return r;
        }

        [EnableCors]
        [HttpGet("{Id}")]
        [NoPropertyNamingPolicy]
        public RespuestaAPI<Dispositivo> Obtener(Int32 Id)
        {
            RespuestaAPI<Dispositivo> r = new RespuestaAPI<Dispositivo>();
            try
            {
                //Obtencion de datos de sesion... me falta cachar esa parte
                r.Data = contexto.Dispositivos.Where(x => x.Id == Id).FirstOrDefault();
                r.Code = 0;
                r.Mensaje = "OK";
            }
            catch (Exception ee)
            {
                r.Code = 100;
                r.Mensaje = ee.Message;
            }
            return r;
        }

        [EnableCors]
        [HttpGet]
        [NoPropertyNamingPolicy]
        public RespuestaAPI<IEnumerable<Dispositivo>> Listar()
        {
            RespuestaAPI<IEnumerable<Dispositivo>> r = new RespuestaAPI<IEnumerable<Dispositivo>>();
            try
            {
                //Obtencion de datos de sesion... me falta cachar esa parte
                r.Data = contexto.Dispositivos.Where(x => x.Id == x.Id).ToList();
                r.Code = 0;
                r.Mensaje = "OK";
            }
            catch (Exception ee)
            {
                r.Code = 100;
                r.Mensaje = ee.Message;
            }
            return r;
        }
    }
}
