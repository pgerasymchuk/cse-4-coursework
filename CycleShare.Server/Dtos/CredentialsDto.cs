using System.Text.Json.Serialization;

namespace CycleShare.Server.Dtos
{
    public class CredentialsDto
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
