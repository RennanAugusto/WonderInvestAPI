using System.Collections;
using Wonder.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Wonder.Domain.Interfaces.Repository
{
    public interface IStockRepository: IBaseRepository<Stock>
    {
        Stock GetByCode(string pCode);
        IList<Stock> GetByCompanyId(int pIdCompany);
        IList<Stock> GetByCompanyName(string pNameCompany);
        Task<IList<Stock>> GetStocksByPage(int pPage, int pCount, string pCodeFilter);
        int CountStocks(string pCodeFilter);
        Task<IList<StockProgression>> GetStockProgression(int stockId, string type);

    }
}