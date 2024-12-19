using CycleShare.Server.Dtos;
using CycleShare.Server.Entities;
using CycleShare.Server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CycleShare.Server.Repositories
{
    public class BikeRepository : IBikeRepository
    {
        private readonly DbContext _context;

        public BikeRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Bike>> GetAllAsync(BikeFilterParams filterParams)
        {
            var bikesQuery = _context.Set<Bike>().AsQueryable();

            if (!string.IsNullOrEmpty(filterParams?.Type))
            {
                bikesQuery = bikesQuery.Where(b => b.Type == filterParams.Type);
            }
            if (!string.IsNullOrEmpty(filterParams?.Wheelsize))
            {
                bikesQuery = bikesQuery.Where(b => b.Wheelsize == filterParams.Wheelsize);
            }
            if (!string.IsNullOrEmpty(filterParams?.Material))
            {
                bikesQuery = bikesQuery.Where(b => b.Material == filterParams.Material);
            }
            if (filterParams?.MinPrice != null)
            {
                bikesQuery = bikesQuery.Where(b => b.Price >= filterParams.MinPrice);
            }
            if (filterParams?.MaxPrice != null)
            {
                bikesQuery = bikesQuery.Where(b => b.Price <= filterParams.MaxPrice);
            }
            if (filterParams?.MinRating != null)
            {
                bikesQuery = bikesQuery.Where(b => b.Rating >= filterParams.MinRating);
            }

            if (filterParams?.SortBy == "Price")
            {
                bikesQuery = filterParams.Order == "asc" ? bikesQuery.OrderBy(b => b.Price) : bikesQuery.OrderByDescending(b => b.Price);
            }
            else if (filterParams?.SortBy == "CreatedDate")
            {
                bikesQuery = filterParams.Order == "asc" ? bikesQuery.OrderBy(b => b.CreatedDate) : bikesQuery.OrderByDescending(b => b.CreatedDate);
            }
            else if (filterParams?.SortBy == "Rating")
            {
                bikesQuery = filterParams.Order == "asc" ? bikesQuery.OrderBy(b => b.Rating) : bikesQuery.OrderByDescending(b => b.Rating);
            }

            var skip = (filterParams?.PageNumber - 1) * filterParams?.PageSize ?? 0;
            var take = filterParams?.PageSize ?? 10;

            return await bikesQuery.Skip(skip).Take(take).ToListAsync();
        }

        public async Task<IEnumerable<Bike>> GetAllByUserAsync(int userId)
        {
            return await _context.Set<Bike>()
                .Include(b => b.User)
                .Where(b => b.User.Id == userId)
                .OrderByDescending(b => b.CreatedDate)
                .ToListAsync();
        }
        public async Task<Bike> GetByIdAsync(int id)
        {
            return await _context.Set<Bike>().FindAsync(id);
        }

        public async Task AddAsync(Bike entity)
        {
            await _context.AddAsync(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            Bike entity = await _context.Set<Bike>().FindAsync(id);
            if (entity != null)
            {
                _context.Remove(entity);
            }
        }

        public async void Update(Bike entity)
        {
            _context.Update(entity);
        }
    }
}
