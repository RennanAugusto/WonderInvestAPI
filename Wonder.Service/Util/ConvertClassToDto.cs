using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Wonder.Domain.Models;
using Wonder.Service.Contracts.DTO;
using Wonder.Service.Contracts.DTO.Wallet;

namespace Wonder.Service.Util
{
    public static class ConvertClassToDto
    {
        public static StockDto ConvertStockClass(Stock pStock)
        {
            StockDto lStockDto = new StockDto();
            lStockDto.Id = pStock.Id;
            lStockDto.Code = pStock.Code;
            lStockDto.CompanyName = pStock.Company.Name;
            lStockDto.CompanyLogo64 =  pStock.Company.LogoBase64;
            
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
            ListStocksDto listStockDto = new ListStocksDto();
            foreach (var stock in pListStock)
            {
                listStockDto.Add(ConvertStockClass(stock));
            }
            return listStockDto;
        }

        public static StockFavorites ConvertFavoritesDTOToClass(PostFavoriteDTO postFavoriteDTO)
        {
            var favorite = new StockFavorites();
            favorite.Id = postFavoriteDTO.IdFavorite;
            favorite.IdWonderUsers = postFavoriteDTO.GetUser();
            favorite.StockId = postFavoriteDTO.IdStock;
            favorite.Active = postFavoriteDTO.Active;
            return favorite;
        }

        public static GetFavoriteDTO ConvertStockFavoriteToGetDTO(StockFavorites favorite)
        {
            var favoriteDTo = new GetFavoriteDTO();
            favoriteDTo.IdFavorite = favorite.Id;
            favoriteDTo.IdStock = favorite.StockId;
            favoriteDTo.IdUser = favorite.IdWonderUsers;
            favoriteDTo.Stock = ConvertStockClass(favorite.Stock);
            favoriteDTo.Active = favorite.Active;
            return favoriteDTo;
        }

        public static IList<GetFavoriteDTO> ConvertListStockFavoriteToGetDTO(IList<StockFavorites> favoritesList)
        {
            var listFavoritosDTO = new List<GetFavoriteDTO>();
            foreach (var fav in favoritesList)
            {
               listFavoritosDTO.Add(ConvertStockFavoriteToGetDTO(fav)); 
            }

            return listFavoritosDTO;
        }

        public static InfoWalletDTO ConvertListInfoWalletToDTO(IList<InfoWallet> infoWallets)
        {
            InfoWalletDTO infoWallet = new InfoWalletDTO();
            foreach (var info in infoWallets)
            {
                var infoTicketDTO = new InfoTicketDTO();
                
                infoTicketDTO.IdWallet = info.IdWallet;
                infoTicketDTO.IdUser = info.IdUsualo;
                infoTicketDTO.IdTicket = info.IdTicket;
                infoTicketDTO.Name = info.Code;
                infoTicketDTO.CompanyName = info.Name;
                infoTicketDTO.LastPrice = info.LastStockPrice;
                infoTicketDTO.AveragePrice = info.AveragePrice;
                infoTicketDTO.Percent = info.Percent;
                infoTicketDTO.Amount = info.Amount;
                infoTicketDTO.TotalStock = info.TotalStock;
                infoTicketDTO.TotalVariation = info.TotalVariation;
                infoTicketDTO.CompanyLogo = info.CompanyLogo;
                infoTicketDTO.LastOperation = info.LastOperation;
                infoWallet.ListInfoTicket.Add(infoTicketDTO);
                infoWallet.TotalTicketsValue += infoTicketDTO.LastPrice * infoTicketDTO.Amount;
                infoWallet.TotalWalletValue += infoTicketDTO.AveragePrice * infoTicketDTO.Amount;
            }

            infoWallet.PercentWallet =
                (( infoWallet.TotalTicketsValue - infoWallet.TotalWalletValue) / infoWallet.TotalWalletValue) * 100;

            return infoWallet;
        }

        public static IList<StockProgressionDTO> ConvertListProgressionToListDTO(IList<StockProgression> listStockProgression)
        {
            var listProgressionDTO = new List<StockProgressionDTO>();

            foreach (var item in listStockProgression)
            {
                var stockProgressionDTO = new StockProgressionDTO();
                stockProgressionDTO.Code = item.Code;
                stockProgressionDTO.Date = item.Date;
                stockProgressionDTO.Description = item.Description.Trim();
                stockProgressionDTO.Legend = item.Legend;
                stockProgressionDTO.Price = item.Price;
                stockProgressionDTO.StockId = item.StockId;
                stockProgressionDTO.TypeProgression = item.TypeProgression;
                listProgressionDTO.Add(stockProgressionDTO);
            }

            return listProgressionDTO;
        }
    }
}