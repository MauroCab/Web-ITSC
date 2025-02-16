using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Web_ITSC_BD.Data.Entity;
using Web_ITSC_Repositorio.Repositorios;
using Web_ITSC_Shared.DTO.DTOs_sin_uso;

namespace Web_ITSC_Server.Controllers
{
    [ApiController]
    [Route("api/Turno")]
    public class TurnoController : ControllerBase
    {
        private readonly ITurnoRepositorio eRepositorio;
        private readonly IMapper mapper;

        public TurnoController(ITurnoRepositorio eRepositorio,
                                  IMapper mapper)
        {

            this.eRepositorio = eRepositorio;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<Turno>>> GetAll()
        {
            var turnos = await eRepositorio.FullGetAll();

            return Ok(turnos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Turno>> GetById(int id)
        {
            var turno = await eRepositorio.FullGetById(id);
            if (turno == null) return NotFound();

            return Ok(turno);
        }
        #region Peticiones Get

        //[HttpGet]
        //public async Task<ActionResult<List<Turno>>> Get()
        //{
        //    return await repositorio.Select();
        //}

        //[HttpGet("{id:int}")]
        //public async Task<ActionResult<Turno>> Get(int id)
        //{
        //    Turno? sel = await repositorio.SelectById(id);
        //    if (sel == null)
        //    {
        //        return NotFound();
        //    }
        //    return sel;
        //}

        //[HttpGet("existe/{id:int}")]
        //public async Task<ActionResult<bool>> Existe(int id)
        //{
        //    var existe = await repositorio.Existe(id);
        //    return existe;

        //}

        #endregion

        [HttpPost]
        public async Task<ActionResult<int>> Post(CrearTurnoDTO entidadDTO)
        {
            try
            {
                Turno entidad = mapper.Map<Turno>(entidadDTO);

                return await eRepositorio.Insert(entidad);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] Turno entidad)
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


            sel = mapper.Map<Turno>(entidad);

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
                return NotFound($"El Turno {id} no existe");
            }
            Turno EntidadABorrar = new Turno();
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
