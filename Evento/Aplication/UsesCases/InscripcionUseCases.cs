using Aplication.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplication.UsesCases
{
    public class InscripcionUseCases
    {
        private readonly IInscripcionRepository _inscripcionRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IEventoRepository _eventoRepository;

        public InscripcionUseCases(
            IInscripcionRepository inscripcionRepository,
            IUsuarioRepository usuarioRepository,
            IEventoRepository eventoRepository)
        {
            _inscripcionRepository = inscripcionRepository;
            _usuarioRepository = usuarioRepository;
            _eventoRepository = eventoRepository;
        }

        /*
        public async Task<IEnumerable<Inscripcion>> ObtenerInscripciones()
        {
            return await _inscripcionRepository.ObtenerList();
        }*/
        public async Task<IEnumerable<InscripcionDto>> ObtenerInscripciones()
        {
            var inscripciones = await _inscripcionRepository.ObtenerList();
            var usuarios = await _usuarioRepository.ObtenerList();
            var eventos = await _eventoRepository.ObtenerList();

            return inscripciones.Select(i =>
            {
                var usuario = usuarios.FirstOrDefault(u => u.Id == i.Id_Usuario);
                var evento = eventos.FirstOrDefault(e => e.Id == i.Id_Evento);

                return new InscripcionDto
                {
                    Id = i.Id,
                    Fecha_Inscripcion = i.Fecha_Inscripcion,
                    Estado = i.Estado,
                    Id_Usuario = i.Id_Usuario,
                    Id_Evento = i.Id_Evento,
                    Usuario = usuario == null ? null : new UsuarioDto
                    {
                        Id = usuario.Id,
                        Nombre = usuario.Nombre,
                        Apellido = usuario.Apellido,
                        Correo = usuario.Correo,
                        Contrasenia = usuario.Contrasenia,
                        Token = usuario.Token,
                        Rol = usuario.Rol
                    },
                    Evento = evento == null ? null : new EventoDto
                    {
                        Id = evento.Id,
                        Nombre_Evento = evento.Nombre_Evento,
                        Tipo = evento.Tipo,
                        Fecha = evento.Fecha,
                        Lugar = evento.Lugar,
                        Capacidad_Max = evento.Capacidad_Max,
                        Estado = evento.Estado,
                        Encuesta = evento.Encuesta,
                        Costo = evento.Costo,
                        Archivos = evento.Archivos?.Select(a => new ArchivoDto
                        {
                            Orden = a.Orden,
                            Url = a.Url,
                            Tipo = a.Tipo
                        }).ToList()
                    }
                };
            });
        }


        public async Task<Inscripcion?> ObtenerInscripcionPorId(Guid id)
        {
            return await _inscripcionRepository.ObtenerId(id);
        }

        public async Task<Inscripcion> CrearInscripcion(Inscripcion inscripcion)
        {
            if (inscripcion == null)
                throw new ArgumentNullException(nameof(inscripcion));

            // Verifica si ya existe una inscripción con ese usuario y evento
            var todas = await _inscripcionRepository.ObtenerList();
            var yaExiste = todas.Any(i =>
                i.Id_Usuario == inscripcion.Id_Usuario &&
                i.Id_Evento == inscripcion.Id_Evento
            );

            if (yaExiste)
                throw new Exception("El usuario ya está inscrito en este evento.");

            await _inscripcionRepository.Crear(inscripcion);
            return inscripcion;
        }

        public async Task<Inscripcion> ActualizarInscripcion(Inscripcion inscripcion)
        {
            var existente = await _inscripcionRepository.ObtenerId(inscripcion.Id);
            if (existente == null)
                throw new Exception("Inscripción no encontrada");

            await _inscripcionRepository.Actualizar(inscripcion);
            return inscripcion;
        }
    }
}
