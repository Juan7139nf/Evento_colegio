using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
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
        public string? Encuesta { get; set; }
        public double? Costo { get; set; }

        // Guardados en la BD como JSON
        public string? ContentJson { get; set; }
        public string? ArchivosJson { get; set; }

        // Propiedades que no se guardan directamente, pero sirven para acceder a listas
        public List<Seccion>? Content
        {
            get => string.IsNullOrEmpty(ContentJson)
                ? new List<Seccion>()
                : JsonSerializer.Deserialize<List<Seccion>>(ContentJson);
            set => ContentJson = JsonSerializer.Serialize(value);
        }

        public List<Archivo>? Archivos
        {
            get => string.IsNullOrEmpty(ArchivosJson)
                ? new List<Archivo>()
                : JsonSerializer.Deserialize<List<Archivo>>(ArchivosJson);
            set => ArchivosJson = JsonSerializer.Serialize(value);
        }
    }

    // Clases auxiliares
    public class Seccion
    {
        public int Orden { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
    }

    public class Archivo
    {
        public int Orden { get; set; }
        public required string Url { get; set; }
        public required string Tipo { get; set; }
    }
}
