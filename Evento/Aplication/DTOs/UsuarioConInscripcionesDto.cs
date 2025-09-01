using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.DTOs
{
    public class UsuarioConInscripcionesDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = "";
        public string Apellido { get; set; } = "";
        public string Correo { get; set; } = "";
        public string Rol { get; set; } = "";
        public int Completadas { get; set; }
        public int Pendientes { get; set; }
    }
}
