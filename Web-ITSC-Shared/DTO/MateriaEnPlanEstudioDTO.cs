using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_ITSC_BD.Data.Entity;

namespace Web_ITSC_Shared.DTO
{
    public class CrearMateriaEnPlanEstudioDTO
    {
        [Required(ErrorMessage = "La materia es necesario")]
        public int MateriaId { get; set; }

        [Required(ErrorMessage = "El plan de estudio es necesario")]
        public int PlanEstudioId { get; set; }
        public PlanEstudio PlanEstudio { get; set; }

        [Required(ErrorMessage = "Las horas de reloj anuales son necesarias")]
        public int HrsRelojAnuales { get; set; }

        [Required(ErrorMessage = "Las horas de cátedra semanales son necesarias")]
        public int HrsCatedraSemanales { get; set; }

        [Required(ErrorMessage = "Saber si la materia es anual o cuatrimestral es necesario")]
        [MaxLength(20, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Anual_Cuatrimestral { get; set; } //Se coloca anual si la materia es anual y cuatrimestral si es cuatrimestral

        public int Anno { get; set; } //Año de la carrera en el que está la materia. Ejemplo: en nuestro plan,
                                      //para programación II, año 2 (segundo año); para Base de Datos, año 1 (primer año),
                                      //Estadística y Probabilidad, año 2 (segundo año)... y así.
        public int? NumeroOrden { get; set; }
    }

    public class TraerMateriaEnPlanDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Las horas de reloj anuales son necesarias")]
        public int HrsRelojAnuales { get; set; }


        [Required(ErrorMessage = "Las horas de cátedra semanales son necesarias")]
        public int HrsCatedraSemanales { get; set; }

        [Required(ErrorMessage = "Saber si la materia es anual o cuatrimestral es necesario")]
        [MaxLength(20, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Anual_Cuatrimestral { get; set; }

        [Required(ErrorMessage = "Saber el año en que se cursa la materia es necesario")]
        public int Anno { get; set; }
        public int? NumeroOrden { get; set; }

        [Required(ErrorMessage = "Nombre de la materia asociada es necesario")]
        public string NombreMateria { get; set; }

        [Required(ErrorMessage = "Id de la materia asociada es necesario")]
        public int MateriaId;

        [Required(ErrorMessage = "Id del plan de estudio asociado es necesario")]
        public int PlanEstudioId;
        public int AnnoPlanEstudio { get; set; }
    }
    public class MateriaEnPlanPutDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Las horas de reloj anuales son necesarias")]
        public int HrsRelojAnuales { get; set; }

        [Required(ErrorMessage = "Las horas de cátedra semanales son necesarias")]
        public int HrsCatedraSemanales { get; set; }

        [Required(ErrorMessage = "Saber si la materia es anual o cuatrimestral es necesario")]
        [MaxLength(20, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Anual_Cuatrimestral { get; set; }
        public int Anno { get; set; }
        public int? NumeroOrden { get; set; }
    }
    public class MateriaEnPlanSearchDTO
    {
        public string NombreCarrera { get; set; }
        public int? Anno { get; set; }
    }
}
