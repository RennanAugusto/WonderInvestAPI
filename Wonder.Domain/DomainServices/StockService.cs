using System.Collections.Generic;
using DotNurse.Injector.Attributes;
using Microsoft.AspNetCore.Mvc;
using Wonder.Domain.Interfaces.Repository;
using Wonder.Domain.Models;

namespace Wonder.Domain.DomainServices
{
    public class StockService
    {
        [InjectService] public IStockRepository StockRepo { get; set; }

        public Stock GetStockByCode(string pCode)
        {
            return this.StockRepo.GetByCode(pCode);
        }
    }
}