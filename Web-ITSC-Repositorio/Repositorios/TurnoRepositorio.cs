using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_ITSC_BD.Data;
using Web_ITSC_BD.Data.Entity;
using Web_ITSC_Repositorio.Repositorios.Genericos;

namespace Web_ITSC_Repositorio.Repositorios
{
    public class TurnoRepositorio : Repositorio<Turno>, ITurnoRepositorio
    {
        private readonly Context context;
        public TurnoRepositorio(Context context) : base(context)
        {
            this.context = context;
        }
        public async Task<Turno> FullGetById(int id)
        {
            return await context.Turnos
                .Include(u => u.MateriaEnPlanEstudio)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<Turno>> FullGetAll()
        {
            return await context.Turnos
                .Include(u => u.MateriaEnPlanEstudio)
                .ToListAsync();
        }

    }
}
