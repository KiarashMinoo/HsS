using HsS.Ifs.Data;
using HsS.Ifs.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace HsS.Data
{
    internal class Repository<TEntity, TPk> : IRepository<TEntity, TPk> where TEntity : Entity<TPk> where TPk : struct
    {
        protected DbSet<TEntity> Db { get; private set; }

        public Repository(IUnitOfWork unitOfWork)
        {
            Db = ((UnitOfWork)unitOfWork).Db.Set<TEntity>();
        }

        public TEntity Add(TEntity entity)
        {
            Db.Add(entity);
            return entity;
        }

        public ICollection<TEntity> List(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = Db;

            if (includes != null && includes.Any())
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                return orderBy.Invoke(query).ToHashSet();
            }

            return query.ToHashSet();
        }

        public void Remove(TPk id)
        {
            var entity = Retrieve(id);
            Remove(entity);
        }

        public void Remove(TEntity entity)
        {
            Db.Remove(entity);
        }

        public TEntity Retrieve(TPk id)
        {
            return Db.Find(id);
        }
    }
}
