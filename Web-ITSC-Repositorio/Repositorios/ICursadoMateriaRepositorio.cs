using Web_ITSC_BD.Data.Entity;
using Web_ITSC_Repositorio.Repositorios.Genericos;

namespace Web_ITSC_Repositorio.Repositorios
{
    public interface ICursadoMateriaRepositorio : IRepositorio<CursadoMateria>
    {
        Task<List<CursadoMateria>> FullGetAll();
        Task<CursadoMateria> FullGetById(int id);
    }
}