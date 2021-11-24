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
        private readonly IWalletRepository _walletRepo;
        private readonly IRlcWalletRepository _rlcWalletRepo;

        public StockService(IStockRepository stockRepo, IStockFavoriteRepository favoriteRepo, IWalletRepository walletRepo, IRlcWalletRepository rlcWalletRepo)
        {
            _stockRepo = stockRepo;
            _favoriteStockRepo = favoriteRepo;
            _walletRepo = walletRepo;
            _rlcWalletRepo = rlcWalletRepo;
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
                    await this._favoriteStockRepo.Insert(favoriteStock);
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

        public async Task<bool> PostPurchase(RlcWalletTicket purchase)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetIdCarteira(string user)
        {
            var wallet = await this._walletRepo.GetWalletByUser(user);
            return wallet.Id;
        }

        public async Task<RlcWalletTicket> GetLasRlcWalletTicket(int idWallet, int idTicket)
        {
            var rlcWallet = await this._rlcWalletRepo.GetLastRlcWalletTicket(idWallet, idTicket);
            return rlcWallet;
        }

        public async Task<bool> InsertRlcWalletTicket(RlcWalletTicket rlcWallet)
        {
            return await this._rlcWalletRepo.Insert(rlcWallet);
        }

        public async Task<IList<InfoWallet>> GetInfoWallet(string user)
        {
            return await this._rlcWalletRepo.GetInfoWallet(user);
        }

        public async Task<IList<StockProgression>> GetStockProgression(int stockId, string type)
        {
            var listProgression = await this._stockRepo.GetStockProgression(stockId, type);
            return listProgression;
        }
        
    }
}