using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Inscripcion
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Fecha_Inscripcion { get; set; } = DateTime.UtcNow;
        public required string Estado { get; set; }

        // Relaciones
        public Guid Id_Usuario { get; set; }
        [JsonIgnore]
        public Usuario? Usuario { get; set; }  // Navegación

        public Guid Id_Evento { get; set; }
        [JsonIgnore]
        public Evento? Evento { get; set; }    // Navegación
    }
}
