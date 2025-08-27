using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Encuesta
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public required String Titulo { get; set; }
        public DateTime Fecha_Creacion { get; set; }

        // Relacion
        public Guid Id_Evento { get; set; }
        [JsonIgnore]
        public Evento? Evento { get; set; }    // Navegación

        public Guid Id_Inscripcion { get; set; }
        [JsonIgnore]
        public Inscripcion? Inscripcion { get; set; }    // Navegación
    }
}
