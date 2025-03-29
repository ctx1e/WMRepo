
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WMAPI.DTO;
using WMAPI.Models;
using WMAPI.Service.Interfaces;

namespace WMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseInController : ControllerBase
    {

        private readonly WarehouseManagementContext _context;
        private readonly IWarehouseInService _warehouseInService;


        public WarehouseInController(IWarehouseInService warehouseInService, WarehouseManagementContext context)
        {
            _warehouseInService = warehouseInService;
            _context = context;
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

        [HttpDelete("deleteWI/{inId}")]
        public async Task<IActionResult> DeleteWarehouseIn(int inId)
        {
            var isSuccess = await _warehouseInService.DeleteWI(inId);
            if (!isSuccess)
            {
                return Ok(isSuccess);
            }
            return Ok(isSuccess);
        }

        [HttpGet("GetMaxPrice/{proId}")]
        public async Task<IActionResult> GetMaxPrice(int proId)
        {
   var getMaxPrice = await _context.WarehouseInDetails
                                    .Where(x => x.ProductId == proId)
                                    .MaxAsync(y => (decimal?)y.PriceIn) ?? 0; // Trả về 0 nếu không có giá trị

    return Ok(new { priceIn = getMaxPrice });
        }

    }
}
