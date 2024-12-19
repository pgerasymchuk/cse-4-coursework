using CycleShare.Server.Entities;
using CycleShare.Server.Repositories;
using CycleShare.Server.Repositories.Interfaces;
using CycleShare.Server.Services.Interfaces;

namespace CycleShare.Server.Services
{
    public class GalleryItemService : IGalleryItemService
    {
        private readonly IGalleryItemRepository _repository;

        public GalleryItemService(IGalleryItemRepository repository)
        {
            _repository = repository;
        }
        public async Task SaveImagesAsync(int bikeId, List<IFormFile> images)
        {
            await _repository.SaveGalleryItemsAsync(bikeId, images);
        }

        public async Task<List<GalleryItem>> GetImagesByBikeIdAsync(int bikeId)
        {
            return await _repository.GetGalleryItemsByBikeIdAsync(bikeId);
        }
    }
}
