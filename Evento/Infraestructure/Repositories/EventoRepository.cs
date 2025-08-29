using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class EventoRepository: IEventoRepository
    {
        private readonly BdContext _context;
        public EventoRepository(BdContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Evento>> ObtenerList()
        {
            return await _context.eventos.ToListAsync();
        }

        public async Task<Evento?> ObtenerId(Guid id)
        {
            return await _context.eventos.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task Crear(Evento evento)
        {
            evento.Id = Guid.NewGuid();
            _context.eventos.Add(evento);
            await _context.SaveChangesAsync();
        }
        public async Task Actualizar(Evento evento)
        {
            var eventoExiste = await _context.eventos.FirstOrDefaultAsync(e => e.Id == evento.Id);
            if (eventoExiste == null)
                throw new Exception("Evento no encontrado");

            eventoExiste.Nombre_Evento = evento.Nombre_Evento;
            eventoExiste.Tipo = evento.Tipo;
            eventoExiste.Fecha = evento.Fecha;
            eventoExiste.Lugar = evento.Lugar;
            eventoExiste.Capacidad_Max = evento.Capacidad_Max;
            eventoExiste.Costo = evento.Costo;
            eventoExiste.Encuesta = evento.Encuesta;
            eventoExiste.Estado = evento.Estado;
            eventoExiste.ContentJson = evento.ContentJson;
            eventoExiste.ArchivosJson = evento.ArchivosJson;

            await _context.SaveChangesAsync();
        }
    }
}
