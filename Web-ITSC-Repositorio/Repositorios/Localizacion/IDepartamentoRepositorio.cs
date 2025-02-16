using Web_ITSC_BD.Data.Entity;
using Web_ITSC_Repositorio.Repositorios.Genericos;

namespace Web_ITSC_Repositorio.Repositorios.Localizacion
{
    public interface IDepartamentoRepositorio : IRepositorio<Departamento>
    {
        Task<Departamento> GetByIdAsync(int id);
        Task<List<Departamento>> SelectDepartamentosPorProvinciaAsync(int ProvinciaId);
    }
}