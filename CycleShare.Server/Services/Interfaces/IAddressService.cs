using CycleShare.Server.Dtos;

namespace CycleShare.Server.Services.Interfaces
{
    public interface IAddressService
    {
        Task<IEnumerable<AddressDto>> GetAllAsync();

        Task<AddressDto> GetByIdAsync(int id);

        Task<int> AddAsync(AddressDto dto);

        Task DeleteByIdAsync(int id);

        Task UpdateAsync(AddressDto dto);
    }
}
