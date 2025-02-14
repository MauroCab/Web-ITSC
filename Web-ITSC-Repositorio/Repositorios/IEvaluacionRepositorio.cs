using Web_ITSC_BD.Data.Entity;
using Web_ITSC_Repositorio.Repositorios.Genericos;

namespace Web_ITSC_Repositorio.Repositorios
{
    public interface IEvaluacionRepositorio : IRepositorio<Evaluacion>
    {
        Task<List<Evaluacion>> FullGetAll();
        Task<Evaluacion> FullGetById(int id);
    }
}