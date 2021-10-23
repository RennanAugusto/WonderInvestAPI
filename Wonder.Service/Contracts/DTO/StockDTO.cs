using System.Collections.Generic;

namespace Wonder.Service.Contracts.DTO
{
    public class StockDto
    {
        
        public int Id { get; set; }
        public string Code { get; set; }
        public string CompanyName { get; set; }
        
        public string CompanyLogo64 { get; set; }
        public List<PriceStockDto> PriceList { get; }
        public StockDto()
        {
            this.PriceList = new List<PriceStockDto>();
        }
        
    }

    public class ListStocksDto : List<StockDto>
    {
        
    }
}