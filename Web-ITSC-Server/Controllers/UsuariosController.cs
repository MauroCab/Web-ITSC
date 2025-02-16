using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Web_ITSC_BD.Data.Entity;
using Web_ITSC_Repositorio.Repositorios;
using Web_ITSC_Shared.DTO;

namespace Web_ITSC_Server.Controllers
{
    [ApiController]
    [Route("api/Usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio eRepositorio;
        private readonly IMapper mapper;


        public UsuarioController(IUsuarioRepositorio eRepositorio,
                                  IMapper mapper)
        {

            this.eRepositorio = eRepositorio;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<GetUsuarioDTO>>> GetAll()
        {// Obtener todos los usuarios
            var usuarios = await eRepositorio.FullGetAll();

            // Usar AutoMapper para mapear la lista de 'Usuario' a 'GetUsuarioDTO'
            var usuariosDTO = mapper.Map<List<GetUsuarioDTO>>(usuarios);

            // Devolver la respuesta mapeada
            return Ok(usuariosDTO);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Usuario>> GetById(int id)
        {
            var usuario = await eRepositorio.FullGetById(id);
            if (usuario == null) return NotFound();

            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(CrearUsuarioDTO entidadDTO)
        {
            try
            {
                Usuario entidad = mapper.Map<Usuario>(entidadDTO);

                return await eRepositorio.Insert(entidad);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] Usuario entidad)
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


            sel = mapper.Map<Usuario>(entidad);

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
                return NotFound($"El Usuario {id} no existe");
            }
            Usuario EntidadABorrar = new Usuario();
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
