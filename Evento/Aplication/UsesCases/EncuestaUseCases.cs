using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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

            var encuestasExistentes = await _encuestaRepository.ObtenerList();

            bool yaExiste = encuestasExistentes.Any(e =>
                e.Value == encuesta.Value &&
                e.Id_Evento == encuesta.Id_Evento &&
                e.Id_Inscripcion == encuesta.Id_Inscripcion
            );

            if (yaExiste)
                throw new InvalidOperationException("Ya existe una encuesta con los mismos datos.");

            await _encuestaRepository.Crear(encuesta);
            return encuesta;
        }

        public async Task<Encuesta> ActualizarEncuesta(Encuesta encuesta)
        {
            if (encuesta == null)
                throw new ArgumentNullException(nameof(encuesta));

            var existente = await _encuestaRepository.ObtenerId(encuesta.Id);
            if (existente == null)
                throw new Exception("Encuesta no encontrada");

            bool sinCambios =
                existente.Value == encuesta.Value &&
                existente.Id_Evento == encuesta.Id_Evento &&
                existente.Id_Inscripcion == encuesta.Id_Inscripcion;

            if (sinCambios)
                throw new InvalidOperationException("No hay cambios en la encuesta. Actualización omitida.");

            await _encuestaRepository.Actualizar(encuesta);
            return encuesta;
        }
    }
}
