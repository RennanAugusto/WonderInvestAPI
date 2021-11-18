using System.Collections.Generic;
using System.Threading.Tasks;
using Wonder.Domain.Models;

namespace Wonder.Domain.Interfaces.Repository
{
    public interface IRlcWalletRepository: IBaseRepository<RlcWalletTicket>
    {
        Task<RlcWalletTicket> GetLastRlcWalletTicket(int idCarteira, int idTicket);
        Task<IList<InfoWallet>> GetInfoWallet(string user);
    }
}