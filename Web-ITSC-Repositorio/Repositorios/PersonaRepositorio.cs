using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_ITSC_BD.Data.Entity;
using Web_ITSC_BD.Data;
using Web_ITSC_Repositorio.Repositorios.Genericos;
using Microsoft.EntityFrameworkCore;

namespace Web_ITSC_Repositorio.Repositorios
{
    public class PersonaRepositorio : Repositorio<Persona>, IPersonaRepositorio
    {
        private readonly Context context;

        public PersonaRepositorio(Context context) : base(context)
        {
            this.context = context;
        }

        public async Task<Persona> FullGetById(int id)
        {
            return await context.Personas
                .Include(p => p.TipoDocumento)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Persona>> FullGetAll()
        {
            return await context.Personas
                .Include(p => p.TipoDocumento)
                .ToListAsync();
        }

        public async Task FullInsert(Persona persona)
        {
            await context.Personas.AddAsync(persona);
            await context.SaveChangesAsync();
        }

        public async Task FullUpdate(Persona persona)
        {
            context.Personas.Update(persona);
            await context.SaveChangesAsync();
        }


    }
}
