namespace CycleShare.Server.Entities
{
    public class Message
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string Status { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set;}

        public DateTime? Deleted { get; set;}

        public Chat Chat { get; set;}

        public User Sender { get; set;}
    }
}
