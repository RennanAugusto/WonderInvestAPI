using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotNurse.Injector.Attributes;
using Microsoft.AspNetCore.Mvc;
using Wonder.Domain.Interfaces.Repository;
using Wonder.Domain.Models;

namespace Wonder.Domain.DomainServices
{
    public class StockService
    {
        private readonly IStockRepository _stockRepo;
        private readonly IStockFavoriteRepository _favoriteStockRepo;

        public StockService(IStockRepository stockRepo, IStockFavoriteRepository favoriteRepo)
        {
            _stockRepo = stockRepo;
            _favoriteStockRepo = favoriteRepo;
        }

        public Stock GetStockByCode(string pCode)
        {
            return _stockRepo.GetByCode(pCode);
        }

        public async Task<IList<Stock>> GetStocksByPage(int pPage, int pCount, string pCodeFilter)
        {
            return await _stockRepo.GetStocksByPage(pPage, pCount, pCodeFilter);
        }

        public int CountStocks(string pCodeFilter)
        {
            return this._stockRepo.CountStocks(pCodeFilter);
        }

        public async Task<bool> PostStockFavorite(StockFavorites favoriteStock)
        {
            try
            {
                if (favoriteStock.Id > 0)
                    this._favoriteStockRepo.Update(favoriteStock);
                else
                {   
                    var stock = this._favoriteStockRepo.GetByIdUserIdStock(favoriteStock.IdWonderUsers, favoriteStock.StockId);
                    if (stock != null)
                        return true;
                    this._favoriteStockRepo.Insert(favoriteStock);
                }
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível adicionar a ação aos favoritos: " + e.Message);
            }
        }

        public async Task<IList<StockFavorites>> GetFavorites(string pIdUser)
        {
            return await this._favoriteStockRepo.GetStockFavorite(pIdUser);
        }
    }
}