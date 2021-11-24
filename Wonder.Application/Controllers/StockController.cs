using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wonder.Service.Contracts;
using Wonder.Service.Contracts.DTO;

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
        
        [Authorize]
        [HttpGet("/Stock/Paginator")]
        [Produces("application/json")]
        public async Task<IActionResult> GetStocksByPage(int pPage, int pCount, string pCodeFilter)
        {
            try
            {
                var stocks = await _stockService.GetStocksByPage(pPage, pCount, pCodeFilter);
                return Ok(Json(stocks));
            }
            catch (Exception e)
            {
                return BadRequest("Deu ruim Raryson: " + e.Message);
            }
        }
        
        [Authorize]
        [HttpPost("/Stock/PostFavorite")]
        [Produces("application/json")]
        public async Task<IActionResult> PostFavorite([FromBody] PostFavoriteDTO postFavorite)
        {
            try
            {
                var idUser = User.FindFirst(ClaimTypes.Sid)?.Value;
                postFavorite.SetUser(idUser);
                var result = await  this._stockService.PostFavoriteStock(postFavorite);

                if (result)
                    return Ok("Success");
                else
                    return BadRequest("Vixe");
            }
            catch (Exception e)
            {
                return BadRequest("Error: " + e.Message);
            }
        }
        
        [Authorize]
        [HttpGet("/Stock/GetFavorites")]
        [Produces("application/json")]
        public async Task<IActionResult> GetFavorites()
        {
            try
            {
                var idUser = User.FindFirst(ClaimTypes.Sid)?.Value;
                var result = await  this._stockService.GetFavorites(idUser);
                return Ok(Json(result));
            }
            catch (Exception e)
            {
                return BadRequest("Error: " + e.Message);
            }
        }
        
        [Authorize]
        [HttpGet("/Stock/GetInfoWallet")]
        [Produces("application/json")]
        public async Task<IActionResult> GetInfoWallet()
        {
            try
            {
                var idUser = User.FindFirst(ClaimTypes.Sid)?.Value;
                var result = await this._stockService.GetInfoWallet(idUser);
                return Ok(Json(result));
            }
            catch (Exception e)
            {
                return BadRequest("Error: " + e.Message);
            }
        }
        
        [Authorize]
        [HttpPost("/Stock/PostOperation")]
        [Produces("application/json")]
        public async Task<IActionResult> PostOperation([FromBody] RlcWalletDTO rlcWallet)
        {
            try
            {
                var idUser = User.FindFirst(ClaimTypes.Sid)?.Value;
                rlcWallet.IdUser = idUser;
                var result = await this._stockService.PostPurchase(rlcWallet);

                if (result)
                    return Ok("Success");
                else
                    return BadRequest("Vixe");
            }
            catch (Exception e)
            {
                return BadRequest("Error: " + e.Message);
            }
        }
        
        [Authorize]
        [HttpGet("/Stock/StockProgression")]
        [Produces("application/json")]
        public async Task<IActionResult> GetStockProgression(int stockId, string type)
        {
            try
            {
                var result =
                    await this._stockService.GetStockProgression(stockId,
                        type);
                return Ok(Json(result));
            }
            catch (Exception e)
            {
                return BadRequest("Error: " + e.Message);
            }
        }

    }
}