namespace CycleShare.Server.Entities
{
    public class Bike
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Type { get; set; }

        public string Wheelsize { get; set; }

        public string Material { get; set; }

        public decimal Rating { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool Active { get; set; }

        public DateTime? Deleted { get; set; }

        public User User { get; set; }

        public Address Address { get; set; }

        public ICollection<Booking> Bookings { get; set; }

        public ICollection<Chat> Chats { get; set; }

        public ICollection<GalleryItem> GalleryItems { get; set; }
    }
}
