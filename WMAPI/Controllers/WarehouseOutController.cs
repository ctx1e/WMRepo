using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WMAPI.DTO;
using WMAPI.Service.Interfaces;

namespace WMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseOutController : ControllerBase
    {
        private readonly IWarehouseOutService _warehouseOutService;


        public WarehouseOutController(IWarehouseOutService warehouseOutService)
        {
            _warehouseOutService = warehouseOutService;
        }


        [HttpGet("getWarehouseOuts")]
        public async Task<IActionResult> GetWOs()
        {
            var (wos, message) = await _warehouseOutService.GetAllWOs();
            if (wos == null && !(wos.Any()))
            {
                return Ok(new { Message = message, Data = wos });
            }
            return Ok(new { Message = message, Data = wos });
        }


        [HttpGet("getWOById/{outId}")]
        public async Task<IActionResult> GetWOById(int outId)
        {

            var (woById, message) = await _warehouseOutService.GetWOById(outId);
            if (woById == null)
            {
                return Ok(new { Message = message, Data = woById });
            }
            return Ok(new { Message = message, Data = woById });
        }


        [HttpPost("addWarehouseOut")]
        public async Task<IActionResult> AddWarehouseOut([FromBody] GetWarehouseOutRequest warehouseOutRequest)
        {
            if (warehouseOutRequest.WarehouseOutDetailDTOs == null || !(warehouseOutRequest.WarehouseOutDetailDTOs.Any()))
            {
                return BadRequest("WarehouseOutDetailDTOs is null!!");
            }

            var (isSuccess, message) = await _warehouseOutService.AddWO(warehouseOutRequest);
            if (!isSuccess)
            {
                return Ok(new { Message = message });
            }
            return Ok(new { Message = message });
        }
    }
}
