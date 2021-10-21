using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using DotNurse.Injector.Attributes;
using Microsoft.AspNetCore.Http;
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
        public string GetStockByCode(string pCode)
        {
            try
            {
                var stock = _stockService.GetByCode(pCode);
                return stock.ToString();
            }
            catch
            {
                Response.Clear();
                Response.StatusCode = 404;
                throw new HttpListenerException(404, "Stock: " + pCode + " Not Found");
            }
        }
        
        [HttpGet]
        [Route("StocksByPage")]
        public string GetStocksByPage(int pPage, string pCode)
        {
            try
            {
                var stocks = _stockService.GetStocksByPage(pPage, pCode);
                return stocks;
            }
            catch (Exception e)
            {
                throw new HttpListenerException(404, "Stocks: Not Found");
            }
        }
    }
}