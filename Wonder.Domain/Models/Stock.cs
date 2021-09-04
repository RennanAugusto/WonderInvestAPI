using System.Collections.Generic;

namespace Wonder.Domain.Models
{
    public class Stock: Base
    {
        public string Code { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public List<PriceStock> PricesList { get; set; }
    }
}