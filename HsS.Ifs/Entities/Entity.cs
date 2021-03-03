using System.ComponentModel.DataAnnotations;

namespace HsS.Ifs.Entities
{
    public class Entity<TPk> where TPk : struct
    {
        [Key]
        public TPk Id { get; set; }
    }
}
