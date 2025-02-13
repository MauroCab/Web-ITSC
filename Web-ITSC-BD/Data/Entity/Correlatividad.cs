using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_ITSC_BD.Data.Entity
{
    public class Correlatividad : EntityBase
    {
        [Required(ErrorMessage = "La materia es necesaria")]
        public int MateriaEnPlanEstudioId { get; set; }
        public MateriaEnPlanEstudio MateriaEnPlanEstudio { get; set; } //La materia que vamos a usar de referencia. Tomemos de ejemplo: Programación II

        [Required(ErrorMessage = "El ID de la materia correlativa es necesario")]
        public int MateriaCorrelativaId { get; set; }
        public MateriaEnPlanEstudio MateriaCorrelativa { get; set; } //La materia que está bloqueando a la materia de referencia. Siguiendo el ejemplo anterior: Programación I.

        //Si hubiese muchas materias bloqueando una sola materia (que sí hay), la idea es crear tantas entidades de esta clase como materias estén bloqueando la Materia de referencia. 
        //Repetirías la 'MateriaEnPlanEstudio', y cambiarías la 'MateriaCorrelativa' hasta haber cubierto todas las materias que bloquean a esa 'MateriaEnPlanEstudio'.
    }
}
