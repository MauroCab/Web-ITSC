using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_ITSC_BD.Migrations
{
    /// <inheritdoc />
    public partial class Inicio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carreras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Abreviatura = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DuracionCarrera = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Modalidad = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carreras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Materias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Formato = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Formacion = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Anno = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Paises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposDocumento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposDocumento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlanesEstudio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarreraId = table.Column<int>(type: "int", nullable: false),
                    ResolucionMinisterial = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Anno = table.Column<int>(type: "int", nullable: false),
                    EstadoPlan = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanesEstudio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanesEstudio_Carreras_CarreraId",
                        column: x => x.CarreraId,
                        principalTable: "Carreras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Provincias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaisId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provincias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Provincias_Paises_PaisId",
                        column: x => x.PaisId,
                        principalTable: "Paises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Personas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Documento = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    TipoDocumentoId = table.Column<int>(type: "int", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: false),
                    Domicilio = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Personas_TiposDocumento_TipoDocumentoId",
                        column: x => x.TipoDocumentoId,
                        principalTable: "TiposDocumento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MateriasEnPlanEstudio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MateriaId = table.Column<int>(type: "int", nullable: false),
                    PlanEstudioId = table.Column<int>(type: "int", nullable: false),
                    HrsRelojAnuales = table.Column<int>(type: "int", nullable: false),
                    HrsCatedraSemanales = table.Column<int>(type: "int", nullable: false),
                    Anual_Cuatrimestral = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Anno = table.Column<int>(type: "int", nullable: false),
                    NumeroOrden = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MateriasEnPlanEstudio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MateriasEnPlanEstudio_Materias_MateriaId",
                        column: x => x.MateriaId,
                        principalTable: "Materias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MateriasEnPlanEstudio_PlanesEstudio_PlanEstudioId",
                        column: x => x.PlanEstudioId,
                        principalTable: "PlanesEstudio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Departamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProvinciaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departamentos_Provincias_ProvinciaId",
                        column: x => x.ProvinciaId,
                        principalTable: "Provincias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonaId = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Contrasena = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Personas_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Correlatividades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MateriaEnPlanEstudioId = table.Column<int>(type: "int", nullable: false),
                    MateriaCorrelativaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Correlatividades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Correlatividades_MateriasEnPlanEstudio_MateriaCorrelativaId",
                        column: x => x.MateriaCorrelativaId,
                        principalTable: "MateriasEnPlanEstudio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Correlatividades_MateriasEnPlanEstudio_MateriaEnPlanEstudioId",
                        column: x => x.MateriaEnPlanEstudioId,
                        principalTable: "MateriasEnPlanEstudio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Localidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartamentoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localidades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Localidades_Departamentos_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Departamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Profesores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profesores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profesores_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Alumnos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Sexo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CUIL = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    TituloBase = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    FotocopiaDNI = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    ConstanciaCUIL = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    PartidaNacimiento = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Analitico = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    FotoCarnet = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CUS = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    PaisId = table.Column<int>(type: "int", nullable: false),
                    ProvinciaId = table.Column<int>(type: "int", nullable: false),
                    DepartamentoId = table.Column<int>(type: "int", nullable: false),
                    LocalidadId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alumnos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alumnos_Departamentos_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Departamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Alumnos_Localidades_LocalidadId",
                        column: x => x.LocalidadId,
                        principalTable: "Localidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Alumnos_Paises_PaisId",
                        column: x => x.PaisId,
                        principalTable: "Paises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Alumnos_Provincias_ProvinciaId",
                        column: x => x.ProvinciaId,
                        principalTable: "Provincias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Alumnos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Turnos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MateriaEnPlanEstudioId = table.Column<int>(type: "int", nullable: false),
                    ProfesorId = table.Column<int>(type: "int", nullable: true),
                    Sede = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Horario = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    AnnoCicloLectivo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turnos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Turnos_MateriasEnPlanEstudio_MateriaEnPlanEstudioId",
                        column: x => x.MateriaEnPlanEstudioId,
                        principalTable: "MateriasEnPlanEstudio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Turnos_Profesores_ProfesorId",
                        column: x => x.ProfesorId,
                        principalTable: "Profesores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CertificadosAlumno",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlumnoId = table.Column<int>(type: "int", nullable: false),
                    FechaEmision = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertificadosAlumno", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CertificadosAlumno_Alumnos_AlumnoId",
                        column: x => x.AlumnoId,
                        principalTable: "Alumnos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InscripcionesCarrera",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlumnoId = table.Column<int>(type: "int", nullable: false),
                    CarreraId = table.Column<int>(type: "int", nullable: false),
                    Cohorte = table.Column<int>(type: "int", nullable: false),
                    Legajo = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    EstadoAlumno = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LibroMatriz = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NroOrdenAlumno = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InscripcionesCarrera", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InscripcionesCarrera_Alumnos_AlumnoId",
                        column: x => x.AlumnoId,
                        principalTable: "Alumnos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InscripcionesCarrera_Carreras_CarreraId",
                        column: x => x.CarreraId,
                        principalTable: "Carreras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Clases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TurnoId = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clases_Turnos_TurnoId",
                        column: x => x.TurnoId,
                        principalTable: "Turnos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CursadosMateria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlumnoId = table.Column<int>(type: "int", nullable: false),
                    TurnoId = table.Column<int>(type: "int", nullable: false),
                    FechaInscripcion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Anno = table.Column<int>(type: "int", nullable: false),
                    CondicionActual = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    VencimientoCondicion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CursadosMateria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CursadosMateria_Alumnos_AlumnoId",
                        column: x => x.AlumnoId,
                        principalTable: "Alumnos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CursadosMateria_Turnos_TurnoId",
                        column: x => x.TurnoId,
                        principalTable: "Turnos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Evaluaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TurnoId = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipoEvaluacion = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Folio = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Libro = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Sede = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evaluaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Evaluaciones_Turnos_TurnoId",
                        column: x => x.TurnoId,
                        principalTable: "Turnos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClaseAsistencias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CursadoMateriaId = table.Column<int>(type: "int", nullable: false),
                    ClaseId = table.Column<int>(type: "int", nullable: false),
                    Asistencia = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Observacion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaseAsistencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClaseAsistencias_Clases_ClaseId",
                        column: x => x.ClaseId,
                        principalTable: "Clases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClaseAsistencias_CursadosMateria_CursadoMateriaId",
                        column: x => x.CursadoMateriaId,
                        principalTable: "CursadosMateria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CursadoMateriaId = table.Column<int>(type: "int", nullable: false),
                    EvaluacionId = table.Column<int>(type: "int", nullable: false),
                    ValorNota = table.Column<int>(type: "int", nullable: false),
                    Asistencia = table.Column<string>(type: "nvarchar(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notas_CursadosMateria_CursadoMateriaId",
                        column: x => x.CursadoMateriaId,
                        principalTable: "CursadosMateria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notas_Evaluaciones_EvaluacionId",
                        column: x => x.EvaluacionId,
                        principalTable: "Evaluaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alumnos_DepartamentoId",
                table: "Alumnos",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Alumnos_LocalidadId",
                table: "Alumnos",
                column: "LocalidadId");

            migrationBuilder.CreateIndex(
                name: "IX_Alumnos_PaisId",
                table: "Alumnos",
                column: "PaisId");

            migrationBuilder.CreateIndex(
                name: "IX_Alumnos_ProvinciaId",
                table: "Alumnos",
                column: "ProvinciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Alumnos_UsuarioId",
                table: "Alumnos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_CertificadosAlumno_AlumnoId",
                table: "CertificadosAlumno",
                column: "AlumnoId");

            migrationBuilder.CreateIndex(
                name: "AsistenciasDeAlumnoEnTurnoIDX",
                table: "ClaseAsistencias",
                column: "CursadoMateriaId");

            migrationBuilder.CreateIndex(
                name: "AsistenciasDeUnaClaseIDX",
                table: "ClaseAsistencias",
                column: "ClaseId");

            migrationBuilder.CreateIndex(
                name: "FaltaronEstaClaseIDX",
                table: "ClaseAsistencias",
                columns: new[] { "Asistencia", "ClaseId" });

            migrationBuilder.CreateIndex(
                name: "Clases_UQ",
                table: "Clases",
                columns: new[] { "TurnoId", "Fecha" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ClasesDeUnTurnoIDX",
                table: "Clases",
                column: "TurnoId");

            migrationBuilder.CreateIndex(
                name: "IX_Correlatividades_MateriaCorrelativaId",
                table: "Correlatividades",
                column: "MateriaCorrelativaId");

            migrationBuilder.CreateIndex(
                name: "IX_Correlatividades_MateriaEnPlanEstudioId",
                table: "Correlatividades",
                column: "MateriaEnPlanEstudioId");

            migrationBuilder.CreateIndex(
                name: "IX_CursadosMateria_AlumnoId",
                table: "CursadosMateria",
                column: "AlumnoId");

            migrationBuilder.CreateIndex(
                name: "IX_CursadosMateria_TurnoId",
                table: "CursadosMateria",
                column: "TurnoId");

            migrationBuilder.CreateIndex(
                name: "IX_Departamentos_ProvinciaId",
                table: "Departamentos",
                column: "ProvinciaId");

            migrationBuilder.CreateIndex(
                name: "EvaluacionesDeUnTurnoIDX",
                table: "Evaluaciones",
                column: "TurnoId");

            migrationBuilder.CreateIndex(
                name: "EvaluacionesEnFechaIDX",
                table: "Evaluaciones",
                columns: new[] { "TurnoId", "Fecha" });

            migrationBuilder.CreateIndex(
                name: "EvaluacionesTipoIDX",
                table: "Evaluaciones",
                columns: new[] { "TurnoId", "TipoEvaluacion" });

            migrationBuilder.CreateIndex(
                name: "CohorteIDX",
                table: "InscripcionesCarrera",
                columns: new[] { "CarreraId", "Cohorte" });

            migrationBuilder.CreateIndex(
                name: "InscripcionCarrera_UQ",
                table: "InscripcionesCarrera",
                columns: new[] { "AlumnoId", "CarreraId", "Cohorte" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Localidades_DepartamentoId",
                table: "Localidades",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "MateriasPorNombreIDX",
                table: "Materias",
                column: "Nombre");

            migrationBuilder.CreateIndex(
                name: "IX_MateriasEnPlanEstudio_MateriaId",
                table: "MateriasEnPlanEstudio",
                column: "MateriaId");

            migrationBuilder.CreateIndex(
                name: "MateriaEnPlanEstudio_UQ",
                table: "MateriasEnPlanEstudio",
                columns: new[] { "PlanEstudioId", "MateriaId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "NotasDeAlumnoEnMateriaIDX",
                table: "Notas",
                column: "CursadoMateriaId");

            migrationBuilder.CreateIndex(
                name: "NotasDeEvaluacionIDX",
                table: "Notas",
                column: "EvaluacionId");

            migrationBuilder.CreateIndex(
                name: "Apellido-NombreIDX",
                table: "Personas",
                columns: new[] { "Apellido", "Nombre" });

            migrationBuilder.CreateIndex(
                name: "IX_Personas_TipoDocumentoId",
                table: "Personas",
                column: "TipoDocumentoId");

            migrationBuilder.CreateIndex(
                name: "Nombre-ApellidoIDX",
                table: "Personas",
                columns: new[] { "Nombre", "Apellido" });

            migrationBuilder.CreateIndex(
                name: "PersonaUnica_UQ",
                table: "Personas",
                columns: new[] { "Documento", "TipoDocumentoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "PlanesDeUnaCarreraIDX",
                table: "PlanesEstudio",
                column: "CarreraId");

            migrationBuilder.CreateIndex(
                name: "PlanEstudio_UQ",
                table: "PlanesEstudio",
                columns: new[] { "CarreraId", "Anno" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Profesores_UsuarioId",
                table: "Profesores",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Provincias_PaisId",
                table: "Provincias",
                column: "PaisId");

            migrationBuilder.CreateIndex(
                name: "TDocumentoUnico_UQ",
                table: "TiposDocumento",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "InscripcionCarrera_UQ",
                table: "Turnos",
                columns: new[] { "MateriaEnPlanEstudioId", "Sede", "Horario", "AnnoCicloLectivo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_ProfesorId",
                table: "Turnos",
                column: "ProfesorId");

            migrationBuilder.CreateIndex(
                name: "MateriasQueDaUnProfeIDX",
                table: "Turnos",
                columns: new[] { "MateriaEnPlanEstudioId", "ProfesorId" });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_PersonaId",
                table: "Usuarios",
                column: "PersonaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CertificadosAlumno");

            migrationBuilder.DropTable(
                name: "ClaseAsistencias");

            migrationBuilder.DropTable(
                name: "Correlatividades");

            migrationBuilder.DropTable(
                name: "InscripcionesCarrera");

            migrationBuilder.DropTable(
                name: "Notas");

            migrationBuilder.DropTable(
                name: "Clases");

            migrationBuilder.DropTable(
                name: "CursadosMateria");

            migrationBuilder.DropTable(
                name: "Evaluaciones");

            migrationBuilder.DropTable(
                name: "Alumnos");

            migrationBuilder.DropTable(
                name: "Turnos");

            migrationBuilder.DropTable(
                name: "Localidades");

            migrationBuilder.DropTable(
                name: "MateriasEnPlanEstudio");

            migrationBuilder.DropTable(
                name: "Profesores");

            migrationBuilder.DropTable(
                name: "Departamentos");

            migrationBuilder.DropTable(
                name: "Materias");

            migrationBuilder.DropTable(
                name: "PlanesEstudio");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Provincias");

            migrationBuilder.DropTable(
                name: "Carreras");

            migrationBuilder.DropTable(
                name: "Personas");

            migrationBuilder.DropTable(
                name: "Paises");

            migrationBuilder.DropTable(
                name: "TiposDocumento");
        }
    }
}
