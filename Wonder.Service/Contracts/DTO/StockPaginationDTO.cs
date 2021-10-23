using System.Collections.Generic;

namespace Wonder.Service.Contracts.DTO
{
    public class StockPaginationDTO
    {
        public int ActualPage { get; set; }
        public int PreviousPage { get; set; }
        public int NextPage { get; set; }
        public bool ExistsNextPage { get; set; }
        public bool ExistsPreviousPage { get; set; }
        public IList<StockDto> ListStock {get; set; }
    }
}