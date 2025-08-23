using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Encuesta
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required String Titulo { get; set; }
        public DateTime Fecha_Creacion { get; set; }

        // Relacion
        public Guid Id_Evento { get; set; }
        public required Evento Evento { get; set; }    // Navegación

        public Guid Id_Inscripcion { get; set; }
        public required Inscripcion Inscripcion { get; set; }    // Navegación
    }
}
