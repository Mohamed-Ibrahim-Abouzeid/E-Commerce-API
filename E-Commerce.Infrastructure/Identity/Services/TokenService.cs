using E_Commerce.Application.Contracts;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static E_Commerce.Infrastructure.Identity.Services.TokenService;

namespace E_Commerce.Infrastructure.Identity.Services
{
    public class TokenService(IOptions<JWTSettings> jwtoptions) : ITokenService
    {
        private readonly JWTSettings _Settings = jwtoptions.Value;
        public string CreateToken(string userId, string userName, string email, IEnumerable<string> roles)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, userId),
                new(ClaimTypes.Name, userName),
                new(ClaimTypes.Email, email)
            };
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            if (string.IsNullOrEmpty(_Settings.SecretKey))
            {
                throw new InvalidOperationException("JWT secretkey is missing");
            }
            if (_Settings.SecretKey.Length < 32)
            {
                throw new InvalidOperationException("JWT secretkey is too short");
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Settings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var Token = new JwtSecurityToken(
                issuer: _Settings.Issuer,
                audience: _Settings.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(_Settings.ExpirationInMinutes),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
        public class JWTSettings
        {
            public string SecretKey { get; init; } = default!;
            public string Issuer { get; init; } = default!;
            public string Audience { get; init; } = default!;
            public int ExpirationInMinutes { get; init; } = 60;
        }
    }
}
