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
    public class PagoRepository: IPagoRepository
    {
        private readonly BdContext _context;
        public PagoRepository(BdContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pago>> ObtenerList()
        {
            return await _context.pagos
                .Include(p => p.Inscripcion)
                .ToListAsync();
        }

        public async Task<Pago?> ObtenerId(Guid id)
        {
            return await _context.pagos
                .Include(p => p.Inscripcion)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task Crear(Pago pago)
        {
            pago.Id = Guid.NewGuid();
            pago.Fecha_Pago = DateTime.UtcNow;

            _context.pagos.Add(pago);
            await _context.SaveChangesAsync();
        }
        public async Task Actualizar(Pago pago)
        {
            var pagoExiste = await _context.pagos.FirstOrDefaultAsync(p => p.Id == pago.Id);
            if (pagoExiste == null)
                throw new Exception("Pago no encontrado");

            pagoExiste.Monto = pago.Monto;
            pagoExiste.Fecha_Pago = pago.Fecha_Pago;
            pagoExiste.Metodo = pago.Metodo;
            pagoExiste.Estado = pago.Estado;
            pagoExiste.Id_Inscripcion = pago.Id_Inscripcion;

            await _context.SaveChangesAsync();
        }
        public async Task<Pago?> ObtenerPorInscripcion(Guid idInscripcion)
        {
            return await _context.pagos
                .Include(p => p.Inscripcion)
                .FirstOrDefaultAsync(p => p.Id_Inscripcion == idInscripcion);
        }
    }
}
