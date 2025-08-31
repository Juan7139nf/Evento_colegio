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

        public InscripcionUseCases(IInscripcionRepository inscripcionRepository)
        {
            _inscripcionRepository = inscripcionRepository;
        }

        public async Task<IEnumerable<Inscripcion>> ObtenerInscripciones()
        {
            return await _inscripcionRepository.ObtenerList();
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
