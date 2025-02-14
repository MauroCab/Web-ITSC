using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_ITSC_Shared.DTO
{
    public class CrearUsuarioDTO
    {
        [Required(ErrorMessage = "El E-mail es necesario")]
        [MaxLength(100, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es necesaria")]
        [MaxLength(30, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Contrasena { get; set; }

        //public bool Estado { get; set; } = true; //si está activo o no el usuario, para bloquear o dar acceso al usuario entero
    }

    public class GetUsuarioDTO
    {
        public int Id { get; set; }

        //[Required(ErrorMessage = "La persona que es usuario es necesaria")]
        //public int PersonaId { get; set; }

        [Required(ErrorMessage = "El nombre es necesario")]
        [MaxLength(80, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string NombrePersona { get; set; }

        [Required(ErrorMessage = "El apellido es necesario")]
        [MaxLength(80, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string ApellidoPersona { get; set; }

        [Required(ErrorMessage = "El Número de Documento es necesario")]
        [MaxLength(16, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string DocumentoPersona { get; set; }


        [Required(ErrorMessage = "El E-mail es necesario")]
        [MaxLength(100, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es necesaria")]
        [MaxLength(30, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Contrasena { get; set; }

    }
}
