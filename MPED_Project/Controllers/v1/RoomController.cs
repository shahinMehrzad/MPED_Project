using Microsoft.AspNetCore.Mvc;
using MPED.Application.Features.Rooms.Commands.Create;
using MPED.Application.Features.Rooms.Commands.Delete;
using MPED.Application.Features.Rooms.Commands.Update;
using MPED.Application.Features.Rooms.Queries.GetAll;
using MPED.Application.Features.Rooms.Queries.GetById;
using MPED_Project.Services.RefreshTokenRepositories;
using System.Threading.Tasks;

namespace MPED_Project.Controllers.v1
{
    public class RoomController : BaseApiController<RoomController>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public RoomController(IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }

        [HttpGet("GetRoomsList")]
        public async Task<IActionResult> GetRoomsList()
        {
            var roomsList = await _mediator.Send(new GetAllRoomsQuery());
            return Ok(roomsList);
        }

        [HttpGet("GetRoomById")]
        public async Task<IActionResult> GetRoomById(int id = 0)
        {
            var room = await _mediator.Send(new GetRoomByIdQuery() { Id = id});
            return Ok(room);
        }

        [HttpPost("AddRoom")]
        public async Task<IActionResult> AddRoom(CreateRoomCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("UpdateRoom")]
        public async Task<IActionResult> UpdateRoom(UpdateRoomCommand command)
        {            
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("DeleteRoom/{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            return Ok(await _mediator.Send(new DeleteRoomCommand { Id = id }));
        }
    }
}
