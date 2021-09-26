using Wonder.Domain.Interfaces.Repository;
using Wonder.Domain.Models;
using Wonder.Infra.Data.Context;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.DependencyInjection;

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
            
            var listPriceQry = this._postgreSqlContext.PriceStocks
                .AsNoTracking()
                .Where(s => s.StockId == stock.Id).ToList();

            stock.PricesList = new List<PriceStock>();
            foreach (var price in listPriceQry)
            {
               stock.PricesList.Add(price); 
            }

            return stock;
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
                _postgreSqlContext.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
    }
}