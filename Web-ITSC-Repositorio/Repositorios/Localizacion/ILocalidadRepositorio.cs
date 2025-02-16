using Web_ITSC_BD.Data.Entity;
using Web_ITSC_Repositorio.Repositorios.Genericos;

namespace Web_ITSC_Repositorio.Repositorios.Localizacion
{
    public interface ILocalidadRepositorio : IRepositorio<Localidad>
    {
        Task<Localidad> GetByIdAsync(int id);
        Task<List<Localidad>> SelectLocalidadesPorDepartamentoAsync(int DepartamentoId);
    }
}