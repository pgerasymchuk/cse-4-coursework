namespace CycleShare.Server.Entities
{
    public class Booking
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal Price { get; set; }

        public string Status { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? Deleted { get; set; }

        public Bike Bike { get; set; }

        public User Renter { get; set; }

        public Address Address { get; set; }

        public ICollection<Payment> Payments { get; set; }
    }
}
