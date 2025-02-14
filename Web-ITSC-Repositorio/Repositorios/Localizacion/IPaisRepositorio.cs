using Web_ITSC_BD.Data.Entity;

namespace Web_ITSC_Repositorio.Repositorios.Localizacion
{
    public interface IPaisRepositorio
    {
        Task<Pais> GetByIdAsync(int id);
    }
}