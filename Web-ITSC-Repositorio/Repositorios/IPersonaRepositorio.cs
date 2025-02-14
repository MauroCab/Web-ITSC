using Web_ITSC_BD.Data.Entity;
using Web_ITSC_Repositorio.Repositorios.Genericos;

namespace Web_ITSC_Repositorio.Repositorios
{
    public interface IPersonaRepositorio : IRepositorio<Persona>
    {
        Task<List<Persona>> FullGetAll();
        Task<Persona> FullGetById(int id);
        Task FullInsert(Persona persona);
        Task FullUpdate(Persona persona);
    }
}