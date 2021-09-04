using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNurse.Injector.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Wonder.Service.Contracts;

namespace Wonder.Application.Controllers
{    
    [ApiController]
    [Route("[controller]")]
    
    public class StockController: ControllerBase
    {
        [InjectService] private IAppStockContracts StockService;
        
        [HttpGet]
        public string GetStockByCode(string pCode)
        {
            return StockService.GetByCode(pCode);
        }

    }
}