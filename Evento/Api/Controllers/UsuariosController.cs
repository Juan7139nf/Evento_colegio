using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuariosController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository ?? throw new ArgumentNullException(nameof(usuarioRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> ObtenerTodos()
        {
            var usuarios = await _usuarioRepository.ObtenerList();
            return Ok(usuarios);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Usuario>> ObtenerPorId(Guid id)
        {
            var usuario = await _usuarioRepository.ObtenerId(id);
            if (usuario is null) return NotFound();
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] Usuario usuario)
        {
            try
            {
                await _usuarioRepository.Crear(usuario);
                return Ok(new
                {
                    success = true,
                    message = "Usuario creado correctamente",
                    data = new
                    {
                        usuario.Id,
                        usuario.Nombre,
                        usuario.Apellido,
                        usuario.Correo,
                        usuario.Token,
                        usuario.Rol
                    }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Actualizar(Guid id, [FromBody] Usuario usuario)
        {
            var existente = await _usuarioRepository.ObtenerId(id);
            if (existente is null) return NotFound();

            usuario.Id = id;
            await _usuarioRepository.Actualizar(usuario);
            return NoContent();
        }
    }
}
