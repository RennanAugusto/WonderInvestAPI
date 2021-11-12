using System.Threading.Tasks;
using Wonder.Domain.Models;

namespace Wonder.Domain.Interfaces.Repository
{
    public interface IRlcWalletRepository: IBaseRepository<RlcWalletTicket>
    {
        Task<RlcWalletTicket> GetLastRlcWalletTicket(int idCarteira, int idTicket);
    }
}