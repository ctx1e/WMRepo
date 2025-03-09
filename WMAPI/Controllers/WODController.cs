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

        [HttpGet("getAllByOutId/{outId}")]
        public async Task<IActionResult> GetAllWODByOutId(int outId)
        {
            var getWODsById = await _wodService.GetAllWODs(outId);
            if (!(getWODsById.Any()))
                return Ok(getWODsById);

            return Ok(getWODsById);
        }
    }
}
