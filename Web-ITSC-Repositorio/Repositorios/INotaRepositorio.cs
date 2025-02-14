using Web_ITSC_BD.Data.Entity;
using Web_ITSC_Repositorio.Repositorios.Genericos;
using Web_ITSC_Shared.DTO;

namespace Web_ITSC_Repositorio.Repositorios
{
    public interface INotaRepositorio : IRepositorio<Nota>
    {
        Task<List<Nota>> FullGetAll();
        Task<Nota> FullGetById(int id);
        Task<List<GetNotaNBTDTO>> SelectNotasByTurno(int turnoId);
    }
}