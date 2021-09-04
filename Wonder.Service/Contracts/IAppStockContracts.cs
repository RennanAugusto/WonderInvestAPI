using Wonder.Domain.Models;

namespace Wonder.Service.Contracts
{
    public interface IAppStockContracts
    {
        string GetByCode(string pCode);
    }
}