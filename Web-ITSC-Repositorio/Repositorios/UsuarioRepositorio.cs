using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_ITSC_BD.Data.Entity;
using Web_ITSC_BD.Data;
using Web_ITSC_Repositorio.Repositorios.Genericos;
using Microsoft.EntityFrameworkCore;

namespace Web_ITSC_Repositorio.Repositorios
{
    public class UsuarioRepositorio : Repositorio<Usuario>, IUsuarioRepositorio
    {
        private readonly Context context;
        public UsuarioRepositorio(Context context) : base(context)
        {
            this.context = context;
        }
        public async Task<Usuario> FullGetById(int id)
        {
            return await context.Usuarios
                .Include(u => u.Persona)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<Usuario>> FullGetAll()
        {
            return await context.Usuarios
                .Include(u => u.Persona)
                .ToListAsync();
        }

    }
}
