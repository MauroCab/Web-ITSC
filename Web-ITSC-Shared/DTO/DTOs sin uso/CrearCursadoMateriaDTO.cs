using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_ITSC_Shared.DTO.DTOs_sin_uso
{
    public class CrearCursadoMateriaDTO
    {
        [Required(ErrorMessage = "El alumno es necesario")]
        public int AlumnoId { get; set; }

        [Required(ErrorMessage = "El turno es necesario")]
        public int TurnoId { get; set; }
        public Turno Turno { get; set; }

        [Required(ErrorMessage = "La fecha de inscripción en la materia es necesario")]
        public DateTime FechaInscripcion { get; set; }

        [Required(ErrorMessage = "El año de cursado de la materia es necesario")]
        public int Anno { get; set; }  //Año de cursado actualmente por el alumno

        [Required(ErrorMessage = "El nombre es necesario")]
        [MaxLength(50, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string CondicionActual { get; set; } //libre, regular, promocionado

        public DateTime? VencimientoCondicion { get; set; }

        public List<Nota>? Notas { get; set; } = new List<Nota>();
        public List<ClaseAsistencia>? ClaseAsistencias { get; set; } = new List<ClaseAsistencia>();
    }
}
