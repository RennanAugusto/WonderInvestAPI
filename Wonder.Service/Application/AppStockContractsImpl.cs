using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wonder.Domain.DomainServices;
using Wonder.Service.Contracts;
using Wonder.Service.Contracts.DTO;
using Wonder.Service.Util;

namespace Wonder.Service.Application
{
    public class AppStockContractsImpl: IAppStockContracts
    {
        private readonly StockService _stockService;

        public AppStockContractsImpl(StockService pStockService)
        {
            this._stockService = pStockService;
        }
        
        public JsonResult GetByCode(string pCode)
        {
            var stock = this._stockService.GetStockByCode(pCode);
            var stockDto = ConvertClassToDto.ConvertStockClass(stock);

            return new JsonResult(JsonSerializer.Serialize(stockDto));
        }

        public async Task<StockPaginationDTO> GetStocksByPage(int pPage, int pCount, string pCodeFilter)
        {
            var result = new StockPaginationDTO();
            var listStock = await this._stockService.GetStocksByPage(pPage, pCount, pCodeFilter);
            var totalStocks = this._stockService.CountStocks(pCodeFilter);
            var dtoStocks = ConvertClassToDto.ConvertListStockClass(listStock);
            result.ListStock = dtoStocks;
            result.ActualPage = pPage;
            result.NextPage = pPage + 1;
            result.PreviousPage = pPage - 1;
            result.ExistsNextPage = (pPage * pCount) < totalStocks;
            //decimal totalPages = Decimal.Divide(Convert.ToDecimal(totalStocks), Convert.ToDecimal(pCount));
            //result.TotalPages = System.Math.Ceiling(Convert.ToInt32(totalPages));
            result.ExistsPreviousPage = (pPage > 1);
            return result;
        }

        public async Task<bool> PostFavoriteStock(PostFavoriteDTO postFavorite)
        {
            return await this._stockService.PostStockFavorite(ConvertClassToDto.ConvertFavoritesDTOToClass(postFavorite));
        }

        public async Task<IList<GetFavoriteDTO>> GetFavorites(string pIdUser)
        {
            var favorites = await this._stockService.GetFavorites(pIdUser);
            var favoritesDTO = ConvertClassToDto.ConvertListStockFavoriteToGetDTO(favorites);
            foreach (var fav in favoritesDTO)
            {
                var percentual = (fav.Stock.PriceList[1].Price - fav.Stock.PriceList[0].Price) / 
                                 fav.Stock.PriceList[0].Price * 100;
                fav.Stock.Percentual = percentual;
            }

            return favoritesDTO;
        }
    }
}