using AutoMapper;
using CycleShare.Server.Dtos;
using CycleShare.Server.Entities;
using CycleShare.Server.Repositories.Interfaces;
using CycleShare.Server.Services.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace CycleShare.Server.Services
{
    public class BikeService : IBikeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAddressService _addressService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public BikeService(IUnitOfWork unitOfWork, 
                           IAddressService addressService, 
                           IUserService userService, 
                           IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _addressService = addressService;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BikeDto>> GetAllAsync(BikeFilterParams filterParams)
        {
            var entities = await _unitOfWork.BikeRepository.GetAllAsync(filterParams);
            return _mapper.Map<IEnumerable<BikeDto>>(entities);
        }

        public async Task<IEnumerable<BikeDto>> GetAllByUserAsync(int userId)
        {
            var entities = await _unitOfWork.BikeRepository.GetAllByUserAsync(userId);
            return _mapper.Map<IEnumerable<BikeDto>>(entities);
        }

        public async Task<BikeDto> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.BikeRepository.GetByIdAsync(id);
            return _mapper.Map<BikeDto>(entity);
        }

        public async Task<int> AddAsync(BikeDto bikeDto, AddressDto addressDto)
        {
            var bikeEntity = _mapper.Map<Bike>(bikeDto);
            var addressEntity = _mapper.Map<Address>(addressDto);

            int addressId = await _addressService.AddAsync(addressDto);
            var address = await _addressService.GetByIdAsync(addressId);
            bikeEntity.Address = addressEntity;

            var user = _userService.GetById(bikeDto.UserId);
            bikeEntity.User = user;

            await _unitOfWork.BikeRepository.AddAsync(bikeEntity);
            await _unitOfWork.SaveAsync();
            return bikeEntity.Id;
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _unitOfWork.BikeRepository.DeleteByIdAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(BikeDto bikeDto, AddressDto addressDto)
        {
            var bikeEntity = _mapper.Map<Bike>(bikeDto);
            var addressEntity = _mapper.Map<Address>(addressDto);

            int addressId = await _addressService.AddAsync(addressDto);
            var address = await _addressService.GetByIdAsync(addressId);
            bikeEntity.Address = addressEntity;

            var user = _userService.GetById(bikeDto.UserId);
            bikeEntity.User = user;

            _unitOfWork.BikeRepository.Update(bikeEntity);
            await _unitOfWork.SaveAsync();
        }
    }
}
