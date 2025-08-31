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
            if (string.IsNullOrWhiteSpace(notificacion.Tipo))
                throw new Exception("El tipo de notificación es obligatorio");

            if (string.IsNullOrWhiteSpace(notificacion.Estado))
                throw new Exception("El estado de la notificación es obligatorio");

            var existentes = await _notificacionRepository.ObtenerList();
            bool duplicada = existentes.Any(n =>
                n.Tipo.Equals(notificacion.Tipo, StringComparison.OrdinalIgnoreCase) &&
                n.Estado.Equals(notificacion.Estado, StringComparison.OrdinalIgnoreCase) &&
                n.Id_Usuario == notificacion.Id_Usuario &&
                n.Id_Evento == notificacion.Id_Evento &&
                n.Value?.Trim() == notificacion.Value?.Trim());

            if (duplicada)
                throw new Exception("Ya existe una notificación con los mismos datos.");

            await _notificacionRepository.Crear(notificacion);
            return notificacion;
        }


        public async Task<Notificacion> ActualizarNotificacion(Notificacion notificacion)
        {
            var existente = await _notificacionRepository.ObtenerId(notificacion.Id);
            if (existente == null)
                throw new Exception("Notificación no encontrada");

            bool sinCambios =
                existente.Tipo == notificacion.Tipo &&
                existente.Estado == notificacion.Estado &&
                existente.Id_Usuario == notificacion.Id_Usuario &&
                existente.Id_Evento == notificacion.Id_Evento &&
                existente.Value?.Trim() == notificacion.Value?.Trim();

            if (sinCambios)
                throw new Exception("No se realizaron cambios en la notificación.");

            await _notificacionRepository.Actualizar(notificacion);
            return notificacion;
        }

    }
}
