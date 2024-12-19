using CycleShare.Server.Dtos;
using CycleShare.Server.Entities;

namespace CycleShare.Server.Services.Interfaces
{
    public interface IAuthService
    {
        Credentials AuthUser(string email, string password);
        JsonWebToken GenerateAccessToken(Credentials c);
        JsonWebToken GenerateAccessToken(int userId);
        JsonWebToken RefreshAccessToken(string token);
        string CreateRefreshToken(int userId);
        void RevokeRefreshToken(string token);
        void RevokeRefreshToken(int userId);
    }
}
