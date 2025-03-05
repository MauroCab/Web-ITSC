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
                var document = new Document(pdf);

                
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
                    tablaDatos.AddCell(GetCell("Apellido y Nombre:", verdanaFont, 5.5f, true));
                    tablaDatos.AddCell(GetCell($"{datos.ApellidoyNombre}", verdanaFont, 5.5f));
                    tablaDatos.AddCell(GetCell("Legajo:", verdanaFont, 5.5f, true));
                    tablaDatos.AddCell(GetCell($"{datos.Legajo}", verdanaFont, 5.5f));

                    tablaDatos.AddCell(GetCell("Tipo Doc.:", verdanaFont, 5.5f, true));
                    tablaDatos.AddCell(GetCell($"{datos.TipoDocumentoCertificado}", verdanaFont, 5.5f));
                    tablaDatos.AddCell(GetCell("Nro. Doc.:", verdanaFont, 5.5f, true));
                    tablaDatos.AddCell(GetCell($"{datos.NroDocumento}", verdanaFont, 5.5f));

                    tablaDatos.AddCell(GetCell("Fecha de Nacimiento:", verdanaFont, 5.5f, true));
                    tablaDatos.AddCell(GetCell($"{datos.FechadeNacimiento}", verdanaFont, 5.5f));
                    tablaDatos.AddCell(GetCell("Libro Matriz:", verdanaFont, 5.5f, true));
                    tablaDatos.AddCell(GetCell($"{datos.LibroMatriz}", verdanaFont, 5.5f));

                    tablaDatos.AddCell(GetCell("Lugar de Nacimiento:", verdanaFont, 5.5f, true));
                    tablaDatos.AddCell(GetCell($"{datos.LugarNacimiento}", verdanaFont, 5.5f));
                    tablaDatos.AddCell(GetCell("Folio:", verdanaFont, 5.5f, true));
                    tablaDatos.AddCell(GetCell($"{datos.Folio}", verdanaFont, 5.5f));

                    tablaDatos.AddCell(GetCell("Teléfono:", verdanaFont, 5.5f, true));
                    tablaDatos.AddCell(GetCell($"{datos.NroTelefono}", verdanaFont, 5.5f));
                    tablaDatos.AddCell(new Cell(1, 2).SetBorder(Border.NO_BORDER)); // Celda vacía sin bordes

                    tablaDatos.AddCell(GetCell("Título Habilitante:", verdanaFont, 5.5f, true));
                    tablaDatos.AddCell(GetCell($"{datos.TituloHabilitante}", verdanaFont, 5.5f));
                    tablaDatos.AddCell(new Cell(1, 2).SetBorder(Border.NO_BORDER)); // Celda vacía sin bordes


                    #endregion

                    // Agrega la tabla de datos del alumno al PDF
                    document.Add(tablaDatos);

                    document.Add(new Paragraph("Certifico que el/la estudiante mencionado/a cursa en carácter de REGULAR, la Tecnicatura Superior en Desarrollo de Software (Res 462/12)  y ha aprobado los espacios curriculares que se detallan a continuación:")
                                .SetFont(tahomaFont)
                                .SetFontSize(5)
                                .SetTextAlignment(TextAlignment.CENTER));

                    // Tabla de materias
                    Table tablaNotas = new Table(10).UseAllAvailableWidth();

                    #region Tablas Encabezado
                    tablaNotas.AddHeaderCell(new Cell(1, 9).Add(new Paragraph("CALIFICACIONES")));
                    tablaNotas.AddHeaderCell(new Cell(1, 1).Add(new Paragraph("Establecimiento")));

                    tablaNotas.AddHeaderCell(new Cell(2, 1).Add(new Paragraph("ASIGNATURAS").SimulateBold()));
                    tablaNotas.AddHeaderCell(new Cell(1, 2).Add(new Paragraph("Nota").SimulateBold()));
                    tablaNotas.AddHeaderCell(new Cell(1, 2).Add(new Paragraph("Acta").SimulateBold()));
                    tablaNotas.AddHeaderCell(new Cell(1, 3).Add(new Paragraph("Aprobado el:").SimulateBold()));
                    tablaNotas.AddHeaderCell(new Cell(1, 1).Add(new Paragraph("Condicion")));
                    tablaNotas.AddHeaderCell(new Cell(1, 1).Add(new Paragraph("Sede").SimulateBold()));

                    tablaNotas.AddHeaderCell(new Cell().Add(new Paragraph("Número")));
                    tablaNotas.AddHeaderCell(new Cell().Add(new Paragraph("Letras")));
                    tablaNotas.AddHeaderCell(new Cell().Add(new Paragraph("Libro")));
                    tablaNotas.AddHeaderCell(new Cell().Add(new Paragraph("Folio")));
                    tablaNotas.AddHeaderCell(new Cell().Add(new Paragraph("Día")));
                    tablaNotas.AddHeaderCell(new Cell().Add(new Paragraph("Mes")));
                    tablaNotas.AddHeaderCell(new Cell().Add(new Paragraph("Año")));
                    tablaNotas.AddHeaderCell(new Cell().Add(new Paragraph("Reg/Prom/Libre")));
                    tablaNotas.AddHeaderCell(new Cell().Add(new Paragraph("CENTRAL/NORTE")));
                    #endregion

                    // Filas de la tabla de las condicion de cada materia
                    foreach (var fila in datos.FilasTabla)
                    {
                        tablaNotas.AddCell(GetCell(fila.Asignatura, verdanaFont, 6.5f));
                        tablaNotas.AddCell(GetCell(fila.ValorNota.ToString(), verdanaFont, 6.5f));
                        tablaNotas.AddCell(GetCell(fila.NotaLetra, verdanaFont, 6.5f));
                        tablaNotas.AddCell(GetCell(fila.Libro, verdanaFont, 6.5f));
                        tablaNotas.AddCell(GetCell(fila.Folio, verdanaFont, 6.5f));
                        tablaNotas.AddCell(GetCell($"{fila.Dia}", verdanaFont, 6.5f));
                        tablaNotas.AddCell(GetCell($"{fila.Mes}", verdanaFont, 6.5f));
                        tablaNotas.AddCell(GetCell($"{fila.Anno}", verdanaFont, 6.5f));
                        tablaNotas.AddCell(GetCell(fila.CondicionActual, verdanaFont, 6.5f));
                        tablaNotas.AddCell(GetCell(fila.Sede, verdanaFont, 6.5f));

                        //tablaNotas.AddCell(new Cell().Add(new Paragraph(fila.Asignatura)));
                        //tablaNotas.AddCell(new Cell().Add(new Paragraph(fila.ValorNota.ToString())));
                        //tablaNotas.AddCell(new Cell().Add(new Paragraph(fila.NotaLetra)));
                        //tablaNotas.AddCell(new Cell().Add(new Paragraph(fila.Libro)));
                        //tablaNotas.AddCell(new Cell().Add(new Paragraph(fila.Folio)));
                        //tablaNotas.AddCell(new Cell().Add(new Paragraph($"{fila.Dia}")));
                        //tablaNotas.AddCell(new Cell().Add(new Paragraph($"{fila.Mes}")));
                        //tablaNotas.AddCell(new Cell().Add(new Paragraph($"{fila.Anno}")));
                        //tablaNotas.AddCell(new Cell().Add(new Paragraph(fila.CondicionActual)));
                        //tablaNotas.AddCell(new Cell().Add(new Paragraph(fila.Sede)));
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

        static Cell GetCell(string text, PdfFont fuente, float size, bool bold = false)
        {
            Cell cell = new Cell().Add(new Paragraph(text)
                                .SetFont(fuente)
                                .SetFontSize(size));
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
