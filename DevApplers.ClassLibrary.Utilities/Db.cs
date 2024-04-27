using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DevApplers.ClassLibrary.Utilities
{
    public class Db
    {
        private static string ConnectionString { get; set; }

        public static DbContext SetContext(string connectionString)
        {
            ConnectionString = connectionString;
            return new DbContext(ConnectionString);
        }

        public static List<T> ApiGet<T>() where T : class
        {
            var context = new DbContext(ConnectionString);
            context.Configuration.LazyLoadingEnabled = false;
            var genericDataRepository = new GenericDataRepository<T>(context);
            return genericDataRepository.Get().ToList();
        }

        public static List<T> ApiGet<T>(Func<T, bool> where) where T : class
        {
            var context = new DbContext(ConnectionString);
            context.Configuration.LazyLoadingEnabled = false;
            var genericDataRepository = new GenericDataRepository<T>(context);
            return genericDataRepository.Get(where).ToList();
        }

        public static T ApiGet<T>(int id) where T : class
        {
            var context = new DbContext(ConnectionString);
            context.Configuration.LazyLoadingEnabled = false;
            var genericDataRepository = new GenericDataRepository<T>(context);
            return genericDataRepository.GetSingle(r =>
                typeof(T).GetProperty("Id")?.GetValue(r).ToString() == id.ToString());
        }

        public static List<T> Get<T>() where T : class
        {
            var genericDataRepository = new GenericDataRepository<T>(new DbContext(ConnectionString));
            return genericDataRepository.Get().ToList();
        }

        public static List<T> Get<T>(Func<T, bool> where) where T : class
        {
            var genericDataRepository = new GenericDataRepository<T>(new DbContext(ConnectionString));
            return genericDataRepository.Get(where).ToList();
        }

        public static T Get<T>(int id) where T : class
        {
            var genericDataRepository = new GenericDataRepository<T>(new DbContext(ConnectionString));
            return genericDataRepository.GetSingle(r =>
                typeof(T).GetProperty("Id")?.GetValue(r).ToString() == id.ToString());
        }

        public static T[] Create<T>(params T[] items) where T : class
        {
            var genericDataRepository = new GenericDataRepository<T>(new DbContext(ConnectionString));
            return genericDataRepository.Add(items);
        }

        public static bool Edit<T>(params T[] items) where T : class
        {
            var genericDataRepository = new GenericDataRepository<T>(new DbContext(ConnectionString));
            return genericDataRepository.Update(items);
        }

        public static bool Delete<T>(params T[] items) where T : class
        {
            var genericDataRepository = new GenericDataRepository<T>(new DbContext(ConnectionString));
            return genericDataRepository.Remove(items);
        }
    }
}
