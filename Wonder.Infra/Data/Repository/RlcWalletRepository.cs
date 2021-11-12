using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Wonder.Domain.Interfaces.Repository;
using Wonder.Domain.Models;
using Wonder.Infra.Data.Context;

namespace Wonder.Infra.Data.Repository
{
    public class RlcWalletRepository: BaseRepositoryImpl<RlcWalletTicket>, IRlcWalletRepository
    {
        public RlcWalletRepository(PostgreSqlContext postgreSqlContext) : base(postgreSqlContext)
        {
        }

        public async Task<RlcWalletTicket> GetLastRlcWalletTicket(int idCarteira, int idTicket)
        {
            var rlcWallet = await this._postgreSqlContext.RlcWalletTickets.AsNoTracking()
                .Where(w => w.IdWallet == idCarteira && w.IdTicket == idTicket)
                .OrderByDescending(r => r.Id)
                .FirstAsync();
            
            return rlcWallet;
        }
    }
}