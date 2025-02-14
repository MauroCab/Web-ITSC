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
    public class EvaluacionRepositorio : Repositorio<Evaluacion>, IEvaluacionRepositorio
    {
        private readonly Context context;
        public EvaluacionRepositorio(Context context) : base(context)
        {
            this.context = context;
        }
        public async Task<Evaluacion> FullGetById(int id)
        {
            return await context.Evaluaciones
                .Include(u => u.Turno)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<Evaluacion>> FullGetAll()
        {
            return await context.Evaluaciones
                .Include(u => u.Turno)
                .ToListAsync();
        }

    }
}
