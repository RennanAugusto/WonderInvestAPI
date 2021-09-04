using System.Text.Json;
using Wonder.Domain.DomainServices;
using Wonder.Domain.Models;
using Wonder.Service.Contracts;
using Wonder.Service.Util;

namespace Wonder.Service.Application
{
    public class AppStockContractsImpl: IAppStockContracts
    {
        private StockService _stockService;

        public AppStockContractsImpl()
        {
            this._stockService = new StockService();
        }
        
        public string GetByCode(string pCode)
        {
            var stock = this._stockService.GetStockByCode(pCode);
            var stockDto = ConvertClassToDto.ConvertStockClass(stock);

            return JsonSerializer.Serialize(stockDto);
        }
        
        
    }
}