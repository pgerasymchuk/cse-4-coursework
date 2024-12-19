namespace CycleShare.Server.Entities
{
    public class Address
    {
        public int Id { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string Building { get; set; }

        public string Note { get; set; }

        public DateTime? Deleted { get; set; }
    }
}
