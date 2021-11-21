using System;
using System.Collections.Generic;
using System.IO;
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
            var infoWallets = await this._postgreSqlContext.InfoWallets
                .Where(i=> i.IdUsualo == user)
                .ToListAsync();
            
            return infoWallets;
        }
    }
}