using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wonder.Domain.Interfaces.Repository;
using Wonder.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Wonder.Infra.Data.Context;

namespace Wonder.Infra.Data.Repository
{
    public class FavoritesRepository: BaseRepositoryImpl<StockFavorites>, IStockFavoriteRepository
    {
        public FavoritesRepository(IServiceProvider provider) : base(provider)
        {
        }

        public async Task<IList<StockFavorites>> GetStockFavorite(string pIdUser)
        {
            var favorites =  this._postgreSqlContext.StockFavorites
                .AsNoTracking()
                .Where(f => f.IdWonderUsers == pIdUser && f.Active)
                .Include(f => f.Stock)
                .ToList();

            foreach (var favorite in favorites)
            {
                favorite.Stock =  await this._postgreSqlContext.Stocks.FindAsync(favorite.StockId);
                favorite.Stock.Company = await this._postgreSqlContext.Companies.FindAsync(favorite.Stock.CompanyId);
                favorite.Stock.PricesList =  await this._postgreSqlContext.PriceStocks
                    .Where(p=> p.StockId == favorite.StockId)
                    .OrderByDescending(p => p.Date)
                    .Take(2)
                    .ToListAsync();
            }
            return favorites;
        }

        public StockFavorites GetByIdUserIdStock(string pIdUser, int pIdStock)
        {
            try
            {
                return this._postgreSqlContext.StockFavorites
                    .Where(f => (f.IdWonderUsers == pIdUser) && (f.StockId == pIdStock) && (f.Active)).First();
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}