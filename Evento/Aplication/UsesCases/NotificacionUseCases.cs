using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplication.UsesCases
{
    public class NotificacionUseCases
    {
        private readonly INotificacionRepository _notificacionRepository;

        public NotificacionUseCases(INotificacionRepository notificacionRepository)
        {
            _notificacionRepository = notificacionRepository;
        }

        public async Task<IEnumerable<Notificacion>> ObtenerNotificaciones()
        {
            return await _notificacionRepository.ObtenerList();
        }

        public async Task<Notificacion?> ObtenerNotificacionPorId(Guid id)
        {
            return await _notificacionRepository.ObtenerId(id);
        }

        public async Task<Notificacion> CrearNotificacion(Notificacion notificacion)
        {
            // Validación simple
            if (string.IsNullOrWhiteSpace(notificacion.Tipo))
                throw new Exception("El tipo de notificación es obligatorio");

            if (string.IsNullOrWhiteSpace(notificacion.Estado))
                throw new Exception("El estado de la notificación es obligatorio");

            await _notificacionRepository.Crear(notificacion);
            return notificacion;
        }

        public async Task<Notificacion> ActualizarNotificacion(Notificacion notificacion)
        {
            var existente = await _notificacionRepository.ObtenerId(notificacion.Id);
            if (existente == null)
                throw new Exception("Notificación no encontrada");

            await _notificacionRepository.Actualizar(notificacion);
            return notificacion;
        }
    }
}
