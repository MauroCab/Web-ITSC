using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_ITSC_Shared.DTO
{
    public class CrearPaisDTO
    {
        [Required(ErrorMessage = "El nombre del pais es necesario")]
        public string Nombre { get; set; }

    }

    public class GetPaisDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre del pais es necesario")]
        public string Nombre { get; set; }
    }
    public class CrearProvinciaDTO
    {
        public string Nombre { get; set; }
        public int PaisId { get; set; }
    }
    public class GetProvinciaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

    public class CrearDepartamentosDTO
    {
        [Required(ErrorMessage = "El nombre del departamento es necesario")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La provincia del departamento es necesaria")]
        public int ProvinciaId { get; set; }
    }
    public class GetDepartamentosDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del departamento es necesario")]
        public string Nombre { get; set; }


    }

    public class CrearLocalidadesDTO
    {
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El departamento de la localidad es necesario")]
        public int DepartamentoId { get; set; }
    }
    public class GetLocalidadesDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

    }
}
