using CycleShare.Server.Dtos;
using CycleShare.Server.Entities;

namespace CycleShare.Server.Services.Interfaces
{
    public interface IUserService
    {
        // User GetById(int userId, string[] includes = null);

        // User GetByEmail(string email, string[] includes = null);

        // User CreateUser(User user, string token, string password);

        User GetById(int userId);
        public User GetByEmail(string email);
        User CreateUser(SignUpDto signup);
        void ForgotPassword(string email);
        User ResetPassword(string token, string password);
    }
}
