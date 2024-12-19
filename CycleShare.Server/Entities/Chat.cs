namespace CycleShare.Server.Entities
{
    public class Chat
    {

        public int Id { get; set; }

        public string Status { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? Deleted { get; set; }

        public Bike Bike { get; set; }

        public User Renter { get; set; }

        public ICollection<Message> Messages { get; set; }
    }
}
