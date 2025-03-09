using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VMWeb.Models.ViewModel;
using VMWeb.Service;

namespace VMWeb.Controllers
{
    public class WarehouseInController : Controller
    {

        private readonly WarehouseInService _warehouseInService;

        public WarehouseInController(WarehouseInService warehouseInService)
        {
            _warehouseInService = warehouseInService;
        }
        public async Task<IActionResult> WarehouseInView(int page = 1, int pageSize = 8)
        {

            var warehouseIns = await _warehouseInService.GetWarehouseInsAsync();

            // Tính toán phân trang
            int totalItems = warehouseIns.Count;
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            var pagedData = warehouseIns.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.Page = page;
            ViewBag.TotalPages = totalPages;
            return View(warehouseIns);
        }

        public async Task<IActionResult> WarehouseInDetail(int inId)
        {
            var getWIByInId = await _warehouseInService.GetWarehouseInByInIdAsync(inId);
            var getAllWIByInId = await _warehouseInService.GetWIDByInIdAsync(inId);

            var getWIDVM = new WIDViewModel
            {
                WarehouseIn = getWIByInId,
                WarehouseInDetails = getAllWIByInId,
            };

            return View(getWIDVM);
        }

    }
}
