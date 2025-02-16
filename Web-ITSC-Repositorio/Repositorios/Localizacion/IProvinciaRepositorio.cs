using Web_ITSC_BD.Data.Entity;
using Web_ITSC_Repositorio.Repositorios.Genericos;

namespace Web_ITSC_Repositorio.Repositorios.Localizacion
{
    public interface IProvinciaRepositorio : IRepositorio<Provincia>
    {
        Task<Provincia> GetByIdAsync(int id);
        Task<List<Departamento>> SelectDepartamentosPorProvincia(int ProvinciaId);
        Task<List<Provincia>> SelectProvinciasPorPaisAsync(int PaisId);
    }
}