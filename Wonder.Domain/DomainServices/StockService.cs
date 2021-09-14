using System.Collections.Generic;
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
    }
}