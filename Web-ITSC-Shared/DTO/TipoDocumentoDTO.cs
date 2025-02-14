using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_ITSC_Shared.DTO
{
    public class CrearTipoDocumentoDTO
    {
        [Required(ErrorMessage = "El nombre es necesario")]
        [MaxLength(70, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El codigo es necesario")]
        [MaxLength(6, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Codigo { get; set; }
    }

    public class GetTipoDocumentoDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es necesario")]
        [MaxLength(70, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El codigo es necesario")]
        [MaxLength(6, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Codigo { get; set; }
    }
}
