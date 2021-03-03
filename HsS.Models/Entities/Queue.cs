using HsS.Ifs.Entities;
using System;

namespace HsS.Models.Entities
{
    public class Queue : Entity<int>
    {
        public bool Processed { get; set; }
        public DateTime? ProcessDateTime { get; set; }
        public long TransactionId { get; set; }
        public string HubId { get; set; }
        public string Request { get; set; }
        public string TypeDefinition { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
