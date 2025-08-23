using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Inscripcion
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Fecha_Inscripcion { get; set; } = DateTime.UtcNow;
        public required string Estado { get; set; }

        // Relaciones
        public Guid Id_Usuario { get; set; }
        public required Usuario Usuario { get; set; }  // Navegación

        public Guid Id_Evento { get; set; }
        public required Evento Evento { get; set; }    // Navegación
    }
}
