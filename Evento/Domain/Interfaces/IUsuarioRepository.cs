using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> ObtenerList();
        Task<Usuario?> ObtenerId(Guid id);
        Task Crear(Usuario usuario);
        Task Actualizar(Usuario usuario);
    }
}
