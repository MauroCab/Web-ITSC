using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_ITSC_BD.Data.Entity;

namespace Web_ITSC_Shared.DTO.DTOs_sin_uso
{
    public class CrearCorrelatividadDTO
    {
        [Required(ErrorMessage = "La materia es necesaria")]
        public int MateriaEnPlanEstudioId { get; set; }
        public MateriaEnPlanEstudio MateriaEnPlanEstudio { get; set; }

        [Required(ErrorMessage = "El ID de la materia correlativa es necesario")]
        public int MateriaCorrelativaId { get; set; }
        public MateriaEnPlanEstudio MateriaCorrelativa { get; set; }
    }
}
