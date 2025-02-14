using Web_ITSC_BD.Data.Entity;
using Web_ITSC_Repositorio.Repositorios.Genericos;

namespace Web_ITSC_Repositorio.Repositorios
{
    public interface IUsuarioRepositorio : IRepositorio<Usuario>
    {
        Task<List<Usuario>> FullGetAll();
        Task<Usuario> FullGetById(int id);
    }
}