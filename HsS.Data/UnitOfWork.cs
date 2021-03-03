using HsS.Ifs.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HsS.Data
{
    internal class UnitOfWork : IUnitOfWork
    {
        private static object _lock = new object();
        private IDictionary<Type, object> repositories;
        private static ICollection<Type> definedRepositories;

        public UnitOfWork(ApplicationDbContext db)
        {
            Db = db;
            repositories = new Dictionary<Type, object>();
        }

        internal static void FillDefinedRepositories()
        {
            var repositoryType = typeof(IRepository<,>);
            definedRepositories = AppDomain.CurrentDomain.GetAssemblies().
                SelectMany(a => a.GetTypes()).
                Where(t => !t.IsAbstract && t.IsClass && t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == repositoryType)).
                ToHashSet();
        }

        internal DbContext Db { get; private set; }

        public TRepository GetRepository<TRepository>()
        {
            lock (_lock)
            {
                var repositoryType = typeof(TRepository);
                if (!repositoryType.IsInterface)
                    throw new ArgumentException("TRepository type must be a interface type");

                if (!repositories.ContainsKey(repositoryType))
                {
                    var type = definedRepositories.FirstOrDefault(t => repositoryType.IsAssignableFrom(t));
                    if (type == null)
                        throw new ArgumentException("Invalid repository type");

                    repositories.Add(repositoryType, Activator.CreateInstance(type, this));
                }

                return (TRepository)repositories[repositoryType];
            }
        }

        public void SaveChanges()
        {
            Db.SaveChanges();
        }
    }
}
