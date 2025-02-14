using Web_ITSC_BD.Data.Entity;
using Web_ITSC_Repositorio.Repositorios.Genericos;

namespace Web_ITSC_Repositorio.Repositorios
{
    public interface IPlanEstudioRepositorio : IRepositorio<PlanEstudio>
    {
        Task<List<PlanEstudio>> FullGetAll();
        Task<PlanEstudio> FullGetById(int id);
        Task<PlanEstudio> GetByCarreraAnno(int carreraId, int anno);
        Task<int> GetIdByCarreraAnno(int carreraId, int anno);
    }
}