using Domain.Entities;
using Aplication.UsesCases;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("Cors")]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioUseCases _usuarioUseCases;

        public UsuariosController(UsuarioUseCases usuarioUseCases)
        {
            _usuarioUseCases = usuarioUseCases ?? throw new ArgumentNullException(nameof(usuarioUseCases));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> ObtenerTodos()
        {
            var usuarios = await _usuarioUseCases.ObtenerUsuarios();
            return Ok(usuarios);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Usuario>> ObtenerPorId(Guid id)
        {
            var usuario = await _usuarioUseCases.ObtenerUsuarioPorId(id);
            if (usuario == null) return NotFound();
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] Usuario usuario)
        {
            try
            {
                var creado = await _usuarioUseCases.RegistrarUsuario(usuario);
                return Ok(new
                {
                    success = true,
                    message = "Usuario creado correctamente",
                    data = new
                    {
                        creado.Id,
                        creado.Nombre,
                        creado.Apellido,
                        creado.Correo,
                        creado.Token,
                        creado.Rol
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
            try
            {
                usuario.Id = id;
                var actualizado = await _usuarioUseCases.ActualizarUsuario(usuario);
                return Ok(new
                {
                    success = true,
                    message = "Usuario actualizado correctamente",
                    data = new
                    {
                        actualizado.Id,
                        actualizado.Nombre,
                        actualizado.Apellido,
                        actualizado.Correo,
                        actualizado.Token,
                        actualizado.Rol
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
    }
}
