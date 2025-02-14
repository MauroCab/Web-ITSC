using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_ITSC_BD.Data.Entity;
using Web_ITSC_BD.Data;
using Web_ITSC_Repositorio.Repositorios.Genericos;

namespace Web_ITSC_Repositorio.Repositorios
{
    public class TipoDocumentoRepositorio : Repositorio<TipoDocumento>, ITipoDocumentoRepositorio
    {
        private readonly Context context;

        public TipoDocumentoRepositorio(Context context) : base(context)
        {
            this.context = context;
        }

        // Método para obtener TipoDocumento por Id
        public async Task<TipoDocumento> GetByIdAsync(int id)
        {
            return await context.Set<TipoDocumento>().FindAsync(id);
        }
    }
}
