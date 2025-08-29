using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.DTOs
{
    public class LoginDto
    {
        public required string Correo { get; set; }
        public required string Contrasenia { get; set; }
    }
}
