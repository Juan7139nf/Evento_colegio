using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Evento
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Nombre_Evento { get; set; }
        public required string Tipo { get; set; }
        public DateTime Fecha { get; set; }
        public string? Lugar { get; set; }
        public int Capacidad_Max { get; set; }
        public string? Estado { get; set; } = "Activo";

        // Guardados en la BD como JSON
        [Column(TypeName = "nvarchar(max)")]
        public string? ContentJson { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string? ArchivosJson { get; set; }
    }
}
