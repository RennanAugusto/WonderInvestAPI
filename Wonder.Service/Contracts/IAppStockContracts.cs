using Microsoft.AspNetCore.Mvc;
using Wonder.Domain.Models;

namespace Wonder.Service.Contracts
{
    public interface IAppStockContracts
    {
        JsonResult GetByCode(string pCode);
    }
}