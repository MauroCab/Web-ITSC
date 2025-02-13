using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_ITSC_BD.Data.Entity;

namespace Web_ITSC_BD.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Carrera> Carreras { get; set; }
        public DbSet<CertificadoAlumno> CertificadosAlumno { get; set; }
        public DbSet<Clase> Clases { get; set; }
        public DbSet<ClaseAsistencia> ClaseAsistencias { get; set; }
        public DbSet<Correlatividad> Correlatividades { get; set; }
        public DbSet<CursadoMateria> CursadosMateria { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Evaluacion> Evaluaciones { get; set; }
        public DbSet<InscripcionCarrera> InscripcionesCarrera { get; set; }
        public DbSet<Localidad> Localidades { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<MateriaEnPlanEstudio> MateriasEnPlanEstudio { get; set; }
        public DbSet<Nota> Notas { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<PlanEstudio> PlanesEstudio { get; set; }
        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<Provincia> Provincias { get; set; }
        public DbSet<TipoDocumento> TiposDocumento { get; set; }
        public DbSet<Turno> Turnos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                                     .SelectMany(t => t.GetForeignKeys())
                                     .Where(fk => !fk.IsOwnership &&
                                     fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }

    }
}
