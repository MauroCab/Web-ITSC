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
using iText.Kernel.Pdf.Canvas.Draw;
using iText.IO.Image;

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


        public byte[] GenerarCertificadoPDF(GetDatosCertificadosDTO datos)
        {
            using (var stream = new MemoryStream())
            {
                var writer = new PdfWriter(stream);
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf);

                #region Fuentes e imágenes Personalizadas
                // Ruta a las fuentes en Windows
                string fontPathTahoma = @"C:\Windows\Fonts\tahoma.ttf";
                string fontPathTimesNewRoman = @"C:\Windows\Fonts\times.ttf";
                string fontPathVerdana = @"C:\Windows\Fonts\verdana.ttf";
                string fontPathArialMT = @"C:\Windows\Fonts\arial.ttf"; // Arial MT es Arial en Windows

                // Cargar fuentes personalizadas
                PdfFont tahomaFont = PdfFontFactory.CreateFont(fontPathTahoma, iText.IO.Font.PdfEncodings.WINANSI, PdfFontFactory.EmbeddingStrategy.FORCE_EMBEDDED);
                PdfFont timesNewRomanFont = PdfFontFactory.CreateFont(fontPathTimesNewRoman, iText.IO.Font.PdfEncodings.WINANSI, PdfFontFactory.EmbeddingStrategy.FORCE_EMBEDDED);
                PdfFont verdanaFont = PdfFontFactory.CreateFont(fontPathVerdana, iText.IO.Font.PdfEncodings.WINANSI, PdfFontFactory.EmbeddingStrategy.FORCE_EMBEDDED);
                PdfFont arialMTFont = PdfFontFactory.CreateFont(fontPathArialMT, iText.IO.Font.PdfEncodings.WINANSI, PdfFontFactory.EmbeddingStrategy.FORCE_EMBEDDED);


                string rutaEscudo = Path.Combine(AppContext.BaseDirectory, "DocImages", "Escudo.png");
                string rutaLogo = Path.Combine(AppContext.BaseDirectory, "DocImages", "LogoIT.png");

                ImageData imageDataLogo = ImageDataFactory.Create(rutaLogo);
                Image imgLogo = new Image(imageDataLogo).SetWidth(40);




                ImageData imageDataEscudo = ImageDataFactory.Create(rutaEscudo);
                Image imgEscudo = new Image(imageDataEscudo).SetWidth(40);



                #endregion

                //Fecha
                document.Add(new Paragraph($"Córdoba, {DateTime.Now.Day}/{DateTime.Now.Month}/{DateTime.Now.Year}")
                                .SetFont(tahomaFont)
                                .SetFontSize(4.5f)
                                .SetTextAlignment(TextAlignment.RIGHT));
                //Imágenes

                #region Logo y Escudo
                float[] columnWidths = { 50f, 30f, 30f }; // Porcentajes
                Table headerTable = new Table(UnitValue.CreatePercentArray(columnWidths))
                    .UseAllAvailableWidth();

                // Logo en la columna izquierda
                Cell logoCell = new Cell()
                    .Add(imgLogo)
                    .SetBorder(Border.NO_BORDER)
                    .SetTextAlignment(TextAlignment.LEFT)
                    .SetVerticalAlignment(VerticalAlignment.MIDDLE);

                // Escudo en la columna central
                Cell escudoCell = new Cell()
                    .Add(imgEscudo)
                    .SetBorder(Border.NO_BORDER)
                    .SetTextAlignment(TextAlignment.CENTER);

                // Celda vacía en la columna derecha
                Cell emptyCell = new Cell()
                    .SetBorder(Border.NO_BORDER)
                    .Add(new Paragraph(""));

                // Agregar celdas a la tabla
                headerTable.AddCell(logoCell);     // Columna 1: Logo izquierda
                headerTable.AddCell(escudoCell);   // Columna 2: Escudo centro  
                headerTable.AddCell(emptyCell);    // Columna 3: Vacía

                document.Add(headerTable);

                #endregion

                // Título

                #region Encabezados 
                document.Add(new Paragraph("REPUBLICA ARGENTINA")
                                .SetFont(tahomaFont)
                                .SetFontSize(6.5f)
                                .SimulateBold()
                                .SetMarginTop(0)
                                .SetMarginBottom(0)
                                .SetTextAlignment(TextAlignment.CENTER));

                document.Add(new Paragraph("LEY DE EDUCACION NACIONAL Nº 26.206")
                            .SetFont(tahomaFont)
                            .SetFontSize(6.5f)
                            .SetMarginTop(0)
                            .SetMarginBottom(0)
                            .SetTextAlignment(TextAlignment.CENTER));

                document.Add(new Paragraph("MINISTERIO DE EDUCACION DE LA PROVINCIA DE CORDOBA")
                                .SetFont(tahomaFont)
                                .SetFontSize(6.5f)
                                .SimulateBold()
                                .SetMarginTop(0)
                                .SetMarginBottom(0)
                                .SetTextAlignment(TextAlignment.CENTER));

                document.Add(new Paragraph("LEY DE EDUCACION PROVINCIAL N.º 8113")
                            .SetFont(tahomaFont)
                            .SetFontSize(6.5f)
                            .SetMarginTop(0)
                            .SetMarginBottom(0)
                            .SetTextAlignment(TextAlignment.CENTER));

                document.Add(new Paragraph("DIRECCION GENERAL DE EDUCACION TÉCNICA Y FORMACIÓN PROFESIONAL")
                                .SetFont(tahomaFont)
                                .SetFontSize(6.5f)
                                .SimulateBold()
                                .SetMarginTop(0)
                                .SetMarginBottom(0)//---
                                .SetTextAlignment(TextAlignment.CENTER));

                document.Add(new Paragraph("Instituto Técnico Superior Córdoba")
                            .SetFont(timesNewRomanFont)
                            .SetFontSize(15.5f)
                            .SetMarginBottom(0)
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetFontColor(ColorConstants.BLUE));

                document.Add(new Paragraph("Tecnicatura Superior en Desarrollo de Software")
                            .SetFont(timesNewRomanFont)
                            .SetFontSize(15.5f)
                            .SetMarginTop(0)
                            .SetMarginBottom(0)
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetFontColor(ColorConstants.RED));

                #endregion

                
                LineSeparator ls = new LineSeparator(new SolidLine(2f));
                ls.SetOpacity(0.5f)
                  .SetWidth(UnitValue.CreatePercentValue(100));



                document.Add(ls);

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
                    tablaDatos.AddCell(GetCell("Apellido y Nombre:", verdanaFont, 5.5f, false, true));
                    tablaDatos.AddCell(GetCell($"{datos.ApellidoyNombre}", verdanaFont, 5.5f));
                    tablaDatos.AddCell(GetCell("Legajo:", verdanaFont, 5.5f, false, true));
                    tablaDatos.AddCell(GetCell($"{datos.Legajo}", verdanaFont, 5.5f));

                    tablaDatos.AddCell(GetCell("Tipo Doc.:", verdanaFont, 5.5f, false, true));
                    tablaDatos.AddCell(GetCell($"{datos.TipoDocumentoCertificado}", verdanaFont, 5.5f));
                    tablaDatos.AddCell(GetCell("Nro. Doc.:", verdanaFont, 5.5f, false, true));
                    tablaDatos.AddCell(GetCell($"{datos.NroDocumento}", verdanaFont, 5.5f));

                    tablaDatos.AddCell(GetCell("Fecha de Nacimiento:", verdanaFont, 5.5f, false, true));
                    tablaDatos.AddCell(GetCell($"{datos.FechadeNacimiento}", verdanaFont, 5.5f));
                    tablaDatos.AddCell(GetCell("Libro Matriz:", verdanaFont, 5.5f, false, true));
                    tablaDatos.AddCell(GetCell($"{datos.LibroMatriz}", verdanaFont, 5.5f));

                    tablaDatos.AddCell(GetCell("Lugar de Nacimiento:", verdanaFont, 5.5f, false, true));
                    tablaDatos.AddCell(GetCell($"{datos.LugarNacimiento}", verdanaFont, 5.5f));
                    tablaDatos.AddCell(GetCell("Folio:", verdanaFont, 5.5f, false, true));
                    tablaDatos.AddCell(GetCell($"{datos.Folio}", verdanaFont, 5.5f));

                    tablaDatos.AddCell(GetCell("Teléfono:", verdanaFont, 5.5f, false, true));
                    tablaDatos.AddCell(GetCell($"{datos.NroTelefono}", verdanaFont, 5.5f));
                    tablaDatos.AddCell(new Cell(1, 2).SetBorder(Border.NO_BORDER)); // Celda vacía sin bordes

                    tablaDatos.AddCell(GetCell("Título Habilitante:", verdanaFont, 5.5f, false, true));
                    tablaDatos.AddCell(GetCell($"{datos.TituloHabilitante}", verdanaFont, 5.5f));
                    tablaDatos.AddCell(new Cell(1, 2).SetBorder(Border.NO_BORDER)); // Celda vacía sin bordes


                    #endregion

                    // Agrega la tabla de datos del alumno al PDF
                    document.Add(tablaDatos);

                    document.Add(ls);

                    document.Add(new Paragraph("Certifico que el/la estudiante mencionado/a cursa en carácter de REGULAR, la Tecnicatura Superior en Desarrollo de Software (Res 462/12)  y ha aprobado los espacios curriculares que se detallan a continuación:")
                                .SetFont(tahomaFont)
                                .SetFontSize(6)
                                .SetTextAlignment(TextAlignment.CENTER));

                    Table tablaNotas = new Table(10).UseAllAvailableWidth()
                                                    .SetMarginBottom(10);

                    #region Tablas Encabezado
                    
                    tablaNotas.AddHeaderCell(new Cell(1, 9).Add(new Paragraph("CALIFICACIONES").SetFont(verdanaFont).SetFontSize(6.5f).SetHorizontalAlignment(HorizontalAlignment.CENTER)));
                    tablaNotas.AddHeaderCell(new Cell(1, 1).Add(new Paragraph("Establecimiento").SetFont(verdanaFont).SetFontSize(6.5f)));

                    tablaNotas.AddHeaderCell(new Cell(2, 1).Add(new Paragraph("ASIGNATURAS").SimulateBold().SetFont(verdanaFont).SetFontSize(6.5f).SetHorizontalAlignment(HorizontalAlignment.CENTER)));
                    tablaNotas.AddHeaderCell(new Cell(1, 2).Add(new Paragraph("Nota").SimulateBold().SetFont(verdanaFont).SetFontSize(6.5f)));
                    tablaNotas.AddHeaderCell(new Cell(1, 2).Add(new Paragraph("Acta").SimulateBold().SetFont(verdanaFont).SetFontSize(6.5f)));
                    tablaNotas.AddHeaderCell(new Cell(1, 3).Add(new Paragraph("Aprobado el:").SimulateBold().SetFont(verdanaFont).SetFontSize(6.5f)));
                    tablaNotas.AddHeaderCell(new Cell(1, 1).Add(new Paragraph("Condicion").SetFont(verdanaFont).SetFontSize(6.5f)));
                    tablaNotas.AddHeaderCell(new Cell(1, 1).Add(new Paragraph("Sede").SimulateBold().SetFont(verdanaFont).SetFontSize(6.5f)));

                    tablaNotas.AddHeaderCell(new Cell().Add(new Paragraph("Número").SetFont(verdanaFont).SetFontSize(6.5f)));
                    tablaNotas.AddHeaderCell(new Cell().Add(new Paragraph("Letras").SetFont(verdanaFont).SetFontSize(6.5f)));
                    tablaNotas.AddHeaderCell(new Cell().Add(new Paragraph("Libro").SetFont(verdanaFont).SetFontSize(6.5f)));
                    tablaNotas.AddHeaderCell(new Cell().Add(new Paragraph("Folio").SetFont(verdanaFont).SetFontSize(6.5f)));
                    tablaNotas.AddHeaderCell(new Cell().Add(new Paragraph("Día").SetFont(verdanaFont).SetFontSize(6.5f)));
                    tablaNotas.AddHeaderCell(new Cell().Add(new Paragraph("Mes").SetFont(verdanaFont).SetFontSize(6.5f)));
                    tablaNotas.AddHeaderCell(new Cell().Add(new Paragraph("Año").SetFont(verdanaFont).SetFontSize(6.5f)));
                    tablaNotas.AddHeaderCell(new Cell().Add(new Paragraph("Reg/Prom/Libre").SetFont(verdanaFont).SetFontSize(6.5f)));
                    tablaNotas.AddHeaderCell(new Cell().Add(new Paragraph("CENTRAL/NORTE").SetFont(verdanaFont).SetFontSize(6.5f)));
                    #endregion


                    // Filas de la tabla de las condicion de cada materia
                    foreach (var fila in datos.FilasTabla)
                    {
                        tablaNotas.AddCell(GetCell(fila.Asignatura, verdanaFont, 6.5f, true));
                        tablaNotas.AddCell(GetCell(fila.ValorNota.ToString(), verdanaFont, 6.5f, true));
                        tablaNotas.AddCell(GetCell(fila.NotaLetra, verdanaFont, 6.5f, true));
                        tablaNotas.AddCell(GetCell(fila.Libro, verdanaFont, 6.5f, true)); 
                        tablaNotas.AddCell(GetCell(fila.Folio, verdanaFont, 6.5f, true));
                        tablaNotas.AddCell(GetCell($"{fila.Dia}", verdanaFont, 6.5f, true));
                        tablaNotas.AddCell(GetCell($"{fila.Mes}", verdanaFont, 6.5f, true));
                        tablaNotas.AddCell(GetCell($"{fila.Anno}", verdanaFont, 6.5f, true));
                        tablaNotas.AddCell(GetCell(fila.CondicionActual, verdanaFont, 6.5f, true));
                        tablaNotas.AddCell(GetCell(fila.Sede, verdanaFont, 6.5f, true));

                        
                    }

                    document.Add(tablaNotas);

                    #region tablas de promedios
                    // Tabla 1 - Promedio General
                    Table tablaPromedio = new Table(2)
                        .UseAllAvailableWidth()  // ← Agregar esto
                        .SetWidth(UnitValue.CreatePercentValue(30))  // ← 30% del ancho disponible
                        .SetBorder(new SolidBorder(ColorConstants.GRAY, 1))
                        .SetMarginBottom(4);

                    // Asegurar que GetCell() no quite los bordes, o crear celdas directamente:
                    tablaPromedio.AddCell(new Cell()
                        .Add(new Paragraph("Promedio General:").SetFont(verdanaFont).SetFontSize(5.5f))
                        .SetBorder(Border.NO_BORDER));

                    tablaPromedio.AddCell(new Cell()
                        .Add(new Paragraph("PromNum").SetFont(verdanaFont).SetFontSize(5.5f))
                        .SetBorder(Border.NO_BORDER));

                    
                    document.Add(tablaPromedio);

                    document.Add(ls);

                    // Tabla 2 - Promedios por Año

                    Table tablaPromedioPorAnio = new Table(2)
                        .UseAllAvailableWidth()  // ← Agregar esto
                        .SetWidth(UnitValue.CreatePercentValue(30))  // ← 30% del ancho disponible
                        .SetBorder(new SolidBorder(ColorConstants.GRAY, 1))
                        .SetMarginTop(4)
                        .SetMarginBottom(4);

                    // Agregar celdas con bordes
                    tablaPromedioPorAnio.AddCell(new Cell()
                        .Add(new Paragraph("PRIMER AÑO").SetFont(verdanaFont).SetFontSize(5.5f))
                        .SetBorder(Border.NO_BORDER));

                    tablaPromedioPorAnio.AddCell(new Cell()
                        .Add(new Paragraph("PromNum").SetFont(verdanaFont).SetFontSize(5.5f))
                        .SetBorder(Border.NO_BORDER));

                    // Repetir para las demás celdas...
                    tablaPromedioPorAnio.AddCell(new Cell()
                        .Add(new Paragraph("SEGUNDO AÑO").SetFont(verdanaFont).SetFontSize(5.5f))
                        .SetBorder(Border.NO_BORDER));

                    tablaPromedioPorAnio.AddCell(new Cell()
                        .Add(new Paragraph("PromNum").SetFont(verdanaFont).SetFontSize(5.5f))
                        .SetBorder(Border.NO_BORDER));

                    tablaPromedioPorAnio.AddCell(new Cell()
                        .Add(new Paragraph("TERCER AÑO").SetFont(verdanaFont).SetFontSize(5.5f))
                        .SetBorder(Border.NO_BORDER));

                    tablaPromedioPorAnio.AddCell(new Cell()
                        .Add(new Paragraph("PromNum").SetFont(verdanaFont).SetFontSize(5.5f))
                        .SetBorder(Border.NO_BORDER));

                    
                    document.Add(tablaPromedioPorAnio);
#endregion


                    document.Add(ls);

                    document.Add(new Paragraph("Para constancia y a petición del/la interesado/a" +
                                                " se expide el presente para ser presentado a los fines que hubiere lugar," +
                                                " sin raspaduras ni enmiendas en la ciudad de \r\nCórdoba" +
                                                " de la Provincia de Córdoba, República Argentina.")
                                                .SetFont(verdanaFont)
                                                .SetFontSize(6f)
                                                .SetTextAlignment(TextAlignment.LEFT));

                    var cultura = new System.Globalization.CultureInfo("es-ES");

                    document.Add(new Paragraph($"{DateTime.Now.ToString("dddd", cultura)}, {DateTime.Now.Day} de {IntAMes(DateTime.Now.Month, false)} de {DateTime.Now.Year}")
                                               .SetFont(verdanaFont)
                                               .SetFontSize(6f)
                                               .SetTextAlignment(TextAlignment.LEFT));

                    #region Firma
                    // Contenedor para la firma
                    Div contenedorFirma = new Div()
                        .SetHorizontalAlignment(HorizontalAlignment.RIGHT)
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetWidth(UnitValue.CreatePointValue(150))
                        .SetMarginTop(10);

                    // Línea de firma

                    
                    LineSeparator lineaFirma = new LineSeparator(new SolidLine(0.5f));
                    lineaFirma.SetOpacity(0.5f)
                    .SetWidth(UnitValue.CreatePointValue(150))
                    .SetMarginBottom(2);

                    contenedorFirma.Add(lineaFirma);

                    // Texto de firma
                    Paragraph textoFirma = new Paragraph("Firma")
                        .SetFont(verdanaFont)
                        .SetFontSize(6f)
                        .SetMarginBottom(0);

                    contenedorFirma.Add(textoFirma);

                    document.Add(contenedorFirma);
                    #endregion

                    document.Add(ls);
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
        
        static Cell GetCell(string text, PdfFont fuente, float size, bool border = false, bool bold = false)
        {
            Cell cell = new Cell().Add(new Paragraph(text)
                                .SetFont(fuente)
                                .SetFontSize(size));
            if (bold) cell.SimulateBold();

            if (border)
                cell.SetBorder(new SolidBorder(1));
            else
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

        private static string IntAMes(int numMes, bool Dia)
        {
            
            switch (numMes)
            {
                case 1:
                    return "enero";
                case 2:
                    return "febrero";
                case 3:
                    return "marzo";
                case 4:
                    return "abril";
                case 5:
                    return "mayo";
                case 6:
                    return "junio";
                case 7:
                    return "julio";
                case 8:
                    return "agosto";
                case 9:
                    return "septiembre";
                case 10:
                    return "octubre";
                case 11:
                    return "noviembre";
                case 12:
                    return "diciembre";
                default:
                    return "mes inválido";
            }
            
            
               
        }

    }
}
