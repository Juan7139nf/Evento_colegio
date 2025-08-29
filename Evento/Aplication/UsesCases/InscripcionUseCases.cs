using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
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
