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
            var wos = await _warehouseOutService.GetAllWOs();
            if (wos == null && !(wos.Any()))
            {
                return Ok(wos);
            }
            return Ok(wos);
        }


        [HttpGet("getWOById/{outId}")]
        public async Task<IActionResult> GetWOById(int outId)
        {

            var woById = await _warehouseOutService.GetWOById(outId);
            if (woById == null)
            {
                return Ok(woById);
            }
            return Ok(woById);
        }


        [HttpPost("addWarehouseOut")]
        public async Task<IActionResult> AddWarehouseOut([FromBody] GetWarehouseOutRequest warehouseOutRequest)
        {
            if (warehouseOutRequest.WarehouseOutDetailDTOs == null || !(warehouseOutRequest.WarehouseOutDetailDTOs.Any()))
            {
                return BadRequest("WarehouseOutDetailDTOs is null!!");
            }

            var isSuccess= await _warehouseOutService.AddWO(warehouseOutRequest);
            if (!isSuccess)
            {
                return Ok(isSuccess);
            }
            return Ok(isSuccess);
        }

        [HttpDelete("deleteWO/{outId}")]
        public async Task<IActionResult> DeleteWarehouseIn(int outId)
        {
            var isSuccess = await _warehouseOutService.DeleteWO(outId);
            if (!isSuccess)
            {
                return Ok(isSuccess);
            }
            return Ok(isSuccess);
        }
    }
}
