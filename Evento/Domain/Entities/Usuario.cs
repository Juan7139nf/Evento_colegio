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
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required, EmailAddress]
        public string Correo { get; set; }
        [Required]
        public string Contrasenia { get; set; }
        public string? Token {  get; set; }
        public string? Rol { get; set; } = "Usuario";
    }
}
