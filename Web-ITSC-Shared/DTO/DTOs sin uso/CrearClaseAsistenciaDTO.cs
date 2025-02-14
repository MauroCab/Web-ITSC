using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_ITSC_BD.Data.Entity;

namespace Web_ITSC_Shared.DTO.DTOs_sin_uso
{
    public class CrearClaseAsistenciaDTO
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
