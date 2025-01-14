using ClassLibrary.Solucion;
using ClassLibrary.Transporte;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(ILogger<UsuarioController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public RespuestaAPI<Usuario> Agregar(PeticionAPI<Usuario> peticionAPI)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public RespuestaAPI<Usuario> Actualizar(PeticionAPI<Usuario> peticionAPI)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public RespuestaAPI<bool> Borrar(PeticionAPI<Usuario> peticionAPI)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{Id}")]
        public RespuestaAPI<Usuario> Obtener(int Id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public RespuestaAPI<IEnumerable<Usuario>> Listar()
        {
            throw new NotImplementedException();
        }
    }
}
