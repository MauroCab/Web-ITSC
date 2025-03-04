using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Bouncycastleconnector;
using iText.Layout.Properties;
using iText.Kernel.Colors;
using iText.Layout.Borders;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_ITSC_BD.Data.Entity;
using Web_ITSC_BD.Data;
using Web_ITSC_Repositorio.Repositorios.Genericos;
using Web_ITSC_Shared.DTO;
using Microsoft.EntityFrameworkCore;
using iText.Layout.Properties;
using iText.Kernel.Font;

namespace Web_ITSC_Repositorio.Repositorios
{
    public class CertificadoAlumnoRepositorio : Repositorio<CertificadoAlumno>, ICertificadoAlumnoRepositorio
    {
        private readonly Context context;
        public CertificadoAlumnoRepositorio(Context context) : base(context)
        {
            this.context = context;
        }

        public async Task<ActionResult<GetDatosCertificadosDTO>> SelectDatosCertificado(string documento)
        {
            var query = context.Alumnos.Where(a => a.Usuario.Persona.Documento.Contains(documento));

            var datos = await query.Select(a => new GetDatosCertificadosDTO
            {
                ApellidoyNombre = a.Usuario.Persona.Apellido + " " + a.Usuario.Persona.Nombre,
                TipoDocumentoCertificado = a.Usuario.Persona.TipoDocumento.Nombre,
                NroDocumento = a.Usuario.Persona.Documento,
                FechadeNacimiento = DateOnly.FromDateTime(a.FechaNacimiento),
                LugarNacimiento = a.Departamento.Nombre + " " + a.Provincia.Nombre + " " + a.Pais.Nombre,
                NroTelefono = a.Usuario.Persona.Telefono,
                TituloHabilitante = a.TituloBase,
                Legajo = context.InscripcionesCarrera
                            .Where(ic => ic.Alumno.Id == a.Id)
                            .Select(ic => ic.Legajo)
                            .FirstOrDefault(),
                LibroMatriz = context.InscripcionesCarrera
                            .Where(ic => ic.Alumno.Id == a.Id)
                            .Select(ic => ic.LibroMatriz)
                            .FirstOrDefault(),
                Folio = context.Notas
                            .Where(n => n.CursadoMateria.Alumno.Id == a.Id && n.Evaluacion.TipoEvaluacion == "Final")
                            .Select(n => n.Evaluacion.Folio)
                            .FirstOrDefault(),
                FilasTabla = context.CursadosMateria
                            .Where(cm => cm.AlumnoId == a.Id)
                            .Select(cm => new FilasMateriaDTO
                            {
                                Asignatura = cm.Turno.MateriaEnPlanEstudio.Materia.Nombre,
                                ValorNota = context.Notas
                                            .Where(n => n.CursadoMateriaId == cm.Id && n.Evaluacion.TipoEvaluacion == "Final")
                                            .Select(n => n.ValorNota)
                                            .FirstOrDefault(), // Aquí filtro por cada materia específica
                                NotaLetra = NumeroANombre(context.Notas
                                            .Where(n => n.CursadoMateriaId == cm.Id && n.Evaluacion.TipoEvaluacion == "Final")
                                            .Select(n => n.ValorNota)
                                            .FirstOrDefault()),
                                Libro = context.Notas
                                        .Where(n => n.CursadoMateriaId == cm.Id && n.Evaluacion.TipoEvaluacion == "Final")
                                        .Select(n => n.Evaluacion.Libro)
                                        .FirstOrDefault(),
                                Folio = context.Notas
                                        .Where(n => n.CursadoMateriaId == cm.Id && n.Evaluacion.TipoEvaluacion == "Final")
                                        .Select(n => n.Evaluacion.Folio)
                                        .FirstOrDefault(),
                                Dia = context.Notas
                                        .Where(n => n.CursadoMateriaId == cm.Id && n.Evaluacion.TipoEvaluacion == "Final")
                                        .Select(n => n.Evaluacion.Fecha.Day)
                                        .FirstOrDefault(),
                                Mes = context.Notas
                                        .Where(n => n.CursadoMateriaId == cm.Id && n.Evaluacion.TipoEvaluacion == "Final")
                                        .Select(n => n.Evaluacion.Fecha.Month)
                                        .FirstOrDefault(),
                                Anno = context.Notas
                                        .Where(n => n.CursadoMateriaId == cm.Id && n.Evaluacion.TipoEvaluacion == "Final")
                                        .Select(n => n.Evaluacion.Fecha.Year)
                                        .FirstOrDefault(),
                                CondicionActual = cm.CondicionActual,
                                Sede = cm.Turno.Sede
                            }).ToList(),
                Fecha = DateOnly.FromDateTime(DateTime.Now)
            }).FirstOrDefaultAsync();

            return datos;

        }

        #region Fuentes Personalizadas
        // Ruta a las fuentes en Windows
        static string fontPathTahoma = @"C:\Windows\Fonts\tahoma.ttf";
        static string fontPathTimesNewRoman = @"C:\Windows\Fonts\times.ttf";
        static string fontPathVerdana = @"C:\Windows\Fonts\verdana.ttf";
        static string fontPathArialMT = @"C:\Windows\Fonts\arial.ttf"; // Arial MT es Arial en Windows

        // Cargar fuentes personalizadas
        static PdfFont tahomaFont = PdfFontFactory.CreateFont(fontPathTahoma, iText.IO.Font.PdfEncodings.WINANSI, PdfFontFactory.EmbeddingStrategy.FORCE_EMBEDDED);
        static PdfFont timesNewRomanFont = PdfFontFactory.CreateFont(fontPathTimesNewRoman, iText.IO.Font.PdfEncodings.WINANSI, PdfFontFactory.EmbeddingStrategy.FORCE_EMBEDDED);
        static PdfFont verdanaFont = PdfFontFactory.CreateFont(fontPathVerdana, iText.IO.Font.PdfEncodings.WINANSI, PdfFontFactory.EmbeddingStrategy.FORCE_EMBEDDED);
        static PdfFont arialMTFont = PdfFontFactory.CreateFont(fontPathArialMT, iText.IO.Font.PdfEncodings.WINANSI, PdfFontFactory.EmbeddingStrategy.FORCE_EMBEDDED);

        #endregion
        public byte[] GenerarCertificadoPDF(GetDatosCertificadosDTO datos)
        {
            using (var stream = new MemoryStream())
            {
                var writer = new PdfWriter(stream);
                var pdf = new PdfDocument(writer);
                var document = new iText.Layout.Document(pdf);

                
                // Título
                document.Add(new Paragraph("REPUBLICA ARGENTINA")
                                .SetFont(tahomaFont)
                                .SetFontSize(6.5f)
                                .SimulateBold()
                                .SetTextAlignment(TextAlignment.CENTER));

                document.Add(new Paragraph("LEY DE EDUCACION NACIONAL Nº 26.206")
                            .SetFont(tahomaFont)
                            .SetFontSize(6.5f)
                            .SetTextAlignment(TextAlignment.CENTER));

                document.Add(new Paragraph("MINISTERIO DE EDUCACION DE LA PROVINCIA DE CORDOBA")
                                .SetFont(tahomaFont)
                                .SetFontSize(6.5f)
                                .SimulateBold()
                                .SetTextAlignment(TextAlignment.CENTER));

                document.Add(new Paragraph("LEY DE EDUCACION PROVINCIAL N.º 8113")
                            .SetFont(tahomaFont)
                            .SetFontSize(6.5f)
                            .SetTextAlignment(TextAlignment.CENTER));

                document.Add(new Paragraph("DIRECCION GENERAL DE EDUCACION TÉCNICA Y FORMACIÓN PROFESIONAL")
                                .SetFont(tahomaFont)
                                .SetFontSize(6.5f)
                                .SimulateBold()
                                .SetTextAlignment(TextAlignment.CENTER));

                document.Add(new Paragraph("Instituto Técnico Superior Córdoba")
                            .SetFont(timesNewRomanFont)
                            .SetFontSize(15.5f)
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetFontColor(ColorConstants.CYAN));

                document.Add(new Paragraph("Tecnicatura Superior en Desarrollo de Software")
                            .SetFont(timesNewRomanFont)
                            .SetFontSize(15.5f)
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetFontColor(ColorConstants.RED)); 

                document.Add(new Paragraph("CERTIFICADO MATERIAS APROBADAS - ESTUDIANTE REGULAR")
                                .SetFont(verdanaFont)
                                .SetFontSize(7)
                                .SimulateBold()
                                .SetTextAlignment(TextAlignment.CENTER));


                // Datos del alumno
                if (datos != null)
                {
                    #region Tabla Datos Alumno
                    Table tablaDatos = new Table(4).UseAllAvailableWidth();

                    // Agregar filas con datos
                    tablaDatos.AddCell(GetCell("Apellido y Nombre:", true));
                    tablaDatos.AddCell(GetCell($"{datos.ApellidoyNombre}"));
                    tablaDatos.AddCell(GetCell("Legajo:", true));
                    tablaDatos.AddCell(GetCell($"{datos.Legajo}"));

                    tablaDatos.AddCell(GetCell("Tipo Doc.:", true));
                    tablaDatos.AddCell(GetCell($"{datos.TipoDocumentoCertificado}"));
                    tablaDatos.AddCell(GetCell("Nro. Doc.:", true));
                    tablaDatos.AddCell(GetCell($"{datos.NroDocumento}"));

                    tablaDatos.AddCell(GetCell("Fecha de Nacimiento:", true));
                    tablaDatos.AddCell(GetCell($"{datos.FechadeNacimiento}"));
                    tablaDatos.AddCell(GetCell("Libro Matriz:", true));
                    tablaDatos.AddCell(GetCell($"{datos.LibroMatriz}"));

                    tablaDatos.AddCell(GetCell("Lugar de Nacimiento:", true));
                    tablaDatos.AddCell(GetCell($"{datos.LugarNacimiento}"));
                    tablaDatos.AddCell(GetCell("Folio:", true));
                    tablaDatos.AddCell(GetCell($"{datos.Folio}"));

                    tablaDatos.AddCell(GetCell("Teléfono:", true));
                    tablaDatos.AddCell(GetCell($"{datos.NroTelefono}"));
                    tablaDatos.AddCell(new Cell(1, 2).SetBorder(Border.NO_BORDER)); // Celda vacía sin bordes

                    tablaDatos.AddCell(GetCell("Título Habilitante:", true));
                    tablaDatos.AddCell(GetCell($"{datos.TituloHabilitante}"));
                    tablaDatos.AddCell(new Cell(1, 2).SetBorder(Border.NO_BORDER)); // Celda vacía sin bordes


                    #endregion

                    // Agrega la tabla de datos del alumno al PDF
                    document.Add(tablaDatos);

                    document.Add(new Paragraph("Certifico que el/la estudiante mencionado/a cursa en carácter de REGULAR, la Tecnicatura Superior en Desarrollo de Software (Res 462/12)  y ha aprobado los espacios curriculares que se detallan a continuación:")
                                .SetFont(tahomaFont)
                                .SetFontSize(5)
                                .SetTextAlignment(TextAlignment.CENTER));

                    // Tabla de materias
                    iText.Layout.Element.Table tablaNotas = new iText.Layout.Element.Table(8).UseAllAvailableWidth(); // 8 columnas para la tabla de materias
                    tablaNotas.AddHeaderCell(new Cell().Add(new Paragraph("Asignatura").SimulateBold()));
                    tablaNotas.AddHeaderCell(new Cell().Add(new Paragraph("Valor Nota").SimulateBold()));
                    tablaNotas.AddHeaderCell(new Cell().Add(new Paragraph("Nota Letra").SimulateBold()));
                    tablaNotas.AddHeaderCell(new Cell().Add(new Paragraph("Libro").SimulateBold()));
                    tablaNotas.AddHeaderCell(new Cell().Add(new Paragraph("Folio").SimulateBold()));
                    tablaNotas.AddHeaderCell(new Cell().Add(new Paragraph("Fecha").SimulateBold()));
                    tablaNotas.AddHeaderCell(new Cell().Add(new Paragraph("Condición Actual").SimulateBold()));
                    tablaNotas.AddHeaderCell(new Cell().Add(new Paragraph("Sede").SimulateBold()));

                    // Filas de la tabla de las condicion de cada materia
                    foreach (var fila in datos.FilasTabla)
                    {
                        tablaNotas.AddCell(new Cell().Add(new Paragraph(fila.Asignatura)));
                        tablaNotas.AddCell(new Cell().Add(new Paragraph(fila.ValorNota.ToString())));
                        tablaNotas.AddCell(new Cell().Add(new Paragraph(fila.NotaLetra)));
                        tablaNotas.AddCell(new Cell().Add(new Paragraph(fila.Libro)));
                        tablaNotas.AddCell(new Cell().Add(new Paragraph(fila.Folio)));
                        tablaNotas.AddCell(new Cell().Add(new Paragraph($"{fila.Dia}/{fila.Mes}/{fila.Anno}")));
                        tablaNotas.AddCell(new Cell().Add(new Paragraph(fila.CondicionActual)));
                        tablaNotas.AddCell(new Cell().Add(new Paragraph(fila.Sede)));
                    }

                    document.Add(tablaNotas);

                    document.Close();
                    return stream.ToArray();
                }
                else
                {
                    return Array.Empty<byte>();
                }
            }
        }

        public async Task<ActionResult<Alumno>> SelectAlumnoByDoc(string documento)
        {
            Alumno sel = await context.Alumnos.FirstOrDefaultAsync(x => x.Usuario.Persona.Documento == documento);
            return sel;
        }

        static Cell GetCell(string text, bool bold = false)
        {
            Cell cell = new Cell().Add(new Paragraph(text)
                                .SetFont(verdanaFont)
                                .SetFontSize(5.5f));
            if (bold) cell.SimulateBold();

            cell.SetBorder(Border.NO_BORDER);
            return cell;
        }

        private static string NumeroANombre(int numero)
        {
            switch (numero)
            {
                case 0: return "Cero";
                case 1: return "Uno";
                case 2: return "Dos";
                case 3: return "Tres";
                case 4: return "Cuatro";
                case 5: return "Cinco";
                case 6: return "Seis";
                case 7: return "Siete";
                case 8: return "Ocho";
                case 9: return "Nueve";
                case 10: return "Diez";

                default: return "-";
            }
        }

    }
}
