using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Usuario
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string Correo { get; set; }
        public required string Contrasenia { get; set; }
        public string? Rol { get; set; } = "Usuario";
    }
}
