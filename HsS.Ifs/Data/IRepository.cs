using HsS.Ifs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace HsS.Ifs.Data
{    
    public interface IRepository<TEntity, TPk> where TEntity : Entity<TPk> where TPk : struct
    {
        TEntity Retrieve(TPk id);

        TEntity Add(TEntity entity);

        void Remove(TPk id);

        void Remove(TEntity entity);

        ICollection<TEntity> List(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes);
    }
}
