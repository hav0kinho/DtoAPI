using DtoAPI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DtoAPI.Services
{
    public static class TokenService
    {
        public static string Generate(User user)
        {
            // Cria o Handler para os JWT
            var handler = new JwtSecurityTokenHandler();

            // Resgata a chave privada
            var key = Encoding.ASCII.GetBytes(JWTProperties.SecretKey);

            var credentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                );

            // Cria as informações que estarão no token 
            var claims = new ClaimsIdentity();
            claims.AddClaim(
                    new Claim(ClaimTypes.Name, user.Name)
                );

            // Verifica as roles cadastradas no usuário resgatado e coloca elas como claims.
            foreach (var role in user.Roles)
            {
                claims.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddMinutes(1),
            };

            // Gera o token
            var token = handler.CreateToken(tokenDescriptor);

            // Cria a string do token que sera passada para o Front.
            var strToken = handler.WriteToken(token);

            return strToken;
        }
    }
}
