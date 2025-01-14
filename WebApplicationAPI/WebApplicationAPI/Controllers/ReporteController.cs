using ClassLibrary.Solucion;
using ClassLibrary.Transporte;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReporteController : ControllerBase
    {
        private readonly ILogger<ReporteController> _logger;

        public ReporteController(ILogger<ReporteController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public RespuestaAPI<Reporte> Agregar(PeticionAPI<Reporte> peticionAPI)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public RespuestaAPI<Reporte> Actualizar(PeticionAPI<Reporte> peticionAPI)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public RespuestaAPI<bool> Borrar(PeticionAPI<Reporte> peticionAPI)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{Id}")]
        public RespuestaAPI<Reporte> Obtener(int Id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public RespuestaAPI<IEnumerable<Reporte>> Listar()
        {
            throw new NotImplementedException();
        }
    }
}
