namespace CycleShare.Server.Entities
{
    public class Review
    {
        public int Id { get; set; }

        public int Value { get; set; }

        public DateTime CreatedDate { get; set; }   

        public DateTime? Deleted { get; set; }

        public string Advantages { get; set; }

        public string Disadvantages { get; set; }

        public string Comment { get; set; }

        public Booking Booking { get; set; }

        public ICollection<GalleryItem> GalleryItems { get; set; }
    }
}
