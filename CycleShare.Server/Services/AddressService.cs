using AutoMapper;
using CycleShare.Server.Dtos;
using CycleShare.Server.Entities;
using CycleShare.Server.Repositories.Interfaces;
using CycleShare.Server.Services.Interfaces;

namespace CycleShare.Server.Services
{
    public class AddressService : IAddressService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddressService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AddressDto>> GetAllAsync()
        {
            var entities = await _unitOfWork.AddressRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AddressDto>>(entities);
        }

        public async Task<AddressDto> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.AddressRepository.GetByIdAsync(id);
            return _mapper.Map<AddressDto>(entity);
        }

        public async Task<int> AddAsync(AddressDto dto)
        {
            var entity = _mapper.Map<Address>(dto);
            await _unitOfWork.AddressRepository.AddAsync(entity);
            await _unitOfWork.SaveAsync();
            return entity.Id;
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _unitOfWork.AddressRepository.DeleteByIdAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(AddressDto dto)
        {
            var entity = _mapper.Map<Address>(dto);
            _unitOfWork.AddressRepository.Update(entity);
            await _unitOfWork.SaveAsync();
        }
    }
}
