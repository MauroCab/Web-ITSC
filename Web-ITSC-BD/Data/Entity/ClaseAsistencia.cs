using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_ITSC_BD.Data.Entity
{
    [Index(nameof(ClaseId), Name = "AsistenciasDeUnaClaseIDX", IsUnique = false)]
    [Index(nameof(Asistencia), nameof(ClaseId), Name = "FaltaronEstaClaseIDX", IsUnique = false)] //Por si quieren buscar solamente los que NO vinieron (o si) en X clase.
    [Index(nameof(CursadoMateriaId), Name = "AsistenciasDeAlumnoEnTurnoIDX", IsUnique = false)] //Para un alumno en un turno (de una materia) específico. (CursadoMateria es un Alumno en X turno, para que se entienda).
    public class ClaseAsistencia : EntityBase
    {
        [Required(ErrorMessage = "El cursado del alumno en la materia es necesario")]
        public int CursadoMateriaId { get; set; }
        public CursadoMateria CursadoMateria { get; set; }

        [Required(ErrorMessage = "La clase es necesaria")]
        public int ClaseId { get; set; }
        public Clase Clase { get; set; }

        [Required(ErrorMessage = "Poner si el alumno estuvo presente o ausente es necesario")]
        public char Asistencia { get; set; } //A para ausente, P para presente, otra letra para algo más (a decisión del profe).

        [MaxLength(50, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string? Observacion { get; set; } //por cualquier dato que quieran agregar, como justificaciones por ejemplo.
    }
}
