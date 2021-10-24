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
            var  listStock = await this._stockService.GetStocksByPage(pPage, pCount, pCodeFilter);
            var dtoStocks = ConvertClassToDto.ConvertListStockClass(listStock);
            result.ListStock = dtoStocks;
            result.ActualPage = pPage;
            result.NextPage = pPage + 1;
            result.PreviousPage = pPage - 1;
            result.ExistsNextPage = (pPage * pCount) < (this._stockService.CountStocks(pCodeFilter));
            result.ExistsPreviousPage = (pPage > 1);
            return result;
        }
    }
}