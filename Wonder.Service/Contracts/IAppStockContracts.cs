using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wonder.Domain.Models;
using Wonder.Service.Contracts.DTO;

namespace Wonder.Service.Contracts
{
    public interface IAppStockContracts
    {
        JsonResult GetByCode(string pCode);

        Task<StockPaginationDTO> GetStocksByPage(int pPage, int pCount);
    }
}