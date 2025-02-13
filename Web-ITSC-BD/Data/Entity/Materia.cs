using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_ITSC_BD.Data.Entity
{
    [Index(nameof(Nombre), Name = "MateriasPorNombreIDX", IsUnique = false)]
    public class Materia : EntityBase
    {
        [Required(ErrorMessage = "El nombre de la materia es necesario")]
        [MaxLength(36, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El formato de la materia es necesario")]
        [MaxLength(20, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Formato { get; set; }

        [Required(ErrorMessage = "La formación de la materia es necesaria")]
        [MaxLength(20, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Formacion { get; set; }

        [Required(ErrorMessage = "El año de la materia es necesario")]
        public int Anno { get; set; } //Año donde "se crea la materia", usualmente corresponde con el año del Plan de Estudio en donde aparece por primera vez.
                                      //El año en el que se cursa (1ero, 2do, 3ro, etc...) va estar en 'MateriaEnPlanEstudio.
    }
}
