using CycleShare.Server.Dtos;

namespace CycleShare.Server.Services.Interfaces
{
    public interface IMailService
    {
        void SendForgotPasswordEmail(EmailDto email);
    }
}
