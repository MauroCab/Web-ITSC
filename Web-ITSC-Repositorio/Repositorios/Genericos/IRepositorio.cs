﻿using Web_ITSC_BD.Data;

namespace Web_ITSC_Repositorio.Repositorios.Genericos
{
    public interface IRepositorio<E> where E : class, IEntityBase
    {
        Task<bool> Delete(int id);
        Task<bool> Existe(int id);
        Task<int> Insert(E entidad);
        Task<List<E>> Select();
        Task<E> SelectById(int id);
        Task<bool> Update(int id, E entidad);
    }
}