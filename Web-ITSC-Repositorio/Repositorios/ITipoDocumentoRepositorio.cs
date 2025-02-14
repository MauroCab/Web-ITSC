using Web_ITSC_BD.Data.Entity;
using Web_ITSC_Repositorio.Repositorios.Genericos;

namespace Web_ITSC_Repositorio.Repositorios
{
    public interface ITipoDocumentoRepositorio : IRepositorio<TipoDocumento>
    {
        Task<TipoDocumento> GetByIdAsync(int id);
    }
}