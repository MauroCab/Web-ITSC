using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_ITSC_Shared.DTO
{
    public class CrearPersonaDTO
    {
        [Required(ErrorMessage = "El nombre es necesario")]
        [MaxLength(80, ErrorMessage = "Máximo número de caracteres {100}.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es necesario")]
        [MaxLength(80, ErrorMessage = "Máximo número de caracteres {100}.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El Número de Documento es necesario")]
        [MaxLength(16, ErrorMessage = "Máximo número de caracteres {100}.")]
        public string Documento { get; set; }

        [Required(ErrorMessage = "El tipo de documento es necesario")]
        public int TipoDocumentoId { get; set; }

        [MaxLength(18, ErrorMessage = "Máximo número de caracteres {100}.")]
        public string Telefono { get; set; }

        [MaxLength(60, ErrorMessage = "Máximo número de caracteres {100}.")]
        public string Domicilio { get; set; }
    }

    public class GetPersonaDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es necesario")]
        [MaxLength(80, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es necesario")]
        [MaxLength(80, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El Número de Documento es necesario")]
        [MaxLength(16, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Documento { get; set; }

        [Required(ErrorMessage = "El Número de Documento es necesario")]
        [MaxLength(16, ErrorMessage = "Máximo número de caracteres {1}.")]
        public int Cohorte { get; set; }

    }
}
