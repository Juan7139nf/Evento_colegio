using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplication.UsesCases
{
    public class EncuestaUseCases
    {
        private readonly IEncuestaRepository _encuestaRepository;

        public EncuestaUseCases(IEncuestaRepository encuestaRepository)
        {
            _encuestaRepository = encuestaRepository;
        }

        public async Task<IEnumerable<Encuesta>> ObtenerEncuestas()
        {
            return await _encuestaRepository.ObtenerList();
        }

        public async Task<Encuesta?> ObtenerEncuestaPorId(Guid id)
        {
            return await _encuestaRepository.ObtenerId(id);
        }

        public async Task<Encuesta> CrearEncuesta(Encuesta encuesta)
        {
            if (encuesta == null)
                throw new ArgumentNullException(nameof(encuesta));

            await _encuestaRepository.Crear(encuesta);
            return encuesta;
        }

        public async Task<Encuesta> ActualizarEncuesta(Encuesta encuesta)
        {
            var existente = await _encuestaRepository.ObtenerId(encuesta.Id);
            if (existente == null)
                throw new Exception("Encuesta no encontrada");

            await _encuestaRepository.Actualizar(encuesta);
            return encuesta;
        }
    }
}
