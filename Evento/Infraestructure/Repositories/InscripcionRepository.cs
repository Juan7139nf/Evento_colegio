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
    public class InscripcionRepository: IInscripcionRepository
    {
        private readonly BdContext _context;
        public InscripcionRepository(BdContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Inscripcion>> ObtenerList()
        {
            return await _context.inscripciones
                .Include(i => i.Usuario)
                .Include(i => i.Evento)
                .ToListAsync();
        }

        public async Task<Inscripcion?> ObtenerId(Guid id)
        {
            return await _context.inscripciones
                .Include(i => i.Usuario)
                .Include(i => i.Evento)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task Crear(Inscripcion inscripcion)
        {
            inscripcion.Id = Guid.NewGuid();
            inscripcion.Fecha_Inscripcion = DateTime.UtcNow;

            _context.inscripciones.Add(inscripcion);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Actualizar(Inscripcion inscripcion)
        {
            var inscripcionExiste = await _context.inscripciones.FirstOrDefaultAsync(i => i.Id == inscripcion.Id);
            if (inscripcionExiste == null)
                return false;

            inscripcionExiste.Estado = inscripcion.Estado;
            inscripcionExiste.Id_Usuario = inscripcion.Id_Usuario;
            inscripcionExiste.Id_Evento = inscripcion.Id_Evento;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
