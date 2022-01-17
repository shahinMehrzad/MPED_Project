using Microsoft.AspNetCore.Mvc;
using MPED.Application.Features.Booking.Commands.Create;
using MPED.Application.Features.Booking.Commands.Delete;
using MPED.Application.Features.Rooms.Queries.GetByDate;
using MPED_Project.Services.RefreshTokenRepositories;
using System;
using System.Threading.Tasks;

namespace MPED_Project.Controllers.v1
{
    public class BookingController : BaseApiController<BookingController>
    {
        
        [HttpPost("BookingRoom")]
        public async Task<IActionResult> BookingRoom(CreateBookingRoomCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("CancelBooking/{bookingId}")]
        public async Task<IActionResult> CancelBooking(int bookingId)
        {
            return Ok(await _mediator.Send(new DeleteBookingRoomCommand { Id = bookingId }));
        }

        [HttpPost("SearchRoom")]
        public async Task<IActionResult> SearchRoom(GetRoomByDateQuery command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
