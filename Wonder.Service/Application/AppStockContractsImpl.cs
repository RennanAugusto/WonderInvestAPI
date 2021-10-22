using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Wonder.Domain.DomainServices;
using Wonder.Service.Contracts;
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

        public string GetStocksByPage(int pPage, string pCode)
        {
            var  listStock = this._stockService.GetStocksByPage(pPage, pCode);
            var dtoStocks = ConvertClassToDto.ConvertListStockClass(listStock);

            return JsonSerializer.Serialize(dtoStocks);
        }
    }
}