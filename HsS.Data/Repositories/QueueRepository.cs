using HsS.Ifs.Data;
using HsS.Models.Entities;
using System.Linq;

namespace HsS.Data.Repositories
{
    public interface IQueueRepository : IRepository<Queue, int>
    {
        Queue FollowUp(long transactionId);
    }

    internal class QueueRepository : Repository<Queue, int>, IQueueRepository
    {
        public QueueRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Queue FollowUp(long transactionId)
        {
            return Db.FirstOrDefault(e => e.TransactionId == transactionId);
        }
    }
}
