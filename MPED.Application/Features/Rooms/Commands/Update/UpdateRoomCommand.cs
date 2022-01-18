using AspNetCoreHero.Results;
using MediatR;
using MPED.Application.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace MPED.Application.Features.Rooms.Commands.Update
{
    public class UpdateRoomCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
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

        public class UpdateRoomCommandHandler : IRequestHandler<UpdateRoomCommand, Result<int>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IRoomsRepository _roomsRepository;

            public UpdateRoomCommandHandler(IRoomsRepository roomsRepository, IUnitOfWork unitOfWork)
            {
                _roomsRepository = roomsRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(UpdateRoomCommand command, CancellationToken cancellationToken)
            {
                var room = await _roomsRepository.GetByIdAsync(command.Id);

                if (room == null)
                {
                    return Result<int>.Fail($"Room Not Found.");
                }
                else
                {
                    room.RoomNumber = command.RoomNumber ?? room.RoomNumber;
                    room.AirConditioning = command.AirConditioning;
                    room.WiFi = command.WiFi;
                    room.Hairdryer = command.Hairdryer;
                    room.Television = command.Television;
                    room.SeaView = command.SeaView;
                    room.RoomArea = command.RoomArea;
                    room.TwinBed = command.TwinBed;
                    room.SingleBed = command.SingleBed;
                    room.ExtraSingleBed = command.ExtraSingleBed;
                    await _roomsRepository.UpdateAsync(room);
                    await _unitOfWork.Commit(cancellationToken);
                    return Result<int>.Success("The room has been successfully updated.");
                }
            }
        }
    }
}
