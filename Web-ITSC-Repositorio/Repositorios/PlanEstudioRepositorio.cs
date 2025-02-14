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
    public class PlanEstudioRepositorio : Repositorio<PlanEstudio>, IPlanEstudioRepositorio
    {
        private readonly Context context;
        public PlanEstudioRepositorio(Context context) : base(context)
        {
            this.context = context;
        }
        public async Task<PlanEstudio> FullGetById(int id)
        {
            return await context.PlanesEstudio
                .Include(u => u.Carrera)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<PlanEstudio>> FullGetAll()
        {
            return await context.PlanesEstudio
                .Include(u => u.Carrera)
                .ToListAsync();
        }

        public async Task<int> GetIdByCarreraAnno(int carreraId, int anno)
        {
            var a = await context.PlanesEstudio
                .Include(u => u.Carrera)
                .Where(u => u.CarreraId == carreraId && u.Anno == anno)
                .FirstOrDefaultAsync();

            if (a != null)
            {
                return a.Id;
            }
            else
            {
                return 0;
            }
        }

        public async Task<PlanEstudio> GetByCarreraAnno(int carreraId, int anno)
        {

            try
            {
                return await context.PlanesEstudio
                        .Where(u => u.CarreraId == carreraId && u.Anno == anno)
                        .FirstOrDefaultAsync();
            }

            catch (Exception err)
            {
                throw err;
            }

        }
    }
}
