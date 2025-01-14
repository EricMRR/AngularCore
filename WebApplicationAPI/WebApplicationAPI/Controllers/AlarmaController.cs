using ClassLibrary.Solucion;
using ClassLibrary.Transporte;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlarmaController : ControllerBase
    {
        private readonly ILogger<AlarmaController> _logger;

        public AlarmaController(ILogger<AlarmaController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public RespuestaAPI<Alarma> Agregar(PeticionAPI<Alarma> peticionAPI)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public RespuestaAPI<Alarma> Actualizar(PeticionAPI<Alarma> peticionAPI)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public RespuestaAPI<bool> Borrar(PeticionAPI<Alarma> peticionAPI)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{Id}")]
        public RespuestaAPI<Alarma> Obtener(int Id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public RespuestaAPI<IEnumerable<Alarma>> Listar()
        {
            throw new NotImplementedException();
        }
    }
}
