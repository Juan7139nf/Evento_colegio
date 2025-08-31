using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.DTOs
{
    public class EventoDetalleDto
    {
        public EventoDto? Evento { get; set; }
        public InscripcionDto? Inscripcion { get; set; }
        public PagoDto? Pago { get; set; }
        public EncuestaDto? Encuesta { get; set; }
    }
}
