using System;
using System.IdentityModel.Tokens.Jwt;

namespace Wonder.Application.Token
{
    public class TokenJWT
    {
        private JwtSecurityToken Token;

        internal TokenJWT(JwtSecurityToken token)
        {
            this.Token = token;
        }

        public DateTime ValidTo => this.Token.ValidTo;

        public string Value => new JwtSecurityTokenHandler().WriteToken(this.Token);
    }
}