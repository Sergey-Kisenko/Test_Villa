﻿using System.Linq.Expressions;

namespace MagicVilla_VillaApi.Repository.Interfaces
{
    public interface IRepository <T> where T : class
    {
        public Task<T?> Get(Expression<Func<T, bool>> filter = null, bool tracked = false);
        public Task<IEnumerable<T>> GetAll(Expression<Func<T,bool>> filter = null);
        public Task<T> Create(T entity);
        public Task<Task> Delete(T entity);
        public Task Save();
    }
}
