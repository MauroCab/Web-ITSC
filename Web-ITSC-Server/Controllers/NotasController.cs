using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Web_ITSC_BD.Data.Entity;
using Web_ITSC_Repositorio.Repositorios;
using Web_ITSC_Shared.DTO;
using Web_ITSC_Shared.DTO.DTOs_sin_uso;

namespace Web_ITSC_Server.Controllers
{
    [ApiController]
    [Route("api/Nota")]
    public class NotaController : ControllerBase
    {
        private readonly INotaRepositorio eRepositorio;
        private readonly IMapper mapper;

        public NotaController(INotaRepositorio eRepositorio,
                              IMapper mapper)
        {

            this.eRepositorio = eRepositorio;
            this.mapper = mapper;
        }

        #region Peticiones Get
        [HttpGet]
        public async Task<ActionResult<List<Nota>>> GetAll()
        {
            var notas = await eRepositorio.FullGetAll();

            return Ok(notas);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Nota>> GetById(int id)
        {
            var nota = await eRepositorio.FullGetById(id);
            if (nota == null) return NotFound();

            return Ok(nota);
        }


        //NBT = NotasByTurno (Nombre de la función)
        [HttpGet("GetNotasByTurno/{turnoId}")]
        public async Task<ActionResult<List<GetNotaNBTDTO>>> GetNotasByTurno(int turnoId)
        {
            var notas = await eRepositorio.SelectNotasByTurno(turnoId);

            if (notas == null)
            {
                return NotFound("No se encontraron notas para el TurnoId proporcionado.");
            }

            return Ok(notas);
        }

        #endregion

        [HttpPost]
        public async Task<ActionResult<int>> Post(CrearNotaDTO entidadDTO)
        {
            try
            {
                Nota entidad = mapper.Map<Nota>(entidadDTO);

                return await eRepositorio.Insert(entidad);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] Nota entidad)
        {
            if (id != entidad.Id)
            {
                return BadRequest("Datos incorrectos");
            }
            var sel = await eRepositorio.SelectById(id);
            //sel = Seleccion

            if (sel == null)
            {
                return NotFound("No existe el tipo de documento buscado.");
            }


            sel = mapper.Map<Nota>(entidad);

            try
            {
                await eRepositorio.Update(id, sel);
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
            var existe = await eRepositorio.Existe(id);
            if (!existe)
            {
                return NotFound($"El Nota {id} no existe");
            }
            Nota EntidadABorrar = new Nota();
            EntidadABorrar.Id = id;

            if (await eRepositorio.Delete(id))
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
