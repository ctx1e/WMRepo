using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WMAPI.Service.Interfaces;

namespace WMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet("inventories")]
        public async Task<IActionResult> GetInventories()
        {
            var inventories = await _inventoryService.GetAllInventories();

            return Ok(inventories);
        }
        [HttpGet("getQISByProId/{proId}")]
        public async Task<IActionResult> GetQISByProId(int proId)
        {
            var inventory = await _inventoryService.getQISbyProId(proId);

            return Ok(inventory);
        }
    }

}
