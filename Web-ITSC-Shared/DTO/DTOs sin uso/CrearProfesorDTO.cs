using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_ITSC_BD.Data.Entity;

namespace Web_ITSC_Shared.DTO.DTOs_sin_uso
{
    public class CrearProfesorDTO
    {
        [Required(ErrorMessage = "El usuario que será profesor es necesario")]
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public bool Estado { get; set; } = true; //si está activo o no el profesor, para bloquear o dar acceso
                                                 //al usuario en su calidad de profesor
    }
}
