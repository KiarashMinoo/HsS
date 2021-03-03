using HsS.Ifs.Cqrs;

namespace HsS.Services.Queries
{
    public class QueueFollowUpQuery : IQuery
    {
        public long TransactionId { get; set; }
    }
}
