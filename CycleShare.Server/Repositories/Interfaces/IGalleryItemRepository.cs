using CycleShare.Server.Entities;

namespace CycleShare.Server.Repositories.Interfaces
{
    public interface IGalleryItemRepository
    {
        public Task SaveGalleryItemsAsync(int bikeId, List<IFormFile> images);
        public Task<List<GalleryItem>> GetGalleryItemsByBikeIdAsync(int bikeId);
    }
}
