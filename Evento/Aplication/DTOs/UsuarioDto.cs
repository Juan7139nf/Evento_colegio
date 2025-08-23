using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.DTOs
{
    public class UsuarioDto
    {
        public Guid Id { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string Correo { get; set; }
        public required string Contrasenia { get; set; }
        public string? Rol { get; set; } = "Usuario";
    }
}
