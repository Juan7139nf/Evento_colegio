using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.DTOs
{
    public class InscripcionDto
    {
        public Guid Id { get; set; }
        public DateTime Fecha_Inscripcion { get; set; }
        public required string Estado { get; set; }

        // Relaciones
        public Guid Id_Usuario { get; set; }
        public UsuarioDto? Usuario { get; set; }

        public Guid Id_Evento { get; set; }
        public EventoDto? Evento { get; set; }
    }
}
