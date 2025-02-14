using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_ITSC_BD.Data.Entity;
using Web_ITSC_BD.Data;
using Web_ITSC_Repositorio.Repositorios.Genericos;

namespace Web_ITSC_Repositorio.Repositorios.Localizacion
{
    public class ProvinciaRepositorio : Repositorio<Provincia>, IProvinciaRepositorio
    {
        private readonly Context _context;

        public ProvinciaRepositorio(Context context) : base(context)
        {
            _context = context;
        }
        public async Task<Provincia> GetByIdAsync(int id)
        {
            return await _context.Set<Provincia>().FindAsync(id);
        }

        public async Task<List<Provincia>> SelectProvinciasPorPaisAsync(int PaisId)
        {
            try
            {
                if (PaisId == 0)
                {
                    return new List<Provincia>();  // Retorna una lista vacía si no hay país seleccionado
                }

                return await _context.Provincias
                                      .Where(p => p.PaisId == PaisId)
                                      .ToListAsync();
            }
            catch (Exception ex)
            {
                // Loguea el error
                Console.WriteLine($"Error al obtener provincias: {ex.Message}");
                return new List<Provincia>();  // Devuelve una lista vacía en caso de error
            }
        }

        // Método que obtiene los departamentos para una provincia
        public async Task<List<Departamento>> SelectDepartamentosPorProvincia(int ProvinciaId)
        {
            return await _context.Departamentos
                                 .Where(d => d.ProvinciaId == ProvinciaId)
                                 .ToListAsync();
        }

    }
}
