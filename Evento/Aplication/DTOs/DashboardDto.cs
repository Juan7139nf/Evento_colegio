using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.DTOs
{
    public class DashboardDto
    {
        public int TotalUsuarios { get; set; }
        public int TotalEventos { get; set; }
        public int TotalInscripciones { get; set; }
        public int TotalPagos { get; set; }
        public int TotalEncuestas { get; set; }
        public Dictionary<string, int> Inscripciones { get; set; } = new();
    }
}
