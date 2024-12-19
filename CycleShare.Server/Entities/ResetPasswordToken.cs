using System.ComponentModel.DataAnnotations.Schema;

namespace CycleShare.Server.Entities
{
    public class ResetPasswordToken
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public int UserId { get; set; }

        public string Token { get; set; }

        public bool Verified { get; set; }

        public DateTime ExpirationDate { get; set; }

        [NotMapped]
        public bool IsExpired => ExpirationDate < DateTime.Now;
    }
}
