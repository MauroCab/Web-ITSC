using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_ITSC_BD.Data.Entity
{
    public class Usuario : EntityBase
    {
        [Required(ErrorMessage = "La persona que es usuario es necesaria")]
        public int PersonaId { get; set; }
        public Persona Persona { get; set; }

        [Required(ErrorMessage = "El E-mail es necesario")]
        [MaxLength(100, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es necesaria")]
        [MaxLength(30, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Contrasena { get; set; }

        public bool Estado { get; set; } = true; //si está activo o no el usuario, para bloquear o dar acceso al usuario entero
    }
}
