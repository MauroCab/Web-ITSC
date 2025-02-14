using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_ITSC_BD.Data;
using Web_ITSC_BD.Data.Entity;
using Web_ITSC_Repositorio.Repositorios.Genericos;

namespace Web_ITSC_Repositorio.Repositorios.Localizacion
{
    public class LocalidadRepositorio : Repositorio<Localidad>, ILocalidadRepositorio
    {
        private readonly Context _context;

        public LocalidadRepositorio(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<Localidad> GetByIdAsync(int id)
        {
            return await _context.Set<Localidad>().FindAsync(id);
        }
        public async Task<List<Localidad>> SelectLocalidadesPorDepartamentoAsync(int DepartamentoId)
        {
            return await _context.Localidades
                                 .Where(l => l.DepartamentoId == DepartamentoId)
                                 .ToListAsync();
        }
    }
}
