using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_ITSC_Shared.DTO
{
    public class GetNotaNBTDTO
    {
        public int ValorNota { get; set; }

        public char Asistencia { get; set; }

        public string TipoEvaluacion { get; set; }

        public DateTime FechaEvaluacion { get; set; }

        public string Folio { get; set; }
        public string Libro { get; set; }


        public GetCursadoMateriaNBTDTO CursadoMateria { get; set; } //Materia que se da en el turno

        public string MateriaNombre { get; set; }

        public GetTurnoNBTDTO Turno { get; set; }

    }

    public class GetCursadoMateriaNBTDTO
    {
        public string CondicionActual { get; set; }
        public int Anno { get; set; }
        public GetAlumnoNBTDTO Alumno { get; set; }
    }
    public class GetAlumnoNBTDTO
    {
        public bool Estado { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Documento { get; set; }

        public string TipoDeDocumento { get; set; }
        public string Legajo { get; set; }
    }

    public class GetTurnoNBTDTO
    {
        public string Horario { get; set; }

        public string Sede { get; set; }

        public int AnnoNatural { get; set; }
    }
}
