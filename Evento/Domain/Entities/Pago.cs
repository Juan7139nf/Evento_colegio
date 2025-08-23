using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Pago
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public double? Monto { get; set; }
        public DateTime Fecha_Pago { get; set; }
        public required String Metodo {  get; set; }
        public required String Estado { get; set; }

        // Relacion
        public Guid Id_Inscripcion { get; set; }
        public required Inscripcion Inscripcion { get; set; }    // Navegación
    }
}
