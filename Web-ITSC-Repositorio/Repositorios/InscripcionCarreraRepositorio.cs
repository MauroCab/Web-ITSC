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
    public class InscripcionCarreraRepositorio : Repositorio<InscripcionCarrera>, IInscripcionCarreraRepositorio
    {
        private readonly Context context;

        public InscripcionCarreraRepositorio(Context context) : base(context)
        {
            this.context = context;
        }

        public async Task<InscripcionCarrera> FullGetById(int id)
        {
            return await context.InscripcionesCarrera
                .Include(p => p.Alumno)
                .Include(p => p.Carrera)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<InscripcionCarrera>> FullGetAll()
        {
            return await context.InscripcionesCarrera
                .Include(p => p.Alumno)
                .Include(p => p.Carrera)
                .ToListAsync();
        }
        public async Task<InscripcionCarrera> GetInscripcionByAlumnoYCarrera(int alumnoId, int carreraId)
        {
            return await context.InscripcionesCarrera
                .Where(i => i.AlumnoId == alumnoId && i.CarreraId == carreraId)
                .FirstOrDefaultAsync();
        }
        public async Task<List<InscripcionCarrera>> GetInscripcionesPorCarreraYcohorteOpcional(int carreraId, int? cohorte = null)
        {
            // Filtrar por CarreraId, y si Cohorte es proporcionado, también filtrar por Cohorte
            var query = context.InscripcionesCarrera
                .Where(i => i.CarreraId == carreraId);

            // Si se proporciona el Cohorte, lo filtramos también
            if (cohorte.HasValue)
            {
                query = query.Where(i => i.Cohorte == cohorte.Value);
            }

            return await query
                .Include(i => i.Alumno)  // Incluir los detalles del Alumno
                .Include(i => i.Carrera) // Incluir los detalles de la Carrera
                .ToListAsync();
        }
        public async Task<List<InscripcionCarrera>> ObtenerInscripcionesPorCarreraYcohorte(int carreraId, int? cohorte)
        {
            var query = context.InscripcionesCarrera
                .Include(i => i.Alumno)                   // Incluir el Alumno
                .ThenInclude(a => a.Usuario)              // Incluir el Usuario relacionado con el Alumno
                .ThenInclude(u => u.Persona)              // Incluir la Persona relacionada con el Usuario
                 .Where(i => i.CarreraId == carreraId);   // Filtrar por CarreraId

            if (cohorte.HasValue)
            {
                query = query.Where(i => i.Cohorte == cohorte.Value);  // Filtrar por Cohorte si está presente
            }

            return await query.ToListAsync();
        }
    }
}
