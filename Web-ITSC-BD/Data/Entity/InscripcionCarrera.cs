using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_ITSC_BD.Data.Entity
{
    [Index(nameof(AlumnoId), nameof(CarreraId), nameof(Cohorte), Name = "InscripcionCarrera_UQ", IsUnique = true)]
    [Index(nameof(CarreraId), nameof(Cohorte), Name = "CohorteIDX", IsUnique = false)]
    public class InscripcionCarrera : EntityBase
    {
        public InscripcionCarrera Inscripcion_Carrera;

        [Required(ErrorMessage = "El alumno es necesario")]
        public int AlumnoId { get; set; }
        public Alumno Alumno { get; set; }

        [Required(ErrorMessage = "La carrera es necesario")]
        public int CarreraId { get; set; }
        public Carrera Carrera { get; set; }

        [Required(ErrorMessage = "El ID de la carrera es necesario")]
        public int Cohorte { get; set; } //Año de Ingreso

        [Required(ErrorMessage = "El legajo es necesario")]
        [MaxLength(12, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Legajo { get; set; } //Cohorte + Codigo de Carrera + no se que (no en ese orden). Consultar Excel de Gabi si no sabés.

        [Required(ErrorMessage = "El Estado del alumno es necesario")]
        [MaxLength(20, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string EstadoAlumno { get; set; } //Activo/Inactivo

        [Required(ErrorMessage = "El libro matriz del alumno es necesario")]
        [MaxLength(20, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string LibroMatriz { get; set; } //Consultar Excel de Gabi

        [Required(ErrorMessage = "El número de orden del alumno es necesario")]
        [MaxLength(20, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string NroOrdenAlumno { get; set; } //Consultar Excel de Gabi
    }
}
