using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_ITSC_BD.Data.Entity
{
    public class Provincia : EntityBase
    {
        [Required(ErrorMessage = "El nombre de la provincia es necesario")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El pais de la provincia es necesario")]
        public int PaisId { get; set; }
        public Pais Pais { get; set; }

        public List<Departamento> Departamentos { get; set; } = new List<Departamento>();
    }
}
