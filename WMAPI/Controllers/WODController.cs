using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WMAPI.Service.Interfaces;

namespace WMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WODController : ControllerBase
    {
        private readonly IWODService _wodService;

        public WODController(IWODService wodService)
        {
            _wodService = wodService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWIDByInId(int outId)
        {
            var (getWODsById, message) = await _wodService.GetAllWODs(outId);
            if (!(getWODsById.Any()))
                return Ok(new { Message = message, Data = getWODsById });

            return Ok(new { Message = message, Data = getWODsById });
        }
    }
}
