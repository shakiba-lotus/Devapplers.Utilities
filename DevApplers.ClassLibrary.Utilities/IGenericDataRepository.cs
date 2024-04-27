using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DevApplers.ClassLibrary.Utilities 
{ 
    public interface IGenericDataRepository<T> where T : class
    {
        IList<T> Get(params Expression<Func<T, object>>[] navigationProperties);
        IList<T> Get(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
        T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
        T[] Add(params T[] items);
        bool Update(params T[] items);
        bool Remove(params T[] items);
    }
}
