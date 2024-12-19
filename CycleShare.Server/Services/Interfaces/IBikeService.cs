using CycleShare.Server.Dtos;

namespace CycleShare.Server.Services.Interfaces
{
    public interface IBikeService
    {
        Task<IEnumerable<BikeDto>> GetAllAsync(BikeFilterParams filterParams);

        Task<IEnumerable<BikeDto>> GetAllByUserAsync(int userId);

        Task<BikeDto> GetByIdAsync(int id);

        Task<int> AddAsync(BikeDto bikeDto, AddressDto addressDto);

        Task DeleteByIdAsync(int id);

        Task UpdateAsync(BikeDto bikeDto, AddressDto addressDto);
    }
}
