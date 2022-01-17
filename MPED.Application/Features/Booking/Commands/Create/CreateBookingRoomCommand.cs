using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using MPED.Application.Interfaces.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MPED.Application.Features.Booking.Commands.Create
{
    public class CreateBookingRoomCommand : IRequest<Result<int>>
    {
        public int RoomId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        //public int GuestNumber { get; set; }

        public class CreateBookingRoomCommandHandler : IRequestHandler<CreateBookingRoomCommand, Result<int>>
        {
            private readonly IBookingRoomRepository _bookingRoomRepository;
            private readonly IRoomsRepository _roomsRepository;
            private readonly IMapper _mapper;

            private IUnitOfWork _unitOfWork { get; set; }

            public CreateBookingRoomCommandHandler(IBookingRoomRepository bookingRoomRepository, IRoomsRepository roomsRepository, IUnitOfWork unitOfWork, IMapper mapper)
            {
                _bookingRoomRepository = bookingRoomRepository;
                _roomsRepository = roomsRepository;
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<Result<int>> Handle(CreateBookingRoomCommand request, CancellationToken cancellationToken)
            {
                if(await _roomsRepository.GetByIdAsync(request.RoomId) == null)
                {
                    return Result<int>.Fail("This room is not available.");
                }
                var reservedRooms = await _bookingRoomRepository.GetReservedRoomsAsync(request.StartDate, request.EndDate);
                if (reservedRooms.Contains(request.RoomId))
                {
                    return Result<int>.Fail("This room is not available within the time period you requested.");
                }
                var bookingRoom = _mapper.Map<Domain.Entities.BookingRoom>(request);
                await _bookingRoomRepository.InsertAsync(bookingRoom);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(bookingRoom.Id);
            }
        }
    }
}
