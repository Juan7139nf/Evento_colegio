using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.DTOs
{
    public class PagoDto
    {
        public Guid Id { get; set; }
        public double? Monto { get; set; }
        public DateTime Fecha_Pago { get; set; }
        public required String Metodo { get; set; }
        public required String Estado { get; set; }

        // Relacion
        public Guid Id_Inscripcion { get; set; }
        public InscripcionDto? Inscripcion { get; set; }
    }
}
