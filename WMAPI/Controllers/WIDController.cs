using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WMAPI.Service.Interfaces;

namespace WMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WIDController : ControllerBase
    {
        private readonly IWIDService _widService;

        public WIDController(IWIDService wIDService)
        {
            _widService = wIDService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWIDByInId(int inId)
        {
            var (getWIDsById, message) = await _widService.GetAllWIDs(inId);
            if (!(getWIDsById.Any()))
                return Ok(new { Message = message , Data = getWIDsById});

            return Ok(new { Message = message , Data = getWIDsById});
        }
    }
}
