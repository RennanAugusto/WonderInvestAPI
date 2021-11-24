using System;

namespace Wonder.Service.Contracts.DTO
{
    public class StockProgressionDTO
    {
        public string Description { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }
        public string Code { get; set; }
        public int StockId { get; set; }
        public string Legend { get; set; }
        public string TypeProgression { get; set; }
    }
}