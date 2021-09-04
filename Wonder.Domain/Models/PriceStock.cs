using System;
using Wonder.Domain.Shared;

namespace Wonder.Domain.Models
{
    public class PriceStock: Base
    {
       public float Price { get; set; }
       public DateTime Date { get; set; }
       public CloseOpen IsCloseOpen { get; set; }
       public int StockId { get; set; }
    }
}