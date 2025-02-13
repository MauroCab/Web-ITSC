using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_ITSC_BD.Data.Entity
{
    [Index(nameof(TurnoId), nameof(TipoEvaluacion), Name = "EvaluacionesTipoIDX", IsUnique = false)]
    [Index(nameof(TurnoId), nameof(Fecha), Name = "EvaluacionesEnFechaIDX", IsUnique = false)]
    [Index(nameof(TurnoId), Name = "EvaluacionesDeUnTurnoIDX", IsUnique = false)]
    public class Evaluacion : EntityBase
    {
        [Required(ErrorMessage = "El turno es necesario")]
        public int TurnoId { get; set; }
        public Turno Turno { get; set; }

        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El tipo de la evaluacion es necesario")]
        [MaxLength(36, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string TipoEvaluacion { get; set; } //Parcial, Final, IEFI, recuperatorio, etc.

        [MaxLength(20, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string? Folio { get; set; } //

        [MaxLength(20, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string? Libro { get; set; } //

        [MaxLength(30, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string? Sede { get; set; }

        public List<Nota>? Notas { get; set; } = new List<Nota>();
    }
}
