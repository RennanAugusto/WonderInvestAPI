using System.Linq;
using Wonder.Domain.Models;
using Wonder.Service.Contracts.DTO;

namespace Wonder.Service.Util
{
    public static class ConvertClassToDto
    {
        public static StockDto ConvertStockClass(Stock pStock)
        {
            StockDto lStockDto = new StockDto();
            lStockDto.Code = pStock.Code;
            lStockDto.CompanyName = pStock.Company.Name;

            int I = 0;
            
            foreach (var price in pStock.PricesList)
            {
               PriceStockDto priceDto = new PriceStockDto();
                priceDto.Date = price.Date;
                priceDto.Price = price.Price;
                lStockDto.PriceList.Add(priceDto);
            }

            return lStockDto;
        }
    }
}