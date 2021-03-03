using HsS.Data.Repositories;
using HsS.Enums;
using HsS.Ifs.Cqrs;
using HsS.Ifs.Data;

namespace HsS.Services.Queries.Handlers
{
    public interface IQueueFollowUpQueryHandler : IQueryHandler<QueueFollowUpQuery, QueueFollowUpResult>
    {
    }

    internal class QueueFollowUpQueryHandler : QueryHandler<QueueFollowUpQuery, QueueFollowUpResult>, IQueueFollowUpQueryHandler
    {
        private readonly IQueueRepository repository;

        public QueueFollowUpQueryHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            repository = UnitOfWork.GetRepository<IQueueRepository>();
        }

        public override QueueFollowUpResult Handle(QueueFollowUpQuery query)
        {
            var queue = repository.FollowUp(query.TransactionId);

            if (queue == null)
            {
                return QueueFollowUpResult.InvalidTransactionId;
            }

            return QueueFollowUpResult.Persisted;
        }
    }
}
