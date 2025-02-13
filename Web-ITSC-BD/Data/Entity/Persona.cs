using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_ITSC_BD.Data.Entity
{
    [Index(nameof(Documento), nameof(TipoDocumentoId), Name = "PersonaUnica_UQ", IsUnique = true)]
    [Index(nameof(Nombre), nameof(Apellido), Name = "Nombre-ApellidoIDX", IsUnique = false)]
    [Index(nameof(Apellido), nameof(Nombre), Name = "Apellido-NombreIDX", IsUnique = false)]
    public class Persona : EntityBase
    {
        [Required(ErrorMessage = "El nombre es necesario")]
        [MaxLength(80, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es necesario")]
        [MaxLength(80, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El Número de Documento es necesario")]
        [MaxLength(16, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Documento { get; set; }

        [Required(ErrorMessage = "El tipo de documento es necesario")]
        public int TipoDocumentoId { get; set; }
        public TipoDocumento TipoDocumento { get; set; }

        [MaxLength(18, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Telefono { get; set; }

        [MaxLength(60, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Domicilio { get; set; }

        //para el mapper
        public InscripcionCarrera inscripcion_Carrera;
    }
}
