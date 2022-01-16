using Microsoft.AspNetCore.Mvc;

namespace MPED_Project.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/error")]
        public IActionResult Error()
        {
            return Problem();
        }
    }
}
