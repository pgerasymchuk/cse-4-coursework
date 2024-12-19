namespace CycleShare.Server.Entities
{
    public class Payment
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public decimal Amount { get; set; }

        public string Status { get; set; }  

        public DateTime CreatedDate { get; set; }

        public DateTime? Deleted { get; set; }

        public Booking Booking { get; set; }
    }
}
