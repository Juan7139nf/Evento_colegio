using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplication.UsesCases
{
    public class EventoUseCases
    {
        private readonly IEventoRepository _eventoRepository;

        public EventoUseCases(IEventoRepository eventoRepository)
        {
            _eventoRepository = eventoRepository;
        }

        public async Task<IEnumerable<Evento>> ObtenerEventos()
        {
            return await _eventoRepository.ObtenerList();
        }

        public async Task<Evento?> ObtenerEventoPorId(Guid id)
        {
            return await _eventoRepository.ObtenerId(id);
        }

        public async Task<Evento> CrearEvento(Evento evento)
        {
            if (evento == null)
                throw new ArgumentNullException(nameof(evento));

            if (evento.Capacidad_Max <= 0)
                throw new Exception("La capacidad máxima debe ser mayor a 0");

            await _eventoRepository.Crear(evento);
            return evento;
        }

        public async Task<Evento> ActualizarEvento(Evento evento)
        {
            var existente = await _eventoRepository.ObtenerId(evento.Id);
            if (existente == null)
                throw new Exception("Evento no encontrado");

            await _eventoRepository.Actualizar(evento);
            return evento;
        }
    }
}
