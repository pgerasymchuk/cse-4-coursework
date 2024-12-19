using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CycleShare.Server.Dtos;
using CycleShare.Server.Entities;
using CycleShare.Server.Helpers;
using CycleShare.Server.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CycleShare.Server.Services
{
    public class AuthService : IAuthService
    {
        private readonly DbContext _context;
        private readonly AppSettings _appSettings;

        public AuthService(DbContext context, AppSettings appSettings)
        {
            _context = context;
            _appSettings = appSettings;
        }

        public Credentials AuthUser(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            Credentials credentials = _context.Set<Credentials>()
                .Include(c => c.User)
                .SingleOrDefault(c => c.User.Email.Equals(email));
            if (credentials == null)
            {
                return null;
            }
            if (!PasswordUtils.VerifyPasswordHash(password, credentials.Password, credentials.Salt))
            {
                return null;
            }

            return credentials;
        }

        public string CreateRefreshToken(int userId)
        {
            var token = Guid.NewGuid().ToString().Replace("-", "");
            _context.Set<RefreshToken>().Add(new RefreshToken { UserId = userId, Token = token });
            _context.SaveChanges();
            return token;
        }

        public JsonWebToken GenerateAccessToken(Credentials c)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_appSettings.Jwt.Secret);
            var now = DateTime.UtcNow;
            var expiresIn = TimeSpan.FromMinutes(_appSettings.Jwt.ExpirationPeriod);
            var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, c.UserId.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, c.User.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _appSettings.Jwt.Issuer,
                Audience = _appSettings.Jwt.Audience,
                Expires = now.Add(expiresIn),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new JsonWebToken
            {
                AccessToken = tokenHandler.WriteToken(token),
                ExpiresIn = (int)expiresIn.TotalSeconds,
            };
        }

        public JsonWebToken GenerateAccessToken(int userId)
        {
           Credentials credentials = _context.Set<Credentials>()
                .SingleOrDefault(c => c.UserId == userId);
            if (credentials == null)
            {
                throw new Exception("An error ocurred while generating access token");
            }
            return GenerateAccessToken(credentials);
        }

        public JsonWebToken RefreshAccessToken(string token)
        {
            var refreshToken = GetRefreshToken(token);
            if (refreshToken == null || refreshToken.Revoked)
            {
                throw new Exception("Refresh token was not found");
            }
            var jwt = GenerateAccessToken(refreshToken.UserId);
            jwt.RefreshToken = refreshToken.Token;
            return jwt;
        }

        public void RevokeRefreshToken(string token)
        {
            var refreshToken = GetRefreshToken(token);
            if (refreshToken == null || refreshToken.Revoked)
            {
                throw new Exception("Refresh token was not found");
            }
            refreshToken.Revoked = true;
            _context.SaveChanges();
        }

        public void RevokeRefreshToken(int userId)
        {
            var tokens = _context.Set<RefreshToken>()
                                 .Where(t => t.UserId == userId && !t.Revoked);
            foreach (RefreshToken token in tokens)
            {
                token.Revoked = true;
            }
            _context.SaveChanges();
        }

        private RefreshToken GetRefreshToken(string token)
        {
            return _context.Set<RefreshToken>()
                           .SingleOrDefault(x => x.Token == token);
        }
        
    }
}
