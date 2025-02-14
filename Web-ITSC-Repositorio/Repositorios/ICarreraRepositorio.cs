using Web_ITSC_BD.Data.Entity;
using Web_ITSC_Repositorio.Repositorios.Genericos;

namespace Web_ITSC_Repositorio.Repositorios
{
    public interface ICarreraRepositorio : IRepositorio<Carrera>
    {
        Task<int> GetByNombre(string nombreCarrera);
        Task<Carrera> GetCarreraByIdAsync(int carreraId);
    }
}