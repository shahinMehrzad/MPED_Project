using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using MPED.Application.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace MPED.Application.Features.Rooms.Commands.Create
{
    public partial class CreateRoomCommand : IRequest<Result<int>>
    {
        public string RoomNumber { get; set; }
        public bool AirConditioning { get; set; }
        public bool WiFi { get; set; }
        public bool Hairdryer { get; set; }
        public bool Television { get; set; }
        public bool SeaView { get; set; }
        public int RoomArea { get; set; }
        public int TwinBed { get; set; }
        public int SingleBed { get; set; }
        public int ExtraSingleBed { get; set; }
    }

    public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, Result<int>>
    {
        private readonly IRoomsRepository _roomsRepository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateRoomCommandHandler(IRoomsRepository roomsRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _roomsRepository = roomsRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            var room = _mapper.Map<Domain.Entities.Rooms>(request);
            await _roomsRepository.InsertAsync(room);
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(room.Id);
        }
    }
}
