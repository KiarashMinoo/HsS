using HsS.Ifs.Data;
using HsS.Models.Entities;

namespace HsS.Data.Repositories
{
    public interface IShareOrderRepository : IRepository<ShareOrder, int>
    {

    }

    internal class ShareOrderRepository : Repository<ShareOrder, int>, IShareOrderRepository
    {
        public ShareOrderRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
