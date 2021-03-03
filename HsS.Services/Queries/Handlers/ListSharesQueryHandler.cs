using HsS.Data.Repositories;
using HsS.Ifs.Cqrs;
using HsS.Ifs.Data;
using HsS.Models.Entities;
using System.Collections.Generic;

namespace HsS.Services.Queries.Handlers
{
    public interface IListSharesQueryHandler : IQueryHandler<ListSharesQuery, ICollection<Share>>
    {
    }

    internal class ListSharesQueryHandler : QueryHandler<ListSharesQuery, ICollection<Share>>, IListSharesQueryHandler
    {
        private readonly IShareRepository repository;

        public ListSharesQueryHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            repository = UnitOfWork.GetRepository<IShareRepository>();
        }

        public override ICollection<Share> Handle(ListSharesQuery query)
        {
            return repository.List();
        }
    }
}
