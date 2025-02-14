using Web_ITSC_BD.Data.Entity;
using Web_ITSC_Repositorio.Repositorios.Genericos;

namespace Web_ITSC_Repositorio.Repositorios
{
    public interface IInscripcionCarreraRepositorio : IRepositorio<InscripcionCarrera>
    {
        Task<List<InscripcionCarrera>> FullGetAll();
        Task<InscripcionCarrera> FullGetById(int id);
        Task<InscripcionCarrera> GetInscripcionByAlumnoYCarrera(int alumnoId, int carreraId);
        Task<List<InscripcionCarrera>> GetInscripcionesPorCarreraYcohorteOpcional(int carreraId, int? cohorte = null);
        Task<List<InscripcionCarrera>> ObtenerInscripcionesPorCarreraYcohorte(int carreraId, int? cohorte);
    }
}