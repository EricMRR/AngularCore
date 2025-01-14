using ClassLibrary.Solucion;
using ClassLibrary.Transporte;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CuentaController : ControllerBase
    {
        private readonly ILogger<CuentaController> _logger;

        public CuentaController(ILogger<CuentaController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public RespuestaAPI<Cuenta> Agregar(PeticionAPI<Cuenta> peticionAPI)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public RespuestaAPI<Cuenta> Actualizar(PeticionAPI<Cuenta> peticionAPI)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public RespuestaAPI<bool> Borrar(PeticionAPI<Cuenta> peticionAPI)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{Id}")]
        public RespuestaAPI<Cuenta> Obtener(int Id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public RespuestaAPI<IEnumerable<Cuenta>> Listar()
        {
            throw new NotImplementedException();
        }
    }
}
