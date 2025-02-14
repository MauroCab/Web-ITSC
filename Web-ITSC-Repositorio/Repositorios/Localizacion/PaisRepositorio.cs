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
    public class PaisRepositorio : Repositorio<Pais>, IPaisRepositorio
    {
        private readonly Context _context;

        public PaisRepositorio(Context context) : base(context)
        {
            _context = context;
        }
        public async Task<Pais> GetByIdAsync(int id)
        {
            return await _context.Set<Pais>().FindAsync(id);
        }

    }
}
