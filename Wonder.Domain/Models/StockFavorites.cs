namespace Wonder.Domain.Models
{
    public class StockFavorites: Base
    {
        public string IdWonderUsers { get; set; }
        public int StockId { get; set; }
        public Stock Stock { get; set; }
    }
}