using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_ITSC_BD.Data.Entity
{
    public class Carrera : EntityBase
    {
        [Required(ErrorMessage = "El nombre de la carrera es necesario")]
        [MaxLength(40, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El nombre de la carrera es necesario")]
        [MaxLength(10, ErrorMessage = "Máximo número de caracteres {10}.")]
        public string Abreviatura { get; set; }

        [Required(ErrorMessage = "La duración de la carrera (en años) es necesaria")]
        [MaxLength(20, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string DuracionCarrera { get; set; } //Años que dura la carrera actualmente. La nuestra 3 por ejemplo.


        [Required(ErrorMessage = "El nombre de la carrera es necesario")]
        [MaxLength(20, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Modalidad { get; set; } //Presencial, virtual, virtual-presencial, etc. No sé si hay otras pociones pero qsy

        public List<InscripcionCarrera> InscripcionesCarrera { get; set; } = new List<InscripcionCarrera>();
    }
}
