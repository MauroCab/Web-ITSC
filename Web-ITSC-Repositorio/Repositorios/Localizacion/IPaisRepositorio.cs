using Web_ITSC_BD.Data.Entity;
using Web_ITSC_Repositorio.Repositorios.Genericos;

namespace Web_ITSC_Repositorio.Repositorios.Localizacion
{
    public interface IPaisRepositorio : IRepositorio<Pais>
    {
        Task<Pais> GetByIdAsync(int id);
    }
}