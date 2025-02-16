using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Web_ITSC_BD.Data.Entity;
using Web_ITSC_Repositorio.Repositorios;
using Web_ITSC_Shared.DTO;

namespace Web_ITSC_Server.Controllers
{
    [ApiController]
    [Route("api/MateriaEnPlanEstudio")]
    public class MateriaEnPlanEstudioController : ControllerBase
    {
        private readonly IMateriaEnPlanEstudioRepositorio eRepositorio;
        private readonly IPlanEstudioRepositorio planEstudioRepositorio;
        private readonly ICarreraRepositorio carreraRepositorio;
        private readonly IMapper mapper;

        public MateriaEnPlanEstudioController(IMateriaEnPlanEstudioRepositorio eRepositorio,
                                              IPlanEstudioRepositorio planEstudioRepositorio,
                                              ICarreraRepositorio carreraRepositorio,
                                              IMapper mapper)
        {

            this.eRepositorio = eRepositorio;
            this.planEstudioRepositorio = planEstudioRepositorio;
            this.carreraRepositorio = carreraRepositorio;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<MateriaEnPlanEstudio>>> GetAll()
        {
            var MateriasEnPlanDeEstudio = await eRepositorio.FullGetAll();

            return Ok(MateriasEnPlanDeEstudio);
        }

        [HttpGet("{byPlan}")]
        public async Task<ActionResult<List<MateriaEnPlanEstudio>>> GetByPlan(int planEstudioId)
        {
            var MateriasEnPlanDeEstudio = await eRepositorio.FullGetByPlanEstudio(planEstudioId);

            return Ok(MateriasEnPlanDeEstudio);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MateriaEnPlanEstudio>> GetById(int id)
        {
            var MateriaEnPlanDeEstudio = await eRepositorio.FullGetById(id);
            if (MateriaEnPlanDeEstudio == null) return NotFound();

            return Ok(MateriaEnPlanDeEstudio);
        }

        [HttpGet("Simple{id:int}")]
        public async Task<ActionResult<MateriaEnPlanEstudio>> GetSimpleById(int id)
        {
            var MateriaEnPlanDeEstudio = await eRepositorio.SelectById(id);
            if (MateriaEnPlanDeEstudio == null) return NotFound();

            return Ok(MateriaEnPlanDeEstudio);
        }

        [HttpGet("DTO/{id:int}")]
        public async Task<ActionResult<TraerMateriaEnPlanDTO>> GetDTOById(int id)
        {
            var registro = await eRepositorio.SelectById(id);
            if (registro == null)
            {
                return NotFound("El registro solicitado no existe.");
            }

            // Mapeo al DTO (si SelectById no devuelve ya un DTO)
            var dto = new TraerMateriaEnPlanDTO
            {
                Id = registro.Id,
                MateriaId = registro.MateriaId,
                HrsRelojAnuales = registro.HrsRelojAnuales,
                HrsCatedraSemanales = registro.HrsCatedraSemanales,
                Anual_Cuatrimestral = registro.Anual_Cuatrimestral,
                Anno = registro.Anno,
                NumeroOrden = registro.NumeroOrden,
                NombreMateria = registro.Materia.Nombre,
                AnnoPlanEstudio = registro.PlanEstudio.Anno
            };

            return Ok(dto);
        }
        #region Peticiones Get

        //[HttpGet]
        //public async Task<ActionResult<List<MateriaEnPlanEstudio>>> Get()
        //{
        //    return await repositorio.Select();
        //}

        //[HttpGet("{id:int}")]
        //public async Task<ActionResult<MateriaEnPlanEstudio>> Get(int id)
        //{
        //    MateriaEnPlanEstudio? sel = await repositorio.SelectById(id);
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
        public async Task<ActionResult<int>> Post(CrearMateriaEnPlanEstudioDTO entidadDTO)
        {
            try
            {
                MateriaEnPlanEstudio entidad = mapper.Map<MateriaEnPlanEstudio>(entidadDTO);

                return await eRepositorio.Insert(entidad);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] MateriaEnPlanEstudio entidad)
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


            sel = mapper.Map<MateriaEnPlanEstudio>(entidad);

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

        [HttpPut("Actualizar{id:int}")]
        public async Task<ActionResult> PutWithDTO(TraerMateriaEnPlanDTO entidadDTO)
        {
            var sel = await eRepositorio.SelectById(entidadDTO.Id);
            if (sel == null)
            {
                return NotFound("No se encontró el registro a Modificar.");
            }
            sel.HrsCatedraSemanales = entidadDTO.HrsCatedraSemanales;
            sel.Anual_Cuatrimestral = entidadDTO.Anual_Cuatrimestral;
            sel.Anno = entidadDTO.Anno;
            sel.HrsRelojAnuales = entidadDTO.HrsRelojAnuales;
            sel.NumeroOrden = entidadDTO.NumeroOrden;

            try
            {
                await eRepositorio.Update(sel.Id, sel);
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
                return NotFound($"El MateriaEnPlanEstudio {id} no existe");
            }
            MateriaEnPlanEstudio EntidadABorrar = new MateriaEnPlanEstudio();
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

        [HttpGet("GetListByPlan")]
        public async Task<ActionResult<List<TraerMateriaEnPlanDTO>>> GetByCarreraAndAnno(string Carrera, int Anno)
        {

            var a = await carreraRepositorio.GetByNombre(Carrera);
            var b = await planEstudioRepositorio.GetIdByCarreraAnno(a, Anno);
            if (b != 0)
            {
                var resultado = await eRepositorio.FullGetByPlanEstudio(b);

                if (resultado != null)
                {
                    return Ok(resultado);
                }
                else
                {
                    return NotFound($"No se encontraron Materias en el Plan de Estudio del año {Anno}");
                }
            }
            else
            {
                return NotFound("No se encontró un Plan de Estudio");
            }
        }

    }
}
