using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_ITSC_BD.Data.Entity
{
    [Index(nameof(CarreraId), nameof(Anno), Name = "PlanEstudio_UQ", IsUnique = true)]
    [Index(nameof(CarreraId), Name = "PlanesDeUnaCarreraIDX", IsUnique = false)]
    public class PlanEstudio : EntityBase
    {
        [Required(ErrorMessage = "La carrera es necesaria")]
        public int CarreraId { get; set; }
        public Carrera Carrera { get; set; }

        [Required(ErrorMessage = "El nombre del plan es necesario")]
        [MaxLength(20, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string ResolucionMinisterial { get; set; }

        [Required(ErrorMessage = "El año del plan es necesario")]
        public int Anno { get; set; }

        [Required(ErrorMessage = "El estado del plan es necesario")]
        public bool EstadoPlan { get; set; } //  Activo/Inactivo (basicamente, si en algún año de la carrera actualmente se está usando ese plan)

        public List<MateriaEnPlanEstudio> MateriasEnPlanEstudio = new List<MateriaEnPlanEstudio>();
    }
}
