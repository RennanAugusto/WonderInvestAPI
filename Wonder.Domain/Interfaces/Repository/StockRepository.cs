using Wonder.Domain.Models;
using System.Collections.Generic;


namespace Wonder.Domain.Interfaces.Repository
{
    public interface IStockRepository: IBaseRepository<Stock>
    {
        Stock GetByCode(string pCode);
        IList<Stock> GetByCompanyId(int pIdCompany);
        IList<Stock> GetByCompanyName(string pNameCompany);

    }
}