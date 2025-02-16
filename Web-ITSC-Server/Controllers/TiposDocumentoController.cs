using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Web_ITSC_BD.Data.Entity;
using Web_ITSC_Repositorio.Repositorios;
using Web_ITSC_Shared.DTO;

namespace Web_ITSC_Server.Controllers
{
    [ApiController]
    [Route("api/TipoDocumento")]
    public class TipoDocumentosController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ITipoDocumentoRepositorio eRepositorio;

        //______________________________________________________________________________
        public TipoDocumentosController(ITipoDocumentoRepositorio eRepositorio,
                                        IMapper mapper)
        {
            this.eRepositorio = eRepositorio;
            this.mapper = mapper;
        }

        //_______________________________________________________________________________

        [HttpGet]
        public async Task<ActionResult<List<GetTipoDocumentoDTO>>> Get()
        {
            try
            {
                var ListTdocSelect = await eRepositorio.Select();
                var ListTdoc = mapper.Map<List<TipoDocumento>>(ListTdocSelect);
                return Ok(ListTdoc);
            }
            catch (Exception ex)
            {
                // Registrar el error para el diagnóstico
                Console.WriteLine($"Error en el método GET: {ex.Message}");
                return StatusCode(500, $"Ocurrió un error interno: {ex.Message}");
            }
        }
        //_________________________________________________________________

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetTipoDocumentoDTO>> Get(int id)
        {
            var entidad = await eRepositorio.SelectById(id);

            if (entidad == null)
            {
                return NotFound("El tdocumento no existe.");
            }

            // Mapea la entidad a DTO
            var TdocumentoDTO = mapper.Map<TipoDocumento>(entidad);
            return Ok(TdocumentoDTO);
        }
        //________________________________________________________________
        [HttpGet("existe/{id:int}")]
        public async Task<ActionResult<bool>> Existe(int id)
        {
            var existe = await eRepositorio.Existe(id);
            return existe;
        }
        //________________________________________________________________


        [HttpPost]
        public async Task<ActionResult<int>> Post(CrearTipoDocumentoDTO entidadDTO)
        {
            try
            {
                TipoDocumento entidad = mapper.Map<TipoDocumento>(entidadDTO);
                return await eRepositorio.Insert(entidad);
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    return BadRequest($"Error: {e.Message}. Inner Exception: {e.InnerException.Message}");
                }
                return BadRequest(e.Message);
            }
        }
        //______________________________________________________________________________________

        //[HttpPut("{id:int}")]
        //public async Task<ActionResult> Put(int id, [FromBody] PutTdocumentoDTO dto)
        //{
        //    // Validar el modelo
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState); // Devuelve errores de validación
        //    }

        //    var sel = await repositorio.SelectById(id);
        //    if (sel == null)
        //    {
        //        return NotFound("El tdocumento no existe.");
        //    }

        //    // Mapea solo los campos que cambiaron
        //    mapper.Map(dto, sel);

        //    try
        //    {
        //        var actualizado = await repositorio.Update(id, sel);
        //        if (actualizado)
        //        {
        //            return Ok();
        //        }
        //        else
        //        {
        //            return BadRequest("No se pudo actualizar el tdocumento.");
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}
        //_________________________________________________________________________________________

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await eRepositorio.Existe(id);
            if (!existe)
            {
                return NotFound($"La persona {id} no existe");
            }
            TipoDocumento EntidadABorrar = new TipoDocumento();
            EntidadABorrar.Id = id;

            if (await eRepositorio.Delete(id))
            {
                return Ok($"El tipo de documento {id} fue eliminado");
            }
            else
            {
                return BadRequest();
            }

        }

    }
}
