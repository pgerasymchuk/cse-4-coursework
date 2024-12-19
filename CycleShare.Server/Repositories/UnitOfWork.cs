using CycleShare.Server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CycleShare.Server.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public IBikeRepository BikeRepository { get; set; }
        public IAddressRepository AddressRepository { get; set; }

        public UnitOfWork(DbContext context,
                          IBikeRepository bikeRepository,
                          IAddressRepository addressRepository)
        {
            _context = context;
            BikeRepository = bikeRepository;
            AddressRepository = addressRepository;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
