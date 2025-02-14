using Web_ITSC_BD.Data.Entity;
using Web_ITSC_Repositorio.Repositorios.Genericos;

namespace Web_ITSC_Repositorio.Repositorios
{
    public interface ITurnoRepositorio : IRepositorio<Turno>
    {
        Task<List<Turno>> FullGetAll();
        Task<Turno> FullGetById(int id);
    }
}