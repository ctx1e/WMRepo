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

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var (inventories, message) = await _inventoryService.GetAllInventories();
            if (inventories == null && !(inventories.Any()))
            {
                return Ok(new { Message = message, Data = inventories });
            }
            return Ok(new { Message = message, Data = inventories });
        }
    }
}
