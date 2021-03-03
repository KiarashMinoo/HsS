using HsS.Ifs.Entities;
using System;

namespace HsS.Models.Entities
{
    public class Share : Entity<Guid>
    {
        public string Name { get; set; }
    }
}
