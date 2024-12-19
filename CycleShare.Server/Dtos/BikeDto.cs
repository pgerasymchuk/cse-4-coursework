namespace CycleShare.Server.Dtos
{
    public class BikeDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal Rating { get; set; }

        public string Type { get; set; }

        public string Wheelsize { get; set; }

        public string Material { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool Active { get; set; }

        public int UserId { get; set; }

        public int AddressId { get; set; }

        public ICollection<int>? BookingsIds { get; set; }

        public ICollection<int>? ChatsIds { get; set; }

        public ICollection<string>? ImageUrls { get; set; }
    }
}
