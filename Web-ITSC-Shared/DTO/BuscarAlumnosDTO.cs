using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_ITSC_Shared.DTO
{
    public class BuscarAlumnoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Documento { get; set; }
        public string TipoDocumento { get; set; }
        public string Telefono { get; set; }
        public string Domicilio { get; set; }
        public string Email { get; set; }
        public bool EstadoUsuario { get; set; }
        public string Sexo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Edad { get; set; }
        public string Cuil { get; set; }
        public string Pais { get; set; }
        public string Provincia { get; set; }
        public string Departamento { get; set; }
        public string Localidad { get; set; }
        public string TituloBase { get; set; }
        public string FotocopiaDNI { get; set; }
        public string ConstanciaCUIL { get; set; }
        public string PartidaNacimiento { get; set; }
        public string Analitico { get; set; }
        public string FotoCarnet { get; set; }
        public string Cus { get; set; }
        public bool EstadoAlumno { get; set; }

        public List<CertificadoAlumnoDTO> Certificados { get; set; } = new List<CertificadoAlumnoDTO>();

        public List<InscripcionesCarrerasDTO> InscripcionesEnCarreras { get; set; } = new List<InscripcionesCarrerasDTO>();

        public List<MateriasCursadasDTO> MateriasQueCursa { get; set; } = new List<MateriasCursadasDTO>();
    }

    public class CertificadoAlumnoDTO
    {
        public int Id { get; set; }
        public DateTime FechaEmision { get; set; }
    }

    public class InscripcionesCarrerasDTO
    {
        public string NombreCarrera { get; set; }
        public string AbreviaturaCarrera { get; set; }
        public int Cohorte { get; set; }
        public string Legajo { get; set; }
        public string LibroMatriz { get; set; }
        public string NumeroDeOrden { get; set; }
        public string EstadoAlumnoEnCarrera { get; set; }
    }
    public class MateriasCursadasDTO
    {
        public string NombreMateria { get; set; }

        public string ResolucionMinisterial { get; set; }
        public DateTime FechaInscripcion { get; set; }

        public int Anno { get; set; }

        public string Formacion { get; set; }

        public string CondicionActual { get; set; }

        public DateTime? VencimientoCondicion { get; set; }
    }
}
