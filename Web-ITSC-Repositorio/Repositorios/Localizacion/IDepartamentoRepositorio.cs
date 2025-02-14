using Web_ITSC_BD.Data.Entity;

namespace Web_ITSC_Repositorio.Repositorios.Localizacion
{
    public interface IDepartamentoRepositorio
    {
        Task<Departamento> GetByIdAsync(int id);
        Task<List<Departamento>> SelectDepartamentosPorProvinciaAsync(int ProvinciaId);
    }
}