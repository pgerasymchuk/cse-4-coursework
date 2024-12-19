using CycleShare.Server.Entities;
using CycleShare.Server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CycleShare.Server.Repositories
{
    public class GalleryItemRepository : IGalleryItemRepository
    {
        private readonly DbContext _context;
        private readonly IWebHostEnvironment _environment;

        public GalleryItemRepository(DbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public async Task SaveGalleryItemsAsync(int bikeId, List<IFormFile> images)
        {
            if (images == null || !images.Any()) return;

            var imagePath = Path.Combine(_environment.WebRootPath, "images", bikeId.ToString());
            
            if (Directory.Exists(imagePath))
            {
                Directory.Delete(imagePath, true);
            }
            Directory.CreateDirectory(imagePath);

            for (int i = 0; i < images.Count; i++)
            {
                var image = images[i];
                var filePath = Path.Combine(imagePath, image.FileName);
                await using var stream = new FileStream(filePath, FileMode.Create);
                await image.CopyToAsync(stream);

                var galleryItem = new GalleryItem
                {
                    Uri = Path.Combine("images", bikeId.ToString(), image.FileName),
                    CreatedDate = DateTime.UtcNow,
                    SequenceNumber = i, 
                    BikeId = bikeId
                };

                _context.Set<GalleryItem>().Add(galleryItem);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<GalleryItem>> GetGalleryItemsByBikeIdAsync(int bikeId)
        {
            return await _context.Set<GalleryItem>()
                .Where(g => g.BikeId == bikeId && g.Deleted == null)
                .OrderBy(g => g.SequenceNumber)
                .ToListAsync();
        }
    }
}
