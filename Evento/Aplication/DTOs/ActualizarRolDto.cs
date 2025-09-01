using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.DTOs
{
    public class ActualizarRolDto
    {
        public Guid Id { get; set; }
        public string Rol { get; set; } = string.Empty;
    }

}
