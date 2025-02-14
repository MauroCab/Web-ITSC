using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_ITSC_BD.Data.Entity;

namespace Web_ITSC_Shared.DTO.DTOs_sin_uso
{
    public class CrearClaseDTO
    {
        [Required(ErrorMessage = "El turno es necesario")]
        public int TurnoId { get; set; }
        public Turno Turno { get; set; }

        [Required(ErrorMessage = "La fecha de clase es necesaria")]
        public DateTime Fecha { get; set; }
    }

}
