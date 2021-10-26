using Wonder.Domain.Models;

namespace Wonder.Service.Contracts.DTO
{
    public class GetFavoriteDTO
    {
        public string IdUser { get; set; }
        public int IdFavorite { get; set; }
        public bool Active { get; set; }
        public int IdStock { get; set; }
        public StockDto Stock { get; set; }
    }
}