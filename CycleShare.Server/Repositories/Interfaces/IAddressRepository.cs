using CycleShare.Server.Entities;

namespace CycleShare.Server.Repositories.Interfaces
{
    public interface IAddressRepository
    {
        Task<IEnumerable<Address>> GetAllAsync();

        Task<Address> GetByIdAsync(int id);

        Task AddAsync(Address entity);

        Task DeleteByIdAsync(int id);

        void Update(Address entity);
    }
}
