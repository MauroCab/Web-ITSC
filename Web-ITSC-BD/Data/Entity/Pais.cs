using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_ITSC_BD.Data.Entity
{
    public class Pais : EntityBase
    {
        [Required(ErrorMessage = "El nombre del pais es necesario")]
        public string Nombre { get; set; }

        public List<Provincia> Provincias { get; set; } = new List<Provincia>();
    }
}
