using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_ITSC_BD.Data.Entity;
using Web_ITSC_BD.Data;
using Web_ITSC_Repositorio.Repositorios.Genericos;
using Web_ITSC_Shared.DTO;
using Microsoft.EntityFrameworkCore;

namespace Web_ITSC_Repositorio.Repositorios
{
    public class MateriaEnPlanEstudioRepositorio : Repositorio<MateriaEnPlanEstudio>, IMateriaEnPlanEstudioRepositorio
    {
        private readonly Context context;

        public MateriaEnPlanEstudioRepositorio(Context context) : base(context)
        {
            this.context = context;
        }

        public async Task<MateriaEnPlanEstudio> FullGetById(int id)
        {
            return await context.MateriasEnPlanEstudio
                .Include(p => p.Materia)
                .Include(p => p.PlanEstudio)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<List<TraerMateriaEnPlanDTO>> FullGetByPlanEstudio(int planEstudioId)
        {
            var res = await context.MateriasEnPlanEstudio
                                .Where(p => p.PlanEstudioId == planEstudioId)
                                .Select(p => new TraerMateriaEnPlanDTO
                                {
                                    // Datos de MateriasEnPlanEstudio
                                    Id = p.Id,
                                    HrsRelojAnuales = p.HrsRelojAnuales,
                                    HrsCatedraSemanales = p.HrsCatedraSemanales,
                                    Anual_Cuatrimestral = p.Anual_Cuatrimestral,
                                    Anno = p.Anno,
                                    NumeroOrden = p.NumeroOrden,

                                    //Datos Adicionales
                                    NombreMateria = p.Materia.Nombre,
                                    AnnoPlanEstudio = p.PlanEstudio.Anno
                                })
                                .ToListAsync();
            return res;
        }
        public async Task<List<MateriaEnPlanEstudio>> FullGetByMateria(int materiaId)
        {
            return await context.MateriasEnPlanEstudio
                .Include(p => p.Materia)
                .Include(p => p.PlanEstudio)
                .Where(p => p.MateriaId == materiaId)
                .ToListAsync();
        }

        public async Task DeleteByPlanEstudioId(int planEstudioId)
        {
            var materiasEnPlan = await context.MateriasEnPlanEstudio
                .Where(p => p.PlanEstudioId == planEstudioId)
                .ToListAsync();

            if (materiasEnPlan.Any())
            {
                context.MateriasEnPlanEstudio.RemoveRange(materiasEnPlan);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteByMateriaId(int materiaId)
        {
            var materiasEnPlan = await context.MateriasEnPlanEstudio
                .Where(p => p.MateriaId == materiaId)
                .ToListAsync();

            if (materiasEnPlan.Any())
            {
                context.MateriasEnPlanEstudio.RemoveRange(materiasEnPlan);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<MateriaEnPlanEstudio>> FullGetAll()
        {
            return await context.MateriasEnPlanEstudio
                .Include(p => p.Materia)
                .Include(p => p.PlanEstudio)
                .ToListAsync();
        }


        /*
         public async Task<bool> Update(int id, E entidad)
        {
            if (id != entidad.Id)
            {
                return false;
            }
            var EntidadExistente = await SelectById(id);

            if (EntidadExistente == null)
            {
                return false;
            }

            try
            {
                context.Entry(EntidadExistente).CurrentValues.SetValues(entidad);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
         */
    }
}
