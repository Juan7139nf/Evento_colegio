using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.DTOs
{
    public class NotificacionDto
    {
        public Guid Id { get; set; }
        public required string Tipo { get; set; }
        public DateTime Fecha_Envio { get; set; }
        public required String Estado { get; set; }

        // Relaciones
        public Guid Id_Usuario { get; set; }
        public UsuarioDto? Usuario { get; set; }

        public Guid Id_Evento { get; set; }
        public EventoDto? Evento { get; set; }
    }
}
