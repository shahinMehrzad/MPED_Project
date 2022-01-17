using AutoMapper;
using MPED.Application.Features.Booking.Commands.Create;
using MPED.Application.Features.Booking.Commands.Delete;
using MPED.Domain.Entities;

namespace MPED.Application.Mappings
{
    internal class BookingRoomProfile : Profile
    {
        public BookingRoomProfile()
        {
            CreateMap<CreateBookingRoomCommand, BookingRoom>().ReverseMap();
            CreateMap<DeleteBookingRoomCommand, Rooms>().ReverseMap();            
        }
    }
}
