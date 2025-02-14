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
    public class NotaRepositorio : Repositorio<Nota>, INotaRepositorio
    {
        private readonly Context context;

        public NotaRepositorio(Context context) : base(context)
        {
            this.context = context;
        }

        public async Task<Nota> FullGetById(int id)
        {
            return await context.Notas
                .Include(n => n.CursadoMateria)
                .Include(n => n.Evaluacion)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<Nota>> FullGetAll()
        {
            return await context.Notas
                .Include(n => n.CursadoMateria)
                .Include(n => n.Evaluacion)
                .ToListAsync();
        }

        public async Task<List<GetNotaNBTDTO>> SelectNotasByTurno(int turnoId)
        {
            // Verificamos que exista el turno
            var turno = await context.Turnos
                .Include(t => t.MateriaEnPlanEstudio) // Incluimos la relación de materias
                .AnyAsync(t => t.Id == turnoId);

            if (turno != true)
            {
                return null;
            }

            // Consulta para obtener los datos relevantes de las notas
            var notas = await context.Notas
                .Include(n => n.CursadoMateria)
                .Include(n => n.Evaluacion)
                .Where(n => n.CursadoMateria.TurnoId == turnoId && n.Evaluacion.TipoEvaluacion == "Final")
                .Select(n => new GetNotaNBTDTO
                {
                    ValorNota = n.ValorNota,
                    Asistencia = n.Asistencia,
                    TipoEvaluacion = n.Evaluacion.TipoEvaluacion,
                    FechaEvaluacion = n.Evaluacion.Fecha,
                    Folio = n.Evaluacion.Folio,
                    Libro = n.Evaluacion.Libro,
                    CursadoMateria = n.CursadoMateria == null ? null : new GetCursadoMateriaNBTDTO
                    {
                        CondicionActual = n.CursadoMateria.CondicionActual,
                        Anno = n.CursadoMateria.Anno,
                        Alumno = n.CursadoMateria.Alumno == null ? null : new GetAlumnoNBTDTO
                        {
                            Estado = n.CursadoMateria.Alumno.Estado,
                            Nombre = n.CursadoMateria.Alumno.Usuario.Persona.Nombre,
                            Apellido = n.CursadoMateria.Alumno.Usuario.Persona.Apellido,
                            Documento = n.CursadoMateria.Alumno.Usuario.Persona.Documento,
                            TipoDeDocumento = n.CursadoMateria.Alumno.Usuario.Persona.TipoDocumento.Codigo,
                            Legajo = context.InscripcionesCarrera
                            .Where(ic => ic.AlumnoId == n.CursadoMateria.AlumnoId)
                            .Select(ic => ic.Legajo)
                            .FirstOrDefault()
                        }
                    },
                    MateriaNombre = n.CursadoMateria.Turno.MateriaEnPlanEstudio.Materia.Nombre,
                    Turno = n.CursadoMateria.Turno == null ? null : new GetTurnoNBTDTO
                    {
                        Horario = n.CursadoMateria.Turno.Horario,
                        Sede = n.CursadoMateria.Turno.Sede,
                        AnnoNatural = n.CursadoMateria.Turno.AnnoCicloLectivo
                    }
                })
                .ToListAsync();


            return notas;
        }
    }
}
