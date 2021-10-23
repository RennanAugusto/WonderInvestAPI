using Wonder.Domain.Interfaces.Repository;
using Wonder.Domain.Models;
using Wonder.Infra.Data.Context;
using System.Collections.Generic;
using System;
using System.Data.Entity;
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
    public class StockRepositoryImpl: IStockRepository
    {
        private readonly PostgreSqlContext _postgreSqlContext ;

        public StockRepositoryImpl(IServiceProvider provider)
        {
            this._postgreSqlContext = provider.GetService<PostgreSqlContext>();
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

        public bool Insert(Stock obj)
        {
            try
            {
                _postgreSqlContext.Set<Stock>().Add(obj);
                _postgreSqlContext.SaveChanges();
                return true;
            }
            catch (IOException e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Update(Stock obj)
        {
            try
            {
                _postgreSqlContext.Entry(obj).State = EntityState.Modified;
                _postgreSqlContext.SaveChanges();
                return true;
            }
            catch (IOException e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Delete(int id)
        {
            try
            {
                _postgreSqlContext.Set<Stock>().Remove(Select(id));
                _postgreSqlContext.SaveChanges();
                return true;
            }
            catch (IOException e)
            {
                throw new Exception(e.Message);
            }
        }

        public Stock Select(int id)
        {
            return _postgreSqlContext.Set<Stock>().Find(id);
        }

        public IList<Stock> Select()
        {
            return _postgreSqlContext.Set<Stock>().ToList();
        }

        public async Task<IList<Stock>> GetStocksByPage(int pPage, int pCount)
        {
            var stocks =  this._postgreSqlContext.Stocks.OrderBy(s => s.Id)
                .Skip(pCount * (pPage - 1))
                .Take(pCount)
                .ToList();
            
            foreach (var stock in stocks)
            {
                stock.Company = _postgreSqlContext.Companies.Find(stock.CompanyId);
            }

            return stocks;
        }

        public int CountStocks()
        {
            return this._postgreSqlContext.Stocks.Count();
        }
    }
}