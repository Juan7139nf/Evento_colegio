using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface INotificacionRepository
    {
        Task<IEnumerable<Notificacion>> ObtenerList();
        Task<Notificacion?> ObtenerId(Guid id);
        Task Crear(Notificacion notificacion);
        Task<bool> Actualizar(Notificacion notificacion);
    }
}
