using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_ITSC_Shared.DTO
{
    public class CrearAlumnoDTO
    {
        public int CarreraId { get; set; }  // Carrera en la que se va a inscribir
                                            // Datos de Persona
        [Required(ErrorMessage = "El nombre de la persona es necesario")]
        [MaxLength(80, ErrorMessage = "Máximo número de caracteres {100}.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido de la persona es necesario")]
        [MaxLength(80, ErrorMessage = "Máximo número de caracteres {100}.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El número de documento de la persona es necesario")]
        [MaxLength(16, ErrorMessage = "Máximo número de caracteres {100}.")]
        public string Documento { get; set; }

        [Required(ErrorMessage = "El tipo de documento es necesario")]
        public int TipoDocumentoId { get; set; }

        [Required(ErrorMessage = "El número de telefono de la persona es necesario")]
        [MaxLength(16, ErrorMessage = "Máximo número de caracteres {100}.")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El Domicilio de la persona es necesario")]
        [MaxLength(16, ErrorMessage = "Máximo número de caracteres {100}.")]
        public string Domicilio { get; set; }



        // Datos de Usuario
        [Required(ErrorMessage = "El email de la persona es necesario")]
        [MaxLength(80, ErrorMessage = "Máximo número de caracteres {100}.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El nombre de usuario de la persona es necesario")]
        [MaxLength(80, ErrorMessage = "Máximo número de caracteres {100}.")]
        public string Contrasena { get; set; } = "DefaultPassword123";

        // Datos específicos del Alumno
        [Required(ErrorMessage = "El sexo del alumno es necesario")]
        [MaxLength(20, ErrorMessage = "Máximo número de caracteres {100}.")]
        public string Sexo { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento del alumno es necesario")]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "La edad del alumno es necesario")]
        public int Edad { get; set; }

        [MaxLength(16, ErrorMessage = "Máximo número de caracteres {100}.")]
        public string? CUIL { get; set; }

        [Required(ErrorMessage = "El país de nacimiento del alumno es necesario")]
        public int PaisId { get; set; }

        [Required(ErrorMessage = "La provincia de nacimiento del alumno es necesario")]
        public int ProvinciaId { get; set; }

        [Required(ErrorMessage = "El departamento de nacimiento del alumno es necesario")]
        public int DepartamentoId { get; set; }

        [Required(ErrorMessage = "la localidad de nacimiento del alumno es necesario")]
        public int LocalidadId { get; set; }

        // Otros campos opcionales
        [MaxLength(60, ErrorMessage = "Máximo número de caracteres {100}.")]
        public string? TituloBase { get; set; }

        [MaxLength(16, ErrorMessage = "Máximo número de caracteres {100}.")]
        public string? FotocopiaDNI { get; set; }

        [MaxLength(16, ErrorMessage = "Máximo número de caracteres {100}.")]
        public string? ConstanciaCUIL { get; set; }

        [MaxLength(40, ErrorMessage = "Máximo número de caracteres {100}.")]
        public string? PartidaNacimiento { get; set; }

        [MaxLength(40, ErrorMessage = "Máximo número de caracteres {100}.")]
        public string? Analitico { get; set; }

        [MaxLength(40, ErrorMessage = "Máximo número de caracteres {100}.")]
        public string? FotoCarnet { get; set; }

        [MaxLength(40, ErrorMessage = "Máximo número de caracteres {100}.")]
        public string? CUS { get; set; }

        //public string Cohorte { get; set; } /*= DateTime.Now.Year.ToString();*/

    }

    public class EditarAlumnoDTO : CrearAlumnoDTO
    {
        public int Id { get; set; }

    }

    public class GetAlumnoDTO
    {
        public int Id { get; set; }
        //public int CarreraId { get; set; }  // Carrera en la que se va a inscribir
        //public string NombrePersona { get; set; }
        //public string ApellidoPersona { get; set; }
        //public string DocumentoPersona { get; set; }
        //public string? TelefonoPersona { get; set; }
        //public string DomicilioPersona { get; set; }

        public int CarreraId { get; set; }  // Carrera en la que se va a inscribir
        public string NameCarrera { get; set; }
        public string AbreviaturaCarrera { get; set; }


        // Datos de Persona
        [Required(ErrorMessage = "El nombre de la persona es necesario")]
        [MaxLength(80, ErrorMessage = "Máximo número de caracteres {100}.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido de la persona es necesario")]
        [MaxLength(80, ErrorMessage = "Máximo número de caracteres {100}.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El número de documento de la persona es necesario")]
        [MaxLength(16, ErrorMessage = "Máximo número de caracteres {100}.")]
        public string Documento { get; set; }


        [Required(ErrorMessage = "El número de telefono de la persona es necesario")]
        [MaxLength(16, ErrorMessage = "Máximo número de caracteres {100}.")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El Domicilio de la persona es necesario")]
        [MaxLength(16, ErrorMessage = "Máximo número de caracteres {100}.")]
        public string Domicilio { get; set; }



        public string Sexo { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento del alumno es necesario")]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "La edad del alumno es necesario")]
        public int Edad { get; set; }

        [MaxLength(16, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string? CUIL { get; set; }

        [Required(ErrorMessage = "El país de nacimiento del alumno es necesario")]
        [MaxLength(30, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Pais { get; set; }

        [MaxLength(40, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string? Provincia { get; set; }

        [MaxLength(40, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string? Departamento { get; set; }

        [MaxLength(40, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string? Localidad { get; set; }

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

        public bool Estado { get; set; } = true;

        //_persona_____________________________________________________________________ 
        //public string PersonaId { get; set; }

        //_cohorte_____________________________________________________________________
        public int Cohorte { get; set; }
        public List<InscripcionesCarrerasDTO> InscripcionesEnCarreras { get; set; } = new List<InscripcionesCarrerasDTO>();

    }
}
