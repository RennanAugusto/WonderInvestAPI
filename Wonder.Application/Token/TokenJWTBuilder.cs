using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Wonder.Application.Token
{
    public class TokenJWTBuilder
    {
        private SecurityKey _securityKey = null;
        private string _subject = "";
        private string _issuer = "";
        private string _audience = "";
        private Dictionary<string, string> _claims = new Dictionary<string, string>();
        private int _expiryInMinutes = 5;
        private string _keyStr;

        public TokenJWTBuilder AddSecurityKey(SecurityKey securityKey)
        {
            this._securityKey = securityKey;
            return this;
        }     
        
        public TokenJWTBuilder AddSubject(string subject)
        {
            this._subject = subject;
            return this;
        }
        
        public TokenJWTBuilder AddKeyStr(string key)
        {
            this._keyStr = key;
            return this;
        }

        public TokenJWTBuilder AddIssuer(string issuer)
        {
            this._issuer = issuer;
            return this;
        }

        public TokenJWTBuilder AddAudience(string audience)
        {
            this._audience = audience;
            return this;
        }

        public TokenJWTBuilder AddClaim(string type, string value)
        {
            this._claims.Add(type, value);
            return this;
        }

        public TokenJWTBuilder AddClaims(Dictionary<string, string> claims)
        {
            this._claims.Union(claims);
            return this;
        }

        public TokenJWTBuilder AddExpiry(int expiryInMinutes)
        {
            this._expiryInMinutes = expiryInMinutes;
            return this;
        }
        
        private void EnsureArguments()
        {
            if (this._securityKey == null)
                throw new ArgumentNullException("Security Key");

            if (string.IsNullOrEmpty(this._subject))
                throw new ArgumentNullException("Subject");

            if (string.IsNullOrEmpty(this._issuer))
                throw new ArgumentNullException("Issuer");

            if (string.IsNullOrEmpty(this._audience))
                throw new ArgumentNullException("Audience");
        }
        
        public string Builder()
        {
            EnsureArguments();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this._keyStr);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Sid,this._subject),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                
                Expires = DateTime.UtcNow.AddMinutes(this._expiryInMinutes),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            // var claims = new List<Claim>
            // {
            //     new Claim(JwtRegisteredClaimNames.Sub,this._subject),
            //     new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            // }.Union(this._claims.Select(item => new Claim(item.Key, item.Value)));

            // var token = new JwtSecurityToken(
            //     issuer: this._issuer,
            //     audience: this._audience,
            //     claims: claims,
            //     expires: DateTime.UtcNow.AddMinutes(_expiryInMinutes),
            //     signingCredentials: new SigningCredentials(
            //         this._securityKey,
            //         SecurityAlgorithms.HmacSha256)
            //
            // );
            // var token2 = new JwtSecurityTokenHandler().WriteToken(token);

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

            //return new TokenJWT(token);

        }

    }
}