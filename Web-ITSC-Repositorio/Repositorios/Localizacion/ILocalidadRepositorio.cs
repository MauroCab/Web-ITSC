using Web_ITSC_BD.Data.Entity;

namespace Web_ITSC_Repositorio.Repositorios.Localizacion
{
    public interface ILocalidadRepositorio
    {
        Task<Localidad> GetByIdAsync(int id);
        Task<List<Localidad>> SelectLocalidadesPorDepartamentoAsync(int DepartamentoId);
    }
}