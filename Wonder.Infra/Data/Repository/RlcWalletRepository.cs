using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wonder.Domain.Interfaces.Repository;
using Wonder.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Wonder.Infra.Data.Context;

namespace Wonder.Infra.Data.Repository
{
    public class RlcWalletRepository: BaseRepositoryImpl<RlcWalletTicket>, IRlcWalletRepository
    {
        public RlcWalletRepository(IServiceProvider provider) : base(provider)
        {
        }

        public async Task<RlcWalletTicket> GetLastRlcWalletTicket(int idCarteira, int idTicket)
        {
            var rlcWallet = await this._postgreSqlContext.RlcWalletTickets.AsNoTracking()
                .Where(w => w.IdWallet == idCarteira && w.IdTicket == idTicket)
                .OrderByDescending(r => r.Id)
                .FirstOrDefaultAsync();
            
            return rlcWallet;
        }

        public async Task<IList<InfoWallet>> GetInfoWallet(string user)
        {
            var infoWallets = this._postgreSqlContext.InfoWallets
                .FromSqlRaw(String.Format("SELECT I.IdInfo, I.IdUsualo, I.IdWallet, I.IdTicket, I.Code, " + 
                                          "I.LastStockPrice, I.Amount, I.AveragePrice, I.Percent, I.TotalStock, I.Name FROM InfoWallet AS I WHERE I.IdUsualo = '{0}'", user))
                .ToList();
            
            return infoWallets;
        }
    }
}