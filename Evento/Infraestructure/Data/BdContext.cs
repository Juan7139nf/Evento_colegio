using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Data
{
    public class BdContext:DbContext
    {
        public BdContext(DbContextOptions<BdContext> options): base(options) { }
        public DbSet<Usuario> usuarios { get; set; }
        public DbSet<Evento> eventos { get; set; }
        public DbSet<Inscripcion> inscripciones { get; set; }
        public DbSet<Pago> pagos { get; set; }
        public DbSet<Encuesta> encuestas { get; set; }
        public DbSet<Notificacion> notificaciones { get; set; }
        public DbSet<Reporte> reportes { get; set; }
    }
}
