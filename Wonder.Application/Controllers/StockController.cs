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
    
    public class StockController: Controller
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
        
        [HttpGet("/Stock/Paginator")]
        [Produces("application/json")]
        public async Task<IActionResult> GetStocksByPage(int pPage, int pCount)
        {
            try
            {
                var stocks = await _stockService.GetStocksByPage(pPage, pCount);
                return Ok(Json(stocks));
            }
            catch (Exception e)
            {
                return BadRequest("Deu ruim Raryson");
            }
        }
        
        // [Produces("application/json")]
        // [HttpPost("/api/Register")]
        // public async Task<IActionResult> RegisterUser([FromBody] InputUserDto input)
        // {
        //     try
        //     {
        //         var result = await this._userService.Register(input);
        //         if (result.Success)
        //         {
        //             return Ok(JsonSerializer.Serialize(result));
        //         }
        //         else
        //         {
        //             return Problem(JsonSerializer.Serialize(result));
        //         }
        //     }
        //     catch (Exception)
        //     {
        //         return Problem("Erro desconhecido");
        //     }
        // }
    }
}