using System;
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

        public StockService(IStockRepository stockRepo)
        {
            _stockRepo = stockRepo;
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
    }
}