using Wonder.Domain.Interfaces.Repository;
using Wonder.Domain.Models;
using Wonder.Infra.Data.Context;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;

namespace Wonder.Infra.Data.Repository
{
    public class StockRepositoryImpl: BaseRepositoryImpl<Stock>, IStockRepository
    {
        public StockRepositoryImpl(PostgreSqlContext postgreSqlContext): base(postgreSqlContext)
        {
            this._postgreSqlContext = postgreSqlContext;
        }
        public Stock GetByCode(string pCode)
        {
            return this._postgreSqlContext.Set<Stock>().Find(pCode);
        }

        public IList<Stock> GetByCompanyName(string pNameCompany)
        {
            throw new NotImplementedException(); 
        }

        public IList<Stock> GetByCompanyId(int pIdCompany)
        {
            throw new NotImplementedException();
        }
    }
}