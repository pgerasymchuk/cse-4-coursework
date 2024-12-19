using CycleShare.Server.Dtos;
using CycleShare.Server.Entities;

namespace CycleShare.Server.Repositories.Interfaces
{
    public interface IBikeRepository
    {
        Task<IEnumerable<Bike>> GetAllAsync(BikeFilterParams filterParams);

        Task<IEnumerable<Bike>> GetAllByUserAsync(int userId);

        Task<Bike> GetByIdAsync(int id);

        Task AddAsync(Bike entity);

        Task DeleteByIdAsync(int id);

        void Update(Bike entity);
    }
}
