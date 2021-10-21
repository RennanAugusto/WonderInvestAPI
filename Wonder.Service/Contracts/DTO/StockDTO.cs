using System.Collections.Generic;

namespace Wonder.Service.Contracts.DTO
{
    public class StockDto
    {
        public StockDto()
        {
            this.PriceList = new List<PriceStockDto>();
        }
        public string Code { get; set; }
        public string CompanyName { get; set; }
        public List<PriceStockDto> PriceList { get; }
    }

    public class ListStocksDto : List<StockDto>
    {
        
    }
}