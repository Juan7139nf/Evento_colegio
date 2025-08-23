using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class UsuarioRepository: IUsuarioRepository
    {
        private readonly BdContext _context;
        public UsuarioRepository(BdContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usuario>> ObtenerList()
        {
            return await _context.usuarios.ToListAsync();
        }

        public async Task<Usuario?> ObtenerId(Guid id)
        {
            return await _context.usuarios.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task Crear(Usuario usuario)
        {
            usuario.Id = Guid.NewGuid();
            _context.usuarios.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Actualizar(Usuario usuario)
        {
            var usuarioExiste = await _context.usuarios.FirstOrDefaultAsync(u => u.Id == usuario.Id);
            if (usuarioExiste == null)
            {
                return false;
            }

            usuarioExiste.Nombre = usuario.Nombre;
            usuarioExiste.Apellido = usuario.Apellido;
            usuarioExiste.Correo = usuario.Correo;
            usuarioExiste.Contrasenia = usuario.Contrasenia;
            usuarioExiste.Rol = usuario.Rol;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
