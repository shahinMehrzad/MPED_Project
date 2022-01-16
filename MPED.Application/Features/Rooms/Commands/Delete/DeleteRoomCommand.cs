using AspNetCoreHero.Results;
using MediatR;
using MPED.Application.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace MPED.Application.Features.Rooms.Commands.Delete
{
    public class DeleteRoomCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand, Result<int>>
        {
            private readonly IRoomsRepository _roomsRepository;
            private readonly IUnitOfWork _unitOfWork;

            public DeleteRoomCommandHandler(IRoomsRepository roomsRepository, IUnitOfWork unitOfWork)
            {
                _roomsRepository = roomsRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(DeleteRoomCommand command, CancellationToken cancellationToken)
            {
                var room = await _roomsRepository.GetByIdAsync(command.Id);
                await _roomsRepository.DeleteAsync(room);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(room.Id);
            }
        }
    }
}
