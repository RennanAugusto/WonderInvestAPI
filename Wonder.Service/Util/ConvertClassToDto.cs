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

        public static IList<InfoWalletDTO> ConvertListInfoWalletToDTO(IList<InfoWallet> infoWallets)
        {
            IList<InfoWalletDTO> listInfoWalletDTO = new List<InfoWalletDTO>();

            foreach (var info in infoWallets)
            {
                var infoWalletDTO = new InfoWalletDTO();
                
                infoWalletDTO.IdWallet = info.IdWallet;
                infoWalletDTO.IdUser = info.IdUsualo;
                infoWalletDTO.IdTicket = info.IdTicket;
                infoWalletDTO.Name = info.Name;
                infoWalletDTO.Code = info.Code;
                infoWalletDTO.LastPrice = info.LastStockPrice;
                infoWalletDTO.AveragePrice = info.AveragePrice;
                infoWalletDTO.Percent = info.Percent;
                infoWalletDTO.Amount = info.Amount;
                infoWalletDTO.TotalStock = info.TotalStock;
                listInfoWalletDTO.Add(infoWalletDTO);
            }

            return listInfoWalletDTO;
        }
    }
}