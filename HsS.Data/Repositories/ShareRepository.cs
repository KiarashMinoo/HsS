using HsS.Ifs.Data;
using HsS.Models.Entities;
using System;

namespace HsS.Data.Repositories
{
    public interface IShareRepository : IRepository<Share, Guid>
    {

    }

    internal class ShareRepository : Repository<Share, Guid>, IShareRepository
    {
        public ShareRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
