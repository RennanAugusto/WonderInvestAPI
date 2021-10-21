using System.Collections.Generic;

namespace Wonder.Domain.Models
{
    public class Stock: Base
    {
        public Stock()
        {
            this.PricesList = new List<PriceStock>();
        }
        public string Code { get; set; }
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public List<PriceStock> PricesList { get; set; }
    }
}