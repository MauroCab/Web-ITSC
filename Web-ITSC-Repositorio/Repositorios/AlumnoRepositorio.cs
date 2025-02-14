using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_ITSC_BD.Data;
using Web_ITSC_BD.Data.Entity;
using Web_ITSC_Repositorio.Repositorios.Genericos;
using Web_ITSC_Shared.DTO;

namespace Web_ITSC_Repositorio.Repositorios
{
    public class AlumnoRepositorio : Repositorio<Alumno>, IAlumnoRepositorio
    {
        private readonly Context context;

        public AlumnoRepositorio(Context context) : base(context)
        {
            this.context = context;
        }

        public async Task<List<Alumno>> FullGetAll()
        {
            var alumnos = await context.Alumnos
                    .Include(a => a.Usuario)                       // Incluye la relación Usuario
                    .ThenInclude(u => u.Persona)                   // Incluye la relación Persona dentro de Usuario
                    .Include(a => a.InscripcionesCarreras)         // Incluye las inscripciones de carreras
                    .ThenInclude(ic => ic.Carrera)                 //  la carrera asociada a cada inscripción
                            .ToListAsync();

            // Verifica si los datos de Persona están siendo cargados correctamente
            foreach (var alumno in alumnos)
            {
                Console.WriteLine($"Alumno: {alumno.Usuario.Persona.Nombre} {alumno.Usuario.Persona.Apellido}, " +
                                  $"Telefono: {alumno.Usuario.Persona.Telefono}, Domicilio: {alumno.Usuario.Persona.Domicilio}");
            }

            return alumnos;
        }


        public async Task<Alumno> FullGetById(int id)
        {
            return await context.Alumnos
                .Include(a => a.Usuario)
                .ThenInclude(u => u.Persona)         // Asegúrate de incluir Persona
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        //________________________________________________
        public async Task<ActionResult<IEnumerable<BuscarAlumnoDTO>>> BuscarAlumnos(string? nombre, string? apellido, string? documento, int? cohorte)
        {
            var query = context.Alumnos.Include(a => a.Usuario).AsQueryable();

            if (!string.IsNullOrWhiteSpace(nombre) ||
                !string.IsNullOrWhiteSpace(apellido) ||
                !string.IsNullOrWhiteSpace(documento) ||
                cohorte.HasValue)
            {
                if (!string.IsNullOrWhiteSpace(nombre))
                {
                    query = query.Where(a => a.Usuario.Persona.Nombre.Contains(nombre));
                }

                if (!string.IsNullOrWhiteSpace(apellido))
                {
                    query = query.Where(a => a.Usuario.Persona.Apellido.Contains(apellido));
                }

                if (!string.IsNullOrWhiteSpace(documento))
                {
                    query = query.Where(a => a.Usuario.Persona.Documento.Contains(documento));
                }

                if (cohorte.HasValue)
                {
                    query = query.Where(a => a.InscripcionesCarreras.Any(ic => ic.Cohorte == cohorte));
                }
            }

            var resultados = await query.Select(a => new BuscarAlumnoDTO
            {
                Nombre = a.Usuario.Persona.Nombre,
                Apellido = a.Usuario.Persona.Apellido,
                Documento = a.Usuario.Persona.Documento,
                TipoDocumento = a.Usuario.Persona.TipoDocumento.Nombre,
                Email = a.Usuario.Email,
                EstadoUsuario = a.Usuario.Estado,
                Sexo = a.Sexo,
                FechaNacimiento = a.FechaNacimiento,
                Edad = CalcularEdad(a.FechaNacimiento),
                Cuil = a.CUIL,
                Pais = a.Pais.Nombre,
                Provincia = a.Provincia.Nombre,
                Departamento = a.Departamento.Nombre,
                Localidad = a.Localidad.Nombre,
                TituloBase = a.TituloBase,
                FotocopiaDNI = a.FotocopiaDNI,
                ConstanciaCUIL = a.ConstanciaCUIL,
                PartidaNacimiento = a.PartidaNacimiento,
                Analitico = a.Analitico,
                FotoCarnet = a.FotoCarnet,
                Cus = a.CUS,
                EstadoAlumno = a.Estado,
                Telefono = a.Usuario.Persona.Telefono,
                Domicilio = a.Usuario.Persona.Domicilio,
                Certificados = a.CertificadosAlumno.Select(ca => new CertificadoAlumnoDTO
                {
                    Id = ca.Id,
                    FechaEmision = ca.FechaEmision
                }).ToList(),
                InscripcionesEnCarreras = a.InscripcionesCarreras.Select(ic => new InscripcionesCarrerasDTO
                {
                    AbreviaturaCarrera = ic.Carrera.Abreviatura,
                    Cohorte = ic.Cohorte,
                    Legajo = ic.Legajo,
                    LibroMatriz = ic.LibroMatriz,
                    NumeroDeOrden = ic.NroOrdenAlumno,
                    EstadoAlumnoEnCarrera = ic.EstadoAlumno

                }).ToList(),
                MateriasQueCursa = a.MateriasCursadas.Select(mqc => new MateriasCursadasDTO
                {
                    NombreMateria = mqc.Turno.MateriaEnPlanEstudio.Materia.Nombre,
                    ResolucionMinisterial = mqc.Turno.MateriaEnPlanEstudio.PlanEstudio.ResolucionMinisterial,
                    FechaInscripcion = mqc.FechaInscripcion,
                    Anno = mqc.Turno.MateriaEnPlanEstudio.Materia.Anno,
                    Formacion = mqc.Turno.MateriaEnPlanEstudio.Materia.Formacion,
                    CondicionActual = mqc.CondicionActual,
                    VencimientoCondicion = mqc.VencimientoCondicion
                }).ToList()

            }).ToListAsync();
            return resultados;

        }


        //---validar Cuil---
        public async Task<Alumno> GetAlumnoPorCUIL(string cuil)
        {
            return await context.Alumnos
                .Include(a => a.Usuario)
                .Include(a => a.Usuario.Persona)
                .FirstOrDefaultAsync(a => a.CUIL == cuil);
        }


        //-----------editar alumno------------------------

        public async Task<Alumno> GetAlumnoPorDocumento(string documento)
        {
            return await context.Alumnos
                .Include(a => a.Usuario)
                .Include(a => a.Usuario.Persona)
                .FirstOrDefaultAsync(a => a.Usuario.Persona.Documento == documento);
        }


        public async Task<bool> Update(Alumno alumno)
        {
            // Actualizar la entidad Alumno y sus entidades relacionadas
            context.Alumnos.Update(alumno);
            context.Usuarios.Update(alumno.Usuario);
            context.Personas.Update(alumno.Usuario.Persona);

            return await context.SaveChangesAsync() > 0;
        }


        //---------------------------------------------------

        public async Task<bool> EliminarAlumno(int alumnoId)
        {
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Eliminar inscripciones en carreras relacionadas con el alumno
                    var inscripciones = await context.InscripcionesCarrera
                        .Where(i => i.AlumnoId == alumnoId)
                        .ToListAsync();
                    if (inscripciones.Any())
                    {
                        context.InscripcionesCarrera.RemoveRange(inscripciones);
                        await context.SaveChangesAsync();
                    }

                    // Eliminar el alumno
                    var alumno = await context.Alumnos
                        .Where(a => a.Id == alumnoId)
                        .FirstOrDefaultAsync();
                    if (alumno != null)
                    {
                        context.Alumnos.Remove(alumno);
                        await context.SaveChangesAsync();
                    }

                    // Eliminar el usuario asociado al alumno
                    var usuario = await context.Usuarios
                        .Where(u => u.Id == alumno.UsuarioId)
                        .FirstOrDefaultAsync();
                    if (usuario != null)
                    {
                        context.Usuarios.Remove(usuario);
                        await context.SaveChangesAsync();
                    }

                    // Eliminar la persona asociada al usuario
                    var persona = await context.Personas
                        .Where(p => p.Id == usuario.PersonaId)
                        .FirstOrDefaultAsync();
                    if (persona != null)
                    {
                        context.Personas.Remove(persona);
                        await context.SaveChangesAsync();
                    }

                    await transaction.CommitAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Console.WriteLine($"Error: {ex.Message}");
                    return false;
                }
            }
        }


        private static int CalcularEdad(DateTime fechaCumple)
        {
            int edad = DateTime.Now.Year - fechaCumple.Year;
            if (DateTime.Now.DayOfYear < fechaCumple.DayOfYear)
                edad--;
            return edad;
        }
    }
}
