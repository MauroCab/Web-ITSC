using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Web_ITSC_BD.Data.Entity;
using Web_ITSC_Repositorio.Repositorios;
using Web_ITSC_Shared.DTO;

namespace Web_ITSC_Server.Controllers
{
    [ApiController]
    [Route("api/CertificadoAlumnos")]
    public class CertificadoAlumnosController : ControllerBase
    {
        private readonly ICertificadoAlumnoRepositorio repositorio;
        private readonly IMapper mapper;

        public CertificadoAlumnosController(ICertificadoAlumnoRepositorio repositorio,
                                  IMapper mapper)
        {

            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        #region Peticiones Get

        [HttpGet]
        public async Task<ActionResult<List<CertificadoAlumno>>> Get()
        {
            return await repositorio.Select();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CertificadoAlumno>> Get(int id)
        {
            CertificadoAlumno? sel = await repositorio.SelectById(id);
            if (sel == null)
            {
                return NotFound();
            }
            return sel;
        }

        [HttpGet("existe/")]
        public async Task<ActionResult<bool>> Existe(int id)
        {
            var existe = await repositorio.Existe(id);
            return existe;

        }

        #endregion

        //A pesar de lo que parezca, funciona como un POST
        [HttpGet("generarCertificado")]
        public async Task<ActionResult> GenerarCertificado([Required] string documento)
        {
            var alumno = await repositorio.SelectDatosCertificado(documento);

            if (alumno == null)
            {
                return NotFound("No se encontro alumno.");
            }
            if (alumno.Value != null)
            {
                var pdfData = repositorio.GenerarCertificadoPDF(alumno.Value);

                if (pdfData.Any())
                {
                    var EntidadAlumno = await repositorio.SelectAlumnoByDoc(alumno.Value.NroDocumento);
                    var entidadDTO = new CrearCertificadoAlumnoDTO(EntidadAlumno.Value, DateTime.Now);
                    var entidad = mapper.Map<CertificadoAlumno>(entidadDTO);

                    await repositorio.Insert(entidad);
                }

                return File(pdfData, "application/pdf", "Certificado.pdf");
            }
            else
            {
                return NotFound("No se encontró alumno.");
            }

        }




        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] CertificadoAlumno entidad)
        {
            if (id != entidad.Id)
            {
                return BadRequest("Datos incorrectos");
            }
            var sel = await repositorio.SelectById(id);
            //sel = Seleccion

            if (sel == null)
            {
                return NotFound("No existe el tipo de documento buscado.");
            }


            sel = mapper.Map<CertificadoAlumno>(entidad);

            try
            {
                await repositorio.Update(id, sel);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await repositorio.Existe(id);
            if (!existe)
            {
                return NotFound($"La persona {id} no existe");
            }
            CertificadoAlumno EntidadABorrar = new CertificadoAlumno();
            EntidadABorrar.Id = id;

            if (await repositorio.Delete(id))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }



    }
}
