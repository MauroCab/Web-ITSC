using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_ITSC_BD.Data.Entity
{
    [Index(nameof(EvaluacionId), Name = "NotasDeEvaluacionIDX", IsUnique = false)]
    [Index(nameof(CursadoMateriaId), Name = "NotasDeAlumnoEnMateriaIDX", IsUnique = false)]
    public class Nota : EntityBase
    {

        [Required(ErrorMessage = "El cursado de la materia del alumno es necesario")]
        public int CursadoMateriaId { get; set; }
        public CursadoMateria CursadoMateria { get; set; }


        [Required(ErrorMessage = "La evaluación es necesaria")]
        public int EvaluacionId { get; set; }
        public Evaluacion Evaluacion { get; set; }

        [Required(ErrorMessage = "El valor de la nota es necesario")]
        public int ValorNota { get; set; }

        [Required(ErrorMessage = "Poner si el alumno estuvo presente o ausente es necesario")]
        public char Asistencia { get; set; } //Diferente a la asistencia de la clase, no relacionada (por consejo de Algorry)
                                             //pero es la misma funcion obviamente
    }
}
