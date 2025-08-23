using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Notificacion
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Tipo { get; set; }
        public DateTime Fecha_Envio { get; set; }
        public required String Estado {  get; set; }

        // Relaciones
        public Guid Id_Usuario { get; set; }
        public required Usuario Usuario { get; set; }  // Navegación

        public Guid Id_Evento { get; set; }
        public required Evento Evento { get; set; }    // Navegación
    }
}
