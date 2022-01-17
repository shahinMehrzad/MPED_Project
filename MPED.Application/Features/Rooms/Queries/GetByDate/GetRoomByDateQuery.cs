using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using MPED.Application.Features.Rooms.Queries.GetById;
using MPED.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MPED.Application.Features.Rooms.Queries.GetByDate
{
    public class GetRoomByDateQuery : IRequest<Result<List<GetRoomByIdResponse>>>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public class GetRoomByDateQueryHandler : IRequestHandler<GetRoomByDateQuery, Result<List<GetRoomByIdResponse>>>
        {
            private readonly IRoomsRepository _roomsRepository;
            private readonly IBookingRoomRepository _bookingRoomRepository;
            private readonly IMapper _mapper;

            public GetRoomByDateQueryHandler(IRoomsRepository roomsRepository, IBookingRoomRepository bookingRoomRepository, IMapper mapper)
            {
                _roomsRepository = roomsRepository;
                _bookingRoomRepository = bookingRoomRepository; 
                _mapper = mapper;
            }

            public async Task<Result<List<GetRoomByIdResponse>>> Handle(GetRoomByDateQuery query, CancellationToken cancellationToken)
            {
                var reservedRooms = await _bookingRoomRepository.GetReservedRoomsAsync(query.StartDate, query.EndDate);
                var roomsList = await _roomsRepository.GetListAsync();
                var mappedRooms = _mapper.Map<List<GetRoomByIdResponse>>(roomsList.Where(x=> !reservedRooms.Contains (x.Id)));
                return Result<List<GetRoomByIdResponse>>.Success(mappedRooms);
            }
        }
    }
}
