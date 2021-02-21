using Microsoft.IdentityModel.Tokens;
using Results.Common;
using Results.Model.Common;
using Results.Service.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Results.Service
{
    public class TokenService : ITokenService
    {
        public async Task<string> GenerateTokenAsync(IUser user, bool rememberMe)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            var claimsIdentity = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FullName),
                new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "AppUser"),
            });

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Convert.FromBase64String(TokenSettings.Secret));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Issuer = TokenSettings.Issuer,
                Audience = TokenSettings.Audience,
                Expires = rememberMe ? DateTime.Now.AddDays(30) : DateTime.Now.AddDays(7),
                SigningCredentials = signingCredentials,
            };

            JwtSecurityToken token = await Task.Run(() =>
                tokenHandler.CreateJwtSecurityToken(tokenDescriptor));

            return await Task.Run(() => tokenHandler.WriteToken(token));
        }

        public async Task<ClaimsPrincipal> GetPrincipalAsync(string token)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

                JwtSecurityToken jwtToken = await Task.Run(() => (JwtSecurityToken)tokenHandler.ReadToken(token));

                if (jwtToken == null)
                {
                    return null;
                }

                TokenValidationParameters validationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = TokenSettings.Issuer,
                    ValidAudience = TokenSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(TokenSettings.Secret))
                };

                SecurityToken securityToken;

                return await Task.Run(() => tokenHandler.ValidateToken(token, validationParameters, out securityToken));
            }
            catch
            {
                return null;
            }
        }
    }
}
