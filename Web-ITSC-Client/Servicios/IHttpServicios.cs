using Web_ITSC_BD.Data.Entity;

namespace Web_ITSC_Client.Servicios
{
    public interface IHttpServicios
    {
        Task<HttpRespuesta<object>> Delete(string url);
        Task<HttpRespuesta<O>> Get<O>(string url);
        Task<HttpRespuesta<object>> GetNotasByTurno(string url);
        Task<HttpRespuesta<object>> Post<O>(string url, O entidad);
        Task<HttpRespuesta<object>> Put<O>(string url, O entidad);
        Task<List<Departamento>> SelectDepartamentosPorProvinciaAsync(int ProvinciaId);
        Task<List<Localidad>> SelectLocalidadesPorDepartamentoAsync(int DepartamentoId);
        Task<List<Pais>> SelectPaisesAsync();
        Task<List<Provincia>> SelectProvinciasPorPaisAsync(int PaisId);
    }
}