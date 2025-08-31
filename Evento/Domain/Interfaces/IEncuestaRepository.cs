using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IEncuestaRepository
    {
        Task<IEnumerable<Encuesta>> ObtenerList();
        Task<Encuesta?> ObtenerId(Guid id);
        Task Crear(Encuesta encuesta);
        Task Actualizar(Encuesta encuesta);
        Task<Encuesta?> ObtenerPorEventoEInscripcion(Guid idEvento, Guid idInscripcion);
    }
}
