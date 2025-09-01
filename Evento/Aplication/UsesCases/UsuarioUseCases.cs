using Aplication.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplication.UsesCases
{
    public class UsuarioUseCases
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IInscripcionRepository _inscripcionRepository;

        public UsuarioUseCases(IUsuarioRepository usuarioRepository, IInscripcionRepository inscripcionRepository)
        {
            _usuarioRepository = usuarioRepository;
            _inscripcionRepository = inscripcionRepository;
        }
        public async Task<IEnumerable<UsuarioConInscripcionesDto>> ObtenerUsuariosConInscripciones()
        {
            var usuarios = await _usuarioRepository.ObtenerList();
            var inscripciones = await _inscripcionRepository.ObtenerList();

            return usuarios.Select(u => new UsuarioConInscripcionesDto
            {
                Id = u.Id,
                Nombre = u.Nombre,
                Apellido = u.Apellido,
                Correo = u.Correo,
                Rol = u.Rol,
                Completadas = inscripciones.Count(i => i.Id_Usuario == u.Id && i.Estado == "Completado"),
                Pendientes = inscripciones.Count(i => i.Id_Usuario == u.Id && i.Estado == "Pendiente")
            });
        }

        public async Task<Usuario> ActualizarRol(ActualizarRolDto dto)
        {
            var usuario = await _usuarioRepository.ObtenerId(dto.Id);
            if (usuario == null)
                throw new Exception("Usuario no encontrado");

            usuario.Rol = dto.Rol;
            await _usuarioRepository.Actualizar(usuario);

            return usuario;
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

            // Validar que no exista un usuario con el mismo correo
            var existente = await _usuarioRepository.ObtenerPorCorreo(usuario.Correo);
            if (existente != null)
                throw new Exception("Ya existe un usuario con ese correo");

            await _usuarioRepository.Crear(usuario);
            return usuario;
        }

        public async Task<Usuario> ActualizarUsuario(Usuario usuario)
        {
            var existente = await _usuarioRepository.ObtenerId(usuario.Id);
            if (existente == null)
                throw new Exception("Usuario no encontrado");

            // Validar que no se actualice a un correo que pertenece a otro usuario
            var otroUsuario = await _usuarioRepository.ObtenerPorCorreo(usuario.Correo);
            if (otroUsuario != null && otroUsuario.Id != usuario.Id)
                throw new Exception("Ya existe otro usuario con ese correo");

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
            // await _usuarioRepository.Actualizar(usuario);

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
