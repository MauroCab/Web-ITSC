using Microsoft.AspNetCore.Mvc;
using Web_ITSC_BD.Data.Entity;
using Web_ITSC_Repositorio.Repositorios.Genericos;
using Web_ITSC_Shared.DTO;

namespace Web_ITSC_Repositorio.Repositorios
{
    public interface ICertificadoAlumnoRepositorio : IRepositorio<CertificadoAlumno>
    {
        byte[] GenerarCertificadoPDF(GetDatosCertificadosDTO datos);
        Task<ActionResult<Alumno>> SelectAlumnoByDoc(string documento);
        Task<ActionResult<GetDatosCertificadosDTO>> SelectDatosCertificado(string documento);
    }
}