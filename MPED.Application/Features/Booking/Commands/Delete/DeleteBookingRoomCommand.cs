using AspNetCoreHero.Results;
using MediatR;
using MPED.Application.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace MPED.Application.Features.Booking.Commands.Delete
{
    public class DeleteBookingRoomCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeleteBookingRoomCommandHandler : IRequestHandler<DeleteBookingRoomCommand, Result<int>>
        {
            private readonly IBookingRoomRepository _bookingRoomRepository;
            private readonly IUnitOfWork _unitOfWork;

            public DeleteBookingRoomCommandHandler(IBookingRoomRepository bookingRoomRepository, IUnitOfWork unitOfWork)
            {
                _bookingRoomRepository = bookingRoomRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(DeleteBookingRoomCommand command, CancellationToken cancellationToken)
            {
                var bookingRoom = await _bookingRoomRepository.GetByIdAsync(command.Id);
                bookingRoom.IsDeleted = true;
                bookingRoom.DeletionTime = System.DateTime.Now;
                await _bookingRoomRepository.UpdateAsync(bookingRoom);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(bookingRoom.Id);
            }
        }
    }
}
