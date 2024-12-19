namespace CycleShare.Server.Entities
{
    public class User
    {
        public int Id { get; set; }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? Deleted { get; set; }

        public ICollection<Chat> Chats { get; set; }

        public ICollection<Bike> Bikes { get; set; }

        public ICollection<Booking> Bookings { get; set; }

        public ICollection<GalleryItem> GalleryItems { get; set; }
    }
}
