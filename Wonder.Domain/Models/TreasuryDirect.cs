using System;
using Wonder.Domain.Shared;

namespace Wonder.Domain.Models
{
    public class TreasuryDirect: Base
    {
        public string Name { get; set; }
        public string Year { get; set; }
    }

    public class PriceTreasuryDirect : Base
    {
        public float Price { get; set; }
        public DateTime Date { get; set; }
        public CloseOpen CloseOpen { get; set; }
    }
}