using CycleShare.Server.Entities;
using CycleShare.Server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CycleShare.Server.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly DbContext _context;

        public AddressRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Address>> GetAllAsync()
        {
            return await _context.Set<Address>().ToListAsync();
        }

        public async Task<Address> GetByIdAsync(int id)
        {
            return await _context.Set<Address>().FindAsync(id);
        }

        public async Task AddAsync(Address entity)
        {
            await _context.Set<Address>().AddAsync(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            Address entity = await _context.Set<Address>().FindAsync(id);
            if (entity != null)
            {
                _context.Remove(entity);
            }
        }

        public async void Update(Address entity)
        {
            _context.Update(entity);
        }
    }
}
