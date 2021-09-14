using System.ComponentModel.DataAnnotations;

namespace Wonder.Domain.Models
{
    public class Base
    {
        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }
    }
}
