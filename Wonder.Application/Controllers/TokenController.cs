using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Wonder.Application.Token;
using Wonder.Domain.Models;

namespace Wonder.Application.Controllers
{
    public class TokenController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public TokenController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> usermanager)
        {
            this._userManager = usermanager;
            this._signInManager = signInManager;
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/CreateToken")]
        public async Task<IActionResult> CreateToken([FromBody] LoginModel.InputModel input)
        {
            if (string.IsNullOrWhiteSpace(input.Email) || string.IsNullOrWhiteSpace(input.Password))
                return Unauthorized();

            var result =
                await _signInManager.PasswordSignInAsync(input.Email, input.Password, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var token = new TokenJWTBuilder()
                    .AddSecurityKey(JwtSecurityKey.Create("Secrete_Key-12345678"))
                    .AddSubject("WonderInvest")
                    .AddIssuer("Teste.Security.Bearer")
                    .AddAudience("Teste Meu")
                    .AddClaim("Usuario1", "1")
                    .AddExpiry(5)
                    .Builder();

                return Ok(token.Value);
            }
            else
            {
                return Unauthorized();
            }
        }
        
        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/Register")]
        public async Task<IActionResult> RegisterUser([FromBody] LoginModel.InputModel input)
        {
            if (string.IsNullOrWhiteSpace(input.Email) || string.IsNullOrWhiteSpace(input.Password))
                return Unauthorized();

            var user = new ApplicationUser();
            user.Email = input.Email;
            user.UserName = input.Email;

            var result = await this._userManager.CreateAsync(user, input.Password);
            
            if (result.Succeeded)
            {
                return Ok("Usuario Criado");
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet("api/HashExample")]
        public async Task<IActionResult> GetHash(string pValue)
        {
            return Ok(Encoding.ASCII.GetBytes(pValue));
        }
    }
}