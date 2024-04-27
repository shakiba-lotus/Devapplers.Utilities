using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace DevApplers.ClassLibrary.Utilities
{
    public class GenericDataRepository<T> : IGenericDataRepository<T> where T : class
    {
        private readonly DbContext _context;
        public GenericDataRepository(DbContext context)
        {
            _context = context;
        }

        public virtual IList<T> Get(params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = _context.Set<T>();

            //Apply eager loading
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<T, object>(navigationProperty);

            var list = dbQuery
                .AsNoTracking()
                .ToList<T>();
            return list;
        }

        public virtual IList<T> Get(Func<T, bool> where,
             params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = _context.Set<T>();

            //Apply eager loading
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<T, object>(navigationProperty);

            var list = dbQuery
                .AsNoTracking().AsEnumerable()
                .Where(where)
                .ToList<T>();
            return list;
        }

        public virtual T GetSingle(Func<T, bool> where,
             params Expression<Func<T, object>>[] navigationProperties)
        {
            T item = null;
            IQueryable<T> dbQuery = _context.Set<T>();

            //Apply eager loading
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<T, object>(navigationProperty);

            item = dbQuery
                .AsNoTracking() //Don't track any changes for the selected item
                .FirstOrDefault(where); //Apply where clause
            return item;
        }

        public T[] Add(params T[] items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));
            foreach (var item in items)
            {
                _context.Set<T>().Add(item);
            }
            _context.SaveChanges();
            return items;
        }

        public bool Update(params T[] items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));
            foreach (var item in items)
            {
                _context.Entry(item).State = EntityState.Modified;
            }
            _context.SaveChanges();
            return true;
        }

        public bool Remove(params T[] items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));
            foreach (var item in items)
            {
                _context.Entry(item).State = EntityState.Deleted;
            }
            _context.SaveChanges();
            return true;
        }
    }
}