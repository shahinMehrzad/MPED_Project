using AutoMapper;
using MPED.Application.Features.Rooms.Commands.Create;
using MPED.Application.Features.Rooms.Commands.Delete;
using MPED.Application.Features.Rooms.Commands.Update;
using MPED.Application.Features.Rooms.Queries.GetAll;
using MPED.Application.Features.Rooms.Queries.GetById;
using MPED.Domain.Entities;

namespace MPED.Application.Mappings
{
    internal class RoomsProfile : Profile
    {
        public RoomsProfile()
        {
            CreateMap<CreateRoomCommand, Rooms>().ReverseMap();
            CreateMap<GetRoomByIdResponse, Rooms>().ReverseMap();
            CreateMap<GetAllRoomsResponse, Rooms>().ReverseMap();
            CreateMap<DeleteRoomCommand, Rooms>().ReverseMap();
            CreateMap<UpdateRoomCommand, Rooms>().ReverseMap();
        }
    }
}
