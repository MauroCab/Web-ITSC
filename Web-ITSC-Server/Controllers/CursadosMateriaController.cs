using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web_ITSC_BD.Data.Entity;
using Web_ITSC_Repositorio.Repositorios;
using Web_ITSC_Shared.DTO.DTOs_sin_uso;

namespace Web_ITSC_Server.Controllers
{
    [ApiController]
    [Route("api/CursadoMateria")]
    public class CursadoMateriaController : ControllerBase
    {
        private readonly ICursadoMateriaRepositorio eRepositorio;
        private readonly IMapper mapper;

        public CursadoMateriaController(ICursadoMateriaRepositorio eRepositorio,
                                        IMapper mapper)
        {
            this.eRepositorio = eRepositorio;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CursadoMateria>>> GetAll()
        {
            var cursadoMaterias = await eRepositorio.FullGetAll();

            return Ok(cursadoMaterias);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CursadoMateria>> GetById(int id)
        {
            var cursadoMateria = await eRepositorio.FullGetById(id);
            if (cursadoMateria == null) return NotFound();

            return Ok(cursadoMateria);
        }
        #region Peticiones Get

        //[HttpGet]
        //public async Task<ActionResult<List<CursadoMateria>>> Get()
        //{
        //    return await repositorio.Select();
        //}

        //[HttpGet("{id:int}")]
        //public async Task<ActionResult<CursadoMateria>> Get(int id)
        //{
        //    CursadoMateria? sel = await repositorio.SelectById(id);
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
        public async Task<ActionResult<int>> Post(CrearCursadoMateriaDTO entidadDTO)
        {
            try
            {
                CursadoMateria entidad = mapper.Map<CursadoMateria>(entidadDTO);

                return await eRepositorio.Insert(entidad);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] CursadoMateria entidad)
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


            sel = mapper.Map<CursadoMateria>(entidad);

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
                return NotFound($"El CursadoMateria {id} no existe");
            }
            CursadoMateria EntidadABorrar = new CursadoMateria();
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
