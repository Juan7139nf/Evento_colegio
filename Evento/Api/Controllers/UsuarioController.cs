using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        // GET: api/usuario
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var usuarios = await _usuarioRepository.ObtenerList();
            return Ok(usuarios);
        }

        // GET: api/usuario/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var usuario = await _usuarioRepository.ObtenerId(id);
            if (usuario == null)
                return NotFound("Usuario no encontrado");
            return Ok(usuario);
        }

        // POST: api/usuario
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Usuario usuario)
        {
            // Hashear contraseña antes de guardar
            usuario.Contrasenia = BCrypt.Net.BCrypt.HashPassword(usuario.Contrasenia);

            await _usuarioRepository.Crear(usuario);
            return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, usuario);
        }

        // PUT: api/usuario/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Usuario usuario)
        {
            var existente = await _usuarioRepository.ObtenerId(id);
            if (existente == null)
                return NotFound("Usuario no encontrado");

            // Si viene una contraseña nueva, se vuelve a hashear
            if (!string.IsNullOrEmpty(usuario.Contrasenia))
            {
                usuario.Contrasenia = BCrypt.Net.BCrypt.HashPassword(usuario.Contrasenia);
            }
            else
            {
                usuario.Contrasenia = existente.Contrasenia; // mantener la actual
            }

            usuario.Id = id;
            await _usuarioRepository.Actualizar(usuario);

            return NoContent();
        }
    }
}
