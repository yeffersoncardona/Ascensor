using Microsoft.AspNetCore.Mvc;

namespace AscensorApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AscensorController : ControllerBase
    {
        private static AscensorEstado _status = new AscensorEstado { CurrentFloor = 0, IsMoving = false, DoorsOpen = false };
        private AscensorRequest _AscensorRequest = new AscensorRequest();
        private static readonly object _lock = new object();

        [HttpPost("call")]
        public IActionResult CallElevator([FromBody] AscensorRequest request)
        { // Lógica para mover el ascensor al piso solicitado
            lock (_lock)
            {
                _status = _AscensorRequest.ProcessRequests(request);
                return Ok(_status);
            }
        }
        [HttpPost("open")]
        public IActionResult OpenDoors()
        {
            _status.DoorsOpen = true; return Ok(_status);
        }
        [HttpPost("close")]
        public IActionResult CloseDoors()
        {
            _status.DoorsOpen = false; return Ok(_status);
        }
        [HttpPost("start")]
        public IActionResult StartElevator()
        {
            _status.IsMoving = true; return Ok(_status);
        }
        [HttpPost("stop")]
        public IActionResult StopElevator()
        {
            _status.IsMoving = false; return Ok(_status);
        }
        [HttpGet("status")]
        public IActionResult GetStatus()
        {
            return Ok(_status);
        }

    }
}
