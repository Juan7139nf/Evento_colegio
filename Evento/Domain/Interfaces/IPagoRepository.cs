using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPagoRepository
    {
        Task<IEnumerable<Pago>> ObtenerList();
        Task<Pago?> ObtenerId(Guid id);
        Task Crear(Pago pago);
        Task Actualizar(Pago pago);
    }
}
