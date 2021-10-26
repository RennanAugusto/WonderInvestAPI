using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using Wonder.Application.Token;
using Wonder.Domain.Models;
using Wonder.Service.Contracts;
using Wonder.Service.Contracts.DTO;

namespace Wonder.Application.Controllers
{
    public class TokenController : Controller
    {
        private readonly IUserContracts _userService;

        public TokenController( IUserContracts userService)
        {
            this._userService = userService;
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/CreateToken")]
        public async Task<IActionResult> CreateToken([FromBody] LoginDTO input)
        {
            var result = await this._userService.GetToken(input);
            if (result.Success)
            {
                var token = new TokenJWTBuilder()
                    .AddSecurityKey(JwtSecurityKey.Create("Secret_Key-12345678"))
                    .AddKeyStr("Secret_Key-12345678")
                    .AddSubject(result.Id)
                    .AddIssuer("Teste.Security.Bearer")
                    .AddAudience("Teste Meu")
                    .AddClaim(input.UserName, "1")
                    .AddExpiry(600)
                    .Builder();
                result.Token = token;
                return Ok(Json(result));
            }
            else
            {
                return Unauthorized("Usuário ou senha inválidos");
            }
        }
        
        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/Register")]
        public async Task<IActionResult> RegisterUser([FromBody] InputUserDto input)
        {
            try
            {
                var result = await this._userService.Register(input);
                if (result.Success)
                {
                    return Ok(JsonSerializer.Serialize(result));
                }
                else
                {
                    return Problem(JsonSerializer.Serialize(result));
                }
            }
            catch (Exception)
            {
                return Problem("Erro desconhecido");
            }
        }

        [Authorize]
        [HttpGet]
        [Route("authenticated")]
        public string Authenticared()
        {
            string userId = User.FindFirst(ClaimTypes.Sid).Value;
            return String.Format("Autenticado {0}", userId);
        } 
    }
}