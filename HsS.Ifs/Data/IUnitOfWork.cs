using Microsoft.EntityFrameworkCore;

namespace HsS.Ifs.Data
{
    public interface IUnitOfWork
    {
        TRepository GetRepository<TRepository>();
        void SaveChanges();
    }
}
