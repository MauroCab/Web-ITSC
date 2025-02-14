using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_ITSC_Shared.DTO
{
    public class GetDatosCertificadosDTO
    {
        public string ApellidoyNombre { get; set; }
        public string TipoDocumentoCertificado { get; set; }

        public string NroDocumento { get; set; }
        public DateOnly FechadeNacimiento { get; set; }
        public string LugarNacimiento { get; set; }
        public string NroTelefono { get; set; }
        public string TituloHabilitante { get; set; }
        public string Legajo { get; set; }

        public string LibroMatriz { get; set; }
        public string Folio { get; set; }
        public List<FilasMateriaDTO> FilasTabla { get; set; }

        public DateOnly Fecha { get; set; }

    }


    public class FilasMateriaDTO
    {
        public string Asignatura { get; set; }
        public int ValorNota { get; set; }
        public string NotaLetra { get; set; }
        public string Libro { get; set; }
        public string Folio { get; set; }
        public int Dia { get; set; }
        public int Mes { get; set; }
        public int Anno { get; set; }

        public string CondicionActual { get; set; }

        public string Sede { get; set; }

    }
}
