using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Reporte
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Tipo { get; set; }
        public DateTime Fecha_Generacion { get; set; }
        public required string Archivo { get; set;}

        // Relacion
        public Guid Id_Evento { get; set; }
        [JsonIgnore]
        public Evento? Evento { get; set; }    // Navegación
    }
}
