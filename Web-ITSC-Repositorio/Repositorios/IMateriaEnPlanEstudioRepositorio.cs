using Web_ITSC_BD.Data.Entity;
using Web_ITSC_Repositorio.Repositorios.Genericos;
using Web_ITSC_Shared.DTO;

namespace Web_ITSC_Repositorio.Repositorios
{
    public interface IMateriaEnPlanEstudioRepositorio : IRepositorio<MateriaEnPlanEstudio>
    {
        Task DeleteByMateriaId(int materiaId);
        Task DeleteByPlanEstudioId(int planEstudioId);
        Task<List<MateriaEnPlanEstudio>> FullGetAll();
        Task<MateriaEnPlanEstudio> FullGetById(int id);
        Task<List<MateriaEnPlanEstudio>> FullGetByMateria(int materiaId);
        Task<List<TraerMateriaEnPlanDTO>> FullGetByPlanEstudio(int planEstudioId);
    }
}