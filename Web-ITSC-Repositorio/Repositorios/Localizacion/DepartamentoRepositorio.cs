using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_ITSC_BD.Data.Entity;
using Web_ITSC_BD.Data;
using Web_ITSC_Repositorio.Repositorios.Genericos;
using Microsoft.EntityFrameworkCore;

namespace Web_ITSC_Repositorio.Repositorios.Localizacion
{
    public class DepartamentoRepositorio : Repositorio<Departamento>, IDepartamentoRepositorio
    {
        private readonly Context _context;

        public DepartamentoRepositorio(Context context) : base(context)
        {
            _context = context;
        }
        public async Task<Departamento> GetByIdAsync(int id)
        {
            return await _context.Set<Departamento>().FindAsync(id);
        }
        public async Task<List<Departamento>> SelectDepartamentosPorProvinciaAsync(int ProvinciaId)
        {
            return await _context.Departamentos
                                 .Where(d => d.ProvinciaId == ProvinciaId)
                                 .ToListAsync();
        }

    }
}
