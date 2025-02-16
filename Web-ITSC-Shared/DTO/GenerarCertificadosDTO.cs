using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_ITSC_BD.Data.Entity;

namespace Web_ITSC_Shared.DTO
{
    public class GetDatosCertificadosDTO
    {
        public string ApellidoyNombre { get; set; }
        public string TipoDocumentoCertificado { get; set; }

        public string NroDocumento { get; set; }
        public DateOnly FechadeNacimiento { get; set; }
        public string LugarNacimiento { get; set; }
        public string NroTelefono { get; set; }
        public string TituloHabilitante { get; set; }
        public string Legajo { get; set; }

        public string LibroMatriz { get; set; }
        public string Folio { get; set; }
        public List<FilasMateriaDTO> FilasTabla { get; set; }

        public DateOnly Fecha { get; set; }

    }


    public class FilasMateriaDTO
    {
        public string Asignatura { get; set; }
        public int ValorNota { get; set; }
        public string NotaLetra { get; set; }
        public string Libro { get; set; }
        public string Folio { get; set; }
        public int Dia { get; set; }
        public int Mes { get; set; }
        public int Anno { get; set; }

        public string CondicionActual { get; set; }

        public string Sede { get; set; }

    }

    public class CrearCertificadoAlumnoDTO
    {
        public CrearCertificadoAlumnoDTO(Alumno alumnoConCertificado, DateTime hoy)
        {
            AlumnoId = alumnoConCertificado.Id;
            Alumno = alumnoConCertificado;
            FechaEmision = hoy;
        }

        [Required(ErrorMessage = "El alumno es necesario")]
        public int AlumnoId { get; set; }
        public Alumno Alumno { get; set; }

        [Required(ErrorMessage = "La fecha de emisión del certificado es necesaria")]
        public DateTime FechaEmision { get; set; }
    }

}
