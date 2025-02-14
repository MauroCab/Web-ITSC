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
    public class CarreraRepositorio : Repositorio<Carrera>, ICarreraRepositorio
    {

        private readonly Context context;

        public CarreraRepositorio(Context context) : base(context)
        {
            this.context = context;
        }
        public async Task<Carrera> GetCarreraByIdAsync(int carreraId)
        {
            return await context.Set<Carrera>().FindAsync(carreraId);
        }

        public async Task<int> GetByNombre(string nombreCarrera)
        {
            var a = await context.Carreras.FirstOrDefaultAsync(u => u.Nombre == nombreCarrera);
            if (a != null)
            {
                return a.Id;
            }
            else
            {
                return 0;
            }
        }
    }
}
