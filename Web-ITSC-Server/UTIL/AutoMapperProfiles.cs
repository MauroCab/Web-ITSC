using AutoMapper;
using System;
using System.Linq;
using Web_ITSC_BD.Data.Entity;
using Web_ITSC_Shared.DTO;
using Web_ITSC_Shared.DTO.DTOs_sin_uso;

namespace Web_ITSC_Server.UTIL
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CrearAlumnoDTO, Alumno>()
            .ForMember(dest => dest.PaisId, opt => opt.MapFrom(src => src.PaisId))
            .ForMember(dest => dest.ProvinciaId, opt => opt.MapFrom(src => src.ProvinciaId))
            .ForMember(dest => dest.DepartamentoId, opt => opt.MapFrom(src => src.DepartamentoId))
            .ForMember(dest => dest.LocalidadId, opt => opt.MapFrom(src => src.LocalidadId));



            CreateMap<CrearAlumnoDTO, Persona>()
                                    .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                                    .ForMember(dest => dest.Apellido, opt => opt.MapFrom(src => src.Apellido))
                                    .ForMember(dest => dest.Documento, opt => opt.MapFrom(src => src.Documento))
                                    .ForMember(dest => dest.TipoDocumentoId, opt => opt.MapFrom(src => src.TipoDocumentoId))
                                    .ForMember(dest => dest.Telefono, opt => opt.MapFrom(src => src.Telefono))
                                    .ForMember(dest => dest.Domicilio, opt => opt.MapFrom(src => src.Domicilio));



            // Mapeo de CrearAlumnoDTO a Usuario (también podrías hacerlo si la lógica de tu usuario depende del DTO)
            CreateMap<CrearAlumnoDTO, Usuario>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Contrasena, opt => opt.MapFrom(src => src.Contrasena));

            //Mapeo de EditarAlumnoDTO

            CreateMap<Alumno, EditarAlumnoDTO>()
     .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
     .ForMember(dest => dest.PaisId, opt => opt.MapFrom(src => src.PaisId))
     .ForMember(dest => dest.ProvinciaId, opt => opt.MapFrom(src => src.ProvinciaId))
     .ForMember(dest => dest.DepartamentoId, opt => opt.MapFrom(src => src.DepartamentoId))
     .ForMember(dest => dest.LocalidadId, opt => opt.MapFrom(src => src.LocalidadId))
     .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Usuario.Persona.Nombre))
     .ForMember(dest => dest.Apellido, opt => opt.MapFrom(src => src.Usuario.Persona.Apellido))
     .ForMember(dest => dest.Documento, opt => opt.MapFrom(src => src.Usuario.Persona.Documento))
     .ForMember(dest => dest.TipoDocumentoId, opt => opt.MapFrom(src => src.Usuario.Persona.TipoDocumentoId))
     .ForMember(dest => dest.Telefono, opt => opt.MapFrom(src => src.Usuario.Persona.Telefono))
     .ForMember(dest => dest.Domicilio, opt => opt.MapFrom(src => src.Usuario.Persona.Domicilio))
     .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Usuario.Email))
     //.ForMember(dest => dest.Contrasena, opt => opt.MapFrom(src => src.Usuario.Contrasena))
     .ForMember(dest => dest.Sexo, opt => opt.MapFrom(src => src.Sexo))
     .ForMember(dest => dest.FechaNacimiento, opt => opt.MapFrom(src => src.FechaNacimiento))
     .ForMember(dest => dest.CUIL, opt => opt.MapFrom(src => src.CUIL))
     .ForMember(dest => dest.TituloBase, opt => opt.MapFrom(src => src.TituloBase))
     .ForMember(dest => dest.FotocopiaDNI, opt => opt.MapFrom(src => src.FotocopiaDNI))
     .ForMember(dest => dest.ConstanciaCUIL, opt => opt.MapFrom(src => src.ConstanciaCUIL))
     .ForMember(dest => dest.PartidaNacimiento, opt => opt.MapFrom(src => src.PartidaNacimiento))
     .ForMember(dest => dest.Analitico, opt => opt.MapFrom(src => src.Analitico))
     .ForMember(dest => dest.FotoCarnet, opt => opt.MapFrom(src => src.FotoCarnet))
     .ForMember(dest => dest.CUS, opt => opt.MapFrom(src => src.CUS));


            // mapeo de EditarAlumnoDTO a Persona
            CreateMap<EditarAlumnoDTO, Persona>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.Apellido, opt => opt.MapFrom(src => src.Apellido))
                .ForMember(dest => dest.Documento, opt => opt.MapFrom(src => src.Documento))
                .ForMember(dest => dest.TipoDocumentoId, opt => opt.MapFrom(src => src.TipoDocumentoId))
                .ForMember(dest => dest.Telefono, opt => opt.MapFrom(src => src.Telefono))
                .ForMember(dest => dest.Domicilio, opt => opt.MapFrom(src => src.Domicilio));

            // Nuevo mapeo de EditarAlumnoDTO a Usuario
            CreateMap<EditarAlumnoDTO, Usuario>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Contrasena, opt => opt.MapFrom(src => src.Contrasena));

            CreateMap<EditarAlumnoDTO, Alumno>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());  // Ignorar la clave primaria de Alumno

            CreateMap<EditarAlumnoDTO, Usuario>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())  // Ignorar la clave primaria de Usuario
                .ForMember(dest => dest.PersonaId, opt => opt.Ignore());  // Ignorar la relación con Persona

            CreateMap<EditarAlumnoDTO, Persona>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());  // Ignorar la clave primaria de Persona

            //----------------------------


            CreateMap<Alumno, GetAlumnoDTO>()
                                           .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Usuario.Persona.Nombre))
                                           .ForMember(dest => dest.Apellido, opt => opt.MapFrom(src => src.Usuario.Persona.Apellido))
                                           .ForMember(dest => dest.Documento, opt => opt.MapFrom(src => src.Usuario.Persona.Documento))
                                           .ForMember(dest => dest.Telefono, opt => opt.MapFrom(src => src.Usuario.Persona.Telefono))
                                           .ForMember(dest => dest.Domicilio, opt => opt.MapFrom(src => src.Usuario.Persona.Domicilio))
                                           .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado)) // Si deseas incluir Estado también
                                           .ForMember(dest => dest.Cohorte, opt => opt.MapFrom(src => src.InscripcionesCarreras.FirstOrDefault().Cohorte))
                                           .ForMember(dest => dest.CarreraId, opt => opt.MapFrom(src => src.InscripcionesCarreras.FirstOrDefault().Carrera.Id))
                                           .ForMember(dest => dest.NameCarrera, opt => opt.MapFrom(src => src.InscripcionesCarreras.FirstOrDefault().Carrera.Nombre))
                                           .ForMember(dest => dest.AbreviaturaCarrera, opt => opt.MapFrom(src => src.InscripcionesCarreras.FirstOrDefault().Carrera.Abreviatura))
                                           .ForMember(dest => dest.Pais, opt => opt.MapFrom(src => src.Pais.Nombre))
                                           .ForMember(dest => dest.Provincia, opt => opt.MapFrom(src => src.Provincia.Nombre))
                                           .ForMember(dest => dest.Departamento, opt => opt.MapFrom(src => src.Departamento.Nombre))
                                           .ForMember(dest => dest.Localidad, opt => opt.MapFrom(src => src.Localidad.Nombre));



            //_usuario________________________________________________________________________________________________________________________________
            CreateMap<CrearUsuarioDTO, Usuario>();
            CreateMap<Usuario, GetUsuarioDTO>()
                                            .ForMember(dest => dest.NombrePersona, opt => opt.MapFrom(src => src.Persona.Nombre))
                                            .ForMember(dest => dest.ApellidoPersona, opt => opt.MapFrom(src => src.Persona.Apellido))
                                            .ForMember(dest => dest.DocumentoPersona, opt => opt.MapFrom(src => src.Persona.Documento))
                                            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                                            .ForMember(dest => dest.Contrasena, opt => opt.MapFrom(src => src.Contrasena));

            //_persona_____________________________________________________________________________________________________________________________________
            CreateMap<CrearPersonaDTO, Persona>().ForPath(dest => dest.TipoDocumento.Nombre, opt => opt.MapFrom(src => src.TipoDocumentoId));
            CreateMap<Persona, GetPersonaDTO>().ForMember(dest => dest.Cohorte, opt => opt.MapFrom(src => DateTime.Now.Year)); // Si "Cohorte" es parte de "Inscripcion_Carrera" en Persona



            //_carrera_____________________________________________________________________________________________________________________________________
            CreateMap<CrearCarreraDTO, Carrera>();
            CreateMap<Carrera, GetCarreraDTO>();

            //_inscripcion_____________________________________________________________________________________________________________________________________
            CreateMap<CrearInscripcionCarreraDTO, InscripcionCarrera>();
            CreateMap<InscripcionCarrera, GetIncripcionCarreraDTO>()
                                                .ForMember(dest => dest.AlumnoNombre, opt => opt.MapFrom(src => src.Alumno.Usuario.Persona.Nombre))
                                                .ForMember(dest => dest.AlumnoApellido, opt => opt.MapFrom(src => src.Alumno.Usuario.Persona.Apellido))
                                                .ForMember(dest => dest.AlumnoDocumento, opt => opt.MapFrom(src => src.Alumno.Usuario.Persona.Documento))
                                                //.ForMember(dest => dest.CarreraName, opt => opt.MapFrom(src => src.Alumno.Usuario.Persona.inscripcion_Carrera.Carrera.Nombre))
                                                .ForMember(dest => dest.Cohorte, opt => opt.MapFrom(src => src.Cohorte));

            CreateMap<CrearAlumnoDTO, InscripcionCarrera>().ForMember(dest => dest.CarreraId, opt => opt.MapFrom(src => src.CarreraId));



            //_TDOCUMENTO_____________________________________________________________________________________________________________________________________
            CreateMap<CrearTipoDocumentoDTO, TipoDocumento>();
            CreateMap<TipoDocumento, GetTipoDocumentoDTO>();

            //PAIS_____________________________________________________________________________________________________________________________________________
            CreateMap<CrearPaisDTO, Pais>();
            CreateMap<Pais, GetPaisDTO>();
            //CreateMap<CrearAlumnoDTO, Pais>().ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.PaisId));

            //CreateMap<Pais, GetAlumnoDTO>().ForMember(dest => dest.Pais, opt => opt.MapFrom(src => src.Nombre));




            //Provincia___________________________________________________________________________________________________________________________________________
            CreateMap<CrearProvinciaDTO, Provincia>();
            CreateMap<Provincia, GetProvinciaDTO>();

            //Departamento___________________________________________________________________________________________________________________________________________
            CreateMap<CrearDepartamentosDTO, Departamento>();
            CreateMap<Departamento, GetDepartamentosDTO>();

            //Localidad___________________________________________________________________________________________________________________________________________
            CreateMap<CrearLocalidadesDTO, Localidad>();
            CreateMap<Localidad, GetLocalidadesDTO>();



            
            CreateMap<CrearClaseDTO, Clase>();
            CreateMap<CrearClaseAsistenciaDTO, ClaseAsistencia>();
            CreateMap<CrearCorrelatividadDTO, Correlatividad>();
            CreateMap<CrearCursadoMateriaDTO, CursadoMateria>();
            CreateMap<CrearEvaluacionDTO, Evaluacion>();
            CreateMap<CrearMateriaDTO, Materia>();
            CreateMap<CrearMateriaEnPlanEstudioDTO, MateriaEnPlanEstudio>();
            CreateMap<CrearNotaDTO, Nota>();
            CreateMap<CrearPlanEstudioDTO, PlanEstudio>();
            CreateMap<CrearProfesorDTO, Profesor>();
            CreateMap<CrearTurnoDTO, Turno>();
            CreateMap<CrearCertificadoAlumnoDTO, CertificadoAlumno>();
        }
    }
}
