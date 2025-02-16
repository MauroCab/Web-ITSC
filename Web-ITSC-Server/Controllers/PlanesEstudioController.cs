using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Web_ITSC_BD.Data.Entity;
using Web_ITSC_Repositorio.Repositorios.Genericos;
using Web_ITSC_Repositorio.Repositorios;
using Web_ITSC_Shared.DTO;

namespace Web_ITSC_Server.Controllers
{
    [ApiController]
    [Route("api/PlanEstudios")]
    public class PlanEstudiosController : ControllerBase
    {
        private readonly IPlanEstudioRepositorio eRepositorio;
        private readonly IMateriaEnPlanEstudioRepositorio materiaEnPlanRepositorio;
        private readonly ICarreraRepositorio carreraRepositorio;
        private readonly IRepositorio<Materia> mRepositorio;
        private readonly IMapper mapper;

        public PlanEstudiosController(IPlanEstudioRepositorio eRepositorio,
                                      IMateriaEnPlanEstudioRepositorio materiaEnPlanRepositorio,
                                      ICarreraRepositorio carreraRepositorio,
                                      IRepositorio<Materia> mRepositorio,
                                      IMapper mapper)
        {

            this.eRepositorio = eRepositorio;
            this.materiaEnPlanRepositorio = materiaEnPlanRepositorio;
            this.carreraRepositorio = carreraRepositorio;
            this.mRepositorio = mRepositorio;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<PlanEstudio>>> GetAll()
        {
            var PlanEstudios = await eRepositorio.FullGetAll();

            return Ok(PlanEstudios);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PlanEstudio>> GetById(int id)
        {
            var planEstudio = await eRepositorio.FullGetById(id);
            if (planEstudio == null) return NotFound();

            return Ok(planEstudio);
        }
        #region Peticiones Get

        //[HttpGet]
        //public async Task<ActionResult<List<PlanEstudio>>> Get()
        //{
        //    return await repositorio.Select();
        //}

        //[HttpGet("{id:int}")]
        //public async Task<ActionResult<PlanEstudio>> Get(int id)
        //{
        //    PlanEstudio? sel = await repositorio.SelectById(id);
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
        public async Task<ActionResult<int>> Post(CrearPlanEstudioDTO entidadDTO)
        {
            try
            {
                PlanEstudio entidad = mapper.Map<PlanEstudio>(entidadDTO);

                return await eRepositorio.Insert(entidad);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] PlanEstudio entidad)
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


            sel = mapper.Map<PlanEstudio>(entidad);

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
                return NotFound($"La persona {id} no existe");
            }
            PlanEstudio EntidadABorrar = new PlanEstudio();
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
