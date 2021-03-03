using HsS.Ifs.Entities;
using HsS.Models.Enums;
using System;

namespace HsS.Models.Entities
{
    public class ShareOrder : Entity<int>
    {
        public int QueueId { get; set; }

        public Guid ShareId { get; set; }

        public ShareOrderType Type { get; set; }

        public decimal UnitAmount { get; set; }

        public decimal Quantity { get; set; }

        public decimal TotalAmount { get; set; }
    }
}
