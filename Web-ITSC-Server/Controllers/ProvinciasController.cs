using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Web_ITSC_BD.Data.Entity;
using Web_ITSC_Repositorio.Repositorios.Localizacion;
using Web_ITSC_Shared.DTO;

namespace Web_ITSC_Server.Controllers
{
    public class ProvinciasController : ControllerBase
    {
        private readonly IProvinciaRepositorio eRepositorio;
        private readonly IMapper mapper;

        public ProvinciasController (IProvinciaRepositorio eRepositorio, IMapper mapper)
        {
            this.eRepositorio = eRepositorio;
            this.mapper = mapper;
        }

        // Obtener todos las provincias
        [HttpGet]
        public async Task<ActionResult<List<GetProvinciaDTO>>> GetAll()
        {
            var provincias = await eRepositorio.Select();

            var provinciasDto = mapper.Map<List<GetProvinciaDTO>>(provincias);
            return Ok(provinciasDto);
        }

        // Obtener un provincia por su ID
        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetProvinciaDTO>> Get(int id)
        {
            // Seleccionamos la entidad Pais
            Provincia? provincia = await eRepositorio.SelectById(id);

            if (provincia == null)
            {
                return NotFound();
            }

            // Mapeamos la entidad Provincia a GetProvDTO
            var provinciaDto = mapper.Map<GetProvinciaDTO>(provincia);

            return Ok(provinciaDto);
        }

        // Verificar si el pais existe por ID
        [HttpGet("existe/{id:int}")]
        public async Task<ActionResult<bool>> Existe(int id)
        {
            var existe = await eRepositorio.Existe(id);
            return existe;
        }

        [HttpGet("porPais/{PaisId:int}")]
        public async Task<ActionResult<List<GetProvinciaDTO>>> GetProvinciasPorPais(int PaisId)
        {
            // Agregar depuración para ver el PaisId
            Console.WriteLine($"Recibiendo solicitud para País ID: {PaisId}");

            var provincias = await eRepositorio.SelectProvinciasPorPaisAsync(PaisId);

            // Verificar si se encontraron provincias
            if (provincias == null || !provincias.Any())
            {
                Console.WriteLine("No se encontraron provincias para este país.");
                return NotFound("No se encontraron provincias para este país.");
            }

            // Mapeamos las provincias a DTOs (Data Transfer Objects)
            var provinciasDto = mapper.Map<List<GetProvinciaDTO>>(provincias);

            // Retornamos las provincias como respuesta
            Console.WriteLine($"Se encontraron {provinciasDto.Count} provincias.");
            return Ok(provinciasDto);
        }


        // Método para obtener los departamentos de una provincia
        [HttpGet("{ProvinciaId:int}/departamentos")]
        public async Task<ActionResult<List<GetDepartamentosDTO>>> GetDepartamentosPorProvincia(int ProvinciaId)
        {
            // Consultamos los departamentos por el ID de la provincia
            var departamentos = await eRepositorio.SelectDepartamentosPorProvincia(ProvinciaId);

            // Si no hay departamentos, devolvemos un 404
            if (departamentos == null || !departamentos.Any())
            {
                return NotFound("No se encontraron departamentos para esta provincia.");
            }

            // Mapeamos los departamentos a DTO
            var departamentosDto = mapper.Map<List<GetDepartamentosDTO>>(departamentos);

            // Retornamos los departamentos
            return Ok(departamentosDto);
        }






        // Crear un nuevo pais
        [HttpPost]
        public async Task<ActionResult<int>> Post(CrearProvinciaDTO provinciaDTO)
        {
            try
            {
                // Mapeamos el DTO CrearProvinciaDTO a la entidad Provincia
                Provincia provincia = mapper.Map<Provincia>(provinciaDTO);

                // Insertamos el nuevo pais
                int result = await eRepositorio.Insert(provincia);
                return Ok(result);  // Regresar el ID de la nueva provincia insertada
            }
            catch (Exception e)
            {
                // Log del error detallado
                Console.WriteLine($"Error al insertar provincia: {e.Message}");
                Console.WriteLine($"Detalles del error: {e.InnerException?.Message}");
                return BadRequest($"Ocurrió un error: {e.Message}");
            }
        }


        // Actualizar un pais existente
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] Provincia provincia)
        {
            if (id != provincia.Id)
            {
                return BadRequest("Datos incorrectos");
            }

            var provinciaExistente = await eRepositorio.SelectById(id);

            if (provinciaExistente == null)
            {
                return NotFound("No existe el pais buscado.");
            }

            provinciaExistente = mapper.Map<Provincia>(provincia);

            try
            {
                await eRepositorio.Update(id, provinciaExistente);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // Eliminar un pais
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await eRepositorio.Existe(id);
            if (!existe)
            {
                return NotFound($"La provincia con ID {id} no existe.");
            }

            Provincia provinciaABorrar = new Provincia { Id = id };

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
