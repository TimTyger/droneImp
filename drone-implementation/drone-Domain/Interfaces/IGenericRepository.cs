﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace drone_Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        T GetById(long id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);

        T Update(T entity);

        IEnumerable<T> UpdateRange(IEnumerable<T> entities);
    }
}
