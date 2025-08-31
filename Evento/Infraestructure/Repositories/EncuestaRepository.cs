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
    public class EncuestaRepository: IEncuestaRepository
    {
        private readonly BdContext _context;
        public EncuestaRepository(BdContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Encuesta>> ObtenerList()
        {
            return await _context.encuestas
                .Include(e => e.Evento)
                .Include(e => e.Inscripcion)
                .ToListAsync();
        }

        public async Task<Encuesta?> ObtenerId(Guid id)
        {
            return await _context.encuestas
                .Include(e => e.Evento)
                .Include(e => e.Inscripcion)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task Crear(Encuesta encuesta)
        {
            encuesta.Id = Guid.NewGuid();
            encuesta.Fecha_Creacion = DateTime.UtcNow;

            _context.encuestas.Add(encuesta);
            await _context.SaveChangesAsync();
        }
        public async Task Actualizar(Encuesta encuesta)
        {
            var encuestaExiste = await _context.encuestas.FirstOrDefaultAsync(e => e.Id == encuesta.Id);
            if (encuestaExiste == null)
                throw new Exception("Encuesta no encontrada");

            encuestaExiste.Fecha_Creacion = encuesta.Fecha_Creacion;
            encuestaExiste.Value = encuesta.Value;
            encuestaExiste.Id_Evento = encuesta.Id_Evento;
            encuestaExiste.Id_Inscripcion = encuesta.Id_Inscripcion;

            await _context.SaveChangesAsync();
        }

        public async Task<Encuesta?> ObtenerPorEventoEInscripcion(Guid eventoId, Guid inscripcionId)
        {
            return await _context.encuestas
                .Include(e => e.Evento)
                .Include(e => e.Inscripcion)
                .FirstOrDefaultAsync(e => e.Id_Evento == eventoId && e.Id_Inscripcion == inscripcionId);
        }

    }
}
