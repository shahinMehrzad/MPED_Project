using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using MPED.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MPED.Application.Features.Rooms.Queries.GetAll
{
    public class GetAllRoomsQuery : IRequest<Result<List<GetAllRoomsResponse>>>
    {
        public GetAllRoomsQuery()
        {
        }
    }

    public class GetAllRoomsQueryHandler : IRequestHandler<GetAllRoomsQuery, Result<List<GetAllRoomsResponse>>>
    {
        private readonly IRoomsRepository _roomsRepository;
        private readonly IMapper _mapper;

        public GetAllRoomsQueryHandler(IRoomsRepository roomsRepository, IMapper mapper)
        {
            _roomsRepository = roomsRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAllRoomsResponse>>> Handle(GetAllRoomsQuery request, CancellationToken cancellationToken)
        {
            var roomsList = await _roomsRepository.GetListAsync();
            var mappedRooms = _mapper.Map<List<GetAllRoomsResponse>>(roomsList.Where(x => !x.IsDeleted));
            return Result<List<GetAllRoomsResponse>>.Success(mappedRooms);
        }
    }
}
