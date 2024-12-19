using AutoMapper;
using CycleShare.Server.Dtos;
using CycleShare.Server.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CycleShare.Server
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Bike, BikeDto>()
                .ForMember(dto => dto.BookingsIds,
                           e => e.MapFrom(entity => entity.Bookings
                                                          .Select(b => b.Id)))
                .ForMember(dto => dto.ChatsIds,
                           e => e.MapFrom(entity => entity.Chats
                                                          .Select(c => c.Id)))
                .ForMember(dto => dto.ImageUrls,
                           e => e.MapFrom(entity => entity.GalleryItems
                                                          .Select(gi => gi.Uri)))
                /*.ForMember(dto => dto.ImageUrls,
                           e => e.MapFrom(entity => new[] { $"/images/{entity.Id}.jpg" }))*/
                .ReverseMap();

            CreateMap<Address, AddressDto>()
                .ReverseMap();

            CreateMap<SignUpDto, User>();
                
        }

    }
}
