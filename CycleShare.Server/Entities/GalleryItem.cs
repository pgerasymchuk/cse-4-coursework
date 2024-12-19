namespace CycleShare.Server.Entities
{
    public class GalleryItem
    {
        public int Id { get; set; }

        public string Uri { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? Deleted { get; set; }

        public int SequenceNumber { get; set; }

        public Bike? Bike { get; set; }
        public int? BikeId { get; set; }

        public Review? Review { get; set; }
        public int? ReviewId { get; set; }

        public User? User { get; set; }
        public int? UserId { get; set; }
    }
}
