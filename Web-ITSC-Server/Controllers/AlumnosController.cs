using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Web_ITSC_BD.Data.Entity;
using Web_ITSC_Repositorio.Repositorios;
using Web_ITSC_Repositorio.Repositorios.Localizacion;
using Web_ITSC_Shared.DTO;

namespace Web_ITSC_Server.Controllers
{
    [ApiController]
    [Route("/api/Alumnos")]
    public class AlumnosController : ControllerBase
    {
        private readonly IAlumnoRepositorio eRepositorio;
        private readonly IMapper mapper;
        private readonly IPersonaRepositorio personaRepositorio;
        private readonly IUsuarioRepositorio usuarioRepositorio;
        private readonly IInscripcionCarreraRepositorio inscripcionCarreraRepositorio;
        private readonly ICarreraRepositorio carreraRepositorio;
        private readonly ITipoDocumentoRepositorio tipoDocumentoRepositorio;
        private readonly IPaisRepositorio paisRepositorio;
        private readonly IProvinciaRepositorio provinciaRepositorio;
        private readonly IDepartamentoRepositorio departamentoRepositorio;
        private readonly ILocalidadRepositorio localidadRepositorio;

        // Constructor
        public AlumnosController(IAlumnoRepositorio eRepositorio,
                                  IMapper mapper,
                                  IPersonaRepositorio personaRepositorio,
                                  IUsuarioRepositorio usuarioRepositorio,
                                  IInscripcionCarreraRepositorio inscripcionCarreraRepositorio,
                                  ICarreraRepositorio carreraRepositorio,
                                  ITipoDocumentoRepositorio tipoDocumentoRepositorio,
                                  IPaisRepositorio paisRepositorio,
                                  IProvinciaRepositorio provinciaRepositorio,
                                  IDepartamentoRepositorio departamentoRepositorio,
                                  ILocalidadRepositorio localidadRepositorio)
        {
            this.eRepositorio = eRepositorio;
            this.mapper = mapper;
            this.personaRepositorio = personaRepositorio;
            this.usuarioRepositorio = usuarioRepositorio;
            this.inscripcionCarreraRepositorio = inscripcionCarreraRepositorio;
            this.carreraRepositorio = carreraRepositorio;
            this.tipoDocumentoRepositorio = tipoDocumentoRepositorio;
            this.paisRepositorio = paisRepositorio;
            this.provinciaRepositorio = provinciaRepositorio;
            this.departamentoRepositorio = departamentoRepositorio;
            this.localidadRepositorio = localidadRepositorio;


        }

        // Obtener todos los alumnos
        [HttpGet]
        public async Task<ActionResult<List<GetAlumnoDTO>>> GetAll()
        {
            // Obtener todos los alumnos
            var alumnos = await eRepositorio.FullGetAll();

            // Usar AutoMapper para mapear la lista de 'Usuario' a 'GetUsuarioDTO'
            var alumnosDTO = mapper.Map<List<GetAlumnoDTO>>(alumnos);

            // Devolver la respuesta mapeada
            return Ok(alumnosDTO);

        }

        // Obtener alumno por ID

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetAlumnoDTO>> GetById(int id)
        {
            var alumno = await eRepositorio.FullGetById(id);
            if (alumno == null) return NotFound();
            return Ok(alumno);
        }


        //GET: api/alumnos/buscar
        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<BuscarAlumnoDTO>>> BuscarAlumnos(
            [FromQuery] string? nombre,
            [FromQuery] string? apellido,
            [FromQuery] string? documento,
            [FromQuery] int? cohorte)
        {
            var alumnos = await eRepositorio.BuscarAlumnos(nombre, apellido, documento, cohorte);

            if (alumnos == null)
            {
                return NotFound("No se encontraron alumnos.");
            }

            return Ok(alumnos.Value);
        }


        //--------editar alumno------


        [HttpGet("documento/{documento}")]
        public async Task<ActionResult<EditarAlumnoDTO>> GetAlumnoPorDocumento(string documento)
        {
            var alumno = await eRepositorio.GetAlumnoPorDocumento(documento);

            if (alumno == null)
            {
                return NotFound();
            }

            var dto = mapper.Map<EditarAlumnoDTO>(alumno);
            return Ok(dto);
        }

        [HttpPut("documento/{documento}")]
        public async Task<IActionResult> UpdateAlumno(string documento, EditarAlumnoDTO dto)
        {
            // Obtener el alumno por documento utilizando el repositorio
            var alumno = await eRepositorio.GetAlumnoPorDocumento(documento);

            if (alumno == null)
            {
                return NotFound();
            }

            // Asegurarse de cargar las entidades relacionadas
            alumno.Usuario = await usuarioRepositorio.FullGetById(alumno.UsuarioId);
            alumno.Usuario.Persona = await personaRepositorio.FullGetById(alumno.Usuario.PersonaId);

            // Mapeo solo de las propiedades necesarias para actualizar
            // Aquí aseguramos que no estamos modificando las claves de Usuario ni Persona
            mapper.Map(dto, alumno);  // Solo mapeamos el alumno
            mapper.Map(dto, alumno.Usuario.Persona);  // Solo mapeamos Persona
            mapper.Map(dto, alumno.Usuario);  // Solo mapeamos Usuario, sin cambiar el Id

            // Actualizar el alumno en el repositorio
            var result = await eRepositorio.Update(alumno);

            if (!result)
            {
                return BadRequest("No se pudo actualizar el alumno.");
            }

            return NoContent();
        }



        //--------------------------------------------------------------------



        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] CrearAlumnoDTO crearAlumnoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validar CUIL y DNI
            if (!ValidarCUIL(crearAlumnoDTO.CUIL, crearAlumnoDTO.Documento))
            {
                return BadRequest("El CUIL no es válido o no coincide con el DNI.");
            }

            // Validar unicidad en la base de datos
            var alumnoExistente = await eRepositorio.GetAlumnoPorDocumento(crearAlumnoDTO.Documento);
            if (alumnoExistente != null)
            {
                return BadRequest("El DNI ya está registrado.");
            }

            var cuilExistente = await eRepositorio.GetAlumnoPorCUIL(crearAlumnoDTO.CUIL);
            if (cuilExistente != null)
            {
                return BadRequest("El CUIL ya está registrado.");
            }


            // Paso 1: Validar que la carrera exista
            var carrera = await carreraRepositorio.GetCarreraByIdAsync(crearAlumnoDTO.CarreraId);
            if (carrera == null)
            {
                return BadRequest("La carrera especificada no existe.");
            }

            // Paso 2: Validar que el TipoDocumentoId sea válido
            var tipoDocumento = await tipoDocumentoRepositorio.GetByIdAsync(crearAlumnoDTO.TipoDocumentoId);
            if (tipoDocumento == null)
            {
                return BadRequest("El tipo de documento especificado no existe.");
            }

            // Validar que las entidades de país, provincia, departamento y localidad existan
            var pais = await paisRepositorio.GetByIdAsync(crearAlumnoDTO.PaisId);
            if (pais == null)
            {
                return BadRequest("El país especificado no existe.");
            }

            var provincia = await provinciaRepositorio.GetByIdAsync(crearAlumnoDTO.ProvinciaId);
            if (provincia == null)
            {
                return BadRequest("La provincia especificada no existe.");
            }

            var departamento = await departamentoRepositorio.GetByIdAsync(crearAlumnoDTO.DepartamentoId);
            if (departamento == null)
            {
                return BadRequest("El departamento especificado no existe.");
            }

            var localidad = await localidadRepositorio.GetByIdAsync(crearAlumnoDTO.LocalidadId);
            if (localidad == null)
            {
                return BadRequest("La localidad especificada no existe.");
            }

            // Paso 3: Crear la Persona
            var persona = new Persona
            {
                Nombre = crearAlumnoDTO.Nombre,
                Apellido = crearAlumnoDTO.Apellido,
                Documento = crearAlumnoDTO.Documento,
                TipoDocumentoId = crearAlumnoDTO.TipoDocumentoId,
                Domicilio = crearAlumnoDTO.Domicilio,
                Telefono = crearAlumnoDTO.Telefono
            };

            // Usamos el repositorio de Persona para agregarla a la base de datos
            await personaRepositorio.Insert(persona);

            // Paso 4: Crear el Usuario
            var crearUsuarioDTO = new CrearUsuarioDTO
            {
                Email = crearAlumnoDTO.Email,
                Contrasena = crearAlumnoDTO.Contrasena // Asegúrate de encriptar la contraseña
            };

            var usuario = new Usuario
            {
                Email = crearUsuarioDTO.Email,
                Contrasena = crearUsuarioDTO.Contrasena, // Asegúrate de encriptar la contraseña antes de guardar
                PersonaId = persona.Id // Asociamos la Persona al Usuario
            };

            // Usamos el repositorio de Usuario para agregarlo a la base de datos
            await usuarioRepositorio.Insert(usuario);

            // Paso 5: Crear el Alumno
            var alumno = mapper.Map<Alumno>(crearAlumnoDTO);
            alumno.UsuarioId = usuario.Id;  // Asignamos el UsuarioId después de crear el Usuario

            // Asociar los IDs de país, provincia, departamento y localidad al alumno
            alumno.PaisId = crearAlumnoDTO.PaisId;
            alumno.ProvinciaId = crearAlumnoDTO.ProvinciaId;
            alumno.DepartamentoId = crearAlumnoDTO.DepartamentoId;
            alumno.LocalidadId = crearAlumnoDTO.LocalidadId;

            // Usamos el repositorio de Alumno para agregarlo a la base de datos
            await eRepositorio.Insert(alumno);

            // Paso 6: Validar si el alumno ya está inscrito en esta carrera
            var inscripcionExistente = await inscripcionCarreraRepositorio
                .GetInscripcionByAlumnoYCarrera(alumno.Id, crearAlumnoDTO.CarreraId);

            if (inscripcionExistente != null)
            {
                return BadRequest("El alumno ya está inscrito en esta carrera.");
            }

            // Paso 7: Inscribir al alumno en la carrera
            var inscripcionCarrera = new InscripcionCarrera
            {
                AlumnoId = alumno.Id,
                CarreraId = crearAlumnoDTO.CarreraId,
                Cohorte = DateTime.Now.Year, // O la fecha que sea adecuada
                EstadoAlumno = "Activo",
                Legajo = "legajo",
                LibroMatriz = "Libro Matriz",
                NroOrdenAlumno = "NumOrdenAlumno"
            };

            await inscripcionCarreraRepositorio.Insert(inscripcionCarrera); // Inscribir al alumno en la carrera

            // Mapea el Alumno a GetAlumnoDTO para la respuesta
            var getAlumnoDTO = mapper.Map<GetAlumnoDTO>(alumno);

            // Retorna el nuevo alumno creado, con un código HTTP 201 (creado)
            return CreatedAtAction(nameof(GetById), new { id = alumno.Id }, getAlumnoDTO);
        }

        private bool ValidarCUIL(string cuil, string dni)
        {
            var regex = new Regex(@"^\d{2}-\d{8}-\d{1}$");
            if (!regex.IsMatch(cuil))
            {
                return false;
            }

            var partes = cuil.Split('-');
            if (partes[1] != dni)
            {
                return false;
            }

            return true;
        }


        // Actualizar alumno
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] Alumno entidad)
        {
            if (id != entidad.Id)
            {
                return BadRequest("Datos incorrectos");
            }

            // Verifica si la entidad existe antes de continuar
            var sel = await eRepositorio.SelectById(id);
            if (sel == null)
            {
                return NotFound($"El alumno con ID {id} no existe.");
            }

            // Actualiza la entidad
            mapper.Map(entidad, sel); // Mapea los nuevos valores a la entidad existente

            try
            {
                // Usa `eRepositorio.Update` para actualizar
                var updated = await eRepositorio.Update(id, sel);
                if (updated)
                {
                    return Ok();
                }
                return BadRequest("No se pudo actualizar el alumno.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // ---------------Eliminar alumno------------------------
        [HttpDelete("{documento}")]
        public async Task<IActionResult> DeleteAlumno(string documento)
        {
            var alumno = await eRepositorio.GetAlumnoPorDocumento(documento);
            if (alumno == null)
            {
                return NotFound();
            }

            var result = await eRepositorio.EliminarAlumno(alumno.Id);
            if (!result)
            {
                return BadRequest("No se pudo eliminar el alumno.");
            }

            return NoContent();
        }



    }
}
