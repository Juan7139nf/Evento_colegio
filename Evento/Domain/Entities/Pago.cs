using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Pago
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public double? Monto { get; set; }
        public DateTime Fecha_Pago { get; set; }
        public required String Metodo {  get; set; }
        public required String Estado { get; set; }

        // Relacion
        public Guid Id_Inscripcion { get; set; }
        [JsonIgnore]
        public Inscripcion? Inscripcion { get; set; }    // Navegación
    }
}
