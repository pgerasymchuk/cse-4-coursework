namespace CycleShare.Server.Entities
{
    public class Credentials
    {
        public int Id { get; set; }

        public byte[] Password { get; set; }

        public byte[] Salt { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }

        public DateTime? Deleted { get; set; }
    }
}
