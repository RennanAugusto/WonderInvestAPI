using System;
using System.Linq;
using System.Threading.Tasks;
using Wonder.Domain.Interfaces.Repository;
using Wonder.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Wonder.Infra.Data.Context;

namespace Wonder.Infra.Data.Repository
{
    public class WalletRepository: BaseRepositoryImpl<Wallet>, IWalletRepository
    {
        public WalletRepository(IServiceProvider provider) : base(provider)
        {
        }

        public async Task<Wallet> GetWalletByUser(string user)
        {
            var wallet =  await this._postgreSqlContext.Wallet
                .Where(w => w.IdWonderUsers == user)
                .FirstAsync();
            return wallet;
        }
    }
}