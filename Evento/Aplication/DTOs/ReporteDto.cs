using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.DTOs
{
    public class ReporteDto
    {
        public Guid Id { get; set; }
        public required string Tipo { get; set; }
        public DateTime Fecha_Generacion { get; set; }
        public required string Archivo { get; set; }

        // Relacion
        public Guid Id_Evento { get; set; }
        public EventoDto? Evento { get; set; }
    }
}
