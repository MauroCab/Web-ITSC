using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_ITSC_BD.Data.Entity
{
    public class CertificadoAlumno : EntityBase
    {
        [Required(ErrorMessage = "El alumno es necesario")]
        public int AlumnoId { get; set; }
        public Alumno Alumno { get; set; }

        [Required(ErrorMessage = "La fecha de emisión del certificado es necesaria")]
        public DateTime FechaEmision { get; set; }
    }
}
