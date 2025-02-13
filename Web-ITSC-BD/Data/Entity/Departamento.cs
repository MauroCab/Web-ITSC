using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_ITSC_BD.Data.Entity
{
    public class Departamento : EntityBase
    {
        [Required(ErrorMessage = "El nombre del departamento es necesario")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La provincia del departamento es necesaria")]
        public int ProvinciaId { get; set; }
        public Provincia Provincia { get; set; }

        public List<Localidad> Localidades { get; set; } = new List<Localidad>();
    }
}
