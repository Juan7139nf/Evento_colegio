using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.DTOs
{
    public class EncuestaDto
    {
        public Guid Id { get; set; }
        public required String Titulo { get; set; }
        public DateTime Fecha_Creacion { get; set; }

        // Relacion
        public Guid Id_Evento { get; set; }
        public EventoDto? Evento { get; set; }

        public Guid Id_Inscripcion { get; set; }
        public InscripcionDto? Inscripcion { get; set; }
    }
}
