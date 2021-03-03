using HsS.Ifs.Data;

namespace HsS.Ifs.Cqrs
{
    public abstract class QueryHandler<TQuery, TResult> : IQueryHandler<TQuery, TResult> where TQuery : IQuery
    {
        protected IUnitOfWork UnitOfWork { get; private set; }

        public QueryHandler(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public abstract TResult Handle(TQuery query);
    }
}
