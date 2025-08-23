using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IReporteRepository
    {
        Task<IEnumerable<Reporte>> ObtenerList();
        Task<Reporte?> ObtenerId(Guid id);
        Task Crear(Reporte reporte);
        Task<bool> Actualizar(Reporte reporte);
    }
}
