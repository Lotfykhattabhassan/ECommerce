using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MiniECommerce.Modules.Identity.Application.Abstractions.Security;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MiniECommerce.Modules.Identity.Infrastructure.Security
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtOptions _jwtOptions;
        public JwtTokenGenerator(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;

            if (string.IsNullOrWhiteSpace(_jwtOptions.SecretKey))
                throw new InvalidOperationException(
                    "JWT SecretKey is missing from configuration.");
        }

        public JwtTokenResult Generate(Guid userId, string email, IReadOnlyCollection<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, email)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var expirationDate = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpirationMinutes);

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: expirationDate,
                signingCredentials: credentials
            );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            return new JwtTokenResult
            (accessToken, expirationDate);
        }
    }
}
