using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wonder.Domain.Models;
using Wonder.Service.Contracts.DTO;

namespace Wonder.Service.Contracts
{
    public interface IAppStockContracts
    {
        JsonResult GetByCode(string pCode);
        Task<StockPaginationDTO> GetStocksByPage(int pPage, int pCount, string pCodeFilter);
        Task<bool> PostFavoriteStock(PostFavoriteDTO postFavorite);
        Task<IList<GetFavoriteDTO>> GetFavorites(string pIdUser);
    }
}