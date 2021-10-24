using System.Collections;
using System.Collections.Generic;
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

            foreach (var price in pStock.PricesList)
            {
               PriceStockDto priceDto = new PriceStockDto();
                priceDto.Date = price.Date;
                priceDto.Price = price.Price;
                lStockDto.PriceList.Add(priceDto);
            }

            return lStockDto;
        }

        public static ListStocksDto ConvertListStockClass(IList<Stock> pListStock)
        {
            var rarysonbot = true;
            ListStocksDto listStockDto = new ListStocksDto();
            foreach (var stock in pListStock)
            {
                StockDto stockDto = new StockDto();
                stockDto.Id = stock.Id;
                stockDto.Code = stock.Code;
                stockDto.CompanyName = stock.Company.Name;
                stockDto.CompanyLogo64 = rarysonbot ?  "": stock.Company.LogoBase64;
                listStockDto.Add(stockDto);
            }
            return listStockDto;
        }
    }
}