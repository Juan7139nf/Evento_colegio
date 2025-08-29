using Aplication.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplication.UsesCases
{
    public class UsuarioUseCases
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioUseCases(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IEnumerable<Usuario>> ObtenerUsuarios()
        {
            return await _usuarioRepository.ObtenerList();
        }

        public async Task<Usuario?> ObtenerUsuarioPorId(Guid id)
        {
            return await _usuarioRepository.ObtenerId(id);
        }

        public async Task<Usuario> RegistrarUsuario(Usuario usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.Correo))
                throw new Exception("El correo es obligatorio");

            if (string.IsNullOrWhiteSpace(usuario.Contrasenia))
                throw new Exception("La contraseña es obligatoria");

            await _usuarioRepository.Crear(usuario);
            return usuario;
        }

        public async Task<Usuario> ActualizarUsuario(Usuario usuario)
        {
            var existente = await _usuarioRepository.ObtenerId(usuario.Id);
            if (existente == null)
                throw new Exception("Usuario no encontrado");

            await _usuarioRepository.Actualizar(usuario);
            return usuario;
        }

        public async Task<LoginResponseDto> Login(LoginDto loginDto)
        {
            var usuario = await _usuarioRepository.ObtenerPorCorreo(loginDto.Correo);
            if (usuario == null)
            {
                return new LoginResponseDto { Mensaje = "Usuario no encontrado" };
            }

            // Verifica la contraseña
            if (!BCrypt.Net.BCrypt.Verify(loginDto.Contrasenia, usuario.Contrasenia))
            {
                return new LoginResponseDto { Mensaje = "Contraseña incorrecta" };
            }

            // Generar un token sencillo (GUID). En proyectos grandes usar JWT.
            // usuario.Token = Guid.NewGuid().ToString();
            //await _usuarioRepository.Actualizar(usuario);

            return new LoginResponseDto
            {
                Mensaje = "Login correcto",
                Token = usuario.Token,
                Usuario = new UsuarioDto
                {
                    Id = usuario.Id,
                    Nombre = usuario.Nombre,
                    Apellido = usuario.Apellido,
                    Correo = usuario.Correo,
                    Rol = usuario.Rol,
                    Token = usuario.Token,
                    Contrasenia = "********" // nunca exponer la real
                }
            };
        }

        public async Task<Usuario?> ObtenerUsuarioPorTokenOCorreo(string? token, string? correo)
        {
            Usuario? usuario = null;

            if (!string.IsNullOrWhiteSpace(token))
            {
                var lista = await _usuarioRepository.ObtenerList();
                usuario = lista.FirstOrDefault(u => u.Token == token);
            }
            else if (!string.IsNullOrWhiteSpace(correo))
            {
                usuario = await _usuarioRepository.ObtenerPorCorreo(correo);
            }

            return usuario;
        }
    }
}
