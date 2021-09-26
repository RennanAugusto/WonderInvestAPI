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
        private readonly IAppStockContracts _stockService;

        public StockController(IAppStockContracts stockService)
        {
            _stockService = stockService;
        }

        [HttpGet]
        public JsonResult GetStockByCode(string pCode)
        {
            return _stockService.GetByCode(pCode);
        }

    }
}