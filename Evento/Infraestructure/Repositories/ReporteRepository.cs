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
    public class ReporteRepository: IReporteRepository
    {
        private readonly BdContext _context;
        public ReporteRepository(BdContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reporte>> ObtenerList()
        {
            return await _context.reportes
                .Include(r => r.Evento)
                .ToListAsync();
        }

        public async Task<Reporte?> ObtenerId(Guid id)
        {
            return await _context.reportes
                .Include(r => r.Evento)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task Crear(Reporte reporte)
        {
            reporte.Id = Guid.NewGuid();
            reporte.Fecha_Generacion = DateTime.UtcNow;

            _context.reportes.Add(reporte);
            await _context.SaveChangesAsync();
        }
        public async Task Actualizar(Reporte reporte)
        {
            var reporteExiste = await _context.reportes.FirstOrDefaultAsync(r => r.Id == reporte.Id);
            if (reporteExiste == null)
                throw new Exception("Reporte no encontrado");

            reporteExiste.Tipo = reporte.Tipo;
            reporteExiste.Archivo = reporte.Archivo;
            reporteExiste.Id_Evento = reporte.Id_Evento;

            await _context.SaveChangesAsync();
        }
    }
}
