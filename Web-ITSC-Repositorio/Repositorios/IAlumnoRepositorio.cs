using Microsoft.AspNetCore.Mvc;
using Web_ITSC_BD.Data.Entity;
using Web_ITSC_Repositorio.Repositorios.Genericos;
using Web_ITSC_Shared.DTO;

namespace Web_ITSC_Repositorio.Repositorios
{
    public interface IAlumnoRepositorio : IRepositorio<Alumno>
    {
        Task<ActionResult<IEnumerable<BuscarAlumnoDTO>>> BuscarAlumnos(string? nombre, string? apellido, string? documento, int? cohorte);
        Task<bool> EliminarAlumno(int alumnoId);
        Task<List<Alumno>> FullGetAll();
        Task<Alumno> FullGetById(int id);
        Task<Alumno> GetAlumnoPorCUIL(string cuil);
        Task<Alumno> GetAlumnoPorDocumento(string documento);
        Task<bool> Update(Alumno alumno);
    }
}