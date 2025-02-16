using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Web_ITSC_BD.Data.Entity;
using Web_ITSC_Repositorio.Repositorios.Genericos;
using Web_ITSC_Repositorio.Repositorios;
using Web_ITSC_Shared.DTO;

namespace Web_ITSC_Server.Controllers
{
    [ApiController]
    [Route("api/Personas")]
    public class PersonasController : ControllerBase
    {
        private readonly IRepositorio<Persona> repositorio;
        private readonly IPersonaRepositorio eRepositorio;
        private readonly IMapper mapper;

        public PersonasController(IRepositorio<Persona> repositorio,
                                  IPersonaRepositorio eRepositorio,
                                  IMapper mapper)
        {
            this.repositorio = repositorio;
            this.eRepositorio = eRepositorio;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<GetPersonaDTO>>> GetAll()
        {
            // Obtener todos los alumnos
            var personas = await eRepositorio.FullGetAll();

            // Usar AutoMapper para mapear la lista de 'Usuario' a 'GetUsuarioDTO'
            var personasDTO = mapper.Map<List<GetPersonaDTO>>(personas);

            // Devolver la respuesta mapeada
            return Ok(personasDTO); ;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetPersonaDTO>> GetById(int id)
        {
            var persona = await eRepositorio.FullGetById(id);
            if (persona == null) return NotFound();

            return Ok(persona);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] CrearPersonaDTO PersonaDTO)
        {
            var persona = mapper.Map<Persona>(PersonaDTO);
            await eRepositorio.FullInsert(persona);
            return persona.Id;
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] CrearPersonaDTO PersonaDTO)
        {
            var persona = mapper.Map<Persona>(PersonaDTO);

            if (id != persona.Id) return BadRequest();

            await eRepositorio.FullUpdate(persona);
            return Ok();
        }

        //----------------------------------------------------------------------------------------------------------------



        [HttpGet("existe/{id:int}")]
        public async Task<ActionResult<bool>> Existe(int id)
        {
            var existe = await repositorio.Existe(id);
            return existe;

        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await repositorio.Existe(id);
            if (!existe)
            {
                return NotFound($"La persona {id} no existe");
            }
            Persona EntidadABorrar = new Persona();
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
