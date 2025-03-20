using Microsoft.AspNetCore.Mvc;
using VMWeb.Service;

namespace VMWeb.Controllers
{
    public class InventoryController : Controller
    {

        private readonly InventoryService _inventoryService;

        public InventoryController(InventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        public async Task<IActionResult> InventoryView(int page = 1, int pageSize = 10)
        {
            var inventoryData = await _inventoryService.GetInventoryDataAsync();


            // Tính toán phân trang
            int totalItems = inventoryData.Count;
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            var pagedData = inventoryData.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.Page = page;
            ViewBag.TotalPages = totalPages;

            return View(pagedData);
        }

    }

}
