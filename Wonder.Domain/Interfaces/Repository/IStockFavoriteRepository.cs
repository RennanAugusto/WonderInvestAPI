using System.Collections.Generic;
using System.Threading.Tasks;
using Wonder.Domain.Models;

namespace Wonder.Domain.Interfaces.Repository
{
    public interface IStockFavoriteRepository: IBaseRepository<StockFavorites>
    {
        Task<IList<StockFavorites>> GetStockFavorite(string pIdUser);
        StockFavorites GetByIdUserIdStock(string pIdUser, int pIdStock);
    }
}