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
    public class NotificacionRepository: INotificacionRepository
    {
        private readonly BdContext _context;
        public NotificacionRepository(BdContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Notificacion>> ObtenerList()
        {
            return await _context.notificaciones
                .Include(n => n.Usuario)
                .Include(n => n.Evento)
                .ToListAsync();
        }

        public async Task<Notificacion?> ObtenerId(Guid id)
        {
            return await _context.notificaciones
                .Include(n => n.Usuario)
                .Include(n => n.Evento)
                .FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task Crear(Notificacion notificacion)
        {
            notificacion.Id = Guid.NewGuid();
            notificacion.Fecha_Envio = DateTime.UtcNow;

            _context.notificaciones.Add(notificacion);
            await _context.SaveChangesAsync();
        }
        public async Task Actualizar(Notificacion notificacion)
        {
            var notificacionExiste = await _context.notificaciones.FirstOrDefaultAsync(n => n.Id == notificacion.Id);
            if (notificacionExiste == null)
                throw new Exception("Notificación no encontrada");

            notificacionExiste.Tipo = notificacion.Tipo;
            notificacionExiste.Estado = notificacion.Estado;
            notificacionExiste.Value = notificacion.Value;
            notificacionExiste.Id_Usuario = notificacion.Id_Usuario;
            notificacionExiste.Id_Evento = notificacion.Id_Evento;

            await _context.SaveChangesAsync();
        }
    }
}
