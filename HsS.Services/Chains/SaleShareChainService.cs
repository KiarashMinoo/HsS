using HsS.Data.Repositories;
using HsS.Ifs.Chains;
using HsS.Ifs.Data;
using HsS.Models.Entities;
using HsS.Models.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HsS.Services.Chains
{
    public class SaleShareChainService : ChainService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IShareOrderRepository repository;

        public SaleShareChainService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            repository = unitOfWork.GetRepository<IShareOrderRepository>();
        }

        protected override Task Execute(ChainContext context, Action<object> executedCallBack = null)
        {
            if (context.ContextValue is not ShareOrder shareOrder || shareOrder.Type != ShareOrderType.Sale)
                return Next();

            var queue = context.AdditionalParameters.FirstOrDefault();
            shareOrder.QueueId = (queue.Value as Queue).Id;
            shareOrder.TotalAmount = shareOrder.UnitAmount * shareOrder.Quantity;
            repository.Add(shareOrder);
            unitOfWork.SaveChanges();

            executedCallBack?.Invoke(shareOrder);

            return Task.CompletedTask;
        }
    }
}
