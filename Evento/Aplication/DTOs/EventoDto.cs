using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.DTOs
{
    public class EventoDto
    {
        public Guid Id { get; set; }
        public required string Nombre_Evento { get; set; }
        public required string Tipo { get; set; }
        public DateTime Fecha { get; set; }
        public string? Lugar { get; set; }
        public int Capacidad_Max { get; set; }
        public string? Estado { get; set; } = "Activo";
        public List<SeccionDto>? Content { get; set; }
        public List<ArchivoDto>? Archivos { get; set; }
    }

    public class SeccionDto
    {
        public int Orden { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
    }

    public class ArchivoDto
    {
        public int Orden { get; set; }
        public required string Url { get; set; }
        public required string Tipo { get; set; }
    }
}
