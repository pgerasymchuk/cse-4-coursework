using CycleShare.Server.Entities;

namespace CycleShare.Server.Services.Interfaces
{
    public interface IGalleryItemService
    {
        public Task SaveImagesAsync(int bikeId, List<IFormFile> images);
        public Task<List<GalleryItem>> GetImagesByBikeIdAsync(int bikeId);
    }
}
