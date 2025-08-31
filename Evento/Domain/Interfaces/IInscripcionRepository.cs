using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IInscripcionRepository
    {
        Task<IEnumerable<Inscripcion>> ObtenerList();
        Task<Inscripcion?> ObtenerId(Guid id);
        Task Crear(Inscripcion inscripcion);
        Task Actualizar(Inscripcion inscripcion);
        Task<Inscripcion?> ObtenerPorEventoYUsuario(Guid idEvento, Guid idUsuario);
    }
}
