using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_ITSC_Shared.DTO
{
    public class CrearCarreraDTO
    {
        [Required(ErrorMessage = "El nombre de la carrera es necesario")]
        [MaxLength(40, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El nombre de la carrera es necesario")]
        [MaxLength(10, ErrorMessage = "Máximo número de caracteres {10}.")]
        public string Abreviatura { get; set; }

        [Required(ErrorMessage = "La duración de la carrera (en años) es necesaria")]
        [MaxLength(20, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string DuracionCarrera { get; set; }

        [Required(ErrorMessage = "El nombre de la carrera es necesario")]
        [MaxLength(20, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Modalidad { get; set; }

        // public List<InscripcionCarrera> InscripcionesCarrera { get; set; } = new List<InscripcionCarrera>();
    }

    public class GetCarreraDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la carrera es necesario")]
        [MaxLength(40, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El nombre de la carrera es necesario")]
        [MaxLength(10, ErrorMessage = "Máximo número de caracteres {10}.")]
        public string Abreviatura { get; set; }

        [Required(ErrorMessage = "La duración de la carrera (en años) es necesaria")]
        [MaxLength(20, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string DuracionCarrera { get; set; }

        [Required(ErrorMessage = "El nombre de la carrera es necesario")]
        [MaxLength(20, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Modalidad { get; set; }
    }

    public class CarreraSearchDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
