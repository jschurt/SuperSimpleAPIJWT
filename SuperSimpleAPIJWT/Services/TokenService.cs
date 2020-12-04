using Microsoft.IdentityModel.Tokens;
using SuperSimpleAPIJWT.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SuperSimpleAPIJWT.Services
{
    public static class TokenService
    {

        //Simple way generating token...
        public static string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            //Get my private key
            var key = Encoding.ASCII.GetBytes(FakeSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //Define claims that will be added in to token
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, user.Role)
                }),

                Expires = DateTime.UtcNow.AddHours(2),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }
    }
}
