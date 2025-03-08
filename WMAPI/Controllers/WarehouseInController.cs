
using Microsoft.AspNetCore.Mvc;
using WMAPI.DTO;
using WMAPI.Service.Interfaces;

namespace WMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseInController : ControllerBase
    {
        private readonly IWarehouseInService _warehouseInService;


        public WarehouseInController(IWarehouseInService warehouseInService)
        {
            _warehouseInService = warehouseInService;
        }


        [HttpGet("getWarehouseIns")]
        public async Task<IActionResult> GetWIs()
        {
            var wis = await _warehouseInService.GetAllWIs();
            if (wis == null && !(wis.Any()))
            {
                return Ok(wis);
            }
            return Ok(wis);
        }


        [HttpGet("getWIById/{inId}")]
        public async Task<IActionResult> GetWIById(int inId)
        {

            var wiById = await _warehouseInService.GetWIById(inId);
            if (wiById == null)
            {
                return Ok(wiById);
            }
            return Ok(wiById);
        }


        [HttpPost("addWarehouseIn")]
        public async Task<IActionResult> AddWarehouseIn([FromBody] GetWarehouseInRequest warehouseInRequest)
        {
            if (warehouseInRequest.WarehouseInDetailDTOs == null || !(warehouseInRequest.WarehouseInDetailDTOs.Any()))
            {
                return BadRequest("WarehouseInDetailDTOs is null!!");
            }

            var isSuccess = await _warehouseInService.AddWI(warehouseInRequest);
            if (!isSuccess)
            {
                return Ok(isSuccess);
            } 
            return Ok(isSuccess);
        }

    }
}
