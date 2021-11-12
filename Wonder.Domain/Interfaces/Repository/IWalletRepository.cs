using System.Threading.Tasks;
using Wonder.Domain.Models;

namespace Wonder.Domain.Interfaces.Repository
{
    public interface IWalletRepository: IBaseRepository<Wallet>
    {
        Task<Wallet> GetWalletByUser(string user);
    }
}