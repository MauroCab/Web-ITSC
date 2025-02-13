using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_ITSC_BD.Data.Entity
{
    public class Alumno : EntityBase
    {
        [Required(ErrorMessage = "El usuario que es alumno es necesario")]
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        //public Persona Persona { get; set; }

        [Required(ErrorMessage = "El sexo del alumno es necesario")]
        [MaxLength(20, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Sexo { get; set; }


        [Required(ErrorMessage = "La fecha de nacimiento del alumno es necesario")]
        public DateTime FechaNacimiento { get; set; }

        [MaxLength(16, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string? CUIL { get; set; }


        [Required(ErrorMessage = "El país de nacimiento del alumno es necesario")]


        [MaxLength(60, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string? TituloBase { get; set; }

        [MaxLength(16, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string? FotocopiaDNI { get; set; }

        [MaxLength(16, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string? ConstanciaCUIL { get; set; } //esto es para indicar que el alumno trajo o mandó un documento virtual de la constancia de CUIL, no tiene que ver con el atributo "CUIL", el cual es el cuil real.

        [MaxLength(40, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string? PartidaNacimiento { get; set; }

        [MaxLength(40, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string? Analitico { get; set; }

        [MaxLength(40, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string? FotoCarnet { get; set; }

        [MaxLength(40, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string? CUS { get; set; }

        public bool Estado { get; set; } = true; //si está activo o no el alumno, para bloquear o dar acceso
                                                 //al usuario en su calidad de alumno

        [Required(ErrorMessage = "El país de nacimiento del alumno es necesario")]
        public int PaisId { get; set; }
        public Pais Pais { get; set; }

        [Required(ErrorMessage = "La provincia de nacimiento del alumno es necesario")]
        public int ProvinciaId { get; set; }
        public Provincia Provincia { get; set; }

        [Required(ErrorMessage = "El departamento de nacimiento del alumno es necesario")]
        public int DepartamentoId { get; set; }
        public Departamento Departamento { get; set; }

        [Required(ErrorMessage = "la localidad de nacimiento del alumno es necesario")]
        public int LocalidadId { get; set; }
        public Localidad Localidad { get; set; }
        public List<CertificadoAlumno> CertificadosAlumno { get; set; } = new List<CertificadoAlumno>();
        public List<CursadoMateria> MateriasCursadas { get; set; } = new List<CursadoMateria>();
        public List<InscripcionCarrera> InscripcionesCarreras { get; set; } = new List<InscripcionCarrera>();
    }
}
