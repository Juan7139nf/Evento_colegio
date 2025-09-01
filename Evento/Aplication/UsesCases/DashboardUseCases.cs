using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplication.DTOs;
using Domain.Interfaces;

namespace Aplication.UsesCases
{
    public class DashboardUseCases
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IEventoRepository _eventoRepository;
        private readonly IInscripcionRepository _inscripcionRepository;
        private readonly IPagoRepository _pagoRepository;
        private readonly IEncuestaRepository _encuestaRepository;

        public DashboardUseCases(
            IUsuarioRepository usuarioRepository,
            IEventoRepository eventoRepository,
            IInscripcionRepository inscripcionRepository,
            IPagoRepository pagoRepository,
            IEncuestaRepository encuestaRepository)
        {
            _usuarioRepository = usuarioRepository;
            _eventoRepository = eventoRepository;
            _inscripcionRepository = inscripcionRepository;
            _pagoRepository = pagoRepository;
            _encuestaRepository = encuestaRepository;
        }

        public async Task<DashboardDto> ObtenerEstadisticas()
        {
            var usuarios = await _usuarioRepository.ObtenerList();
            var eventos = await _eventoRepository.ObtenerList();
            var inscripciones = await _inscripcionRepository.ObtenerList();
            var pagos = await _pagoRepository.ObtenerList();
            var encuestas = await _encuestaRepository.ObtenerList();

            var inscripcionesPorEstado = inscripciones
                .GroupBy(i => i.Estado)
                .ToDictionary(g => g.Key ?? "SinEstado", g => g.Count());

            return new DashboardDto
            {
                TotalUsuarios = usuarios.Count(),
                TotalEventos = eventos.Count(),
                TotalInscripciones = inscripciones.Count(),
                TotalPagos = pagos.Count(),
                TotalEncuestas = encuestas.Count(),
                Inscripciones = inscripcionesPorEstado
            };
        }
    } 
}
