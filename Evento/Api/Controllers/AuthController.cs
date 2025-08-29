using Aplication.DTOs;
using Aplication.UsesCases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UsuarioUseCases _usuarioUseCases;

        public AuthController(UsuarioUseCases usuarioUseCases)
        {
            _usuarioUseCases = usuarioUseCases;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result = await _usuarioUseCases.Login(loginDto);
            if (string.IsNullOrEmpty(result.Token))
                return Unauthorized(result);

            return Ok(result);
        }

        [HttpPost("user")]
        public async Task<IActionResult> ObtenerUsuario([FromBody] UsuarioRequestDto data)
        {
            var usuario = await _usuarioUseCases.ObtenerUsuarioPorTokenOCorreo(data.Token, data.Correo);
            if (usuario == null) 
                return NotFound(new 
                { 
                    success = false, 
                    message = "Usuario no encontrado" 
                });

            return Ok(new
            {
                success = true,
                message = "Usuario encontrado",
                data = new
                {
                    id = usuario.Id,
                    nombre = usuario.Nombre,
                    apellido = usuario.Apellido,
                    correo = usuario.Correo,
                    rol = usuario.Rol,
                    token = usuario.Token
                }
            });
        }
    }
}
