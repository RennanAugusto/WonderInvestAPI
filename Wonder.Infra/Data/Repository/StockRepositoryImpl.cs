using Wonder.Domain.Interfaces.Repository;
using Wonder.Domain.Models;
using Wonder.Infra.Data.Context;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.DependencyInjection;
using Wonder.Domain.Shared;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;

namespace Wonder.Infra.Data.Repository
{
    public class StockRepositoryImpl: BaseRepositoryImpl<Stock>, IStockRepository
    {

        // public StockRepositoryImpl(IServiceProvider provider)
        // {
        //     this._postgreSqlContext = provider.GetService<PostgreSqlContext>();
        // }
        public StockRepositoryImpl(IServiceProvider provider) : base(provider)
        {
        }
        public Stock GetByCode(string pCode)
        {
            var stock = this._postgreSqlContext.Set<Stock>().First(s => s.Code == pCode);

            stock.Company = this._postgreSqlContext.Companies.Find(stock.CompanyId);
            
            var listPriceQry = EntityFrameworkQueryableExtensions.AsNoTracking(this._postgreSqlContext.PriceStocks)
                .Where(s => s.StockId == stock.Id).ToList();

            foreach (var price in listPriceQry)
            {
               stock.PricesList.Add(price); 
            }
            return stock;
        }

        public IList<Stock> GetTop10Variations(DateTime pDate)
        {
            //var listStocks = this._postgreSqlContext.Stocks.FromSqlRaw("").ToList();
            throw new NotImplementedException();
        }

        public IList<Stock> GetByCompanyId(int pIdCompany)
        {
            throw new NotImplementedException();
        }

        public IList<Stock> GetByCompanyName(string pNameCompany)
        {
            throw new NotImplementedException();
        }

        public IList<Stock> Select()
        {
            return _postgreSqlContext.Set<Stock>().ToList();
        }

        public async Task<IList<Stock>> GetStocksByPage(int pPage, int pCount, string pCodeFilter)
        {
            var stocks =  await this._postgreSqlContext.Stocks
                .OrderBy(s => s.Code)
                .Where(s => s.Code.ToUpper().StartsWith(pCodeFilter != "" && pCodeFilter != null? pCodeFilter.Trim().ToUpper(): s.Code.ToUpper()))
                .Skip(pCount * (pPage - 1))
                .Take(pCount)
                .ToListAsync();
            
            foreach (var stock in stocks)
            {
                stock.Company = _postgreSqlContext.Companies.Find(stock.CompanyId);
            }

            return stocks;
        }

        public int CountStocks(string pCodeFilter)
        {
            return this._postgreSqlContext.Stocks
                .Where(s => s.Code.ToUpper().StartsWith(pCodeFilter != "" && pCodeFilter != null? pCodeFilter.Trim().ToUpper(): s.Code.ToUpper()))
                .Count();
        }

        public async Task<IList<StockProgression>> GetStockProgression(int stockId, string type)
        {
            var listStockProgression = await this._postgreSqlContext.StockProgressions
                .Where(s => s.StockId == stockId && s.TypeProgression == type && s.Date != null)
                .OrderBy(s=> s.Date)
                .ToListAsync();
            
            return listStockProgression;
        }
    }
}