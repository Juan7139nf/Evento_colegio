using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IEventoRepository
    {
        Task<IEnumerable<Evento>> ObtenerList();
        Task<Evento?> ObtenerId(Guid id);
        Task Crear(Evento evento);
        Task<bool> Actualizar(Evento evento);
    }
}
