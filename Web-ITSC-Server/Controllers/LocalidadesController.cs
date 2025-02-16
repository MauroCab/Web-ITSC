using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Web_ITSC_BD.Data.Entity;
using Web_ITSC_Repositorio.Repositorios.Localizacion;
using Web_ITSC_Shared.DTO;

namespace Web_ITSC_Server.Controllers
{
    [ApiController]
    [Route("api/Localidades")]
    public class LocalidadesController : ControllerBase
    {
        private readonly ILocalidadRepositorio eRepositorio;
        private readonly IMapper mapper;

        public LocalidadesController(ILocalidadRepositorio eRepositorio, IMapper mapper)
        {
            this.eRepositorio = eRepositorio;
            this.mapper = mapper;
        }

        // Obtener todas las localidades
        [HttpGet]
        public async Task<ActionResult<List<GetLocalidadesDTO>>> GetAll()
        {
            var localidades = await eRepositorio.Select();
            var localidadesDto = mapper.Map<List<GetLocalidadesDTO>>(localidades);
            return Ok(localidadesDto);
        }

        // Obtener una localidad por su ID
        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetLocalidadesDTO>> Get(int id)
        {
            Localidad? localidad = await eRepositorio.SelectById(id);

            if (localidad == null)
            {
                return NotFound();
            }

            var localidadDto = mapper.Map<GetLocalidadesDTO>(localidad);
            return Ok(localidadDto);
        }

        // Verificar si la localidad existe por ID
        [HttpGet("existe/{id:int}")]
        public async Task<ActionResult<bool>> Existe(int id)
        {
            var existe = await eRepositorio.Existe(id);
            return existe;
        }

        // Este método obtiene las localidades de un departamento dado un DepartamentoId
        [HttpGet("porDepartamento/{DepartamentoId:int}")]
        public async Task<ActionResult<List<GetLocalidadesDTO>>> GetLocalidadesPorDepartamento(int DepartamentoId)
        {
            // Obtenemos las localidades asociadas al DepartamentoId
            var localidades = await eRepositorio.SelectLocalidadesPorDepartamentoAsync(DepartamentoId);

            if (localidades == null || !localidades.Any())
            {
                return NotFound("No se encontraron localidades para este departamento.");
            }

            // Mapeamos las localidades a DTOs (Data Transfer Objects)
            var localidadesDto = mapper.Map<List<GetLocalidadesDTO>>(localidades);

            // Retornamos las localidades como respuesta
            return Ok(localidadesDto);
        }


        // Crear una nueva localidad
        [HttpPost]
        public async Task<ActionResult<int>> Post(CrearLocalidadesDTO localidadDTO)
        {
            try
            {
                // Mapeamos el DTO CrearLocalidadesDTO a la entidad Localidad
                Localidad localidad = mapper.Map<Localidad>(localidadDTO);

                // Insertamos la nueva localidad
                int result = await eRepositorio.Insert(localidad);
                return Ok(result);  // Regresar el ID de la nueva localidad insertada
            }
            catch (Exception e)
            {
                // Log del error detallado
                Console.WriteLine($"Error al insertar localidad: {e.Message}");
                Console.WriteLine($"Detalles del error: {e.InnerException?.Message}");
                return BadRequest($"Ocurrió un error: {e.Message}");
            }
        }

        // Actualizar una localidad existente
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] Localidad localidad)
        {
            if (id != localidad.Id)
            {
                return BadRequest("Datos incorrectos");
            }

            var localidadExistente = await eRepositorio.SelectById(id);

            if (localidadExistente == null)
            {
                return NotFound("No existe la localidad buscada.");
            }

            localidadExistente = mapper.Map<Localidad>(localidad);

            try
            {
                await eRepositorio.Update(id, localidadExistente);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // Eliminar una localidad
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await eRepositorio.Existe(id);
            if (!existe)
            {
                return NotFound($"La localidad con ID {id} no existe.");
            }

            Localidad localidadABorrar = new Localidad { Id = id };

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
