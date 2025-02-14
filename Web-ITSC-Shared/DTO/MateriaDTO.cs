using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_ITSC_Shared.DTO
{
    public class CrearMateriaDTO
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

        [Required(ErrorMessage = "La resolución ministerial de la materia es necesaria")]
        [MaxLength(36, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string ResolucionMinisterial { get; set; }

        [Required(ErrorMessage = "El año de la materia es necesario")]
        public int Anno { get; set; }
    }

    public class GetMateriaDTO
    {
        [Required(ErrorMessage = "El nombre de la materia es necesario")]
        [MaxLength(36, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El año de la materia es necesario")]
        public int Anno { get; set; }
    }

    public class PutNombreMateriaDTO
    {
        [Required(ErrorMessage = "El nombre de la materia es necesario")]
        [MaxLength(36, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Nombre { get; set; }
    }
}
