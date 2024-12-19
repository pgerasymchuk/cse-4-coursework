namespace CycleShare.Server.Dtos
{
    public class BikeFilterParams
    {
        public string? SortBy { get; set; } = "CreatedDate";
        public string? Order { get; set; } = "desc";
        public string? Type { get; set; }
        public string? Wheelsize { get; set; }
        public string? Material { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public decimal? MinRating { get; set; }
        public int? PageNumber { get; set; } = 1;
        public int? PageSize { get; set; } = 10;
    }
}
