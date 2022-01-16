using Microsoft.AspNetCore.Mvc;
using MPED.Application.Features.Rooms.Queries.GetAll;
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
    }
}
