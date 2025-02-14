using Web_ITSC_BD.Data.Entity;

namespace Web_ITSC_Repositorio.Repositorios.Localizacion
{
    public interface IProvinciaRepositorio
    {
        Task<Provincia> GetByIdAsync(int id);
        Task<List<Departamento>> SelectDepartamentosPorProvincia(int ProvinciaId);
        Task<List<Provincia>> SelectProvinciasPorPaisAsync(int PaisId);
    }
}