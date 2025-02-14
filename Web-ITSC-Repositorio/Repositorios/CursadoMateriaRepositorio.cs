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
    public class CursadoMateriaRepositorio : Repositorio<CursadoMateria>, ICursadoMateriaRepositorio
    {
        private readonly Context context;
        public CursadoMateriaRepositorio(Context context) : base(context)
        {
            this.context = context;
        }
        public async Task<CursadoMateria> FullGetById(int id)
        {
            return await context.CursadosMateria
                .Include(u => u.Alumno)
                .Include(u => u.Turno)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<CursadoMateria>> FullGetAll()
        {
            return await context.CursadosMateria
                .Include(u => u.Alumno)
                .Include(u => u.Turno)
                .ToListAsync();
        }

    }
}
