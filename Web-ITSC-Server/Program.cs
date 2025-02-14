using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Web_ITSC_BD.Data;
using Web_ITSC_BD.Data.Entity;
using Web_ITSC_Repositorio.Repositorios;
using Web_ITSC_Repositorio.Repositorios.Genericos;
using Web_ITSC_Repositorio.Repositorios.Localizacion;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<Context>(op => op.UseSqlServer("name=conn"));

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<IAlumnoRepositorio, AlumnoRepositorio>();
builder.Services.AddScoped<IRepositorio<CertificadoAlumno>, Repositorio<CertificadoAlumno>>();
builder.Services.AddScoped<IRepositorio<Clase>, Repositorio<Clase>>();
builder.Services.AddScoped<IRepositorio<ClaseAsistencia>, Repositorio<ClaseAsistencia>>();

builder.Services.AddScoped<IRepositorio<Correlatividad>, Repositorio<Correlatividad>>();

builder.Services.AddScoped<ICursadoMateriaRepositorio, CursadoMateriaRepositorio>();
builder.Services.AddScoped<IEvaluacionRepositorio, EvaluacionRepositorio>();
builder.Services.AddScoped<IInscripcionCarreraRepositorio, InscripcionCarreraRepositorio>();

builder.Services.AddScoped<IRepositorio<Materia>, Repositorio<Materia>>();
builder.Services.AddScoped<IMateriaEnPlanEstudioRepositorio, MateriaEnPlanEstudioRepositorio>();
builder.Services.AddScoped<INotaRepositorio, NotaRepositorio>();
builder.Services.AddScoped<IRepositorio<Persona>, Repositorio<Persona>>();
builder.Services.AddScoped<IPlanEstudioRepositorio, PlanEstudioRepositorio>();
builder.Services.AddScoped<IRepositorio<Profesor>, Repositorio<Profesor>>();
builder.Services.AddScoped<ITurnoRepositorio, TurnoRepositorio>();
builder.Services.AddScoped<ICertificadoAlumnoRepositorio, CertificadoAlumnoRepositorio>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<IPersonaRepositorio, PersonaRepositorio>();
builder.Services.AddScoped<ICarreraRepositorio, CarreraRepositorio>();
builder.Services.AddScoped<ITipoDocumentoRepositorio, TipoDocumentoRepositorio>();

builder.Services.AddScoped<IPaisRepositorio, PaisRepositorio>();
builder.Services.AddScoped<IProvinciaRepositorio, ProvinciaRepositorio>();
builder.Services.AddScoped<IDepartamentoRepositorio, DepartamentoRepositorio>();
builder.Services.AddScoped<ILocalidadRepositorio, LocalidadRepositorio>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
