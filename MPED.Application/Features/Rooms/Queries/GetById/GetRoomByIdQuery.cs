using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using MPED.Application.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace MPED.Application.Features.Rooms.Queries.GetById
{
    public class GetRoomByIdQuery : IRequest<Result<GetRoomByIdResponse>>
    {
        public int Id { get; set; }

        public class GetRoomByIdQueryHandler : IRequestHandler<GetRoomByIdQuery, Result<GetRoomByIdResponse>>
        {
            private readonly IRoomsRepository _roomsRepository;
            private readonly IMapper _mapper;

            public GetRoomByIdQueryHandler(IRoomsRepository roomsRepository, IMapper mapper)
            {
                _roomsRepository = roomsRepository;
                _mapper = mapper;
            }

            public async Task<Result<GetRoomByIdResponse>> Handle(GetRoomByIdQuery query, CancellationToken cancellationToken)
            {
                var model = await _roomsRepository.GetByIdAsync(query.Id);
                var mappedRoom = _mapper.Map<GetRoomByIdResponse>(model);
                return Result<GetRoomByIdResponse>.Success(mappedRoom);
            }
        }
    }
}
